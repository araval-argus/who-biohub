using Microsoft.EntityFrameworkCore;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.Laboratories;
using WHO.BioHub.DAL;
using WHO.BioHub.Public.SQL.Abstractions;
using WHO.BioHub.Shared.Enums;

namespace WHO.BioHub.Public.SQL.Repositories;

public class SQLLaboratoryPublicReadRepository : ILaboratoryPublicReadRepository
{
    private readonly BioHubDbContext _dbContext;

    public SQLLaboratoryPublicReadRepository(BioHubDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<Laboratory>> List(CancellationToken cancellationToken)
    {
        return await _dbContext.Laboratories
            .Where(l => l.DeletedOn == null)
            .Where(l => l.IsPublicFacing == true)
            .ToArrayAsync(cancellationToken);
    }

    public async Task<IEnumerable<Laboratory>> ListMap(CancellationToken cancellationToken)
    {
        return await _dbContext.Laboratories
            .AsNoTracking()           
            .Where(l => l.DeletedOn == null)
            .Where(l => l.IsPublicFacing == true)            
            .ToArrayAsync(cancellationToken);
    }

    public async Task<IEnumerable<WorklistToBioHubItem>> GetWorklistToBioHubItemsForMap(Guid laboratoryId, CancellationToken cancellationToken)
    {
        return await _dbContext.WorklistToBioHubItems
            .AsNoTracking()           
            .Include(x => x.RequestInitiationToBioHubFacility)          
            .Include(x => x.WorklistToBioHubItemMaterials)
            .ThenInclude(x => x.Material)            
            .Where(l => l.RequestInitiationFromLaboratoryId == laboratoryId)
            .Where(l => l.Status == WorklistToBioHubStatus.ShipmentCompleted)            
            .ToArrayAsync(cancellationToken);
    }


    public async Task<IEnumerable<WorklistFromBioHubItem>> GetWorklistFromBioHubItemsForMap(Guid laboratoryId, CancellationToken cancellationToken)
    {
        return await _dbContext.WorklistFromBioHubItems
            .AsNoTracking()
            .Include(x => x.RequestInitiationFromBioHubFacility)
            .Include(x => x.WorklistFromBioHubItemMaterials)
            .ThenInclude(x => x.Material)
            .Where(l => l.RequestInitiationToLaboratoryId == laboratoryId)
            .Where(l => l.Status == WorklistFromBioHubStatus.ShipmentCompleted)            
            .ToArrayAsync(cancellationToken);
    }



    public async Task<Laboratory> Read(Guid id, CancellationToken cancellationToken)
    {
        return await _dbContext.Laboratories
            .AsNoTracking()
            .Where(l => l.IsPublicFacing == true)
            .FirstOrDefaultAsync(l => l.DeletedOn == null && l.IsPublicFacing == true && l.Id == id, cancellationToken);
    }
}