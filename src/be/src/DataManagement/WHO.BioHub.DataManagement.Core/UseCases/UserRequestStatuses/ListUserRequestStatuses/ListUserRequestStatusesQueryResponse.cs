using WHO.BioHub.Models.Models;

namespace WHO.BioHub.DataManagement.Core.UseCases.UserRequestStatuses.ListUserRequestStatuses;

public record struct ListUserRequestStatusesQueryResponse(IEnumerable<UserRequestStatus> UserRequestStatuses) { }