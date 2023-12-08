using WHO.BioHub.Models.Models;

namespace WHO.BioHub.DataManagement.Core.UseCases.WorklistToBioHubEmails.ListWorklistToBioHubEmails;

public record struct ListWorklistToBioHubEmailsQueryResponse(IEnumerable<WorklistToBioHubEmail> WorklistToBioHubEmails) { }