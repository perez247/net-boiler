using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Entities.Inheritable;
using Microsoft.AspNetCore.Http;

namespace Application.Interfaces.IServices
{
    /// <summary>
    /// Document Service
    /// </summary>
    public interface IDocumentService
    {
        /// <summary>
        /// Upload a document
        /// </summary>
        /// <param name="files"></param>
        /// <returns></returns>
        Task<ICollection<AppFile>> UploadMultipleDocument(ICollection<IFormFile> files);

        /// <summary>
        /// Upload a document
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        Task<AppFile> UploadDocument(IFormFile file);

        /// <summary>
        /// Delete an image from the platform
        /// </summary>
        /// <param name="appFile"></param>
       Task DeleteDocument(AppFile appFile);

        /// <summary>
        /// Delete multiple images from the platform
        /// </summary>
        /// <param name="appFiles"></param>
        Task DeleteMultipleDocuments(ICollection<AppFile> appFiles);
    }
}