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

            // MAN MÅSTE FORTFARNDE PLOCKA UT NAME = "Admin", TROR JAG
            List<ApplicationUser> users = new List<ApplicationUser>();
            List<IdentityUserRole<string>> userRoles = new List<IdentityUserRole<string>>();
            foreach (var role in _context.Roles)
            {
                if(role.Name == "Admin")
                {
                    foreach (var userRole in _context.UserRoles)
                    {
                        if (userRole.RoleId == role.Id)
                            userRoles.Add(userRole);
                    }
                }

            }
            foreach(var userRole in userRoles)
            {
                foreach (var user in _userManager.Users)
                {
                    if (user.Id == userRole.UserId)
                        users.Add(user);
                }

            }

            if (users == null)
            {
                return NotFound();
            }
            return Ok(users);
        }
    }
}