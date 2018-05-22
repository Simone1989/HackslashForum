using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HackslashForum.Models
{
    public class RoleViewModel
    {

        public IList<IdentityRole> Roles { get; set; }
        public IList<ApplicationUser> ApplicationUsers { get; set; }
    }
}
