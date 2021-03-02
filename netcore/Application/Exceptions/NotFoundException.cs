using System;
using System.Net;
using Application.Interfaces.IExceptions;

namespace Application.Exceptions
{
    /// <summary>
    /// Not found exception
    /// </summary>
    public class NotFoundException : Exception, IGeneralException
    {
        /// <summary>
        /// Status code for not found (404)
        /// </summary>
        /// <value></value>
        public HttpStatusCode StatusCode { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name"></param>
        /// <param name="key"></param>
        public NotFoundException(string name, object key)
            : base($"Entity \"{name}\" ({key}) was not found.")
        {
            StatusCode = HttpStatusCode.NotFound;
        }
    }
}