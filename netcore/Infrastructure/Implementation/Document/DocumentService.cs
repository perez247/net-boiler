using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Application.Infrastructure.Functions;
using Application.Interfaces.IServices;
using Domain.Entities.Inheritable;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using SixLabors.ImageSharp;
using Microsoft.Extensions.Hosting;

namespace Infrastructure.Implementation.Document
{
    /// <summary>
    /// Service to handle documents
    /// </summary>
    public class DocumentService : IDocumentService
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
        public DocumentService(IWebHostEnvironment ienv)
        {
            _ienv = ienv;
        }

        /// <summary>
        /// Upload a document
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public async Task<AppFile> UploadDocument(IFormFile file)
        {
            if (!_ienv.IsProduction())
            {
                return await UploadDocumentLocally(file);
            }

            var doService = new DoService();

            var image = Image.Load(file.OpenReadStream());

            return await doService.UploadFile(image, file.ContentType, "documents");

        }

        /// <summary>
        /// Upload a document
        /// </summary>
        /// <param name="files"></param>
        /// <returns></returns>
        public async Task<ICollection<AppFile>> UploadMultipleDocument(ICollection<IFormFile> files)
        {

            var newDocs = new List<AppFile>();

            if (files == null)
                return newDocs;

            foreach (var file in files)
            {
                newDocs.Add(await UploadDocument(file));
            }

            return newDocs;
        }


        /// <summary>
        /// Delete an image from the platform
        /// </summary>
        /// <param name="appFile"></param>
        public async Task DeleteDocument(AppFile appFile)
        {
            if (!_ienv.IsProduction())
            {
                DeleteImageLocally(appFile);
                return;
            }

            var doService = new DoService();

            await doService.DeleteFile(appFile.PublicId, "documents");
        }

        /// <summary>
        /// Delete multiple images from the platform
        /// </summary>
        /// <param name="appFiles"></param>
        public async Task DeleteMultipleDocuments(ICollection<AppFile> appFiles)
        {
            foreach (var item in appFiles)
            {
                await DeleteDocument(item);
            }
        }

        private void DeleteImageLocally(AppFile appFile)
        {
            if (string.IsNullOrEmpty(appFile.PublicId))
                return;

            var path = Path.Combine(DocumentService.initialDir, appFile.PublicId);

            if (!File.Exists(path))
                return;

            File.Delete(path);
        }

        private async Task<AppFile> UploadDocumentLocally(IFormFile file)
        {

            var uniqueName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
            var fileName = Path.Combine(DocumentService.initialDir, uniqueName);
            var apiPath = $"{DocumentService.hostName}/api/file/document/{uniqueName}";

            using (var stream = new FileStream(fileName, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return new AppFile()
            {
                Name = file.Name,
                Type = file.ContentType,
                PublicId = uniqueName,
                PublicUrl = apiPath
            };
        }
    }
}