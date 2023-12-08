using WHO.BioHub.Models.Models;

namespace WHO.BioHub.DataManagement.Core.UseCases.Roles.CreateRole;

public interface ICreateRoleMapper
{
    Role Map(CreateRoleCommand command);
}

public class CreateRoleMapper : ICreateRoleMapper
{
    public Role Map(CreateRoleCommand command)
    {        

        Role role = new()
        {
            Id = Guid.NewGuid(),
            CreationDate = DateTime.UtcNow,
            Name = command.Name,
            Description = command.Description,
            AddToRegistration = command.AddToRegistration,
            RoleType = command.RoleType,           

            DeletedOn = null,
        };

        return role;
    }
}