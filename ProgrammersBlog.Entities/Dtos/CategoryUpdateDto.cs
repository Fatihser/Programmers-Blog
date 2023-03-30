using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammersBlog.Entities.Dtos
{
    public class CategoryUpdateDto
    {
        [Required]
        public int Id { get; set; }


        [DisplayName("Kategory Adi")]
        [Required(ErrorMessage = "{0} bos gecilemez")]
        [MaxLength(70, ErrorMessage = "{0} {1} karakterden buyuk olamaz.")]
        [MinLength(3, ErrorMessage = "{0} {1} karakterden az olmamalidir.")]
        public string Name { get; set; }

        [DisplayName("Kategory Aciklamasi")]
        [MaxLength(500, ErrorMessage = "{0} {1} karakterden buyuk olamaz.")]
        [MinLength(3, ErrorMessage = "{0} {1} karakterden az olmamalidir.")]
        public string Description { get; set; }

        [DisplayName("Kategory Ozel Not Alani")]
        [Required(ErrorMessage = "{0} bos gecilemez")]
        [MaxLength(50, ErrorMessage = "{0} {1} karakterden buyuk olamaz.")]
        [MinLength(3, ErrorMessage = "{0} {1} karakterden az olmamalidir.")]
        public string Note { get; set; }

        [DisplayName("Aktif mi")]
        [Required(ErrorMessage = "{0} bos gecilemez")]
        public bool IsActive { get; set; }

        [DisplayName("Silindi mi")]
        [Required(ErrorMessage = "{0} bos gecilemez")]
        public bool IsDeleted { get; set; }
    }
}
