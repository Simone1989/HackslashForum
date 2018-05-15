using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

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