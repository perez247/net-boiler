using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Entities.Inheritable;
using Microsoft.AspNetCore.Http;

namespace Application.Interfaces.IServices
{
    /// <summary>
    /// Interface for handling third party applications for photos
    /// </summary>
    public interface IPhotoService
    {
        /// <summary>
        /// Upload file 
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        Task<AppFile> UploadProfilePicture(IFormFile file);

        /// <summary>
        /// If image is stored locally then download locally
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        string DownloadLocallyToBase64String(string filePath);

        /// <summary>
        /// Delete an image from the platform
        /// </summary>
        /// <param name="appFile"></param>
        Task DeleteImage(AppFile appFile);

        /// <summary>
        /// Upload Background picture
        /// </summary>
        /// <returns></returns>
        Task<AppFile> UploadBackGroundPicture(IFormFile file);
        
        /// <summary>
        /// Upload single pictures
        /// </summary>
        /// <returns></returns>
        Task<AppFile> UploadImage(string base64String);

        /// <summary>
        /// Upload Multiple pictures
        /// </summary>
        /// <param name="files"></param>
        /// <returns></returns>
        Task<ICollection<AppFile>> UploadMultipleImage(ICollection<string> files);

        /// <summary>
        /// Delete multiple images from the platform
        /// </summary>
        /// <param name="appFiles"></param>
        Task DeleteMultipleImage(ICollection<AppFile> appFiles);
    }
}