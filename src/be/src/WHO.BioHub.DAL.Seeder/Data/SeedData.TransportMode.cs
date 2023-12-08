using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
using WHO.BioHub.Models.Models;

namespace WHO.BioHub.DAL.Seeder.Data;

internal partial class SeedData
{
    internal static TransportMode[] TransportModes => new TransportMode[]
        {
                new()
                {
                    Id = TransportModeId1,
                    Name = "Road",
                    Description = "Road",
                    HexColor = "#00FF00",
                    IsActive = true,
                },
                new()
                {
                    Id = TransportModeId2,
                    Name = "Air",
                    Description = "Air",
                    HexColor = "#FF0000",
                    IsActive = true,
                },
                new()
                {
                    Id = TransportModeId3,
                    Name = "Other",
                    Description = "Other",
                    HexColor = "#FF0000",
                    IsActive = true,
                },
        };

    internal static Guid TransportModeId1 => Guid.Parse("da1d36c3-56e7-4469-ad04-5605c62c82a5");
    internal static Guid TransportModeId2 => Guid.Parse("e23135ef-ca6d-4e2a-9d1b-13ec96d92cd1");
    internal static Guid TransportModeId3 => Guid.Parse("ecfdbcdd-f658-4547-8ebb-2cba531762c6");

    private async Task AddOrUpdateTransportModes(CancellationToken cancellationToken)
    {
        foreach (var transportMode in TransportModes)
        {
            if (await _db.TransportModes.Where(x => x.Id == transportMode.Id).AnyAsync(cancellationToken))
            {
                _db.Update(transportMode);
            }
            else
            {
                await _db.AddAsync(transportMode);
            }
        }
    }
}
