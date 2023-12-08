using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Protocols;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace WHO.BioHub.Identity
{
    public class AzureADTokenValidation : IAzureADTokenValidation
    {
        private readonly AzureAd _azureAd;

        public AzureADTokenValidation(AzureAd azureAd)
        {
            _azureAd = azureAd;
        }

        /// <summary>
        /// Validate the request validating the user AccessToken in the Authorization header.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="logger"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<ObjectResult> ValidateRequestAsync(HttpRequest request, ILogger logger, CancellationToken cancellationToken)
        {
            var accessToken = GetAccessToken(request, logger);
            if (accessToken == null)
            {
                logger.LogWarning("Authorization 'Bearer' token not found");
                return new ObjectResult(new { Message = "Authorization 'Bearer' token not found" })
                {
                    StatusCode = StatusCodes.Status403Forbidden
                };
            }

            ClaimsPrincipal claimsPrincipal = await ValidateAccessTokenAsync(accessToken, logger, cancellationToken);
            if (claimsPrincipal == null)
            {
                logger.LogWarning("Invalid Authorization 'Bearer' token found");
                return new ObjectResult(new { Message = "Validation failed for Authorization 'Bearer' token" })
                {
                    StatusCode = StatusCodes.Status401Unauthorized
                };
            }

            return new ObjectResult(new { Message = "Valid Authorization 'Bearer' token" })
            {
                StatusCode = StatusCodes.Status200OK,
                Value = claimsPrincipal
            };
        }

        public string GetAccessToken(HttpRequest request, ILogger logger)
        {
            var authorizationHeader = request.Headers?["Authorization"];
            if (!authorizationHeader.HasValue || (authorizationHeader.HasValue && string.IsNullOrEmpty(authorizationHeader.Value)))
            {
                logger.LogWarning($"Authorization header is null or empty");
                return null;
            }
            string[] authHeader = authorizationHeader?.ToString().Split(null) ?? Array.Empty<string>();
            if (authHeader.Length == 2 && authHeader[0].Equals("Bearer"))
                return authHeader[1];

            logger.LogWarning($"Authorization header does not start with 'Bearer'");
            return null;
        }

        private async Task<ClaimsPrincipal> ValidateAccessTokenAsync(string accessToken, ILogger logger, CancellationToken cancellationToken = default)
        {
            try
            {
                var tokenValidator = new JwtSecurityTokenHandler();
                var signingKeys = await GetSigningKeysAsync(logger, cancellationToken);

                var validationParameters = new TokenValidationParameters
                {
                    ValidateAudience = true,
                    ValidAudience = _azureAd.Audience,

                    ValidateIssuer = true,
                    ValidIssuers = _azureAd.Issuers,

                    ValidateIssuerSigningKey = true,
                    IssuerSigningKeys = signingKeys,

                    ValidateLifetime = true,
                    RequireSignedTokens = true
                };

                ClaimsPrincipal claimsPrincipal = tokenValidator.ValidateToken(accessToken, validationParameters, out SecurityToken securityToken);

                logger.LogInformation($"Token validated, user: {claimsPrincipal?.Identity?.Name}");
                return claimsPrincipal;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error validating token");
                if (ex is SecurityTokenExpiredException)
                    throw; // TODO improve exception handling
            }

            return null;
        }

        private async Task<IEnumerable<SecurityKey>> GetSigningKeysAsync(ILogger logger, CancellationToken cancellationToken)
        {
            logger.LogInformation($"Get SigningKeys from well known endpoint {_azureAd.WellKnownEndpoint}");

            var configurationManager = new ConfigurationManager<OpenIdConnectConfiguration>(_azureAd.WellKnownEndpoint,
                                    new OpenIdConnectConfigurationRetriever());

            var discoveryDocument = await configurationManager.GetConfigurationAsync(cancellationToken);
            return discoveryDocument.SigningKeys;
        }
    }
}