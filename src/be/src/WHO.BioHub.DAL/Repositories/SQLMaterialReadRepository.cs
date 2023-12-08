using Microsoft.EntityFrameworkCore;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.Materials;
using WHO.BioHub.Shared.Enums;

namespace WHO.BioHub.DAL.Repositories;

public class SQLMaterialReadRepository : IMaterialReadRepository
{
    private readonly BioHubDbContext _dbContext;

    public SQLMaterialReadRepository(BioHubDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<Material>> List(CancellationToken cancellationToken)
    {
        return await _dbContext.Materials
            .Include(x => x.WorklistFromBioHubItemMaterials)
            .ThenInclude(x => x.WorklistFromBioHubItem)
            .Where(l => l.DeletedOn == null)
            .ToArrayAsync(cancellationToken);
    }

    public async Task<IEnumerable<Material>> ListForLaboratoryUser(Guid userLaboratoryId, CancellationToken cancellationToken)
    {
        return await _dbContext.Materials
            .Include(x => x.WorklistFromBioHubItemMaterials)
            .ThenInclude(x => x.WorklistFromBioHubItem)
            .Where(l => l.DeletedOn == null)
            .Where(l => l.ProviderLaboratoryId == userLaboratoryId || (l.PublicShare == YesNoOption.Yes && l.Status == MaterialStatus.Completed))
            .ToArrayAsync(cancellationToken);
    }

    public async Task<IEnumerable<Material>> ListForBioHubFacilityUser(Guid userBioHubFacilityId, CancellationToken cancellationToken)
    {
        return await _dbContext.Materials
            .Include(x => x.WorklistFromBioHubItemMaterials)
            .ThenInclude(x => x.WorklistFromBioHubItem)
            .Where(l => l.DeletedOn == null)
            .Where(l => l.OwnerBioHubFacilityId == userBioHubFacilityId || l.Status == MaterialStatus.Completed)
            .ToArrayAsync(cancellationToken);
    }

    public async Task<IEnumerable<Material>> ListForShipmentRequest(CancellationToken cancellationToken)
    {
        return await _dbContext.Materials
            .Where(l => l.DeletedOn == null)
            .Where(l => l.PublicShare == YesNoOption.Yes)
            .Where(l => l.Status == MaterialStatus.Completed)
            .ToArrayAsync(cancellationToken);
    }


    public async Task<IEnumerable<Material>> ListForShipmentRequestFromBioHub(Guid bioHubFacilityId, CancellationToken cancellationToken)
    {
        return await _dbContext.Materials
            .Where(l => l.DeletedOn == null)
            .Where(l => l.PublicShare == YesNoOption.Yes)
            .Where(l => l.Status == MaterialStatus.Completed)
            .Where(l => l.OwnerBioHubFacilityId == bioHubFacilityId)
            .ToArrayAsync(cancellationToken);
    }

    public async Task<Material> Read(Guid id, CancellationToken cancellationToken)
    {
        return await _dbContext.Materials
            .Include(l => l.MaterialCollectedSpecimenTypes)
            .Include(l => l.MaterialGSDInfo)
            .AsNoTracking()
            .AsSplitQuery()
            .FirstOrDefaultAsync(l => l.DeletedOn == null && l.Id == id, cancellationToken);
    }

    public async Task<Material> ReadForLaboratoryUser(Guid id, Guid UserLaboratoryId, CancellationToken cancellationToken)
    {
        return await _dbContext.Materials
            .Include(l => l.MaterialCollectedSpecimenTypes)
            .Include(l => l.MaterialGSDInfo)
            .AsNoTracking()
            .AsSplitQuery()
            .FirstOrDefaultAsync(l => l.DeletedOn == null && l.Id == id && (l.ProviderLaboratoryId == UserLaboratoryId || (l.PublicShare == YesNoOption.Yes && l.Status == MaterialStatus.Completed)), cancellationToken);
    }


    public async Task<Material> ReadForBioHubFacilityUser(Guid id, Guid UserBioHubFacilityId, CancellationToken cancellationToken)
    {
        return await _dbContext.Materials
            .Include(l => l.MaterialCollectedSpecimenTypes)
            .Include(l => l.MaterialGSDInfo)
            .AsNoTracking()
            .AsSplitQuery()
            .FirstOrDefaultAsync(l => l.DeletedOn == null && l.Id == id && (l.OwnerBioHubFacilityId == UserBioHubFacilityId || l.Status == MaterialStatus.Completed), cancellationToken);
    }



    public async Task<Material> ReadWithHistory(Guid id, CancellationToken cancellationToken)
    {
        return await _dbContext.Materials
            .Include(l => l.MaterialsHistory)            
            .AsNoTracking()
            .AsSplitQuery()
            .FirstOrDefaultAsync(l => l.DeletedOn == null && l.Id == id, cancellationToken);
    }

    public async Task<Material> ReadForLaboratoryUserWithHistory(Guid id, Guid UserLaboratoryId, CancellationToken cancellationToken)
    {
        return await _dbContext.Materials
            .Include(l => l.MaterialsHistory)
            .AsNoTracking()
            .AsSplitQuery()
            .FirstOrDefaultAsync(l => l.DeletedOn == null && l.Id == id && (l.ProviderLaboratoryId == UserLaboratoryId || (l.PublicShare == YesNoOption.Yes && l.Status == MaterialStatus.Completed)), cancellationToken);
    }


    public async Task<Material> ReadForBioHubFacilityUserWithHistory(Guid id, Guid UserBioHubFacilityId, CancellationToken cancellationToken)
    {
        return await _dbContext.Materials
            .Include(l => l.MaterialsHistory)
            .AsNoTracking()
            .AsSplitQuery()
            .FirstOrDefaultAsync(l => l.DeletedOn == null && l.Id == id && (l.OwnerBioHubFacilityId == UserBioHubFacilityId || l.Status == MaterialStatus.Completed), cancellationToken);
    }



    public async Task<bool> ReferenceNumberAlreadyPresent(Guid id, string referenceNumber, CancellationToken cancellationToken)
    {
        return await _dbContext.Materials
            .AsNoTracking()
            .AnyAsync(l => l.DeletedOn == null && l.Id != id && l.ReferenceNumber == referenceNumber, cancellationToken);
    }

    public async Task<bool> ReferenceNumberAlreadyPresentForCreation(string referenceNumber, CancellationToken cancellationToken)
    {
        return await _dbContext.Materials
            .AsNoTracking()
            .AnyAsync(l => l.DeletedOn == null && l.ReferenceNumber == referenceNumber, cancellationToken);
    }

    public async Task<IEnumerable<MaterialCollectedSpecimenType>> ReadMaterialCollectedSpecimenTypes(Guid id, CancellationToken cancellationToken)
    {
        return await _dbContext.MaterialCollectedSpecimenTypes
            .Where(x => x.MaterialId == id)
            .ToArrayAsync(cancellationToken);
    }

    public async Task<MaterialHistory> ReadPastInformation(Guid id, DateTime date, CancellationToken cancellationToken)
    {
        return await _dbContext.MaterialsHistory
            .AsNoTracking()
            .AsSplitQuery()
            .Where(l => l.MaterialId == id && l.LastOperationDate <= date)
            .OrderByDescending(x => x.LastOperationDate)
            .FirstOrDefaultAsync(cancellationToken);
    }
}