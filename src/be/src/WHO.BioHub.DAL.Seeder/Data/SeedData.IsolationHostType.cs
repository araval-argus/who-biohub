using EFCore.BulkExtensions;
using WHO.BioHub.Models.Models;
using Microsoft.EntityFrameworkCore;

namespace WHO.BioHub.DAL.Seeder.Data;

internal partial class SeedData
{
    internal static IsolationHostType[] IsolationHostTypes = new IsolationHostType[]
    {
        new()
        {
            Id = IsolationHostTypeId1,
            Name = "Human",
            Description = "Human",
            IsActive = true
        },
    };

    internal static Guid IsolationHostTypeId1 => Guid.Parse("1804863a-c927-4492-9dde-252502d3c619");

    private async Task AddOrUpdateIsolationHostTypes(CancellationToken cancellationToken)
    {       
        foreach (var isolationHostType in IsolationHostTypes)
        {
            if (await _db.IsolationHostTypes.Where(x => x.Id == isolationHostType.Id).AnyAsync(cancellationToken))
            {
                _db.Update(isolationHostType);
            }
            else
            {
                await _db.AddAsync(isolationHostType);
            }
        }
    }
}
