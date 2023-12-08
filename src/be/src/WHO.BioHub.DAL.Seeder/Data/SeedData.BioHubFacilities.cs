using EFCore.BulkExtensions;
using WHO.BioHub.Models.Models;

namespace WHO.BioHub.DAL.Seeder.Data;

internal partial class SeedData
{
    internal static Guid BioHubFacilityId1 => Guid.Parse("4a818141-3268-41a4-bd61-52c1bb5d87d6");
    internal static BioHubFacility[] BioHubFacilities => new BioHubFacility[]
    {
        new()
        {
            Id = BioHubFacilityId1,
            Name = "Spiez Laboratory",
            Abbreviation = "LS",
            IsActive = true,
            Description = "Spiez Laboratory",
            Address = "Spiez Laboratory, Austrasse, CH-3700 Spiez",
            Latitude = 46.6904905167597,
            Longitude = 7.6438726903634,
            BSLLevelId = BSLLevelId4,
            CountryId = Countries.FirstOrDefault(x => x.Name == "Switzerland")?.Id,
        },
    };

    private async Task AddBioHubFacilities(CancellationToken cancellationToken)
    {
        await _db.AddRangeAsync(BioHubFacilities, cancellationToken: cancellationToken);
    }
}
