using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WHO.BioHub.Shared.Enums;

namespace WHO.BioHub.Shared.Dto
{
    public class UserLoginInfo
    {
        public string LandingPage { get; set; }
        public RoleType RoleType { get; set; }
        public string RoleName { get; set; }
        public string RoleDescription { get; set; }
        public string JobTitle { get; set; }
        public string BusinessPhone { get; set; }
        public string MobilePhone { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Guid UserId { get; set; }
        public string LoggedUserName { get; set; }
        public string Email { get; set; }
        public Guid RoleId { get; set; }
        public Guid? LaboratoryId { get; set; }
        public Guid? BioHubFacilityId { get; set; }
        public Guid? CourierId { get; set; }
        public bool UserLogged { get; set; }
        public IEnumerable<UserPermission> UserPermissions { get; set; }
    }

    public class UserPermission
    {
        public Guid? PermissionId { get; set; }
        public string PermissionName { get; set; }
    }
}
