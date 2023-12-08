namespace WHO.BioHub.Graph
{
    public class GraphAd
    {
        public string? TenantId { get; set; }

        public string? ClientId { get; set; }

        public string? ClientSecret { get; set; }

        public IEnumerable<string>? Scopes { get; set; }
    }
}
