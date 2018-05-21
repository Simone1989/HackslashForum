using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HackslashForum.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HackslashForum.Controllers
{
    [Produces("application/json")]
    [Route("api/MostUpvotedPostAPI")]
    public class MostUpvotedPostAPIController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MostUpvotedPostAPIController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetMostUpvotedPost()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var mostUpvotedPost = (from p in _context.Post
                                   orderby p.UpVotes descending
                                   select p).Take(1).SingleOrDefault();
            ViewBag.mostUpvotedPost = mostUpvotedPost;

            if (mostUpvotedPost == null)
            {
                return NotFound();
            }

            return Ok(mostUpvotedPost);

        }

    }
}