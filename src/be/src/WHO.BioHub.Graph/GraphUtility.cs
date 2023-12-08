using WHO.BioHub.Graph;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.Graph
{
    public class GraphUtility : IGraphUtility
    {
        private readonly ApplicationConfiguration _applicationConfiguration;
        private readonly GraphAd _graphAd;
        private readonly GraphInvitation _graphInvitationConfig;

        public GraphUtility(ApplicationConfiguration applicationConfiguration, GraphAd graphAd, GraphInvitation graphInvitationConfig)
        {
            _applicationConfiguration = applicationConfiguration;
            _graphAd = graphAd;
            _graphInvitationConfig = graphInvitationConfig;
        }

        public string BaseUrl()
        {
            return _applicationConfiguration.BaseUrl;
        }

        public GraphAd GraphAd()
        {
            return _graphAd;
        }

        public GraphInvitation GraphInvitationConfig()
        {
            return _graphInvitationConfig;
        }
    }

}
