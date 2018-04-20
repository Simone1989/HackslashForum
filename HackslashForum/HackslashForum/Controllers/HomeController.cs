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
            ViewData["Message"] = "Your contact page.";

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

        public IActionResult Index()
        {
            var mostUpvotedPost = _context.Post.OrderByDescending(p => p.UpVotes).Take(1).SingleOrDefault();
            ViewBag.MostUpvotedPost = mostUpvotedPost;

            var mostCommentedPost = _context.Post.OrderByDescending(p => p.Comments.Count).Take(1).SingleOrDefault();
            ViewBag.MostCommentedPost = mostCommentedPost;

            var posts = _context.Post.ToList();
            return View(posts);
        }

        //query end
    }
}
