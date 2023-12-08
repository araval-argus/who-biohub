using Microsoft.EntityFrameworkCore;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.Laboratories;
using WHO.BioHub.Shared.Enums;

namespace WHO.BioHub.DAL.Repositories;

public class SQLLaboratoryReadRepository : ILaboratoryReadRepository
{
    private readonly BioHubDbContext _dbContext;

    public SQLLaboratoryReadRepository(BioHubDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<Laboratory>> List(CancellationToken cancellationToken)
    {
        return await _dbContext.Laboratories
            .Where(l => l.DeletedOn == null)
            .ToArrayAsync(cancellationToken);
    }

    public async Task<IEnumerable<Laboratory>> ListMap(CancellationToken cancellationToken)
    {
        return await _dbContext.Laboratories            
            .Where(l => l.DeletedOn == null)
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

    public async Task<IEnumerable<Laboratory>> ListForLaboratoryUser(Guid? laboratoryId, CancellationToken cancellationToken)
    {
        return await _dbContext.Laboratories
            .Where(l => l.DeletedOn == null)
            .Where(l => l.Id == laboratoryId || l.IsPublicFacing == true)
            .ToArrayAsync(cancellationToken);
    }

    public async Task<IEnumerable<Laboratory>> ListMapForLaboratoryUser(Guid? laboratoryId, CancellationToken cancellationToken)
    {
        return await _dbContext.Laboratories            
            .Where(l => l.DeletedOn == null)
            .Where(l => l.Id == laboratoryId || l.IsPublicFacing == true)
            .ToArrayAsync(cancellationToken);
    }

    public async Task<Laboratory> Read(Guid id, CancellationToken cancellationToken)
    {
        return await _dbContext.Laboratories
            .AsNoTracking()
            .FirstOrDefaultAsync(l => l.DeletedOn == null && l.Id == id, cancellationToken);
    }
    public async Task<Laboratory> ReadForLaboratoryUser(Guid id, Guid UserLaboratoryId, CancellationToken cancellationToken)
    {
        return await _dbContext.Laboratories
            .AsNoTracking()
            .FirstOrDefaultAsync(l => l.DeletedOn == null && l.Id == id && (l.Id == UserLaboratoryId || l.IsPublicFacing == true), cancellationToken);
    }

    public async Task<LaboratoryHistory> ReadPastInformation(Guid id, DateTime date, CancellationToken cancellationToken)
    {
        return await _dbContext.LaboratoriesHistory
            .Include(x => x.Country)
            .AsNoTracking()
            .AsSplitQuery()
            .Where(l => l.LaboratoryId == id && l.LastOperationDate <= date)
            .OrderByDescending(x => x.LastOperationDate)
            .FirstOrDefaultAsync(cancellationToken);
    }
}