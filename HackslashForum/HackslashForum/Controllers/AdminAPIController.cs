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

            //d12c4240-e49c-49cd-a8c3-6267b710d8a1
            //2e87cb3b-2f8c-457e-a201-df159dc95b4b
            const string adminRole = "d12c4240-e49c-49cd-a8c3-6267b710d8a1";
            var adminList = from ur in _context.UserRoles
                            where ur.RoleId == adminRole
                            let found = _userManager.Users.Where(u => u.Id == ur.UserId).Single()
                            select new { Name = found.UserName, Email = found.Email };
            // [ {name, email}, ... ]
            if (adminList == null)
            {
                return NotFound();
            }

            return Ok(adminList);
        }
    }
}