using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HackslashForum.Controllers
{
    public class UploadPictureViewModel
    {
        public UploadPictureViewModel()
        {
            ProfilePicture = new List<IFormFile>();
        }

        public List<IFormFile> ProfilePicture { get; set; }
    }
}