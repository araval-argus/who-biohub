using WHO.BioHub.Models.Models;

namespace WHO.BioHub.DataManagement.Core.UseCases.UserRequests.UpdateUserRequest;

public interface IUpdateUserRequestMapper
{
    UserRequest Map(UserRequest userRequest, UpdateUserRequestCommand command);
}

public class UpdateUserRequestMapper : IUpdateUserRequestMapper
{
    public UserRequest Map(UserRequest userRequest, UpdateUserRequestCommand command)
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

        return userRequest;
    }
}