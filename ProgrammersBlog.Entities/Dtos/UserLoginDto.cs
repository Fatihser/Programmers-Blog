using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammersBlog.Entities.Dtos
{
    public class UserLoginDto
    {
        [DisplayName("Email Adresi")]
        [Required(ErrorMessage = "{0} bos gecilemez")]
        [MaxLength(100, ErrorMessage = "{0} {1} karakterden buyuk olamaz.")]
        [MinLength(10, ErrorMessage = "{0} {1} karakterden az olmamalidir.")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [DisplayName("Sifre")]
        [Required(ErrorMessage = "{0} bos gecilemez")]
        [MaxLength(30, ErrorMessage = "{0} {1} karakterden buyuk olamaz.")]
        [MinLength(5, ErrorMessage = "{0} {1} karakterden az olmamalidir.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DisplayName("Beni Hatirla")]
        public bool RememberMe { get; set; }
    }
}
