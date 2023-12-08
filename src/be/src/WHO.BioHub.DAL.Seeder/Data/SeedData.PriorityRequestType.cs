using EFCore.BulkExtensions;
using WHO.BioHub.Models.Models;
using Microsoft.EntityFrameworkCore;

namespace WHO.BioHub.DAL.Seeder.Data;

internal partial class SeedData
{
    internal static PriorityRequestType[] PriorityRequestTypes => new PriorityRequestType[]
    {
        new()
        {
            Id = PriorityRequestTypeId1,
            Name = "Normal",
            Description = "Normal",
            HexColor = "#00FF00",
            IsActive = true,
        },
    };

    internal static Guid PriorityRequestTypeId1 => Guid.Parse("0516589d-1868-45ab-891d-0a9af1673360");

    private async Task AddOrUpdatePriorityRequestTypes(CancellationToken cancellationToken)
    {        
        foreach (var priorityRequestType in PriorityRequestTypes)
        {
            if (await _db.PriorityRequestTypes.Where(x => x.Id == priorityRequestType.Id).AnyAsync(cancellationToken))
            {
                _db.Update(priorityRequestType);
            }
            else
            {
                await _db.AddAsync(priorityRequestType);
            }
        }
    }
}
