using WHO.BioHub.Models.Models;

namespace WHO.BioHub.DataManagement.Core.UseCases.Users.ReadUserByExternalId;

public record struct ReadUserByExternalIdQueryResponse(User User) { }