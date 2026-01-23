using System;
using System.IO;
using System.Web;

namespace WebDDHT.Helpers
{
    /// <summary>
    /// Helper for image upload and management
    /// </summary>
    public static class ImageHelper
    {
        private const long MAX_FILE_SIZE = 5 * 1024 * 1024; // 5MB
        private static readonly string[] ALLOWED_EXTENSIONS = { ".jpg", ".jpeg", ".png", ".gif" };

        /// <summary>
        /// Save uploaded image to server
        /// </summary>
        /// <param name="file">Uploaded file</param>
        /// <param name="folder">Subfolder (e.g., "products", "coupons")</param>
        /// <returns>Relative path to saved image</returns>
        public static string SaveImage(HttpPostedFileBase file, string folder)
        {
            if (file == null || file.ContentLength == 0)
            {
                throw new ArgumentException("File không hợp lệ.");
            }

            // Validate file size
            if (file.ContentLength > MAX_FILE_SIZE)
            {
                throw new ArgumentException($"File quá lớn.Kích thước tối đa {MAX_FILE_SIZE / 1024 / 1024}MB.");
            }

            // Validate extension
            string extension = Path.GetExtension(file.FileName).ToLower();
            if (!Array.Exists(ALLOWED_EXTENSIONS, ext => ext == extension))
            {
                throw new ArgumentException("Chỉ chấp nhận file ảnh (jpg, jpeg, png, gif).");
            }

            // Generate unique filename
            string fileName = $"{Guid.NewGuid()}{extension}";

            // Create directory if not exists
            string uploadPath = HttpContext.Current.Server.MapPath($"~/Upload/{folder}");
            if (!Directory.Exists(uploadPath))
            {
                Directory.CreateDirectory(uploadPath);
            }

            // Save file
            string filePath = Path.Combine(uploadPath, fileName);
            file.SaveAs(filePath);

            // Return relative path
            return $"/Upload/{folder}/{fileName}";
        }

        /// <summary>
        /// Delete image from server
        /// </summary>
        /// <param name="imagePath">Relative path to image</param>
        public static bool DeleteImage(string imagePath)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(imagePath))
                    return false;

                string fullPath = HttpContext.Current.Server.MapPath($"~{imagePath}");

                if (File.Exists(fullPath))
                {
                    File.Delete(fullPath);
                    return true;
                }

                return false;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Get image URL or default placeholder
        /// </summary>
        public static string GetImageUrl(string imagePath, string defaultImage = "/Content/images/no-image.png")
        {
            return !string.IsNullOrWhiteSpace(imagePath) ? imagePath : defaultImage;
        }
    }
}