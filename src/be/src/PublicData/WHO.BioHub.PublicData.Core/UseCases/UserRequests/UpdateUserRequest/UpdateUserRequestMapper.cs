using WHO.BioHub.Models.Models;

namespace WHO.BioHub.PublicData.Core.UseCases.UserRequests.UpdateUserRequest;

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
        userRequest.TermsAndConditionAccepted = command.TermsAndConditionAccepted;
        userRequest.InstituteName = command.InstituteName;

        return userRequest;
    }
}