using WHO.BioHub.Models.Models;
using WHO.BioHub.Shared.Dto;

namespace WHO.BioHub.DataManagement.Core.UseCases.Users.ReadUser;

public record struct ReadUserQueryResponse(UserViewModel User) { }