using KD12BlogProject.Bussiness.Extensions;
using KD12BlogProject.Bussiness.Models.VMs;
using KD12BlogProject.Core.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KD12BlogProject.Bussiness.Models.DTOs
{
    public class UpdatePostDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Must to type title")]
        [MinLength(3, ErrorMessage = "Minimum lenght is 3")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Must to type content")]
        [MinLength(3, ErrorMessage = "Minimum lenght is 3")]
        public string Content { get; set; }

        public string? ImagePath { get; set; }


        [PictureFileExtension]
        public IFormFile? UploadPath { get; set; }

        public DateTime UpdateDate => DateTime.Now;
        public Status Status => Status.Modified;


        public int GenreId { get; set; }

        public int AuthorId { get; set; }

        public List<GenreVM>? Genres { get; set; }
        public List<AuthorVM>? Authors { get; set; }
    }
}
