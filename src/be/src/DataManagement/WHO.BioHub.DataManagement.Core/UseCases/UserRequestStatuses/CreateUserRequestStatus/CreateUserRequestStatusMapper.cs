using WHO.BioHub.Models.Models;

namespace WHO.BioHub.DataManagement.Core.UseCases.UserRequestStatuses.CreateUserRequestStatus;

public interface ICreateUserRequestStatusMapper
{
    UserRequestStatus Map(CreateUserRequestStatusCommand command);
}

public class CreateUserRequestStatusMapper : ICreateUserRequestStatusMapper
{
    public UserRequestStatus Map(CreateUserRequestStatusCommand command)
    {

        UserRequestStatus userRequestStatus = new()
        {
            Id = Guid.NewGuid(),
            CreationDate = DateTime.UtcNow,
            IsResponseMessage = command.IsResponseMessage,
            Status = command.Status,
            Message = command.Message
        };

        return userRequestStatus;
    }
}