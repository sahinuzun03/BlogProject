﻿using KD12BlogProject.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KD12BlogProject.Bussiness.Models.DTOs
{
    public class CreateGenreDTO
    {
        
        [Required(ErrorMessage = "Must to type genre name")]
        [MinLength(3, ErrorMessage = "Minimum lenfgt is 3")]
        public string Name { get; set; }
        public DateTime CreateDate => DateTime.Now;
        public Status Status => Status.Active;
    }
}
