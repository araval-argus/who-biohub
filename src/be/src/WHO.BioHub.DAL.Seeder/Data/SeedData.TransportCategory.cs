using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
using WHO.BioHub.Models.Models;

namespace WHO.BioHub.DAL.Seeder.Data;

internal partial class SeedData
{
    internal static TransportCategory[] TransportCategories => new TransportCategory[]
        {
                new()
                {
                    Id = TransportCategoryId1,
                    Name = "Category B (UN3373)",
                    Description = "Category B (UN3373)",
                    HexColor = "#00FF00",
                    IsActive = true,
                },
                new()
                {
                    Id = TransportCategoryId2,
                    Name = "Category A (UN2814)",
                    Description = "Category A (UN2814)",
                    HexColor = "#FF0000",
                    IsActive = true,
                },
                new()
                {
                    Id = TransportCategoryId3,
                    Name = "Other",
                    Description = "Other",
                    HexColor = "#0000FF",
                    IsActive = true,
                },
        };

    internal static Guid TransportCategoryId1 => Guid.Parse("e0da736c-538b-4a48-8d2d-db95c5383a51");
    internal static Guid TransportCategoryId2 => Guid.Parse("914a23b7-1a0e-4fc6-989e-1759ed63b5cf");
    internal static Guid TransportCategoryId3 => Guid.Parse("36eb3f04-a2e7-4834-b5ad-2314d4178cbb");
    private async Task AddOrUpdateTransportCategories(CancellationToken cancellationToken)
    {
        foreach (var transportCategory in TransportCategories)
        {
            if (await _db.TransportCategories.Where(x => x.Id == transportCategory.Id).AnyAsync(cancellationToken))
            {
                _db.Update(transportCategory);
            }
            else
            {
                await _db.AddAsync(transportCategory);
            }
        }
    }            
}
