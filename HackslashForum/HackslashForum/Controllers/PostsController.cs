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

namespace HackslashForum.Controllers
{
    public class PostsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PostsController(ApplicationDbContext context)
        {
            _context = context;
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

            var post = await _context.Post
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

            var post = await _context.Post
                .SingleOrDefaultAsync(m => m.Id == id);
            if (post == null)
            {
                return NotFound();
            }

            ViewBag.Comments = (from x in _context.Comment
                               where x.Post.Id == id
                               select x).ToList();

            return View(post);
        }

        public async Task<IActionResult> Comment(int? id, string content)
        {
            if (id == null)
                return NotFound();

            var post = (from p in _context.Post
                       where p.Id == id
                       select p).Take(1).SingleOrDefault();

            Comment comment = new Comment
            {
                Post = post,
                DateTimeCommentMade = DateTime.Now,
                Content = content
            };

            _context.Comment.Add(comment);
            _context.SaveChanges();

            return RedirectToAction($"Post/{id}");
        }

        // GET: Posts/Create
        public IActionResult Create()
        {
            return View();
        }


        // Shows Enum Discussion / Question
        public IActionResult Category()
        {
            var showCategory = _context.Post.Include(c => c.Category);
            if (showCategory == null)
            {
                return NotFound();
            }

            List<string> categories = new List<string>();

            foreach(var category in showCategory)
            {
                categories.Add(category.Category.ToString());
            }

            ViewData["ShowCategories"] = categories;
            return View();
        }

        // POST: Posts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,DateTimePostCreated,Category,Content,UpVotes,DownVotes")] Post post)
        {
            if (ModelState.IsValid)
            {
                _context.Add(post);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
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
    }
}
