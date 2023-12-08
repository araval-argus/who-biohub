using WHO.BioHub.Models.Models;
using WHO.BioHub.Shared.Enums;

namespace WHO.BioHub.DataManagement.Core.UseCases.UserRequests.ApproveOrRejectUserRequest;

public interface IApproveOrRejectUserRequestMapper
{
    UserRequest Map(UserRequest userRequest, ApproveOrRejectUserRequestCommand command);
}

public class ApproveOrRejectUserRequestMapper : IApproveOrRejectUserRequestMapper
{
    public UserRequest Map(UserRequest userRequest, ApproveOrRejectUserRequestCommand command)
    {
        
        userRequest.Id = command.Id;
        userRequest.FirstName = command.FirstName;
        userRequest.LastName = command.LastName;
        userRequest.Status = command.Status;
        userRequest.Email = command.Email.Replace(" ", "");
        userRequest.Purpose = command.Purpose;
        userRequest.RoleId = command.RoleId;
        userRequest.CountryId = command.CountryId;
        userRequest.LaboratoryId = command.LaboratoryId;
        userRequest.TermsAndConditionAccepted = command.TermsAndConditionAccepted;
        userRequest.IsConfirmed = command.IsConfirmed;
        userRequest.ConfirmationDate = command.ConfirmationDate;
        userRequest.Message = command.Message;
        userRequest.RegistrationDate = command.Status == UserRegistrationStatus.Approved ? command.ConfirmationDate : null;
      

        return userRequest;
    }
}