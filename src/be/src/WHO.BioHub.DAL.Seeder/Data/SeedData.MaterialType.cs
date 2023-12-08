using EFCore.BulkExtensions;
using WHO.BioHub.Models.Models;
using Microsoft.EntityFrameworkCore;

namespace WHO.BioHub.DAL.Seeder.Data;

internal partial class SeedData
{
    internal static MaterialType[] MaterialTypes => new MaterialType[]
    {
        new()
        {
            Id = MaterialTypeId1,
            Name = "Virus",
            Description = "Virus",
            IsActive = true
        },

        new()
        {
            Id = MaterialTypeId2,
            Name = "Bacteria",
            Description = "Bacteria",
            IsActive = true
        },

        new()
        {
            Id = MaterialTypeId3,
            Name = "Fungus",
            Description = "Fungus",
            IsActive = true
        },

        new()
        {
            Id = MaterialTypeId4,
            Name = "Parasite",
            Description = "Parasite",
            IsActive = true
        },

        new()
        {
            Id = MaterialTypeId5,
            Name = "Other",
            Description = "Other",
            IsActive = true
        },
    };

    internal static Guid MaterialTypeId1 => Guid.Parse("310bf918-0d90-420e-bccb-be47964cca26");
    internal static Guid MaterialTypeId2 => Guid.Parse("8b42fb42-a62f-44e2-8ef4-894a192140cb");
    internal static Guid MaterialTypeId3 => Guid.Parse("27d3eea2-0a05-4fbb-976b-dbb6bf0db845");
    internal static Guid MaterialTypeId4 => Guid.Parse("7e6f97e7-2181-4f4c-98d6-735781aaf980");
    internal static Guid MaterialTypeId5 => Guid.Parse("e5d1c893-a579-4cbb-85d6-a552dfb5e89e");

    private async Task AddOrUpdateMaterialTypes(CancellationToken cancellationToken)
    {
        foreach (var materialType in MaterialTypes)
        {
            if (await _db.MaterialTypes.Where(x => x.Id == materialType.Id).AnyAsync(cancellationToken))
            {
                _db.Update(materialType);
            }
            else
            {
                await _db.AddAsync(materialType);
            }
        }
        
    }
}
