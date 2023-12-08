using WHO.BioHub.Models.Models;

namespace WHO.BioHub.DataManagement.Core.UseCases.Users.CreateUserFromUserRequest;

public interface ICreateUserFromUserRequestMapper
{
    User Map(CreateUserFromUserRequestCommand command);
}

public class CreateUserFromUserRequestMapper : ICreateUserFromUserRequestMapper
{
    public User Map(CreateUserFromUserRequestCommand command)
    {        

        User user = new()
        {
            Id = Guid.NewGuid(),
            CreationDate = DateTime.UtcNow,
            FirstName = command.FirstName,
            LastName = command.LastName,
            Email = command.Email.Replace(" ", ""),
            RoleId = command.RoleId,
            LaboratoryId = command.LaboratoryId,
            OperationalFocalPoint = true,
            IsActive = true,
            LastOperationByUserId = command.OperationById,
            LastOperationDate = DateTime.UtcNow,
            DeletedOn = null,
        };

        return user;
    }
}