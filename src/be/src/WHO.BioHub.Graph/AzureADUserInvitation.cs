using Microsoft.Graph;
using Microsoft.Identity.Client;
using WHO.BioHub.Notifications;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.Graph
{
    public class AzureADUserInvitation : IAzureADUserInvitation
    {
        private readonly IGraphUtility _graphUtility;
        private readonly ISendNotification _sendNotification;

        public AzureADUserInvitation(IGraphUtility graphUtility, ISendNotification sendNotification)
        {
            _graphUtility = graphUtility;
            _sendNotification = sendNotification;
        }


        public async Task<string> InviteUserAsync(string email, string accessToken, string firstName, string lastName)
        {
            try
            {
                var userDomain = email.Trim().Split('@')[1];

                //TODO: define configuration variable
                if (userDomain.ToLower() == "who.int")
                {
                    return await SendInvitationEmailAsync(email, firstName, lastName);
                }
                else
                {
                    return await SendUserInvitationAsync(email, accessToken);
                }
            }
            catch (Exception ex)
            {
                throw;  // TODO improve exception handling
            }
        }


        public async Task<string> SendUserInvitationAsync(string email, string accessToken)
        {
            try
            {
                var cca = ConfidentialClientApplicationBuilder
                .Create(_graphUtility.GraphAd().ClientId)
                .WithTenantId(_graphUtility.GraphAd().TenantId)
                .WithClientSecret(_graphUtility.GraphAd().ClientSecret)
                .Build();

                // DelegateAuthenticationProvider is a simple auth provider implementation
                // that allows you to define an async function to retrieve a token                
                var authProvider = new DelegateAuthenticationProvider(async (request) =>
                {
                    // Use Microsoft.Identity.Client to retrieve token
                    var assertion = new UserAssertion(accessToken);
                    var result = await cca.AcquireTokenOnBehalfOf(_graphUtility.GraphAd().Scopes, assertion).ExecuteAsync();

                    request.Headers.Authorization =
                        new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", result.AccessToken);
                });

                var graphClient = new GraphServiceClient(authProvider);

                // Prepare user invitation
                var invitation = new Invitation
                {
                    InvitedUserEmailAddress = email,
                    InviteRedirectUrl = _graphUtility.GraphInvitationConfig().InviteRedirectUrl,
                    SendInvitationMessage = _graphUtility.GraphInvitationConfig().SendInvitationMessage
                };

                var sendInvitation = await graphClient.Invitations.Request().AddAsync(invitation);
                return sendInvitation.Status;
            }
            catch (Exception ex)
            {
                throw; // TODO improve exception handling
            }
        }

        public async Task<string> SendInvitationEmailAsync(string email, string firstName, string lastName)
        {
            try
            {
                var url = _graphUtility.BaseUrl();

                //TODO: define database entry
                var body = $"<p>Dear {firstName} {lastName},</p> <p>You are invited to join the WHO BioHub System Operational Platform.</p><p> Please click the link below to access the tool.<br><a href='{url}'>{url}</a></p><p>Sincerely,</p><p>WHO BioHub Secretariat</p>";

                var subject = "You are invited to join the WHO BioHub System Operational Platform";


                var toEmails = new List<string>() { email };
                await _sendNotification.SendEmail(toEmails, Enumerable.Empty<string>(), Enumerable.Empty<string>(), body, "", subject);
                return string.Empty;
            }
            catch (Exception ex)
            {
                throw; // TODO improve exception handling
            }
        }

    }
}
