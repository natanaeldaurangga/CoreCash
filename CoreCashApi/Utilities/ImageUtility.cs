using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreCashApi.Utilities
{
    public class ImageUtility
    {
        private readonly IWebHostEnvironment _env;

        private readonly string _imagePath = "img\\";

        private readonly IConfiguration _config;

        public ImageUtility(IWebHostEnvironment env, IConfiguration config)
        {
            _env = env;
            _config = config;
        }

        public async Task<string> UploadImageAsync(IFormFile file, string folder = "")
        {
            if (file == null || file.Length == 0)
                return _config.GetSection("Image:Default").Value;

            string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);

            string filePath;
            if (!string.IsNullOrEmpty(folder) || !string.IsNullOrWhiteSpace(folder))
            {
                filePath = Path.Combine(_env.WebRootPath, _imagePath + folder + "/" + fileName);
            }
            else
            {
                filePath = Path.Combine(_env.WebRootPath, _imagePath + fileName);
            }

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return folder + "%5C%5C" + fileName;
        }

        public bool DeleteImage(string fileName)
        {
            var tempFileName = fileName.Replace("%5C", "\\");
            var filePath = Path.Combine(_env.WebRootPath, _imagePath + tempFileName);

            if (!File.Exists(filePath)) return false;

            File.Delete(filePath);
            return true;
        }

        public async Task<byte[]> GetImageAsync(string fileName)
        {
            var tempFileName = fileName.Replace("%5C", "\\");
            var filePath = Path.Combine(_env.WebRootPath, _imagePath + tempFileName);

            if (!File.Exists(filePath))
                throw new FileNotFoundException();

            return await File.ReadAllBytesAsync(filePath);
        }
    }
}