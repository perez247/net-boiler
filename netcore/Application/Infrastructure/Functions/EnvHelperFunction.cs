using System;

namespace Application.Infrastructure.Functions
{
    /// <summary>
    /// Class to help in the environment
    /// </summary>
    public class EnvHelperFunction
    {

       // Database information ----------------------------------------------------------------------------

        /// <summary>
        /// Name of the database password
        /// </summary>
        /// <returns></returns>
        public static readonly string DefaultConnection = Environment.GetEnvironmentVariable("DefaultConnection");


        // Environment setting -------------------------------------------------------------------------------

        /// <summary>
        /// Determine the environment variable 
        /// </summary>
        /// <returns></returns>
        public static readonly string ASPNETCORE_ENVIRONMENT = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

        // Authorization token -------------------------------------------------------------------------------

        /// <summary>
        /// JWT Token 
        /// </summary>
        /// <returns></returns>
        public static readonly string AUTHORIZATION_TOKEN = Environment.GetEnvironmentVariable("AUTHORIZATION_TOKEN");

        // Host name -------------------------------------------------------------------------------

        /// <summary>
        /// JWT Token 
        /// </summary>
        /// <returns></returns>
        public static readonly string HOSTNAME = Environment.GetEnvironmentVariable("HOSTNAME");

        // DO Bucket -------------------------------------------------------------------------------

        /// <summary>
        /// Digital ocean s3 buckets
        /// </summary>
        /// <returns></returns>
        public static readonly string DO_S3_BUCKET = Environment.GetEnvironmentVariable("DO_S3_BUCKET");

        /// <summary>
        /// Email Server
        /// </summary>
        /// <returns></returns>
        public static readonly string EMAIL_SERVER = Environment.GetEnvironmentVariable("EMAIL_SERVER");

        /// <summary>
        /// First admin email 
        /// </summary>
        /// <returns></returns>
        public static readonly string FIRST_ADMIN_EMAIL = Environment.GetEnvironmentVariable("FIRST_ADMIN_EMAIL");


        /// <summary>
        /// first admin password
        /// </summary>
        /// <returns></returns>
        public static readonly string FIRST_ADMIN_PASSWORD = Environment.GetEnvironmentVariable("FIRST_ADMIN_PASSWORD");

    }
}