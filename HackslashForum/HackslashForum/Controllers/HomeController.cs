using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HackslashForum.Models;
using HackslashForum.Data;

namespace HackslashForum.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Kontakt";

            return View();
        }

        public IActionResult TopList()
        {
            ViewData["ToppLista"] = "Topp Tre";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        //query start

        public IActionResult Index(string sortOrder, string searchString, string currentFilter, int? page)
        {

            ViewData["SortByDate"] = sortOrder == "Date";
            ViewData["SortByTitle"] = sortOrder == "Title" ? "title_desc" : "Title";
            ViewData["SortByUpvotes"] = sortOrder == "upvotes_desc" ? "Upvotes" : "upvotes_desc";
            ViewData["SortByComments"] = sortOrder == "comments_desc" ? "Comments" : "comments_desc";
            ViewData["CurrentFilter"] = searchString;

            var postSort = from p in _context.Post
                           select p;

            if (!String.IsNullOrEmpty(searchString))
            {
                postSort = postSort.Where(p => p.Title.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "title_desc":
                    postSort = postSort.OrderByDescending(p => p.Title);
                    break;
                case "Title":
                    postSort = postSort.OrderBy(p => p.Title);
                    break;
                case "Date":
                    postSort = postSort.OrderBy(p => p.DateTimePostCreated);
                    break;
                case "upvotes_desc":
                    postSort = postSort.OrderByDescending(p => p.UpVotes);
                    break;
                case "Upvotes":
                    postSort = postSort.OrderBy(p => p.UpVotes);
                    break;
                case "Comments":
                    postSort = postSort.OrderBy(p => p.Comments.Count);
                    break;
                case "comments_desc":
                    postSort = postSort.OrderByDescending(p => p.Comments.Count);
                    break;
                default:
                    postSort = postSort.OrderByDescending(p => p.DateTimePostCreated);
                    break;
            }

            var mostUpvotedPost = _context.Post.OrderByDescending(p => p.UpVotes).Take(1).SingleOrDefault();
            ViewBag.MostUpvotedPost = mostUpvotedPost;

            var mostCommentedPost = _context.Post.OrderByDescending(p => p.Comments.Count).Take(1).SingleOrDefault();
            ViewBag.MostCommentedPost = mostCommentedPost;

            var posts = _context.Post.ToList();

            return View(postSort.ToList());

        }

        //query end
    }
}
