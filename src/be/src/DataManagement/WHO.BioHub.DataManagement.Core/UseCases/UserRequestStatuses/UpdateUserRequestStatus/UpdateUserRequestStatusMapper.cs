using WHO.BioHub.Models.Models;

namespace WHO.BioHub.DataManagement.Core.UseCases.UserRequestStatuses.UpdateUserRequestStatus;

public interface IUpdateUserRequestStatusMapper
{
    UserRequestStatus Map(UserRequestStatus userRequestStatus, UpdateUserRequestStatusCommand command);
}

public class UpdateUserRequestStatusMapper : IUpdateUserRequestStatusMapper
{
    public UserRequestStatus Map(UserRequestStatus userRequestStatus, UpdateUserRequestStatusCommand command)
    {

        userRequestStatus.Id = command.Id;
        userRequestStatus.IsResponseMessage = command.IsResponseMessage;
        userRequestStatus.Status = command.Status;
        userRequestStatus.Message = command.Message;

        return userRequestStatus;
    }
}