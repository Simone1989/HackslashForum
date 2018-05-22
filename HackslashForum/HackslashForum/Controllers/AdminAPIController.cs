using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HackslashForum.Data;
using HackslashForum.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HackslashForum.Controllers
{
    [Produces("application/json")]
    [Route("api/AdminAPI")]
    public class AdminAPIController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public AdminAPIController(UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public IActionResult GetAdmins()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // DENNA FUNGERAR MEN ÄR INTE OPTIMAL
           /*const string adminRole = "d12c4240-e49c-49cd-a8c3-6267b710d8a1";
            var adminList = from ur in _context.UserRoles
                            where ur.RoleId == adminRole
                            let found = _userManager.Users.Where(u => u.Id == ur.UserId).Single()
                            select new { Name = found.UserName, Email = found.Email };*/

            // FUNGRAR JUST NU BARA FÖR EN ADMIN, SKA FÖRÖSKA FIXA
            const string adminRole = "Admin";
            var adminList = from r in _context.Roles
                            where r.Name == adminRole
                            let foundRole = _context.UserRoles.Where(ur => ur.RoleId == r.Id).Single()
                            let foundUser = _userManager.Users.Where(u => u.Id == foundRole.UserId).Single()
                            select new { Name = foundUser.UserName, Email = foundUser.Email };

            if (adminList == null)
            {
                return NotFound();
            }

            return Ok(adminList);
        }
    }
}