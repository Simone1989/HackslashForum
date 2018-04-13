using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HackslashForum.Models;

namespace HackslashForum
{
    public class Comment
    {
        public int ID { get; set; }
        public string Content { get; set; }
        public DateTime DateTimeCommentMade { get; set; }
        public int Upvotes { get; set; }
        public int Downvotes { get; set; }
        public ApplicationUser User { get; set; }
        public Post Post { get; set; }
    }
}