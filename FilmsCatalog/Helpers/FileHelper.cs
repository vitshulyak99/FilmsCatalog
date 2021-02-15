using System;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace FilmsCatalog.Helpers
{
    public class FileHelper
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private const string Placeholder = "https://via.placeholder.com/1280x720";
        
        public FileHelper(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment ?? throw new ArgumentNullException(nameof(webHostEnvironment));
        }

        public string MakeUrlToFile(string imageName)
            => string.IsNullOrEmpty(imageName) ? Placeholder : Path.Combine("/assets/img", imageName).Replace("\\", "/");

        public void DeleteFile(string filename)
        {
            if (!string.IsNullOrEmpty(filename))
            {
                DeleteFile(GetPathToFile(filename));
            }
        }

        public string GetPathToFile(string imageName)
            => Path.Combine(MakeFileLocationFolder("assets/img"), imageName);

        public string SaveUploadedFile(IFormFile file)
        {
            if (file?.FileName is null) return string.Empty;
            var newName = new Random().Next(1111, 9999).ToString() + file.FileName;
            using var fileStream = new FileStream(GetPathToFile(newName),FileMode.Create);
            file.CopyTo(fileStream);
            return newName;
        }

        private string MakeFileLocationFolder(string rootRelativeFileLocation)
            => Path.Combine(_webHostEnvironment.ContentRootPath, "wwwroot", rootRelativeFileLocation);

        private void DeleteFile(params string[] filePaths)
        {
            foreach (var filePath in filePaths)
            {
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }
            }
        }
    }
}