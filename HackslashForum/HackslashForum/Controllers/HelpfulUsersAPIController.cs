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
    [Route("api/HelpfulUsersAPI")]
    public class HelpfulUsersAPIController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HelpfulUsersAPIController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetHelpfulUsers()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var mostHelpfulUsers = (from u in _context.User
                                    orderby u.Comments.Count descending
                                    select u).Take(3);

            if (mostHelpfulUsers == null)
            {
                return NotFound();
            }

            return Ok(mostHelpfulUsers);

        }

    }
}