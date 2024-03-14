using DemoUploadFileBas64.Models;

namespace DemoUploadFileBas64.Helper
{
    public static class UploadedFileHelper
    {
        public static bool IsImage(this UploadedFile file)
        {
            string[] imageExtensions = { ".jpg", ".jpeg", ".png", ".gif", ".bmp" };
            return imageExtensions.Contains(Path.GetExtension(file.FileName).ToLower());
        }
    }
}
