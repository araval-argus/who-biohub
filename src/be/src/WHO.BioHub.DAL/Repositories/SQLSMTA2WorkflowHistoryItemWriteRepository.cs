using Microsoft.EntityFrameworkCore;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Shared.Utils;
using Microsoft.EntityFrameworkCore.Storage;
using WHO.BioHub.Models.Repositories.SMTA2WorkflowHistoryItems;

namespace WHO.BioHub.DAL.Repositories;

public class SQLSMTA2WorkflowHistoryItemWriteRepository : ISMTA2WorkflowHistoryItemWriteRepository
{
    private readonly BioHubDbContext _dbContext;
    private DbSet<SMTA2WorkflowHistoryItem> SMTA2WorkflowHistoryItems => _dbContext.SMTA2WorkflowHistoryItems;
    private DbSet<SMTA2WorkflowHistoryItemDocument> SMTA2WorkflowHistoryItemDocuments => _dbContext.SMTA2WorkflowHistoryItemDocuments;
    private DbSet<SMTA2WorkflowItemDocument> SMTA2WorkflowItemDocuments => _dbContext.SMTA2WorkflowItemDocuments;


    public SQLSMTA2WorkflowHistoryItemWriteRepository(BioHubDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Either<SMTA2WorkflowHistoryItem, Errors>> Create(SMTA2WorkflowHistoryItem worklisttobiohubhistoryitem, CancellationToken cancellationToken, IDbContextTransaction? transaction = null)
    {
        if (transaction != null)
        {
            _dbContext.Database.UseTransaction(transaction.GetDbTransaction());
        }
        await _dbContext.AddAsync(worklisttobiohubhistoryitem, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return new(worklisttobiohubhistoryitem);
    }

    public async Task<Errors?> Delete(Guid id, CancellationToken cancellationToken)
    {
        SMTA2WorkflowHistoryItem lab = await SMTA2WorkflowHistoryItems.FirstOrDefaultAsync(l => l.DeletedOn == null && l.Id == id, cancellationToken);
        lab.DeletedOn = DateTime.Now;
        SMTA2WorkflowHistoryItems.Update(lab);

        await _dbContext.SaveChangesAsync(cancellationToken);
        return null;
    }

    public async Task<SMTA2WorkflowHistoryItem> ReadForUpdate(Guid id, CancellationToken cancellationToken)
    {
        return await _dbContext.SMTA2WorkflowHistoryItems
            .FirstOrDefaultAsync(l => l.DeletedOn == null && l.Id == id, cancellationToken);
    }

    public async Task<Errors?> Update(SMTA2WorkflowHistoryItem worklisttobiohubhistoryitem, CancellationToken cancellationToken)
    {
        SMTA2WorkflowHistoryItems.Update(worklisttobiohubhistoryitem);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return null;
    }

    public async Task<Errors?> CopyLinkDocumentFromSMTA2WorkflowItem(Guid worklisttobiohubitemId, Guid worklisttobiohubhistoryitemId, CancellationToken cancellationToken, IDbContextTransaction? transaction = null)
    {
        if (transaction != null)
        {
            _dbContext.Database.UseTransaction(transaction.GetDbTransaction());
        }
        var documentsToCopy = SMTA2WorkflowItemDocuments.Where(x => x.SMTA2WorkflowItemId == worklisttobiohubitemId);

        if (documentsToCopy.Any())
        {
            var newHistoryElements = new List<SMTA2WorkflowHistoryItemDocument>();
            foreach (var documentToCopy in documentsToCopy)
            {
                var newHistoryElement = new SMTA2WorkflowHistoryItemDocument();
                newHistoryElement.SMTA2WorkflowHistoryItemId = worklisttobiohubhistoryitemId;
                newHistoryElement.DocumentId = documentToCopy.DocumentId;
                newHistoryElement.IsDocumentFile = documentToCopy.IsDocumentFile;
                newHistoryElements.Add(newHistoryElement);

            }
            SMTA2WorkflowHistoryItemDocuments.AddRange(newHistoryElements);
            await _dbContext.SaveChangesAsync(cancellationToken);

        }

        return null;
    }

    public async Task<IDbContextTransaction> GetTransactionAsync()
    {
        return await _dbContext.Database.BeginTransactionAsync();
    }

}