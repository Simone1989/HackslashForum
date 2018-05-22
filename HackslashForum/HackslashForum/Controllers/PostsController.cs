using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HackslashForum;
using HackslashForum.Data;
using System.Collections;
using Microsoft.AspNetCore.Identity;
using HackslashForum.Models;
using Microsoft.AspNetCore.Http;
using System.IO;
using HackslashForum.Models.ManageViewModels;

namespace HackslashForum.Controllers
{
    public class PostsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _manager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public PostsController(ApplicationDbContext context, UserManager<ApplicationUser> manager, SignInManager<ApplicationUser> signInManager)
        {
            _context = context;
            _manager = manager;
            _signInManager = signInManager;
        }

        // GET: Posts
        public async Task<IActionResult> Index()
        {
         
            return View(await _context.Post.ToListAsync());
        }

        // GET: Posts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Post.Include(u => u.User)
                .SingleOrDefaultAsync(m => m.Id == id);

            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        public async Task<IActionResult> Post(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Post.Include(p => p.UsersWhoVoted)
                .SingleOrDefaultAsync(m => m.Id == id);

            if (post == null)
            {
                return NotFound();
            }
            var user = await _manager.GetUserAsync(User);

            string base64 = "";
            string imgSrc = "";
            if (user.ProfilePicture != null)
            {
                base64 = Convert.ToBase64String(user.ProfilePicture);
                imgSrc = String.Format("data:image/png;base64,{0}", base64);
            }
            var model = new IndexViewModel
            {

                ImgSrc = imgSrc,
            };

            ViewBag.ProfilePicture = model.ImgSrc;


            ViewBag.TotalScore = post.UpVotes - post.DownVotes;

            ViewBag.Comments = (from x in _context.Comment
                                where x.Post.Id == id
                                select x).ToList();

            ViewBag.PostAuthor = (from y in _context.Post
                                 where y.Id == id.Value
                                 select y.User.UserName).Single();

            ViewBag.CommentAuthor = (from x in _context.User
                                     join y in _context.Comment on x.Id equals y.User.Id
                                     select x.UserName).Take(1).SingleOrDefault();

            return View(post);
        }

        public async Task<IActionResult> Comment(int? id, string content)
        {
            if (id == null)
                return NotFound();

            var post = (from p in _context.Post
                       where p.Id == id
                       select p).Take(1).SingleOrDefault();

            var user = await _signInManager.UserManager.GetUserAsync(User);

            Comment comment = new Comment
            {
                User = user,
                Author = user.UserName,
                Post = post,
                DateTimeCommentMade = DateTime.Now,
                Content = content
            };

            _context.Comment.Add(comment);
            _context.SaveChanges();

            return RedirectToAction($"Post/{id}");
        }

        public async Task<IActionResult> Upvote(int? id)
        {
            if (id == null)
                return NotFound();

            var post = _context.Post.Include(p => p.UsersWhoVoted).SingleOrDefault(p => p.Id == id);

            var user = await _signInManager.UserManager.GetUserAsync(User);

            var UserVoteModel = _context.VotedUsers.Where(p => p.PostId == id).Where(p => p.UserId == user.Id).Take(1).SingleOrDefault();

            UserVoteModel VotedUser = null;

            if(UserVoteModel == null)
            {
                VotedUser = new UserVoteModel
                {
                    Post = post,
                    PostId = post.Id,
                    User = user,
                    UserId = user.Id,
                    Vote = Vote.Up
                };
                post.UpVotes++;
                _context.Update(post);
                _context.VotedUsers.Add(VotedUser);
            }
            else
            {
                UserVoteModel.Vote = Vote.Up;
                post.UpVotes += 2;
                _context.Update(post);
                _context.VotedUsers.Update(UserVoteModel);
            }
            _context.SaveChanges();

            return RedirectToAction($"Post/{id}");
        }

        public async Task<IActionResult> Downvote(int? id)
        {
            if (id == null)
                return NotFound();

            var post = _context.Post.Include(p => p.UsersWhoVoted).SingleOrDefault(p => p.Id == id);

            var user = await _signInManager.UserManager.GetUserAsync(User);

            var UserVoteModel = _context.VotedUsers.Where(p => p.PostId == id).Where(p => p.UserId == user.Id).Take(1).SingleOrDefault();

            UserVoteModel VotedUser = null;

            if (UserVoteModel == null)
            {
                VotedUser = new UserVoteModel
                {
                    Post = post,
                    PostId = post.Id,
                    User = user,
                    UserId = user.Id,
                    Vote = Vote.Down
                };
                post.UpVotes--;
                _context.Update(post);
                _context.VotedUsers.Add(VotedUser);
            }
            else
            {
                UserVoteModel.Vote = Vote.Down;
                post.UpVotes -= 2;
                _context.Update(post);
                _context.VotedUsers.Update(UserVoteModel);
            }
            _context.SaveChanges();

            return RedirectToAction($"Post/{id}");
        }

        //// GET: Posts/Create
        //public IActionResult Create()
        //{
        //    return View();
        //}

        // Shows Enum Discussion / Question
        public IActionResult Category()
        {
            var showCategory = _context.Post.Include(c => c.Category);
            if (showCategory == null)
            {
                return NotFound();
            }

            List<string> categories = new List<string>();

            foreach (var category in showCategory)
            {
                categories.Add(category.Category.ToString());
            }

            ViewData["ShowCategories"] = categories;
            return View();
        }

        // POST: Posts/Create
        // GET: Posts/Create
        public IActionResult Create()
        {
            return View();
        }
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,DateTimePostCreated,Category,Content,UpVotes,DownVotes")] Post post)
        {
            if (ModelState.IsValid)
            {
                var user = await _manager.GetUserAsync(User);
                post.User = user;

                _context.Add(post);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Home");
            }
            return View(post);
        }

        // GET: Posts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Post.SingleOrDefaultAsync(m => m.Id == id);
            if (post == null)
            {
                return NotFound();
            }
            return View(post);
        }

        // POST: Posts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,DateTimePostCreated,Category,Content,UpVotes,DownVotes")] Post post)
        {
            if (id != post.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(post);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PostExists(post.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(post);
        }

        // GET: Posts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Post
                .SingleOrDefaultAsync(m => m.Id == id);
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        // POST: Posts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var post = await _context.Post.SingleOrDefaultAsync(m => m.Id == id);
            _context.Post.Remove(post);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PostExists(int id)
        {
            return _context.Post.Any(e => e.Id == id);
        }


        [HttpGet]
        public async Task<IActionResult> UploadPicture()
        {
            var user = await _manager.GetUserAsync(User);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{_manager.GetUserId(User)}'.");
            }

            var model = new UploadPictureViewModel
            {
            };

            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UploadPicture(UploadPictureViewModel model, List<IFormFile> files)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await _manager.GetUserAsync(User);
            // var profile = _context.User.Where(s => s.Id == user.Id).SingleOrDefault();
            foreach (var formFile in files)
            {
                model.ProfilePicture.Add(formFile);
                if (formFile.Length > 0)
                {

                    using (var memoryStream = new MemoryStream())
                    {
                        var file = model.ProfilePicture[0];
                        await file.CopyToAsync(memoryStream);
                        user.ProfilePicture = memoryStream.ToArray();
                    }

                    await _context.SaveChangesAsync();

                }
            }

            return RedirectToAction(nameof(Post));
        }
    }
}
