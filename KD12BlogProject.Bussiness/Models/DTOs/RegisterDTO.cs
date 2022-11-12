using KD12BlogProject.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace KD12BlogProject.Bussiness.Models.DTOs
{
    public class RegisterDTO
    {
        [Required(ErrorMessage = "Must to type user name")]
        [Display(Name = "User Name")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Must to type password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Must to type password")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Must to type password")]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "E-Posta")]
        public string Email { get; set; }

        public DateTime CreateDate => DateTime.Now;

        public Status Status => Status.Active;
    }
}
