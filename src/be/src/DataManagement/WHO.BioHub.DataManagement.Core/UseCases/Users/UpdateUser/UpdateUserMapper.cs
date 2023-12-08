using WHO.BioHub.Models.Models;

namespace WHO.BioHub.DataManagement.Core.UseCases.Users.UpdateUser;

public interface IUpdateUserMapper
{
    User Map(User user, UpdateUserCommand command);
}

public class UpdateUserMapper : IUpdateUserMapper
{
    public User Map(User user, UpdateUserCommand command)
    {
       
        user.Id = command.Id;

        user.FirstName = command.FirstName;
        user.LastName = command.LastName;
        user.Email = command.Email.Replace(" ", "");
        user.JobTitle = command.JobTitle;
        user.MobilePhone = command.MobilePhone;
        user.BusinessPhone = command.BusinessPhone;
        user.OperationalFocalPoint = command.BioHubFacilityId != null ? (command.OperationalFocalPoint == null ? false : command.OperationalFocalPoint.Value) : true;
        user.RoleId = command.RoleId;
        user.LaboratoryId = command.LaboratoryId;
        user.BioHubFacilityId = command.BioHubFacilityId;
        user.CourierId = command.CourierId;
        user.Notes = command.Notes;
        user.LastOperationByUserId = command.OperationById;
        user.LastOperationDate = DateTime.UtcNow;
        return user;
    }
}