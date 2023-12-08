using WHO.BioHub.Models.Models;
using WHO.BioHub.Shared.Enums;

namespace WHO.BioHub.DAL.Seeder.Data;

internal partial class SeedData
{
    internal static Resource[] Resources = new Resource[]
    {
        new()
        {
            Id = ResourceId1,
            Type = ResourceType.Folder,
            Name = "Root"
        },

    };

    internal static Guid ResourceId1 => Guid.Parse("2cfc588c-da63-4cb0-8132-80aadb5bf524");


    private async Task AddResources(CancellationToken cancellationToken)
    {

        await _db.AddRangeAsync(Resources, cancellationToken: cancellationToken);
    }
}