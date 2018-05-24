using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using HackslashForum.Models;

namespace HackslashForum.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<ApplicationUser> User { get; set; }
        public DbSet<Comment> Comment { get; set; }
        public DbSet<Post> Post { get; set; }
        public DbSet<UserVoteModel> VotedUsers { get; set; }
        public DbSet<VoteOnCommentModel> VotedComments { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {

            builder.Entity<Post>().Property(p => p.Title).HasMaxLength(80);

            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);

            builder.Entity<ApplicationUser>()
                .HasMany(u => u.Posts)
                .WithOne(p => p.User);
            builder.Entity<Comment>()
                .HasOne(c => c.User)
                .WithMany(u => u.Comments);
            builder.Entity<Post>()
                .HasMany(p => p.Comments)
                .WithOne(c => c.Post);

        }
    }
}
