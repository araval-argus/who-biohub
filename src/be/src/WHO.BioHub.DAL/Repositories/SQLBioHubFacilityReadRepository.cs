using Microsoft.EntityFrameworkCore;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.BioHubFacilities;
using WHO.BioHub.Shared.Enums;

namespace WHO.BioHub.DAL.Repositories;

public class SQLBioHubFacilityReadRepository : IBioHubFacilityReadRepository
{
    private readonly BioHubDbContext _dbContext;

    public SQLBioHubFacilityReadRepository(BioHubDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<BioHubFacility>> List(CancellationToken cancellationToken)
    {
        return await _dbContext.BioHubFacilities
            .Where(l => l.DeletedOn == null)
            .ToArrayAsync(cancellationToken);
    }

    public async Task<IEnumerable<BioHubFacility>> ListMap(CancellationToken cancellationToken)
    {
        return await _dbContext.BioHubFacilities            
            .Where(l => l.DeletedOn == null)
            .ToArrayAsync(cancellationToken);
    }

    public async Task<IEnumerable<WorklistToBioHubItem>> GetWorklistToBioHubItemsForMap(Guid bioHubFacilityId, CancellationToken cancellationToken)
    {
        return await _dbContext.WorklistToBioHubItems
            .AsNoTracking()
            .Include(x => x.RequestInitiationFromLaboratory)
            .Include(x => x.RequestInitiationToBioHubFacility)
            .Include(x => x.WorklistToBioHubItemMaterials)
            .ThenInclude(x => x.Material)
            .Where(l => l.RequestInitiationToBioHubFacilityId == bioHubFacilityId)
            .Where(l => l.Status == WorklistToBioHubStatus.ShipmentCompleted)            
            .ToArrayAsync(cancellationToken);
    }


    public async Task<IEnumerable<WorklistFromBioHubItem>> GetWorklistFromBioHubItemsForMap(Guid bioHubFacilityId, CancellationToken cancellationToken)
    {
        return await _dbContext.WorklistFromBioHubItems
            .AsNoTracking()
            .Include(x => x.RequestInitiationToLaboratory)
            .Include(x => x.RequestInitiationFromBioHubFacility)
            .Include(x => x.WorklistFromBioHubItemMaterials)
            .ThenInclude(x => x.Material)
            .Where(l => l.RequestInitiationFromBioHubFacilityId == bioHubFacilityId)
            .Where(l => l.Status == WorklistFromBioHubStatus.ShipmentCompleted)
            .ToArrayAsync(cancellationToken);
    }

    public async Task<IEnumerable<BioHubFacility>> ListForLaboratoryUser(CancellationToken cancellationToken)
    {
        return await _dbContext.BioHubFacilities
            .Where(l => l.DeletedOn == null && l.IsPublicFacing == true)
            .ToArrayAsync(cancellationToken);
    }

    public async Task<IEnumerable<BioHubFacility>> ListMapForLaboratoryUser(CancellationToken cancellationToken)
    {
        return await _dbContext.BioHubFacilities            
            .Where(l => l.DeletedOn == null && l.IsPublicFacing == true)
            .ToArrayAsync(cancellationToken);
    }

    public async Task<BioHubFacility> Read(Guid id, CancellationToken cancellationToken)
    {
        return await _dbContext.BioHubFacilities
            .AsNoTracking()
            .FirstOrDefaultAsync(l => l.DeletedOn == null && l.Id == id, cancellationToken);
    }

    public async Task<BioHubFacilityHistory> ReadPastInformation(Guid id, DateTime date, CancellationToken cancellationToken)
    {
        return await _dbContext.BioHubFacilitiesHistory
            .Include(x => x.Country)
            .AsNoTracking()
            .AsSplitQuery()
            .Where(l => l.BioHubFacilityId == id && l.LastOperationDate <= date)
            .OrderByDescending(x => x.LastOperationDate)
            .FirstOrDefaultAsync(cancellationToken);
    }
}