using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammersBlog.Entities.Dtos
{
    public class EmailSendDto
    {
        [DisplayName("Isminiz")]
        [Required(ErrorMessage ="{0} alani zorunludur.")]
        [MaxLength(60,ErrorMessage ="{0} alani en fazla {1} karakterden olusmalidir.")]
        [MinLength(5,ErrorMessage ="{0} alani en az {1} karakterden olusmalidir.")]
        public string Name { get; set; }
        [DisplayName("E-posta Adresiniz:")]
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "{0} alani zorunludur.")]
        [MaxLength(100, ErrorMessage = "{0} alani en fazla {1} karakterden olusmalidir.")]
        [MinLength(10, ErrorMessage = "{0} alani en az {1} karakterden olusmalidir.")]
        public string Email { get; set; }
        [DisplayName("Konu")]
        [Required(ErrorMessage = "{0} alani zorunludur.")]
        [MaxLength(125, ErrorMessage = "{0} alani en fazla {1} karakterden olusmalidir.")]
        [MinLength(5, ErrorMessage = "{0} alani en az {1} karakterden olusmalidir.")]
        public string Subject { get; set; }
        [DisplayName("Mesajiniz")]
        [Required(ErrorMessage = "{0} alani zorunludur.")]
        [MaxLength(1500, ErrorMessage = "{0} alani en fazla {1} karakterden olusmalidir.")]
        [MinLength(20, ErrorMessage = "{0} alani en az {1} karakterden olusmalidir.")]
        public string Message { get; set; }
    }
}
