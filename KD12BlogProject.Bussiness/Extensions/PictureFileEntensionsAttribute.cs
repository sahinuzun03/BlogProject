using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KD12BlogProject.Bussiness.Extensions
{
    //BURADA RESİM YÜKLERKEN RESİM DOSYALARI HARİCİNDE (JPEG/PNG/JPG) DIŞINDA KALAN DOSYALARIN YÜKLENMESİNİ ENGELLEDİĞİMİZ YAPI
    public class PictureFileExtensionAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            IFormFile file = value as IFormFile;

            if(file != null)
            {
                var extension = Path.GetExtension(file.FileName).ToLower();

                string[] extensions = { "jpg", "jpeg", "png" };

                bool result = extensions.Any(x => x.EndsWith(x));

                if (!result)
                {
                    return new ValidationResult("Yükelenen dosya png,jpeg,jpg formatlarında olmak zorundadır");
                }
            }
            return ValidationResult.Success;
        }
    }
}
