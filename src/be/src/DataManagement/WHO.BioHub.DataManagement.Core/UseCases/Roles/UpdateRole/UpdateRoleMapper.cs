using WHO.BioHub.Models.Models;

namespace WHO.BioHub.DataManagement.Core.UseCases.Roles.UpdateRole;

public interface IUpdateRoleMapper
{
    Role Map(Role role, UpdateRoleCommand command);
}

public class UpdateRoleMapper : IUpdateRoleMapper
{
    public Role Map(Role role, UpdateRoleCommand command)
    {
       
        role.Id = command.Id;
        role.Name = command.Name;
        role.Description = command.Description;
        role.AddToRegistration = command.AddToRegistration;
        role.RoleType = command.RoleType;


        return role;
    }
}