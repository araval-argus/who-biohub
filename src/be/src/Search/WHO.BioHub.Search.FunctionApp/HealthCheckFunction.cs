using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Extensions.Logging;

namespace WHO.BioHub.Search.FunctionApp
{
    /// <summary>
    /// This function verifies if Azure Functions are working as expected or not
    /// </summary>
    public class HealthCheckFunction
    {
        public HealthCheckFunction() { }

        [FunctionName(nameof(HealthCheckFunction))]
        [OpenApiOperation(operationId: "Run")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK,
            contentType: "text/plain",
            bodyType: typeof(bool),
            Description = "The OK response")]
        public IActionResult Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "heartbeat")] HttpRequest req,
            ILogger logger,
            CancellationToken hostCancellationToken)
        {
            try
            {
                logger.LogInformation("Health check function started");
                using var cancellationSource = CancellationTokenSource.CreateLinkedTokenSource(hostCancellationToken, req.HttpContext.RequestAborted);
                return new OkResult();
            }
            catch (Exception exception)
            {
                logger.LogError(exception, "Health check function terminated by error {0}", exception.Message);
                throw;
            }
        }
    }
}