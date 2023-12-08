using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace WHO.BioHub.Identity
{
    public interface IAzureADTokenValidation
    {
        Task<ObjectResult> ValidateRequestAsync(HttpRequest request, ILogger logger, CancellationToken cancellationToken = default);

        string GetAccessToken(HttpRequest request, ILogger logger);
    }
}