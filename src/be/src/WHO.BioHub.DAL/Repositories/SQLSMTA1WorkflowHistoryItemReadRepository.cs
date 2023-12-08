using Microsoft.EntityFrameworkCore;
using WHO.BioHub.DAL.Repositories.SMTA1WorkflowHistoryItems;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Shared.Enums;

namespace WHO.BioHub.DAL.Repositories;

public class SQLSMTA1WorkflowHistoryItemReadRepository : ISMTA1WorkflowHistoryItemReadRepository
{
    private readonly BioHubDbContext _dbContext;

    public SQLSMTA1WorkflowHistoryItemReadRepository(BioHubDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<SMTA1WorkflowHistoryItem>> List(CancellationToken cancellationToken)
    {
        return await _dbContext.SMTA1WorkflowHistoryItems
            .Where(l => l.DeletedOn == null)
            .ToArrayAsync(cancellationToken);
    }

    public async Task<SMTA1WorkflowHistoryItem> Read(Guid id, CancellationToken cancellationToken)
    {
        return await _dbContext.SMTA1WorkflowHistoryItems
            .AsNoTracking()
            .FirstOrDefaultAsync(l => l.DeletedOn == null && l.Id == id, cancellationToken);
    }

    public async Task<SMTA1WorkflowHistoryItem> ReadByIdAndStatus(Guid id, SMTA1WorkflowStatus status, CancellationToken cancellationToken)
    {
        return await _dbContext.SMTA1WorkflowHistoryItems
            .AsNoTracking()
            .FirstOrDefaultAsync(l => l.DeletedOn == null && l.Id == id && l.Status == status, cancellationToken);
    }

    public async Task<IEnumerable<SMTA1WorkflowHistoryItem>> ListByWorklistItemIdWithExtraInfo(Guid worklistItemId, CancellationToken cancellationToken)
    {
        return await _dbContext.SMTA1WorkflowHistoryItems
            .Include(x => x.Laboratory)
            .Include(x => x.LastOperationUser)
            .ThenInclude(x => x.Role)
            .Include(x => x.SMTA1WorkflowHistoryItemDocuments)
            .ThenInclude(x => x.Document)
            .Where(l => l.DeletedOn == null && l.SMTA1WorkflowItemId == worklistItemId)
            .AsNoTracking()
            .ToArrayAsync(cancellationToken);
    }

    public async Task<SMTA1WorkflowHistoryItem> ReadByIdWithDocuments(Guid id, CancellationToken cancellationToken)
    {
        return await _dbContext.SMTA1WorkflowHistoryItems
            .AsNoTracking()
            .Include(l => l.SMTA1WorkflowHistoryItemDocuments)
            .ThenInclude(l => l.Document)
            .FirstOrDefaultAsync(l => l.DeletedOn == null && l.Id == id, cancellationToken);
    }

    public async Task<IEnumerable<SMTA1WorkflowHistoryItem>> ListByWorklistItemIdWithDocuments(Guid worklistItemId, CancellationToken cancellationToken)
    {
        return await _dbContext.SMTA1WorkflowHistoryItems
            .Where(l => l.DeletedOn == null && l.SMTA1WorkflowItemId == worklistItemId)
            .Include(l => l.SMTA1WorkflowHistoryItemDocuments)
            .ThenInclude(l => l.Document)
            //.OrderByDescending(l => l.OperationDate)
            .ToArrayAsync(cancellationToken);
    }
}