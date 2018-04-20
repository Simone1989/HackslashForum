using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace HackslashForum.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public DateTime AccountCreationDate { get; set; }
        public string Location { get; set; }
        public string ProfilePicture { get; set; }
        public DateTime? LastLogin { get; set; }


        public IList<Post> Posts { get; set; }
        public IList<Comment> Comments { get; set; }

    }
}
