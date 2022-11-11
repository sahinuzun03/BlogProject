using KD12BlogProject.Core.Abstract.Entities;
using KD12BlogProject.Core.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KD12BlogProject.Entities.Concrete
{
    public class AppUser : IdentityUser, IBaseEntity
    {
        public string ImagePath { get; set; }

        [NotMapped]
        public IFormFile UploadPath { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public DateTime DeletedDate { get; set; }
        public Status Status { get; set; }
    }
}
