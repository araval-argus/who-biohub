using Microsoft.EntityFrameworkCore;
using WHO.BioHub.Models.Models;

namespace WHO.BioHub.DAL.Seeder.Data;

internal partial class SeedData
{
    internal static BSLLevel[] BSLLevels => new BSLLevel[]
    {
        new()
        {
            Id = BSLLevelId1,
            Name = "BSL 1",
            Description = "Biosafety Level 1",
            Code = "BSL1",
        },
        new()
        {
            Id = BSLLevelId2,
            Name = "BSL 2",
            Description = "Biosafety Level 2",
            Code = "BSL2",
        },
        new()
        {
            Id = BSLLevelId3,
            Name = "BSL 3",
            Description = "Biosafety Level 3",
            Code = "BSL3",
        },
        new()
        {
            Id = BSLLevelId4,
            Name = "BSL 4",
            Description = "Biosafety Level 4",
            Code = "BSL4",
        },
    };

    internal static Guid BSLLevelId1 => Guid.Parse("259c7762-f221-4579-8163-2344bde782b5");
    internal static Guid BSLLevelId2 => Guid.Parse("ebd11313-db49-4c13-8d45-998f0a7390c0");
    internal static Guid BSLLevelId3 => Guid.Parse("2ce61486-0caf-407e-9e09-8f8d786bab59");
    internal static Guid BSLLevelId4 => Guid.Parse("4c6100e6-427e-4069-9935-b113136b1e8e");

    private async Task AddOrUpdateBSLLevels(CancellationToken cancellationToken)
    {      
        foreach (var BSLLevel in BSLLevels)
        {
            if (await _db.BSLLevels.Where(x => x.Id == BSLLevel.Id).AnyAsync(cancellationToken))
            {
                _db.Update(BSLLevel);
            }
            else
            {
                await _db.AddAsync(BSLLevel);
            }
        }

    }
}
