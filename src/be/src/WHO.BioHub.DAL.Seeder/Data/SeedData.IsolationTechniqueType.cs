using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
using WHO.BioHub.Models.Models;

namespace WHO.BioHub.DAL.Seeder.Data;

internal partial class SeedData
{
    internal static IsolationTechniqueType[] IsolationTechniqueTypes => new IsolationTechniqueType[]
    {
        new()
        {
            Id = IsolationTechniqueTypeId1,
             Name = "Cell culture",
            Description = "Cell culture",
            IsActive = true
        },
        new()
        {
            Id = IsolationTechniqueTypeId2,
            Name = "NA",
            Description = "NA",
            IsActive = true
        },
    };

    internal static Guid IsolationTechniqueTypeId1 => Guid.Parse("ee7c806f-b271-4966-ac01-a83475f0c4ee");
    internal static Guid IsolationTechniqueTypeId2 => Guid.Parse("1319c8fb-2db4-452d-9029-4d59fc72dbd1");

    private async Task AddOrUpdateIsolationTechniqueTypes(CancellationToken cancellationToken)
    {
        foreach (var isolationTechniqueType in IsolationTechniqueTypes)
        {
            if (await _db.IsolationTechniqueTypes.Where(x => x.Id == isolationTechniqueType.Id).AnyAsync(cancellationToken))
            {
                _db.Update(isolationTechniqueType);
            }
            else
            {
                await _db.AddAsync(isolationTechniqueType);
            }
        }
    }
}
