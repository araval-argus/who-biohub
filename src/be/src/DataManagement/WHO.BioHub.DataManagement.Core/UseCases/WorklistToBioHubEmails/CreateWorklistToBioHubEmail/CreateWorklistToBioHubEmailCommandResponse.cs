using WHO.BioHub.Models.Models;

namespace WHO.BioHub.DataManagement.Core.UseCases.WorklistToBioHubEmails.CreateWorklistToBioHubEmail;

public record struct CreateWorklistToBioHubEmailCommandResponse(WorklistToBioHubEmail WorklistToBioHubEmail) { }