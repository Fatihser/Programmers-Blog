using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProgrammersBlog.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammersBlog.Data.Concrete.EntityFramework.Mappings
{
    public class ArticleMap : IEntityTypeConfiguration<Article>
    {
        public void Configure(EntityTypeBuilder<Article> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a=>a.Id).ValueGeneratedOnAdd();
            builder.Property(a => a.Title).HasMaxLength(100);
            builder.Property(a => a.Title).IsRequired();
            builder.Property(a => a.Content).IsRequired();
            builder.Property(a => a.Content).HasColumnType("NVARCHAR(MAX)");
            builder.Property(a => a.Date).IsRequired();
            builder.Property(a=>a.SeoAuthor).IsRequired();
            builder.Property(a=>a.SeoAuthor).HasMaxLength(50);
            builder.Property(a=>a.SeoDescription).IsRequired();
            builder.Property(a => a.SeoDescription).HasMaxLength(150);
            builder.Property(a => a.SeoTags).IsRequired();
            builder.Property(a => a.SeoTags).HasMaxLength(70);
            builder.Property(a => a.ViewsCount).IsRequired();
            builder.Property(a => a.CommentCount).IsRequired();
            builder.Property(a => a.Thumbnail).IsRequired();
            builder.Property(a => a.Thumbnail).HasMaxLength(250);
            builder.Property(a=>a.CreatedByName).IsRequired();
            builder.Property(a => a.CreatedByName).HasMaxLength(50);
            builder.Property(a => a.ModifiedByName).IsRequired();
            builder.Property(a => a.ModifiedByName).HasMaxLength(50);
            builder.Property(a=>a.CreatedDate).IsRequired();
            builder.Property(a => a.ModifiedDate).IsRequired();
            builder.Property(a => a.IsActive).IsRequired();
            builder.Property(a => a.IsDeleted).IsRequired();
            builder.Property(a => a.Note).HasMaxLength(500);
            builder.HasOne<Category>(a => a.Category).WithMany(c => c.Articles).HasForeignKey(a=>a.CategoryId);
            builder.HasOne<User>(a => a.User).WithMany(u => u.Articles).HasForeignKey(a => a.UserId);
            builder.ToTable("Articles");

            //builder.HasData(new Article
            //{
            //    Id = 1,
            //    CategoryId = 1,
            //    Title = "C# 9.0 ve .NET 5 Yenilikleri",
            //    Content = "ASD ASZD ASD",
            //    Thumbnail="Default.jpg",
            //    SeoDescription="C# 9.0 ve .net 5 yenilikleri",
            //    SeoTags="c#,C# 9,.net framework , .net core",
            //    SeoAuthor="Fatih Suleyman Er",
            //    Date=DateTime.Now,
            //    IsActive = true,
            //    IsDeleted = false,
            //    CreatedByName = "InitialCreate",
            //    CreatedDate = DateTime.Now,
            //    ModifiedByName = "InitialCreate",
            //    ModifiedDate = DateTime.Now,
            //    Note = "C# Blog Categorysi",
            //    UserId=1,
            //    ViewsCount=100,
            //    CommentCount=1,
            //},
            //new Article
            //{
            //    Id = 2,
            //    CategoryId = 2,
            //    Title = "C++ 9.0 ve .NET 5 Yenilikleri",
            //    Content = "ASD ASZD ASD",
            //    Thumbnail = "Default.jpg",
            //    SeoDescription = "C++ 9.0 ve .net 5 yenilikleri",
            //    SeoTags = "c++,C++ 9,.net framework , .net core",
            //    SeoAuthor = "Fatih Suleyman Er",
            //    Date = DateTime.Now,
            //    IsActive = true,
            //    IsDeleted = false,
            //    CreatedByName = "InitialCreate",
            //    CreatedDate = DateTime.Now,
            //    ModifiedByName = "InitialCreate",
            //    ModifiedDate = DateTime.Now,
            //    Note = "C++ Blog Categorysi",
            //    UserId = 1,
            //    ViewsCount=99,
            //    CommentCount=1,
            //},
            //new Article
            //{
            //    Id = 3,
            //    CategoryId = 3,
            //    Title = "java 9.0 ve .NET 5 Yenilikleri",
            //    Content = "ASD ASZD ASD",
            //    Thumbnail = "Default.jpg",
            //    SeoDescription = "java 9.0 ve .net 5 yenilikleri",
            //    SeoTags = "javajava 9,.net framework , .net core",
            //    SeoAuthor = "Fatih Suleyman Er",
            //    Date = DateTime.Now,
            //    IsActive = true,
            //    IsDeleted = false,
            //    CreatedByName = "InitialCreate",
            //    CreatedDate = DateTime.Now,
            //    ModifiedByName = "InitialCreate",
            //    ModifiedDate = DateTime.Now,
            //    Note = "java Blog Categorysi",
            //    UserId = 1,
            //    ViewsCount=12,
            //    CommentCount=1,
            //}

            //);builder.HasData(new Article
            //{
            //    Id = 1,
            //    CategoryId = 1,
            //    Title = "C# 9.0 ve .NET 5 Yenilikleri",
            //    Content = "ASD ASZD ASD",
            //    Thumbnail="Default.jpg",
            //    SeoDescription="C# 9.0 ve .net 5 yenilikleri",
            //    SeoTags="c#,C# 9,.net framework , .net core",
            //    SeoAuthor="Fatih Suleyman Er",
            //    Date=DateTime.Now,
            //    IsActive = true,
            //    IsDeleted = false,
            //    CreatedByName = "InitialCreate",
            //    CreatedDate = DateTime.Now,
            //    ModifiedByName = "InitialCreate",
            //    ModifiedDate = DateTime.Now,
            //    Note = "C# Blog Categorysi",
            //    UserId=1,
            //    ViewsCount=100,
            //    CommentCount=1,
            //},
            //new Article
            //{
            //    Id = 2,
            //    CategoryId = 2,
            //    Title = "C++ 9.0 ve .NET 5 Yenilikleri",
            //    Content = "ASD ASZD ASD",
            //    Thumbnail = "Default.jpg",
            //    SeoDescription = "C++ 9.0 ve .net 5 yenilikleri",
            //    SeoTags = "c++,C++ 9,.net framework , .net core",
            //    SeoAuthor = "Fatih Suleyman Er",
            //    Date = DateTime.Now,
            //    IsActive = true,
            //    IsDeleted = false,
            //    CreatedByName = "InitialCreate",
            //    CreatedDate = DateTime.Now,
            //    ModifiedByName = "InitialCreate",
            //    ModifiedDate = DateTime.Now,
            //    Note = "C++ Blog Categorysi",
            //    UserId = 1,
            //    ViewsCount=99,
            //    CommentCount=1,
            //},
            //new Article
            //{
            //    Id = 3,
            //    CategoryId = 3,
            //    Title = "java 9.0 ve .NET 5 Yenilikleri",
            //    Content = "ASD ASZD ASD",
            //    Thumbnail = "Default.jpg",
            //    SeoDescription = "java 9.0 ve .net 5 yenilikleri",
            //    SeoTags = "javajava 9,.net framework , .net core",
            //    SeoAuthor = "Fatih Suleyman Er",
            //    Date = DateTime.Now,
            //    IsActive = true,
            //    IsDeleted = false,
            //    CreatedByName = "InitialCreate",
            //    CreatedDate = DateTime.Now,
            //    ModifiedByName = "InitialCreate",
            //    ModifiedDate = DateTime.Now,
            //    Note = "java Blog Categorysi",
            //    UserId = 1,
            //    ViewsCount=12,
            //    CommentCount=1,
            //}

            //);
        }
    }
}
