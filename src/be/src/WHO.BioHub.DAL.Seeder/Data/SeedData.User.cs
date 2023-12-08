using WHO.BioHub.Models.Models;
using WHO.BioHub.Shared.Enums;

namespace WHO.BioHub.DAL.Seeder.Data;

internal partial class SeedData
{
    internal static User[] Users = new User[]
    {

    };


    private async Task AddUsers(CancellationToken cancellationToken)
    {

        await _db.AddRangeAsync(Users, cancellationToken: cancellationToken);
    }
}