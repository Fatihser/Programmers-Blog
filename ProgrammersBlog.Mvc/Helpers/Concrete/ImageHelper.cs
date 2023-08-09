using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using ProgrammersBlog.Entities.Dtos;
using ProgrammersBlog.Mvc.Helpers.Abstract;
using ProgrammersBlog.Shared.Utilities.Results.Abstract;
using System.IO;
using System;
using System.Threading.Tasks;
using ProgrammersBlog.Shared.Utilities.Extensions;
using ProgrammersBlog.Shared.Utilities.Results.ComplexTypes;
using ProgrammersBlog.Shared.Utilities.Results.Concrete;
using ProgrammersBlog.Entities.ComplexTypes;

namespace ProgrammersBlog.Mvc.Helpers.Concrete
{
    public class ImageHelper : IImageHelper
    {
        private readonly IWebHostEnvironment _env;
        private readonly string _wwwroot;
        private const string imgFolder = "img";
        private const string userImagesFolder = "userImages";
        private const string postImagesFolder = "postImages";

        public ImageHelper(IWebHostEnvironment env)
        {
            _env = env;
            _wwwroot = _env.WebRootPath;
        }

        public IDataResult<ImageDeletedDto> Delete(string pictureName)
        {
            var filetodelete = Path.Combine($"{_wwwroot}/{imgFolder}/", pictureName);
            if (System.IO.File.Exists(filetodelete))
            {
                var fileInfo = new FileInfo(filetodelete);
                var imageDeletedDto = new ImageDeletedDto
                {
                    FullName=pictureName,
                    Extension=fileInfo.Extension,
                    Path=fileInfo.FullName,
                    Size=fileInfo.Length
                };
                System.IO.File.Delete(filetodelete);
                return new DataResult<ImageDeletedDto>(ResultStatus.Success, imageDeletedDto);
            }
            else
            {
                return new DataResult<ImageDeletedDto>(ResultStatus.Error,$"Boyle bir resim bulunamadi",null);
            }
        }

        public async Task<IDataResult<uploadedImageDto>> Upload(string name, IFormFile pictureFile, PictureType pictureType,string folderName=null)
        {

            folderName ??= pictureType == PictureType.User ? userImagesFolder : postImagesFolder;

            if (!Directory.Exists($"{_wwwroot}/{imgFolder}/{folderName}"))
            {
                Directory.CreateDirectory($"{_wwwroot}/{imgFolder}/{folderName}");
            }
            string oldFileName = Path.GetFileNameWithoutExtension(pictureFile.FileName);
            string fileExtension = Path.GetExtension(pictureFile.FileName);
            DateTime dateTime = DateTime.Now;
            string newFileName = $"{name}_{dateTime.FullDateAndTimeStringWithUnderscore()}{fileExtension}";
            var path = Path.Combine($"{_wwwroot}/{imgFolder}/{folderName}", newFileName);
            await using (var stream = new FileStream(path, FileMode.Create))
            {
                await pictureFile.CopyToAsync(stream);
            }
            string message =PictureType.User==pictureType ? $"{name} adli kullanicinin resmi basariyla yuklenmistir." 
                : $"{name} adli makalenin resmi basariyla yuklenmistir.";
            return new DataResult<uploadedImageDto>(ResultStatus.Success,message,
            new uploadedImageDto
            {
                FullName=$"{folderName}/{newFileName}",
                OldName=oldFileName,
                Extension=fileExtension,
                FolderName=folderName,
                Path=path,
                Size=pictureFile.Length
            });
        }
    }
}
