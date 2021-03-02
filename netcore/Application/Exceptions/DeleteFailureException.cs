using System;
using System.Net;
using Application.Interfaces.IExceptions;

namespace Application.Exceptions
{
    /// <summary>
    /// Failed to delete an entity
    /// </summary>
    public class DeleteFailureException : Exception, IGeneralException
    {
        /// <summary>
        /// Status code for failed deletion
        /// </summary>
        /// <value></value>
        public HttpStatusCode StatusCode { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name"></param>
        /// <param name="key"></param>
        /// <param name="message"></param>
        /// <param name="statusCode"></param>
        public DeleteFailureException(string name, object key, string message, HttpStatusCode statusCode = HttpStatusCode.UnprocessableEntity)
            : base($"Deletion of entity \"{name}\" ({key}) failed. {message}")
        {
            StatusCode = statusCode;
        }
    }
}