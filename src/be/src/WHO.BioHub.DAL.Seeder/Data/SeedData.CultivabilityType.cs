using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
using WHO.BioHub.Models.Models;

namespace WHO.BioHub.DAL.Seeder.Data;

internal partial class SeedData
{
    internal static CultivabilityType[] CultivabilityTypes => new CultivabilityType[]
    {
        new()
        {
            Id = CultivabilityTypeId1,
            Name = "Cultivable",
            Description = "Cultivable",
            IsActive = true
        },
        new()
        {
            Id = CultivabilityTypeId2,
            Name = "NA",
            Description = "NA",
            IsActive = true
        },
        new()
        {
            Id = CultivabilityTypeId3,
            Name = "Not cultivable",
            Description = "Not cultivable",
            IsActive = true
        },
    };

    internal static Guid CultivabilityTypeId1 => Guid.Parse("16329031-22ba-4e44-8b9d-256b4d0c5c62");
    internal static Guid CultivabilityTypeId2 => Guid.Parse("2587ee82-66c5-4b47-b986-b7fddab34e35");
    internal static Guid CultivabilityTypeId3 => Guid.Parse("813f912c-99ed-44e2-a098-821032b7b0f8");

    private async Task AddOrUpdateCultivabilityTypes(CancellationToken cancellationToken)
    {

        foreach (var cultivabilityType in CultivabilityTypes)
        {
            if (await _db.CultivabilityTypes.Where(x => x.Id == cultivabilityType.Id).AnyAsync(cancellationToken))
            {
                _db.Update(cultivabilityType);
            }
            else
            {
                await _db.AddAsync(cultivabilityType);
            }
        }
    }
}
