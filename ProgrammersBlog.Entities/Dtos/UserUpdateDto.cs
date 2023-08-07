using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammersBlog.Entities.Dtos
{
    public class UserUpdateDto
    {
        [Required]
        public int Id { get; set; }
        [DisplayName("kULLANICI Adi")]
        [Required(ErrorMessage = "{0} bos gecilemez")]
        [MaxLength(50, ErrorMessage = "{0} {1} karakterden fAZLA olamaz.")]
        [MinLength(3, ErrorMessage = "{0} {1} karakterden az olmamalidir.")]
        public string UserName { get; set; }
        [DisplayName("Email Adresi")]
        [Required(ErrorMessage = "{0} bos gecilemez")]
        [MaxLength(100, ErrorMessage = "{0} {1} karakterden buyuk olamaz.")]
        [MinLength(10, ErrorMessage = "{0} {1} karakterden az olmamalidir.")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public string Password { get; set; }
        [DisplayName("Telefon Numarasi")]
        [Required(ErrorMessage = "{0} bos gecilemez")]
        [MaxLength(13, ErrorMessage = "{0} {1} karakterden buyuk olamaz.")]
        [MinLength(13, ErrorMessage = "{0} {1} karakterden az olmamalidir.")]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }
        [DisplayName("Resim Ekle")]
        [DataType(DataType.Upload)]
        public IFormFile PictureFile { get; set; }
        [DisplayName("Resim")]
        public string Picture { get; set; }
    }
}
