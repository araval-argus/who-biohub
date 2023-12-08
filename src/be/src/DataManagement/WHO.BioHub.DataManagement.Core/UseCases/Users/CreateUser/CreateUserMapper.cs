using WHO.BioHub.Models.Models;

namespace WHO.BioHub.DataManagement.Core.UseCases.Users.CreateUser;

public interface ICreateUserMapper
{
    User Map(CreateUserCommand command);
}

public class CreateUserMapper : ICreateUserMapper
{
    public User Map(CreateUserCommand command)
    {       
        User user = new()
        {
            Id = Guid.NewGuid(),
            CreationDate = DateTime.UtcNow,
            FirstName = command.FirstName,
            LastName = command.LastName,
            Email = command.Email.Replace(" ", ""),
            JobTitle = command.JobTitle,
            MobilePhone = command.MobilePhone,
            BusinessPhone = command.BusinessPhone,
            OperationalFocalPoint = command.BioHubFacilityId != null ? (command.OperationalFocalPoint == null ? false : command.OperationalFocalPoint.Value) : true,
            RoleId = command.RoleId,
            LaboratoryId = command.LaboratoryId,
            BioHubFacilityId = command.BioHubFacilityId,
            CourierId = command.CourierId,
            Notes = command.Notes,
            IsActive = true,
            LastOperationByUserId = command.OperationById,
            LastOperationDate = DateTime.UtcNow,
            DeletedOn = null,
        };

        return user;
    }
}