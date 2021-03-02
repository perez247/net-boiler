using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Application.Exceptions;
using Application.Infrastructure.Functions;
using Application.Interfaces.IServices;
using Domain.Entities.Inheritable;
using Infrastructure.Implementation.Document;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp.Processing;
using Microsoft.Extensions.Hosting;

namespace Infrastructure.Implementation.Photos
{
    /// <summary>
    /// Implementation of photo service
    /// </summary>
    public class PhotoService : IPhotoService
    {
        /// <summary>
        /// Hosting environment
        /// </summary>
        /// <value></value>
        public IWebHostEnvironment _ienv { get; set; }

        private static string initialDir = "../Infrastructure/data/";
        private static string hostName = EnvHelperFunction.HOSTNAME;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="ienv"></param>
        public PhotoService(IWebHostEnvironment ienv)
        {
            _ienv = ienv;
        }

        /// <summary>
        /// Upload Profile picture
        /// </summary>
        /// <returns></returns>
        public async Task<AppFile> UploadProfilePicture(IFormFile file)
        {
            if (file == null) return null;

            var image = Image.Load(file.OpenReadStream());

            var diff = Math.Abs(image.Height - image.Width);

            if (diff > 5)
                throw new CustomMessageException("An image ratio of 1:1 is required", HttpStatusCode.BadRequest);

            if (image.Size().Width > 300)
                image.Mutate(x => x.Resize(300, 300));

            return await UploadImage(image);

        }

        /// <summary>
        /// Upload Background picture
        /// </summary>
        /// <returns></returns>
        public async Task<AppFile> UploadBackGroundPicture(IFormFile file)
        {
            if (file == null) return null;
            var image = Image.Load(file.OpenReadStream());
            return await UploadImage(image);

        }

        /// <summary>
        /// Upload single pictures
        /// </summary>
        /// <returns></returns>
        public async Task<AppFile> UploadImage(string base64String)
        {
            base64String = Regex.Replace(base64String, @"^data:image\/[a-zA-Z]+;base64,", string.Empty);
            var file = Convert.FromBase64String(base64String);
            var image = Image.Load(file);
            return await UploadImage(image);

        }

        /// <summary>
        /// Upload Multiple pictures
        /// </summary>
        /// <param name="files"></param>
        /// <returns></returns>
        public async Task<ICollection<AppFile>> UploadMultipleImage(ICollection<string> files)
        {
            var col = new List<AppFile>();

            if (files == null || files.Count <= 0)
                return col;

            foreach (var item in files)
            {
                var newItem = Regex.Replace(item, @"^data:image\/[a-zA-Z]+;base64,", string.Empty);
                var file = Convert.FromBase64String(newItem);
                var image = Image.Load(file);
                col.Add(await UploadImage(image));
            }

            return col;

        }

        /// <summary>
        /// Delete an image from the platform
        /// </summary>
        /// <param name="appFile"></param>
        public async Task DeleteImage(AppFile appFile)
        {
            if (!_ienv.IsProduction())
            {
                DeleteImageLocally(appFile);
                return;
            }
            var doService = new DoService();

            await doService.DeleteFile(appFile.PublicId, "images");
        }

        /// <summary>
        /// Delete multiple images from the platform
        /// </summary>
        /// <param name="appFiles"></param>
        public async Task DeleteMultipleImage(ICollection<AppFile> appFiles)
        {
            foreach (var item in appFiles)
            {
                await DeleteImage(item);
            }
        }

        private void DeleteImageLocally(AppFile appFile)
        {
            if (string.IsNullOrEmpty(appFile.PublicUrl))
                return;

            if (!File.Exists(appFile.PublicUrl))
                return;

            File.Delete(appFile.PublicUrl);
        }

        private async Task<AppFile> UploadImage(Image image)
        {
            if (!_ienv.IsProduction())
                return UploadImageLocally(image);

            var doService = new DoService();

            return await doService.UploadFile(image, "jpeg", "images");
        }

        private AppFile UploadImageLocally(Image image)
        {
            var uniqueName = $"{Guid.NewGuid()}.jpeg";
            var fileName = Path.Combine(PhotoService.initialDir, uniqueName);

            var apiPath = $"{hostName}/api/file/image/{uniqueName}";
            // var fileName = $"{PhotoService.initialDir}/{Guid.NewGuid()}.jpg";
            image.Save(fileName);

            return new AppFile()
            {
                PublicId = uniqueName,
                PublicUrl = apiPath
            };
        }

        /// <summary>
        /// If image is stored locally then download locally
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public string DownloadLocallyToBase64String(string filePath)
        {

            if (string.IsNullOrEmpty(filePath))
                return null;

            if (!File.Exists(filePath))
                return null;

            IImageFormat format;

            var image = Image.Load(filePath, out format);
            return image.ToBase64String(format);
        }
    }
}