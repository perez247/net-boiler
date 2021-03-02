using System;

namespace Domain.Entities.Inheritable
{
    /// <summary>
    /// File used in the application
    /// </summary>
    public class AppFile
    {
        /// <summary>
        /// Id of the file
        /// </summary>
        /// <value></value>
        public Guid Id { get; set; }

        /// <summary>
        /// Name of the document
        /// </summary>
        /// <value></value>
        public string Name { get; set; }

        /// <summary>
        /// Type of this document
        /// </summary>
        /// <value></value>
        public string Type { get; set; }

        /// <summary>
        /// In terms of cloudinary
        /// </summary>
        /// <value></value>
        public string PublicId { get; set; }

        /// <summary>
        /// How to get in touch with the file from the public
        /// </summary>
        /// <value></value>
        public string PublicUrl { get; set; }

        /// <summary>
        /// Date File was created
        /// </summary>
        /// <value></value>
        public DateTime DateCreated { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public AppFile()
        {
            DateCreated = DateTime.Now;
        }
    }
}