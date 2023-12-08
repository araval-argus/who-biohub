namespace WHO.BioHub.Graph
{
    public interface IAzureADUserInvitation
    {
        Task<string> InviteUserAsync(string email, string accessToken, string firstName, string lastName);
        Task<string> SendUserInvitationAsync(string email, string accessToken);
    }
}
