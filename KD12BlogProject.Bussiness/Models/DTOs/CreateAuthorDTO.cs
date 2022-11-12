using KD12BlogProject.Bussiness.Extensions;
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
    public class CreateAuthorDTO
    {
        [Required(ErrorMessage = "Must to type first name")]
        [MinLength(3, ErrorMessage = "Minimum lenght is 3")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Must to type last name")]
        [MinLength(3, ErrorMessage = "Minimum lenght is 3")]
        public string LastName { get; set; }

        //View'dan Controller'a dolayısıyla serviceye kullanıcı tarafından yüklenilen resmin yolunu taşıma için kullanılacaktır.
        [PictureFileExtension] //Custom data annatotaion yazdık
        public IFormFile UploadPath { get; set; }

        public string? ImagePath { get; set; }

        public DateTime CreateDate => DateTime.Now;
        public Status Status => Status.Active;
    }
}
