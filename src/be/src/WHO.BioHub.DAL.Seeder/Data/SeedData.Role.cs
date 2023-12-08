using Microsoft.EntityFrameworkCore;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Shared.Enums;

namespace WHO.BioHub.DAL.Seeder.Data;

internal partial class SeedData
{
    internal static Role[] Roles = new Role[]
    {
        new()
        {
            Id = WHOSecretariatRoleId,
            Name = "Secretariat",
            Description = "Secretariat",
            AddToRegistration = false,
            OnBehalfOf = false,
            RoleType = RoleType.WHO

        },
        new()
        {
            Id = WHOOperationalFocalPointRoleId,
            Name = "Operational Focal Point",
            Description = "Operational Focal Point",
            AddToRegistration = false,
            OnBehalfOf = false,
            RoleType = RoleType.WHO
        },
        new()
        {
            Id = WHOBiosafetyBiosecurityFocalPointRoleId,
            Name = "Biosafety & Biosecurity Focal Point",
            Description = "Biosafety & Biosecurity Focal Point",
            AddToRegistration = false,
            OnBehalfOf = false,
            RoleType = RoleType.WHO
        },
        new()
        {
            Id = WHOLaboratoryFocalPointRoleId,
            Name = "Laboratory Focal Point",
            Description = "Laboratory Focal Point",
            AddToRegistration = false,
            OnBehalfOf = false,
            RoleType = RoleType.WHO
        },
        new()
        {
            Id = LaboratoryITToolFocalPointRoleId,
            Name = "Admin",
            Description = "A person who plays administrative roles in this tool on behalf of your institute, including the registration of other staff members and the management of your institute profile",
            PublicName = "BioHub System User",
            AddToRegistration = true,
            OnBehalfOf = false,
            RoleType = RoleType.Laboratory
        },

        new()
        {
            Id = OnBehalfOfLaboratoryITToolFocalPointRoleId,
            Name = "On Behalf of Admin",
            Description = "A person who plays administrative roles in this tool on behalf of your institute, including the registration of other staff members and the management of your institute profile",
            PublicName = "BioHub System User",
            AddToRegistration = false,
            OnBehalfOf = true,
            RoleType = RoleType.Laboratory
        },

        new()
        {
            Id = LaboratoryUserRoleId,
            Name = "Basic",
            Description = "Basic User Role",
            AddToRegistration = false,
            OnBehalfOf = false,
            RoleType = RoleType.Laboratory
        },

        new()
        {
            Id = BioHubFacilityITToolFocalPointRoleId,
            Name = "BioHub Facility IT tool Focal Point",
            Description = "Admin Focal Point",
            AddToRegistration = false,
            OnBehalfOf = false,
            RoleType = RoleType.BioHubFacility
        },

        new()
        {
            Id = OnBehalfOfBioHubFacilityITToolFocalPointRoleId,
            Name = "On Behalf of BioHub Facility IT tool Focal Point",
            Description = "Admin Focal Point",
            AddToRegistration = false,
            OnBehalfOf = true,
            RoleType = RoleType.BioHubFacility
        },

        new()
        {
            Id = BioHubFacilityUserRoleId,
            Name = "User",
            Description = "User",
            AddToRegistration = false,
            OnBehalfOf = false,
            RoleType = RoleType.BioHubFacility
        },

        new()
        {
            Id = CourierUserRoleId,
            Name = "Courier",
            Description = "Courier",
            AddToRegistration = false,
            OnBehalfOf = false,
            RoleType = RoleType.Courier
        },
    };



    //WHO Roles
    internal static Guid WHOSecretariatRoleId => Guid.Parse("1769afec-8f8e-45d7-82cd-45403f664a70");
    internal static Guid WHOOperationalFocalPointRoleId => Guid.Parse("f6832a9e-a9b9-4578-8d29-e35fad8d7f1a");
    internal static Guid WHOBiosafetyBiosecurityFocalPointRoleId => Guid.Parse("2fd97531-eb2d-46d0-b47d-6c0f97a29c96");
    internal static Guid WHOLaboratoryFocalPointRoleId => Guid.Parse("be9444b7-97a6-400e-8e70-7913a2874290");

    //Laboratory
    internal static Guid LaboratoryITToolFocalPointRoleId => Guid.Parse("dc8f16bd-a79e-46bb-8c3d-1f1a7ae80a1e");
    internal static Guid OnBehalfOfLaboratoryITToolFocalPointRoleId => Guid.Parse("8ab5ccaf-7c26-4d18-9502-e1c9c354eb74");
    internal static Guid LaboratoryUserRoleId => Guid.Parse("7be31d2b-8f18-4f9c-98ee-bb6d3ce986db");

    //BioHub Facility
    internal static Guid BioHubFacilityITToolFocalPointRoleId => Guid.Parse("7de2b1cf-9a18-48d4-919c-efeea306ce16");
    internal static Guid OnBehalfOfBioHubFacilityITToolFocalPointRoleId => Guid.Parse("915cb0b3-b55c-4b94-95c0-6bf6f4036845");

    internal static Guid BioHubFacilityUserRoleId => Guid.Parse("7465e776-a0ac-47ec-b29d-0d65802804fd");

    //Courier
    internal static Guid CourierUserRoleId => Guid.Parse("48bdd263-5cd6-4cac-8a43-51601c00b1aa");

    
    private async Task AddOrUpdateRoles(CancellationToken cancellationToken)
    {
        foreach (var role in Roles)
        {
            if (await _db.Roles.Where(x => x.Id == role.Id).AnyAsync(cancellationToken))
            {
                _db.Update(role);
            }
            else
            {
                await _db.AddAsync(role);
            }
        }
    }
}