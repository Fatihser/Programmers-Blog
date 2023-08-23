using ProgrammersBlog.Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammersBlog.Services.Utilities
{
    public static class Messages
    {
        public static class Category
        {
            public static string NotFound(bool isPlural)
            {
                if (isPlural)
                {
                    return "Hicbir kategory bulunamadi.";
                }
                else
                {
                    return "Boyle bir kategori bulunamadi";
                }
            }
            public static string Add(string categoryName)
            {
                return $"{categoryName} adli kategori basariyla eklenmsitir";
            }

            public static string Update(string categoryName)
            {
                return $"{categoryName} adli kategori basariyla guncellendi.";
            }
            public static string Delete(string categoryName)
            {
                return $"{categoryName} adli kategory basariyla silinmistir.";
            }
            public static string HardDelete(string categoryName)
            {
                return $"{categoryName} adli kategori veritabanindan basari ile silinmistir.";
            }
        }
        public static class Article
        {
            public static string NotFound(bool isPlural)
            {
                if (isPlural)
                {
                    return "Hicbir makale bulunamadi.";
                }
                else
                {
                    return "Boyle bir makale bulunamadi";
                }
            }
            public static string Add(string articleName)
            {
                return $"{articleName} adli makale basariyla eklenmsitir";
            }

            public static string Update(string articleName)
            {
                return $"{articleName} adli makale basariyla guncellendi.";
            }
            public static string Delete(string articleName)
            {
                return $"{articleName} adli makale basariyla silinmistir.";
            }
            public static string HardDelete(string articleName)
            {
                return $"{articleName} adli makale veritabanindan basari ile silinmistir.";
            }
        }
        public static class Comment
        {
            public static string NotFound(bool isPlural)
            {
                if (isPlural) return "Hiç bir yorum bulunamadı.";
                return "Böyle bir yorum bulunamadı.";
            }
            public static string Approve(int commentId)
            {
                return $"{commentId} lu yorum başarıyla onaylanmistir.";
            }
            public static string Add(string createdByName)
            {
                return $"Sayın {createdByName}, yorumunuz başarıyla eklenmiştir.";
            }

            public static string Update(string createdByName)
            {
                return $"{createdByName} tarafından eklenen yorum başarıyla güncellenmiştir.";
            }
            public static string Delete(string createdByName)
            {
                return $"{createdByName} tarafından eklenen yorum başarıyla silinmiştir.";
            }
            public static string HardDelete(string createdByName)
            {
                return $"{createdByName} tarafından eklenen yorum başarıyla veritabanından silinmiştir.";
            }
        }
    }
    
}
