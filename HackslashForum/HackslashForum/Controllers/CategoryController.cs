using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HackslashForum.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HackslashForum.Controllers
{
    public class CategoryController : Controller
    {

        private readonly ApplicationDbContext _context;

        public CategoryController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            //return Ok("hej");
            var categories = _context.Post.Select(p => p.Category).Distinct().ToList();
            return View(categories);
        }

        public IActionResult Discussion()
        {
            //return Ok("hej");
            var DiscussionCategoryQuery = (from c in _context.Post
                                     where c.Category.ToString() == "Discussion"
                                     select c).ToList();
            
            return View(DiscussionCategoryQuery);
        }

        public IActionResult Question()
        {
            //return Ok("hej");
            var QuestionCategoryQuery = (from c in _context.Post
                                           where c.Category.ToString() == "Question"
                                           select c).ToList();

            return View(QuestionCategoryQuery);
        }

        public IActionResult Java()
        {
            //return Ok("hej");
            var JavaCategoryQuery = (from c in _context.Post
                                           where c.Category.ToString() == "Java"
                                           select c).ToList();

            return View(JavaCategoryQuery); ;
        }

        public IActionResult Csharp()
        {
            //return Ok("hej");
            var CsharpCategoryQuery = (from c in _context.Post
                                           where c.Category.ToString() == "Csharp"
                                           select c).ToList();

            return View(CsharpCategoryQuery);
        }
    }
}