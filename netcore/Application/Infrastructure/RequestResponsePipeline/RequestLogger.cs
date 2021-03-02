using System.Threading;
using System.Threading.Tasks;
using MediatR.Pipeline;
using Microsoft.Extensions.Logging;

namespace Application.Infrastructure.RequestResponsePipeline
{
    /// <summary>
    /// Request and response logger
    /// </summary>
    /// <typeparam name="TRequest"></typeparam>
    public class RequestLogger<TRequest> : IRequestPreProcessor<TRequest>
    {
        private readonly ILogger _logger;

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="logger"></param>
        public RequestLogger(ILogger<TRequest> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task Process(TRequest request, CancellationToken cancellationToken)
        {
            var name = typeof(TRequest).Name;

            // TODO: Add User Details

            _logger.LogInformation("Eco Request: {Name} {@Request}", name, request);

            return Task.CompletedTask;
        }
    }
}