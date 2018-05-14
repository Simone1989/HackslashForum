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

        // GET: api/UsersAPI
        //[HttpGet]
        //public IEnumerable<ApplicationUser> GetUser()
        //{
        //    var mostProlificUsers = (from u in _context.User
        //                            orderby u.Posts.Count descending
        //                            select u).Take(3);
        //    //return View("~/Views/UsersAPI/GetUser.cshtml");
        //    return mostProlificUsers;
        //}

        // GET: api/UsersAPI/5
        [HttpGet]
        public IActionResult GetApplicationUser()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var mostProlificUsers = (from u in _context.User
                                     orderby u.Posts.Count descending
                                     select u).Take(3);

            if (mostProlificUsers == null)
            {
                return NotFound();
            }

            return Ok(mostProlificUsers);
        }

        // PUT: api/UsersAPI/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutApplicationUser([FromRoute] string id, [FromBody] ApplicationUser applicationUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != applicationUser.Id)
            {
                return BadRequest();
            }

            _context.Entry(applicationUser).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ApplicationUserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/UsersAPI
        [HttpPost]
        public async Task<IActionResult> PostApplicationUser([FromBody] ApplicationUser applicationUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.User.Add(applicationUser);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetApplicationUser", new { id = applicationUser.Id }, applicationUser);
        }

        // DELETE: api/UsersAPI/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteApplicationUser([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var applicationUser = await _context.User.SingleOrDefaultAsync(m => m.Id == id);
            if (applicationUser == null)
            {
                return NotFound();
            }

            _context.User.Remove(applicationUser);
            await _context.SaveChangesAsync();

            return Ok(applicationUser);
        }

        private bool ApplicationUserExists(string id)
        {
            return _context.User.Any(e => e.Id == id);
        }
    }
}