using WHO.BioHub.Models.Models;
using WHO.BioHub.Shared.Dto;
using WHO.BioHub.Shared.Enums;

namespace WHO.BioHub.DataManagement.Core.UseCases.UserRequests.ReadUserRequest;

public interface IReadUserRequestMapper
{
    UserRequestViewModel Map(UserRequest userRequest);
}

public class ReadUserRequestMapper : IReadUserRequestMapper
{
    public UserRequestViewModel Map(UserRequest userRequest)
    {

        UserRequestViewModel userRequestViewModel = new UserRequestViewModel();

        userRequestViewModel.Id = userRequest.Id;
        userRequestViewModel.ConfirmationDate = userRequest.ConfirmationDate;
        userRequestViewModel.RequestDate = userRequest.CreationDate;
        userRequestViewModel.CountryId = userRequest.CountryId;
        userRequestViewModel.Email = userRequest.Email;
        userRequestViewModel.FirstName = userRequest.FirstName;
        userRequestViewModel.LastName = userRequest.LastName;
        userRequestViewModel.InstituteName = userRequest.InstituteName;
        userRequestViewModel.IsConfirmed = userRequest.IsConfirmed;
        userRequestViewModel.LaboratoryId = userRequest.LaboratoryId;
        userRequestViewModel.Message = userRequest.Message;
        userRequestViewModel.Purpose = userRequest.Purpose;
        userRequestViewModel.RegistrationDate = userRequest.RegistrationDate;
        userRequestViewModel.RoleId = userRequest.RoleId;
        userRequestViewModel.Status = userRequest.Status;
        userRequestViewModel.TermsAndConditionAccepted = userRequest.TermsAndConditionAccepted;

        return userRequestViewModel;
    }  
   
}