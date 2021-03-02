
using System.Text.Json.Serialization;

namespace Application.Infrastructure.RequestResponsePipeline
{
    /// <summary>
    /// This class is responsible for sending back server-side error
    /// This should be used as the fundamental.
    /// </summary>
    public class ErrorResponse
    {
        /// <summary>
        /// This is basically used for for sending validation errors
        /// This contains multiple errors and will be stored as a list of error
        /// </summary>
        /// <value>list{objects}</value>
        [JsonPropertyName("errors")]
        public object Errors { get; set; }

        /// <summary>
        /// This should contain only a single error 
        /// This is any other error
        /// </summary>
        /// <value>string</value>
        [JsonPropertyName("error")]
        public string Error { get; set; }

        /// <summary>
        /// Determines what environment this is
        /// </summary>
        /// <value>string</value>
        [JsonPropertyName("environment")]
        public string Environment { get; set; }

        /// <summary>
        /// This is used for debugging and should show only in development or staging environment
        /// </summary>
        /// <value></value>
        [JsonPropertyName("stackTrace")]
        public string StackTrace { get; set; }
    }
}