namespace WHO.BioHub.Graph
{
    public interface IGraphUtility
    {
        string BaseUrl();
        GraphAd GraphAd();
        GraphInvitation GraphInvitationConfig();
    }
}