﻿using HackslashForum.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HackslashForum
{
    public enum Category
    {
        [Display(Name = "Diskussion")]
        Discussion,
        [Display(Name = "Fråga")]
        Question,
        [Display(Name = "Java")]
        Java,
        [Display(Name = "C#")]
        Csharp,
        [Display(Name = "Python")]
        Python,
        [Display(Name = "Övrigt")]
        Övrigt    
    }

    public class Post
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        [DataType("nvarchar")]
        public string Title { get; set; }
        public DateTime DateTimePostCreated { get; set; } = DateTime.Now;
        public Category Category { get; set; }
        public string Content { get; set; }
        public int UpVotes { get; set; }
        public int DownVotes { get; set; }

        public int TotalVotes
        {
            get { return (UpVotes - DownVotes); }
            set { }
        }

        public IList<Comment> Comments { get; set; } = new List<Comment>();
        public IList<ApplicationUser> UsersWhoVoted { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
    }
}
