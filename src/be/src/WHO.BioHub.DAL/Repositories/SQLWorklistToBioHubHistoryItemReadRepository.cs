using Microsoft.EntityFrameworkCore;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.WorklistToBioHubHistoryItems;
using WHO.BioHub.Shared.Enums;

namespace WHO.BioHub.DAL.Repositories;

public class SQLWorklistToBioHubHistoryItemReadRepository : IWorklistToBioHubHistoryItemReadRepository
{
    private readonly BioHubDbContext _dbContext;

    public SQLWorklistToBioHubHistoryItemReadRepository(BioHubDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<WorklistToBioHubHistoryItem>> List(CancellationToken cancellationToken)
    {
        return await _dbContext.WorklistToBioHubHistoryItems
            .Where(l => l.DeletedOn == null)
            .ToArrayAsync(cancellationToken);
    }

    public async Task<WorklistToBioHubHistoryItem> Read(Guid id, CancellationToken cancellationToken)
    {
        return await _dbContext.WorklistToBioHubHistoryItems
            .AsNoTracking()
            .FirstOrDefaultAsync(l => l.DeletedOn == null && l.Id == id, cancellationToken);
    }

    public async Task<WorklistToBioHubHistoryItem> ReadByIdAndStatus(Guid id, WorklistToBioHubStatus status, CancellationToken cancellationToken)
    {
        return await _dbContext.WorklistToBioHubHistoryItems
            .AsNoTracking()
            .FirstOrDefaultAsync(l => l.DeletedOn == null && l.Id == id && l.Status == status, cancellationToken);
    }

    public async Task<IEnumerable<WorklistToBioHubHistoryItem>> ListByWorklistItemIdWithExtraInfo(Guid worklistItemId, CancellationToken cancellationToken)
    {
        return await _dbContext.WorklistToBioHubHistoryItems
            .Include(x => x.RequestInitiationFromLaboratory)
            .Include(x => x.RequestInitiationToBioHubFacility)
            .Include(x => x.LastOperationUser)
            .ThenInclude(x => x.Role)
            .Include(x => x.WorklistToBioHubHistoryItemDocuments)
            .ThenInclude(x => x.Document)
            .Where(l => l.DeletedOn == null && l.WorklistToBioHubItemId == worklistItemId)
            .AsNoTracking()
            .ToArrayAsync(cancellationToken);
    }

    public async Task<WorklistToBioHubHistoryItem> ReadByIdWithDocuments(Guid id, CancellationToken cancellationToken)
    {
        return await _dbContext.WorklistToBioHubHistoryItems
            .AsNoTracking()
            .Include(l => l.WorklistToBioHubHistoryItemDocuments)
            .ThenInclude(l => l.Document)
            .FirstOrDefaultAsync(l => l.DeletedOn == null && l.Id == id, cancellationToken);
    }

    public async Task<IEnumerable<WorklistToBioHubHistoryItem>> ListByWorklistItemIdWithDocuments(Guid worklistItemId, CancellationToken cancellationToken)
    {
        return await _dbContext.WorklistToBioHubHistoryItems
            .Where(l => l.DeletedOn == null && l.WorklistToBioHubItemId == worklistItemId)
            .Include(l => l.WorklistToBioHubHistoryItemDocuments)
            .ThenInclude(l => l.Document)
            //.OrderByDescending(l => l.OperationDate)
            .ToArrayAsync(cancellationToken);
    }
}