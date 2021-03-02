using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Application.Exceptions;
using Microsoft.AspNetCore.Http;

namespace Application.Infrastructure.Validations.Media
{
    /// <summary>
    /// Validating files
    /// </summary>
    public static class FileValidation
    {

        static readonly Regex _base64RegexPattern = new Regex(BASE64_REGEX_STRING, RegexOptions.Compiled);

        private const string BASE64_REGEX_STRING = @"^[a-zA-Z0-9\+/]*={0,3}$";
        
        /// <summary>
        /// Error message to be displayed
        /// </summary>
        public static string ErrorMessage = "";


        /// <summary>
        /// Check if file is a picture
        /// </summary>
        /// <param name="base64String"></param>
        /// <returns></returns>
        public static async Task<bool> BeAValidPicture(string base64String) {
            
            var file = converToByte(base64String);
            return FileValidation.GetImageFormat(file);

            // using (var ms = new MemoryStream())
            // {
            //     await file.CopyToAsync(ms);
                
            //     return FileValidation.GetImageFormat(ms.ToArray());
            // }


        }

        /// <summary>
        /// File must be of valid size
        /// </summary>
        /// <param name="base64String"></param>
        /// <param name="size"></param>
        /// <returns></returns>
         public static bool BeOfValidSize(string base64String, int size = 2097152) { 

             var file = converToByte(base64String);

            // return file.Length < size;

            using (var ms = new MemoryStream())
            {
                // await file.CopyToAsync(ms);
                return ms.Length < size;
            }

        }

        private static byte[] converToByte(string base64String) {
            // var rs = (!string.IsNullOrEmpty(base64String) && !string.IsNullOrWhiteSpace(base64String) && base64String.Length != 0 && base64String.Length % 4 == 0 && !base64String.Contains(" ") && !base64String.Contains("\t") && !base64String.Contains("\r") && !base64String.Contains("\n")) && (base64String.Length % 4 == 0 && _base64RegexPattern.Match(base64String, 0).Success);
        
            // if (!rs)
            //     return new byte[1];

            base64String = Regex.Replace(base64String, @"^data:image\/[a-zA-Z]+;base64,", string.Empty);

            try {
                return Convert.FromBase64String(base64String);
            }
            catch(Exception e) {
                throw new CustomMessageException(e.Message);
                // return new byte[1];
            }
        }

        /// <summary>
        /// Validate list of valid images for this application
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        private static bool GetImageFormat(byte[] bytes)
        {
            var bmp = Encoding.ASCII.GetBytes("BM");     // BMP
            var gif = Encoding.ASCII.GetBytes("GIF");    // GIF
            var png = new byte[] { 137, 80, 78, 71 };              // PNG
            var tiff = new byte[] { 73, 73, 42 };                  // TIFF
            var tiff2 = new byte[] { 77, 77, 42 };                 // TIFF
            var jpeg = new byte[] { 255, 216, 255, 224 };          // jpeg
            var jpeg2 = new byte[] { 255, 216, 255, 225 };         // jpeg canon

            if (bmp.SequenceEqual(bytes.Take(bmp.Length)))
                return true;
            if (gif.SequenceEqual(bytes.Take(gif.Length)))
                return true;
            if (png.SequenceEqual(bytes.Take(png.Length)))
                return true;
            if (tiff.SequenceEqual(bytes.Take(tiff.Length)))
                return true;

            if (tiff2.SequenceEqual(bytes.Take(tiff2.Length)))
                return true;

            if (jpeg.SequenceEqual(bytes.Take(jpeg.Length)))
                return true;

            if (jpeg2.SequenceEqual(bytes.Take(jpeg2.Length)))
                return true;

            return false;
        }
    }
}