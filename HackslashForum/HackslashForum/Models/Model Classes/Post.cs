using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HackslashForum
{
    public class Post
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime DateTimePostCreated { get; set; }
        public string Content { get; set; }
        public int UpVotes { get; set; }
        public int DownVotes { get; set; }
        public IList<Comment> Comments { get; set; }
        public User User { get; set; }
    }
}
