using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Shared.Dto;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.Roles;
using WHO.BioHub.Models.Repositories.Users;
using WHO.BioHub.Shared.Enums;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UseCases.AuthChecks.GetAccessInformation;

public interface IGetAccessInformationHandler
{
    Task<Either<GetAccessInformationQueryResponse, Errors>> Handle(GetAccessInformationQuery query, CancellationToken cancellationToken);
}

public class GetAccessInformationHandler : IGetAccessInformationHandler
{
    private readonly ILogger<GetAccessInformationHandler> _logger;
    private readonly GetAccessInformationQueryValidator _validator;
    private readonly IUserReadRepository _userReadRepository;
    private readonly IRoleReadRepository _roleReadRepository;
    private readonly IUserWriteRepository _userWriteRepository;

    public GetAccessInformationHandler(
        ILogger<GetAccessInformationHandler> logger,
        GetAccessInformationQueryValidator validator,
        IUserReadRepository userReadRepository,
        IUserWriteRepository userWriteRepository,
        IRoleReadRepository roleReadRepository)
    {
        _logger = logger;
        _validator = validator;
        _userReadRepository = userReadRepository;
        _roleReadRepository = roleReadRepository;
        _userWriteRepository = userWriteRepository;
    }

    public async Task<Either<GetAccessInformationQueryResponse, Errors>> Handle(
        GetAccessInformationQuery query,
        CancellationToken cancellationToken)
    {


        ValidationResult validationResult = await _validator.ValidateAsync(query, cancellationToken);
        if (!validationResult.IsValid)
            return new(new Errors(ErrorType.Unauthorized, "/unauthorized"));


        try
        {
            string landingPage = string.Empty;
            string roleName = string.Empty;
            string roleDescription = string.Empty;
            List<UserPermission> userPermissions;

            RoleType roleType = RoleType.Unknown;
            if (query.ExternalId.HasValue)
            {
                User user = await _userReadRepository.ReadByExternalId(query.ExternalId.GetValueOrDefault(), cancellationToken);
                if (user != null)
                {
                    (roleType, landingPage, roleName, roleDescription, userPermissions) = await GetUserAreaInfo(user.RoleId.GetValueOrDefault(), cancellationToken);


                    if (string.IsNullOrEmpty(landingPage) || roleType == RoleType.Unknown || roleType == RoleType.Courier)
                    {
                        return new(new Errors(ErrorType.Unauthorized, "unauthorized"));
                    }
                }
                else
                {
                    if (query.IsLoginCheck == false)
                    {
                        return new(new Errors(ErrorType.Unauthorized, "unauthorized"));
                    }

                    user = await _userReadRepository.ReadByEmailAuth(query.Email, query.ExternalId.GetValueOrDefault(), cancellationToken);

                    if (user != null)
                    {
                        if (user.ExternalId == null)
                        {
                            user.ExternalId = query.ExternalId.GetValueOrDefault();

                            await _userWriteRepository.Update(user, cancellationToken);
                        }
                        (roleType, landingPage, roleName, roleDescription, userPermissions) = await GetUserAreaInfo(user.RoleId.GetValueOrDefault(), cancellationToken);
                        if (string.IsNullOrEmpty(landingPage) || roleType == RoleType.Unknown || roleType == RoleType.Courier)
                        {
                            return new(new Errors(ErrorType.Unauthorized, "unauthorized"));
                        }
                    }

                    else
                    {
                        return new(new Errors(ErrorType.Unauthorized, "unauthorized"));

                    }
                }

                UserLoginInfo userLoginInfo = new UserLoginInfo();
                userLoginInfo.LaboratoryId = user.LaboratoryId;
                userLoginInfo.UserId = user.Id;
                userLoginInfo.BioHubFacilityId = user.BioHubFacilityId;
                userLoginInfo.CourierId = user.CourierId;
                userLoginInfo.RoleId = user.RoleId.GetValueOrDefault();
                userLoginInfo.RoleName = roleName;
                userLoginInfo.RoleDescription = roleDescription;
                userLoginInfo.JobTitle = user.JobTitle;
                userLoginInfo.Email = user.Email;
                userLoginInfo.BusinessPhone = user.BusinessPhone;
                userLoginInfo.MobilePhone = user.MobilePhone;
                userLoginInfo.FirstName = user.FirstName;
                userLoginInfo.LastName = user.LastName;
                userLoginInfo.LoggedUserName = $"{user.FirstName} {user.LastName}";
                userLoginInfo.RoleType = roleType;
                userLoginInfo.LandingPage = landingPage;
                userLoginInfo.UserLogged = true;
                userLoginInfo.UserPermissions = userPermissions;

                return new(new GetAccessInformationQueryResponse(userLoginInfo));
            }

            return new(new Errors(ErrorType.Unauthorized, "unauthorized"));

        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error reading AuthCheck with Id {id} and email {email}", query.ExternalId, query.Email);
            return new(new Errors(ErrorType.Unauthorized, "unauthorized"));
        }
    }

    private async Task<(RoleType, string, string, string, List<UserPermission>)> GetUserAreaInfo(Guid roleId, CancellationToken cancellationToken)
    {
        Role role = await _roleReadRepository.ReadWithPermissions(roleId, cancellationToken);
        var userPermissions = GetUserPermissions(role);
        switch (role.RoleType)
        {
            case RoleType.WHO:

                return (role.RoleType, "whoarea", role.Name, role.Description, userPermissions);

            case RoleType.Laboratory:
                return (role.RoleType, "laboratoryarea", role.Name, role.Description, userPermissions);

            case RoleType.BioHubFacility:
                return (role.RoleType, "biohubfacilityarea", role.Name, role.Description, userPermissions);

            default:
                return (RoleType.Unknown, String.Empty, String.Empty, String.Empty, null);

        }
    }

    private List<UserPermission> GetUserPermissions(Role role)
    {
        List<UserPermission> userPermissions = new List<UserPermission>();
        var permissions = role?.RolePermissions?.Select(x => x.Permission).ToList();
        foreach (var permission in permissions)
        {
            UserPermission userPermission = new UserPermission();
            userPermission.PermissionId = permission.Id;
            userPermission.PermissionName = permission.Name;
            userPermissions.Add(userPermission);
        }

        return userPermissions;
    }
}