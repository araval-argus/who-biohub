using WHO.BioHub.Models.Models;
using WHO.BioHub.Shared.Dto;

namespace WHO.BioHub.DataManagement.Core.UseCases.Users.ReadUser;

public interface IReadUserMapper
{
    UserViewModel Map(User user);
}

public class ReadUserMapper : IReadUserMapper
{
    public UserViewModel Map(User user)
    {

        var userViewModel = new UserViewModel();

        userViewModel.Id = user.Id;

        userViewModel.FirstName = user.FirstName;
        userViewModel.LastName = user.LastName;
        userViewModel.Email = user.Email;
        userViewModel.JobTitle = user.JobTitle;
        userViewModel.MobilePhone = user.MobilePhone;
        userViewModel.BusinessPhone = user.BusinessPhone;
        userViewModel.OperationalFocalPoint = user.OperationalFocalPoint;
        userViewModel.RegistrationDate = user.CreationDate;
        userViewModel.RoleId = user.RoleId;
        userViewModel.LaboratoryId = user.LaboratoryId;
        userViewModel.BioHubFacilityId = user.BioHubFacilityId;
        userViewModel.CourierId = user.CourierId;
        userViewModel.IsActive = user.IsActive;

        return userViewModel;
    }
}