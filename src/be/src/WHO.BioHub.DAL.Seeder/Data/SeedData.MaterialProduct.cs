using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
using WHO.BioHub.Models.Models;

namespace WHO.BioHub.DAL.Seeder.Data;

internal partial class SeedData
{
    internal static MaterialProduct[] MaterialProducts => new MaterialProduct[]
    {
        new()
        {
            Id = MaterialProductId1,
            Name = "Clinical specimen",
            Description = "Category B",
            IsActive = true
        },
        new()
        {
            Id = MaterialProductId2,
            Name = "Cultured isolate of human origin",
            Description = "Category A",
            IsActive = true
        },

        new()
        {
            Id = MaterialProductId3,
            Name = "Other",
            Description = "Other",
            IsActive = true
        },
    };

    internal static Guid MaterialProductId1 => Guid.Parse("1a432032-26fd-4830-9881-a115953c9177");
    internal static Guid MaterialProductId2 => Guid.Parse("10212429-dd69-41c9-8f8e-04172f11aeb5");
    internal static Guid MaterialProductId3 => Guid.Parse("151bacee-8e5f-4438-865e-d3fcb3e8215b");

    private async Task AddMaterialProducts(CancellationToken cancellationToken)
    {
        await _db.AddRangeAsync(MaterialProducts, cancellationToken: cancellationToken);
    }

    private async Task AddOrUpdateMaterialProducts(CancellationToken cancellationToken)
    {
        foreach (var materialProduct in MaterialProducts)
        {
            if (await _db.MaterialProducts.Where(x => x.Id == materialProduct.Id).AnyAsync(cancellationToken))
            {
                _db.Update(materialProduct);
            }
            else
            {
                await _db.AddAsync(materialProduct);
            }
        }
    }
}
