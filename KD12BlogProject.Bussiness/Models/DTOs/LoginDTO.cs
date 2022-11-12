using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace KD12BlogProject.Bussiness.Models.DTOs
{
    public class LoginDTO
    {
        [Required(ErrorMessage = "Must to type user name")]
        [Display(Name = "User Name")]
        public string? UserName { get; set; }

        [Required(ErrorMessage = "Must to type password")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
    }
}
