using Microsoft.EntityFrameworkCore;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.WorklistFromBioHubHistoryItems;
using WHO.BioHub.Shared.Enums;

namespace WHO.BioHub.DAL.Repositories;

public class SQLWorklistFromBioHubHistoryItemReadRepository : IWorklistFromBioHubHistoryItemReadRepository
{
    private readonly BioHubDbContext _dbContext;

    public SQLWorklistFromBioHubHistoryItemReadRepository(BioHubDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<WorklistFromBioHubHistoryItem>> List(CancellationToken cancellationToken)
    {
        return await _dbContext.WorklistFromBioHubHistoryItems
            .Where(l => l.DeletedOn == null)
            .ToArrayAsync(cancellationToken);
    }

    public async Task<WorklistFromBioHubHistoryItem> Read(Guid id, CancellationToken cancellationToken)
    {
        return await _dbContext.WorklistFromBioHubHistoryItems
            .AsNoTracking()
            .FirstOrDefaultAsync(l => l.DeletedOn == null && l.Id == id, cancellationToken);
    }

    public async Task<WorklistFromBioHubHistoryItem> ReadByIdAndStatus(Guid id, WorklistFromBioHubStatus status, CancellationToken cancellationToken)
    {
        return await _dbContext.WorklistFromBioHubHistoryItems
            .AsNoTracking()
            .FirstOrDefaultAsync(l => l.DeletedOn == null && l.Id == id && l.Status == status, cancellationToken);
    }

    public async Task<IEnumerable<WorklistFromBioHubHistoryItem>> ListByWorklistItemIdWithExtraInfo(Guid worklistItemId, CancellationToken cancellationToken)
    {
        return await _dbContext.WorklistFromBioHubHistoryItems
            .Include(x => x.RequestInitiationToLaboratory)
            .Include(x => x.RequestInitiationFromBioHubFacility)
            .Include(x => x.LastOperationUser)
            .ThenInclude(x => x.Role)
            .Include(x => x.WorklistFromBioHubHistoryItemDocuments)
            .ThenInclude(x => x.Document)
            .Where(l => l.DeletedOn == null && l.WorklistFromBioHubItemId == worklistItemId)
            .AsNoTracking()
            .ToArrayAsync(cancellationToken);
    }

    public async Task<WorklistFromBioHubHistoryItem> ReadByIdWithDocuments(Guid id, CancellationToken cancellationToken)
    {
        return await _dbContext.WorklistFromBioHubHistoryItems
            .AsNoTracking()
            .Include(l => l.WorklistFromBioHubHistoryItemDocuments)
            .ThenInclude(l => l.Document)
            .FirstOrDefaultAsync(l => l.DeletedOn == null && l.Id == id, cancellationToken);
    }

    public async Task<IEnumerable<WorklistFromBioHubHistoryItem>> ListByWorklistItemIdWithDocuments(Guid worklistItemId, CancellationToken cancellationToken)
    {
        return await _dbContext.WorklistFromBioHubHistoryItems
            .Where(l => l.DeletedOn == null && l.WorklistFromBioHubItemId == worklistItemId)
            .Include(l => l.WorklistFromBioHubHistoryItemDocuments)
            .ThenInclude(l => l.Document)
            //.OrderByDescending(l => l.OperationDate)
            .ToArrayAsync(cancellationToken);
    }
}