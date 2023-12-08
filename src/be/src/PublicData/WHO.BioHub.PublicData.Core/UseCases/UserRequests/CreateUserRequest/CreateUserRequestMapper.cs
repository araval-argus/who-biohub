using WHO.BioHub.Models.Models;

namespace WHO.BioHub.PublicData.Core.UseCases.UserRequests.CreateUserRequest;

public interface ICreateUserRequestMapper
{
    UserRequest Map(CreateUserRequestCommand command);
}

public class CreateUserRequestMapper : ICreateUserRequestMapper
{
    public UserRequest Map(CreateUserRequestCommand command)
    {
       
        UserRequest userRequest = new()
        {
            Id = Guid.NewGuid(),
            CreationDate = DateTime.UtcNow,
            FirstName = command.FirstName,
            LastName = command.LastName,
            Email = command.Email.Replace(" ",""),
            Purpose = command.Purpose,
            TermsAndConditionAccepted = command.TermsAndConditionAccepted,
            RoleId = command.RoleId,
            CountryId = command.CountryId,
            InstituteName = command.InstituteName,
            DeletedOn = null,            
        };

        return userRequest;
    }
}