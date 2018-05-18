using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HackslashForum.Data;
using HackslashForum.Models;

namespace HackslashForum.Controllers
{
    [Produces("application/json")]
    [Route("api/UsersAPI")]
    public class UsersAPIController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UsersAPIController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetApplicationUser()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var mostProlificUsers = (from u in _context.User
                                     orderby u.Posts.Count descending
                                     select new { userName = u.UserName, email = u.Email }).Take(3);

            if (mostProlificUsers == null)
            {
                return NotFound();
            }

            return Ok(mostProlificUsers);
        }
    }
}