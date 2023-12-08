using WHO.BioHub.Graph;

namespace WHO.BioHub.Identity
{
    public class BaseAuthService
    {
        protected readonly IAzureADTokenValidation _azureADTokenValidation;
        protected readonly IAzureADUserInvitation _azureADUserInvitation;

        public BaseAuthService(IAzureADTokenValidation azureADTokenValidation, IAzureADUserInvitation azureADUserInvitation)
        {
            _azureADTokenValidation = azureADTokenValidation;
            _azureADUserInvitation = azureADUserInvitation;
        }
    }
}
