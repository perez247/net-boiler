using System;
using System.Net;
using Application.Interfaces.IExceptions;

namespace Application.Exceptions
{
    /// <summary>
    /// Creating an entity failed
    /// </summary>
    public class CreationFailureException : Exception, IGeneralException
    {
        /// <summary>
        /// COnstructor
        /// </summary>
        /// <param name="name"></param>
        /// <param name="message"></param>
        /// <param name="statusCode"></param>
        public CreationFailureException(string name, string message, HttpStatusCode statusCode = HttpStatusCode.UnprocessableEntity)
            : base($"Failed to create \"{name}\": {message}")
        {
            StatusCode = statusCode;
        }

        /// <summary>
        /// Status code for failed creation of code
        /// </summary>
        /// <value></value>
        public HttpStatusCode StatusCode { get; set; }
    }
}