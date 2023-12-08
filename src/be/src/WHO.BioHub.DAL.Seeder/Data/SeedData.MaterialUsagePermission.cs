using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
using WHO.BioHub.Models.Models;

namespace WHO.BioHub.DAL.Seeder.Data;

internal partial class SeedData
{
    internal static MaterialUsagePermission[] MaterialUsagePermissions => new MaterialUsagePermission[]
    {
        new()
        {
            Id = MaterialUsagePermissionId1,
            Name = "Non-commercial use",
            Description = "Non-commercial use",
            IsActive = true
        },

         new()
        {
            Id = MaterialUsagePermissionId2,
            Name = "Commercial use",
            Description = "Commercial use",
            IsActive = true
        },

          new()
        {
            Id = MaterialUsagePermissionId3,
            Name = "Other",
            Description = "Other",
            IsActive = true
        },
    };

    internal static Guid MaterialUsagePermissionId1 => Guid.Parse("918f2f86-0f51-4272-8ea9-065df1ca29fa");
    internal static Guid MaterialUsagePermissionId2 => Guid.Parse("0e424976-745f-414a-b989-94a5253394bc");
    internal static Guid MaterialUsagePermissionId3 => Guid.Parse("3a9e1964-7964-4b2b-80c1-07748ad4aeb8");

    private async Task AddOrUpdateMaterialUsagePermissions(CancellationToken cancellationToken)
    {
        foreach (var materialUsagePermission in MaterialUsagePermissions)
        {
            if (await _db.MaterialUsagePermissions.Where(x => x.Id == materialUsagePermission.Id).AnyAsync(cancellationToken))
            {
                _db.Update(materialUsagePermission);
            }
            else
            {
                await _db.AddAsync(materialUsagePermission);
            }
        }        
    }
}
