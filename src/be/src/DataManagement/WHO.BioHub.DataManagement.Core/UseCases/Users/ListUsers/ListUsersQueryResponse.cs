using WHO.BioHub.Models.Models;
using WHO.BioHub.Shared.Dto;

namespace WHO.BioHub.DataManagement.Core.UseCases.Users.ListUsers;

public record struct ListUsersQueryResponse(IEnumerable<UserViewModel> Users) { }