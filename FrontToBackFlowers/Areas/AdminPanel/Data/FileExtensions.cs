using Microsoft.AspNetCore.Hosting;

namespace FrontToBackFlowers.Areas.AdminPanel.Data
{
    public static class FileExtensions
    {
        public static bool IsImage(this IFormFile file)
        {
            if (!file.ContentType.Contains("image")) return false;
            return true;
        }
        public static bool IsCorrectSize(this IFormFile file, int mb)
        {
            if (file.Length > 1024 * 1024 * mb)
            
                return false;
            return true;       
        }
        public async static Task<string> GenerateFile(this IFormFile file, string rootPath)
        {
            var unicalName = $"{Guid.NewGuid}-{file.FileName}";
            var imagePath = Path.Combine(rootPath, "img", unicalName);
            var fStream = new FileStream(imagePath, FileMode.Create);
            await file.CopyToAsync(fStream);
            fStream.Close();

            return unicalName;
        }
    }
}
