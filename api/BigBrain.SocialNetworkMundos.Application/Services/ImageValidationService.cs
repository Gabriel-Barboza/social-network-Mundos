using BigBrain.SocialNetworkMundos.Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using static System.Net.Mime.MediaTypeNames;
using System.Drawing;
using System.Drawing.Imaging;

namespace BigBrain.SocialNetworkMundos.Application.Services
{
    public class ImageValidationService : IImageValidationService
    {
        private readonly List<string> _allowedExtensions = new() { ".jpg", ".jpeg", ".png", ".gif" };
        private readonly long _maxFileSize = 2 * 1024 * 1024;

        public bool IsValidImage(IFormFile file, out string validationError)
        {
            validationError = string.Empty;

            // Check if the file is null
            if (file == null || file.Length == 0)
            {
                validationError = "File is null.";
                return false;
            }

            if (file.Length > _maxFileSize)
            {
                validationError = $"File size exceeds the maximum limit of {_maxFileSize / (1024 * 1024)} MB.";
                return false;
            }

            var extension = Path.GetExtension(file.FileName).ToLower();
            if (!_allowedExtensions.Contains(extension))
            {
                validationError = $"Invalid file extension. Allowed extensions are: {string.Join(", ", _allowedExtensions)}";
                return false;
            }

            try
            {
                using var image = System.Drawing.Image.FromStream(file.OpenReadStream());
                // Extra validation: MIME type
                if (ImageFormat.Jpeg.Equals(image.RawFormat) ||
                    ImageFormat.Png.Equals(image.RawFormat) ||
                    ImageFormat.Gif.Equals(image.RawFormat))


                {
                    return true;
                }
                else
                {
                    validationError = "Invalid image format.";
                    return false;
                }
            }
            catch
            {
                validationError = "The file is not a valid image.";
                return false;
            }
        }
    }
}
            

