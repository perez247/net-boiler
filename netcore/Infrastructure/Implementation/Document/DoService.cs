using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using Application.Infrastructure.Functions;
using Domain.Entities.Inheritable;
using SixLabors.ImageSharp;

namespace Infrastructure.Implementation.Document
{
    /// <summary>
    /// S3 bucket implementation similar to AWS S3
    /// </summary>
    public class DoService
    {
        private readonly string S3_SECRET_KEY;
        private readonly string S3_ACCESS_KEY;
        private readonly string S3_HOST_ENDPOINT;
        private readonly string S3_BUCKET_NAME;
        private readonly IAmazonS3 _s3Client;

        /// <summary>
        /// Constructor
        /// </summary>
        public DoService()
        {
            var data = EnvHelperFunction.DO_S3_BUCKET;
            var arrayData = data.Split('|');
            S3_SECRET_KEY = arrayData[0];
            S3_ACCESS_KEY = arrayData[1];
            S3_BUCKET_NAME = arrayData[2];
            S3_HOST_ENDPOINT = arrayData[3];

            AmazonS3Config ClientConfig = new AmazonS3Config();
            ClientConfig.ServiceURL = "https://" + S3_HOST_ENDPOINT;
            _s3Client = new AmazonS3Client(S3_ACCESS_KEY, S3_SECRET_KEY, ClientConfig);
            // _s3Client.UploadObjectFromStreamAsync()
        }

        /// <summary>
        /// Uplaod Image to cloudinary
        /// </summary>
        /// <param name="image"></param>
        /// <param name="extention"></param>
        /// <param name="directory"></param>
        /// <returns></returns>
        public async Task<AppFile> UploadFile(Image image, string extention, string directory = null) {

            var outputStream = new MemoryStream();
            image.SaveAsJpeg(outputStream);
            outputStream.Seek(0, SeekOrigin.Begin);

            var fileTransferUtility = new TransferUtility(_s3Client);
            var bucketPath = !string.IsNullOrWhiteSpace(directory)
                    ? S3_BUCKET_NAME + @"/" + directory
                    : S3_BUCKET_NAME;

            var fileName = Guid.NewGuid() + "." + extention;

            var fileUploadRequest = new TransferUtilityUploadRequest()
                {
                    CannedACL = S3CannedACL.PublicRead,
                    BucketName = bucketPath,
                    Key = fileName,
                    InputStream = outputStream
                };

            await fileTransferUtility.UploadAsync(fileUploadRequest);

            return new AppFile() {
                PublicId = fileName,
                PublicUrl = "https://" + S3_BUCKET_NAME + "." + S3_HOST_ENDPOINT + "/" + directory + "/" + fileName,
            };
        }

        /// <summary>
        /// Uplaod Image to cloudinary
        /// </summary>
        /// <param name="publicId"></param>
        /// <param name="directory"></param>
        /// <returns></returns>
        public async Task DeleteFile(string publicId, string directory = null) {

            if (string.IsNullOrEmpty(publicId))
                return;

            var fileTransferUtility = new TransferUtility(_s3Client);
            var bucketPath = !string.IsNullOrWhiteSpace(directory)
                    ? S3_BUCKET_NAME + @"/" + directory
                    : S3_BUCKET_NAME;


            var deleteObject = new DeleteObjectRequest()
                {
                    BucketName = bucketPath,
                    Key = publicId,
                };

            await fileTransferUtility.S3Client.DeleteObjectAsync(deleteObject);

        }

    }
}