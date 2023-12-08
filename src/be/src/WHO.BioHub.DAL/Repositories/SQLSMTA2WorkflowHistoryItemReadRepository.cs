using Microsoft.EntityFrameworkCore;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.SMTA2WorkflowHistoryItems;
using WHO.BioHub.Shared.Enums;

namespace WHO.BioHub.DAL.Repositories;

public class SQLSMTA2WorkflowHistoryItemReadRepository : ISMTA2WorkflowHistoryItemReadRepository
{
    private readonly BioHubDbContext _dbContext;

    public SQLSMTA2WorkflowHistoryItemReadRepository(BioHubDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<SMTA2WorkflowHistoryItem>> List(CancellationToken cancellationToken)
    {
        return await _dbContext.SMTA2WorkflowHistoryItems
            .Where(l => l.DeletedOn == null)
            .ToArrayAsync(cancellationToken);
    }

    public async Task<SMTA2WorkflowHistoryItem> Read(Guid id, CancellationToken cancellationToken)
    {
        return await _dbContext.SMTA2WorkflowHistoryItems
            .AsNoTracking()
            .FirstOrDefaultAsync(l => l.DeletedOn == null && l.Id == id, cancellationToken);
    }

    public async Task<SMTA2WorkflowHistoryItem> ReadByIdAndStatus(Guid id, SMTA2WorkflowStatus status, CancellationToken cancellationToken)
    {
        return await _dbContext.SMTA2WorkflowHistoryItems
            .AsNoTracking()
            .FirstOrDefaultAsync(l => l.DeletedOn == null && l.Id == id && l.Status == status, cancellationToken);
    }

    public async Task<IEnumerable<SMTA2WorkflowHistoryItem>> ListByWorklistItemIdWithExtraInfo(Guid worklistItemId, CancellationToken cancellationToken)
    {
        return await _dbContext.SMTA2WorkflowHistoryItems
            .Include(x => x.Laboratory)
            .Include(x => x.LastOperationUser)
            .ThenInclude(x => x.Role)
            .Include(x => x.SMTA2WorkflowHistoryItemDocuments)
            .ThenInclude(x => x.Document)
            .Where(l => l.DeletedOn == null && l.SMTA2WorkflowItemId == worklistItemId)
            .AsNoTracking()
            .ToArrayAsync(cancellationToken);
    }

    public async Task<SMTA2WorkflowHistoryItem> ReadByIdWithDocuments(Guid id, CancellationToken cancellationToken)
    {
        return await _dbContext.SMTA2WorkflowHistoryItems
            .AsNoTracking()
            .Include(l => l.SMTA2WorkflowHistoryItemDocuments)
            .ThenInclude(l => l.Document)
            .FirstOrDefaultAsync(l => l.DeletedOn == null && l.Id == id, cancellationToken);
    }

    public async Task<IEnumerable<SMTA2WorkflowHistoryItem>> ListByWorklistItemIdWithDocuments(Guid worklistItemId, CancellationToken cancellationToken)
    {
        return await _dbContext.SMTA2WorkflowHistoryItems
            .Where(l => l.DeletedOn == null && l.SMTA2WorkflowItemId == worklistItemId)
            .Include(l => l.SMTA2WorkflowHistoryItemDocuments)
            .ThenInclude(l => l.Document)
            //.OrderByDescending(l => l.OperationDate)
            .ToArrayAsync(cancellationToken);
    }
}