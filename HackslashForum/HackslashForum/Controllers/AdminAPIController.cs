using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HackslashForum.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HackslashForum.Controllers
{
    [Produces("application/json")]
    [Route("api/AdminAPI")]
    public class AdminAPIController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminAPIController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult GetAdmins()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var hej = _context.Users
                .Include(u => u.);

            var adminList = from ur in _context.UserRoles
                            where ur.RoleId == "2e87cb3b-2f8c-457e-a201-df159dc95b4b"
                            select ur;

            if (adminList == null)
            {
                return NotFound();
            }

            return Ok(adminList);
        }


    }
}