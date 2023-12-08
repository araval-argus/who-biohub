namespace WHO.BioHub.DataManagement.Core.UseCases.AuthChecks.GetAccessInformation;

public record struct GetAccessInformationQuery(Guid? ExternalId, string Email, bool IsLoginCheck = false) { }