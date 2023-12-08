namespace WHO.BioHub.Identity
{
    public class AzureAd
    {
        public string? Instance { get; set; }

        public string? ClientId { get; set; }

        public string? Domain { get; set; }

        public string? TenantId { get; set; }

        public string? Audience { get; set; }

        public IEnumerable<string> Audiences => new List<string>() {
            Audience
        };

        public IEnumerable<string> Issuers => new List<string>() {
            $"{Instance}/{TenantId}/",
            $"{Instance}/{TenantId}/v2.0",
            $"https://sts.windows.net/{TenantId}/",
            $"https://sts.windows.net/{TenantId}/v2.0",
            $"https://login.windows.net/{TenantId}/",
            $"https://login.microsoft.com/{TenantId}/"
        };

        public string WellKnownEndpoint => $"{Instance}/{TenantId}/v2.0/.well-known/openid-configuration";
    }
}
