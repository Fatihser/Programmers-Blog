using Microsoft.AspNetCore.Http;
using ProgrammersBlog.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ProgrammersBlog.Mvc.Areas.Admin.Models
{
    public class ArticleUpdateViewModel
    {
        [Required]
        public int Id { get; set; }
        [DisplayName("Baslik")]
        [Required(ErrorMessage = "{0} alani bos gecilmemelidir.")]
        [MaxLength(100, ErrorMessage = "{0} alani {1} karakterden buyuk olmamalidir.")]
        [MinLength(5, ErrorMessage = "{0} alani {1} karakterden kucuk olmamalidir.")]
        public string Title { get; set; }

        [DisplayName("Icerik")]
        [Required(ErrorMessage = "{0} alani bos gecilmemelidir.")]
        [MinLength(20, ErrorMessage = "{0} alani {1} karakterden kucuk olmamalidir.")]
        public string Content { get; set; }
        [DisplayName("Thumbnail")]
        public string Thumbnail { get; set; }

        [DisplayName("Thumbnail Add")]
        public IFormFile ThumbnailFile { get; set; }

        [DisplayName("Tarih")]
        [Required(ErrorMessage = "{0} alani bos gecilmemelidir.")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Date { get; set; }

        [DisplayName("Yazar Adi")]
        [Required(ErrorMessage = "{0} alani bos gecilmemelidir.")]
        [MaxLength(50, ErrorMessage = "{0} alani {1} karakterden buyuk olmamalidir.")]
        [MinLength(0, ErrorMessage = "{0} alani {1} karakterden kucuk olmamalidir.")]
        public string SeoAuthor { get; set; }

        [DisplayName("Makale Aciklama")]
        [Required(ErrorMessage = "{0} alani bos gecilmemelidir.")]
        [MaxLength(150, ErrorMessage = "{0} alani {1} karakterden buyuk olmamalidir.")]
        [MinLength(0, ErrorMessage = "{0} alani {1} karakterden kucuk olmamalidir.")]
        public string SeoDescription { get; set; }

        [DisplayName("Makale Etiketler")]
        [Required(ErrorMessage = "{0} alani bos gecilmemelidir.")]
        [MaxLength(70, ErrorMessage = "{0} alani {1} karakterden buyuk olmamalidir.")]
        [MinLength(5, ErrorMessage = "{0} alani {1} karakterden kucuk olmamalidir.")]
        public string SeoTags { get; set; }

        [DisplayName("Kategory")]
        [Required(ErrorMessage = "{0} alani bos gecilmemelidir.")]
        public int CategoryId { get; set; }
        [DisplayName("Aktif Mi?")]
        [Required(ErrorMessage = "{0} alani bos gecilmemelidir.")]
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }

        [Required]
        public int UserId { get; set; }
        public IList<Category> Categories { get; set; }
    }
}
