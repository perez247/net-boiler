using System.Net;

namespace Application.Interfaces.IExceptions
{
    /// <summary>
    /// Format for General Exception
    /// </summary>
    public interface IGeneralException
    {
        /// <summary>
        /// Status code
        /// </summary>
        /// <value></value>
        HttpStatusCode StatusCode { get; set; }
    }
}