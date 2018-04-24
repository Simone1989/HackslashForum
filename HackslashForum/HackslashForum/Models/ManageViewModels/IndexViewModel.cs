using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HackslashForum.Models.ManageViewModels
{
    public class IndexViewModel
    {
        public string Username { get; set; }

        public bool IsEmailConfirmed { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Phone]
        [Display(Name = "Phone number")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Profilbild")]
        public byte[] ProfilePicture { get; set; }

        public string StatusMessage { get; set; }

        public int PostUpVotes { get; set; }
        public int PostDownVotes { get; set; }

        public int CommentUpVotes { get; set; }
        public int CommentDownVotes { get; set; }

        public DateTime? LastLogin { get; set; } 
        public DateTime AccountCreated { get; set; }

        public int NumberOfPosts { get; set; }



    }
}
