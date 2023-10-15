using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammersBlog.Entities.Concrete
{
    public class AboutUsPageInfo
    {
        [DisplayName("Baslik")]
        [Required(ErrorMessage = "{0} alani bos gecilmemelidir.")]
        [MaxLength(100, ErrorMessage = "{0} alani {1} karakterden buyuk olmamalidir.")]
        [MinLength(5, ErrorMessage = "{0} alani {1} karakterden kucuk olmamalidir.")]
        public string Header { get; set; }
        [DisplayName("Icerik")]
        [Required(ErrorMessage = "{0} alani bos gecilmemelidir.")]
        [MaxLength(1500, ErrorMessage = "{0} alani {1} karakterden buyuk olmamalidir.")]
        [MinLength(5, ErrorMessage = "{0} alani {1} karakterden kucuk olmamalidir.")]
        public string Content { get; set; }
        [DisplayName("Seo Aciklamasi")]
        [Required(ErrorMessage = "{0} alani bos gecilmemelidir.")]
        [MaxLength(100, ErrorMessage = "{0} alani {1} karakterden buyuk olmamalidir.")]
        [MinLength(5, ErrorMessage = "{0} alani {1} karakterden kucuk olmamalidir.")]
        public string SeoDescription { get; set; }
        [DisplayName("Seo Aciklamasi")]
        [Required(ErrorMessage = "{0} alani bos gecilmemelidir.")]
        [MaxLength(100, ErrorMessage = "{0} alani {1} karakterden buyuk olmamalidir.")]
        [MinLength(5, ErrorMessage = "{0} alani {1} karakterden kucuk olmamalidir.")]
        public string SeoTags { get; set; }
        [DisplayName("Seo Yazar")]
        [Required(ErrorMessage = "{0} alani bos gecilmemelidir.")]
        [MaxLength(60, ErrorMessage = "{0} alani {1} karakterden buyuk olmamalidir.")]
        [MinLength(5, ErrorMessage = "{0} alani {1} karakterden kucuk olmamalidir.")]
        public string SeoAuthor { get; set; }
    }
}
