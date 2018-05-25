using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HackslashForum.Models
{
    public enum Vote
    {
        Up,
        Down
    }

    public class UserVoteModel
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public int PostId { get; set; }
        public Post Post { get; set; }
        public Vote Vote { get; set; }
    }
}
