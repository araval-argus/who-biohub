using Microsoft.EntityFrameworkCore;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.Materials;
using WHO.BioHub.DAL;
using WHO.BioHub.Public.SQL.Abstractions;
using WHO.BioHub.Shared.Enums;

namespace WHO.BioHub.Public.SQL.Repositories;

public class SQLMaterialPublicReadRepository : IMaterialPublicReadRepository
{
    private readonly BioHubDbContext _dbContext;

    public SQLMaterialPublicReadRepository(BioHubDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<Material>> List(CancellationToken cancellationToken)
    {
        return await _dbContext.Materials
            .Include(x => x.MaterialsHistory)
            .Include(l => l.WorklistToBioHubItemMaterials)
            .ThenInclude(l => l.WorklistToBioHubItem)
            .ThenInclude(l => l.BookingForms)
            .Where(l => l.DeletedOn == null)
            .Where(l => l.BHFShareReadiness == Readiness.Ready)
            .Where(l => l.PublicShare == YesNoOption.Yes)
            .Where(l => l.Status == MaterialStatus.Completed)
            .Where(l => l.OwnerBioHubFacilityId != null && l.OwnerBioHubFacility.IsPublicFacing == true && l.OwnerBioHubFacility.DeletedOn == null)
            .Where(l => (l.ProviderBioHubFacility != null && l.ProviderBioHubFacility.DeletedOn == null && l.ProviderBioHubFacility.IsPublicFacing == true) ||
             (l.ProviderLaboratory != null && l.ProviderLaboratory.DeletedOn == null && l.ProviderLaboratory.IsPublicFacing == true))
            .ToArrayAsync(cancellationToken);
    }

    public async Task<Material> Read(Guid id, CancellationToken cancellationToken)
    {
        return await _dbContext.Materials
            .Include(l => l.WorklistToBioHubItemMaterials)
            .ThenInclude(l => l.WorklistToBioHubItem)
            .ThenInclude(l => l.BookingForms)
            .AsNoTracking()
            .AsSplitQuery()
            .Where(l => l.OwnerBioHubFacilityId != null && l.OwnerBioHubFacility.IsPublicFacing == true && l.OwnerBioHubFacility.DeletedOn == null)
            .FirstOrDefaultAsync(l => l.DeletedOn == null && l.Id == id && l.PublicShare == YesNoOption.Yes && l.BHFShareReadiness == Readiness.Ready && l.Status == MaterialStatus.Completed && ((l.ProviderBioHubFacility != null && l.ProviderBioHubFacility.DeletedOn == null && l.ProviderBioHubFacility.IsPublicFacing == true) ||
             (l.ProviderLaboratory != null && l.ProviderLaboratory.DeletedOn == null && l.ProviderLaboratory.IsPublicFacing == true)), cancellationToken);
    }
}