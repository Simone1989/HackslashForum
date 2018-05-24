using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HackslashForum.Models
{
    public class VoteOnCommentModel
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public int CommentId { get; set; }
        public Comment Comment { get; set; }
        public Vote Vote { get; set; }
    }
}
