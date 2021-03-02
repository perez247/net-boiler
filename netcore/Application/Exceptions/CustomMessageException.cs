using System;
using System.Net;
using Application.Interfaces.IExceptions;

namespace Application.Exceptions
{
    /// <summary>
    /// Send a custom error message
    /// </summary>
    public class CustomMessageException : Exception, IGeneralException
    {
        /// <summary>
        /// Custructor
        /// </summary>
        /// <param name="message"></param>
        /// <param name="statusCode"></param>
        public CustomMessageException(string message, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
            :base(message)
        {
            StatusCode = statusCode;
        }

        /// <summary>
        /// Status code to use so the application will understand
        /// </summary>
        /// <value></value>
        public HttpStatusCode StatusCode { get; set; }
    }
}