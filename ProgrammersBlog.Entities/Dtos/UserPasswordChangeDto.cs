using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammersBlog.Entities.Dtos
{
    public class UserPasswordChangeDto
    {
        [DisplayName("Su Anki Sifreniz")]
        [Required(ErrorMessage = "{0} bos gecilemez")]
        [MaxLength(30, ErrorMessage = "{0} {1} karakterden buyuk olamaz.")]
        [MinLength(5, ErrorMessage = "{0} {1} karakterden az olmamalidir.")]
        [DataType(DataType.Password)]
        public string CurrentPassword { get; set; }
        [DisplayName("Yeni Sifreniz")]
        [Required(ErrorMessage = "{0} bos gecilemez")]
        [MaxLength(30, ErrorMessage = "{0} {1} karakterden buyuk olamaz.")]
        [MinLength(5, ErrorMessage = "{0} {1} karakterden az olmamalidir.")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }
        [DisplayName("Sifrenizin Tekrari")]
        [Required(ErrorMessage = "{0} bos gecilemez")]
        [MaxLength(30, ErrorMessage = "{0} {1} karakterden buyuk olamaz.")]
        [MinLength(5, ErrorMessage = "{0} {1} karakterden az olmamalidir.")]
        [DataType(DataType.Password)]
        [Compare("NewPassword",ErrorMessage ="Sifreler uyusmuyor.")]
        public string RepeatPassword { get; set; }
    }
}
