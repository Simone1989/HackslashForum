using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace HackslashForum.Data
{
    public class SeedData
    {
        public static void Initialize(ApplicationDbContext seedContext)
        {
            seedContext.Database.EnsureCreated();

            if (seedContext.Roles.Any())
                return;

            seedContext.Roles.AddRange(
                new IdentityRole
                {
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                   
                },
                new IdentityRole
                {
                    Name = "Member",
                    NormalizedName = "MEMBER"
                });

            seedContext.SaveChanges();
        }
    }
}
