using Microsoft.EntityFrameworkCore;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Shared.Utils;
using Microsoft.EntityFrameworkCore.Storage;
using WHO.BioHub.DAL.Repositories.SMTA1WorkflowHistoryItems;

namespace WHO.BioHub.DAL.Repositories;

public class SQLSMTA1WorkflowHistoryItemWriteRepository : ISMTA1WorkflowHistoryItemWriteRepository
{
    private readonly BioHubDbContext _dbContext;
    private DbSet<SMTA1WorkflowHistoryItem> SMTA1WorkflowHistoryItems => _dbContext.SMTA1WorkflowHistoryItems;
    private DbSet<SMTA1WorkflowHistoryItemDocument> SMTA1WorkflowHistoryItemDocuments => _dbContext.SMTA1WorkflowHistoryItemDocuments;
    private DbSet<SMTA1WorkflowItemDocument> SMTA1WorkflowItemDocuments => _dbContext.SMTA1WorkflowItemDocuments;


    public SQLSMTA1WorkflowHistoryItemWriteRepository(BioHubDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Either<SMTA1WorkflowHistoryItem, Errors>> Create(SMTA1WorkflowHistoryItem worklisttobiohubhistoryitem, CancellationToken cancellationToken, IDbContextTransaction? transaction = null)
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
        SMTA1WorkflowHistoryItem lab = await SMTA1WorkflowHistoryItems.FirstOrDefaultAsync(l => l.DeletedOn == null && l.Id == id, cancellationToken);
        lab.DeletedOn = DateTime.Now;
        SMTA1WorkflowHistoryItems.Update(lab);

        await _dbContext.SaveChangesAsync(cancellationToken);
        return null;
    }

    public async Task<SMTA1WorkflowHistoryItem> ReadForUpdate(Guid id, CancellationToken cancellationToken)
    {
        return await _dbContext.SMTA1WorkflowHistoryItems
            .FirstOrDefaultAsync(l => l.DeletedOn == null && l.Id == id, cancellationToken);
    }

    public async Task<Errors?> Update(SMTA1WorkflowHistoryItem worklisttobiohubhistoryitem, CancellationToken cancellationToken)
    {
        SMTA1WorkflowHistoryItems.Update(worklisttobiohubhistoryitem);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return null;
    }

    public async Task<Errors?> CopyLinkDocumentFromSMTA1WorkflowItem(Guid worklisttobiohubitemId, Guid worklisttobiohubhistoryitemId, CancellationToken cancellationToken, IDbContextTransaction? transaction = null)
    {
        if (transaction != null)
        {
            _dbContext.Database.UseTransaction(transaction.GetDbTransaction());
        }
        var documentsToCopy = SMTA1WorkflowItemDocuments.Where(x => x.SMTA1WorkflowItemId == worklisttobiohubitemId);

        if (documentsToCopy.Any())
        {
            var newHistoryElements = new List<SMTA1WorkflowHistoryItemDocument>();
            foreach (var documentToCopy in documentsToCopy)
            {
                var newHistoryElement = new SMTA1WorkflowHistoryItemDocument();
                newHistoryElement.SMTA1WorkflowHistoryItemId = worklisttobiohubhistoryitemId;
                newHistoryElement.DocumentId = documentToCopy.DocumentId;
                newHistoryElement.IsDocumentFile = documentToCopy.IsDocumentFile;
                newHistoryElements.Add(newHistoryElement);

            }
            SMTA1WorkflowHistoryItemDocuments.AddRange(newHistoryElements);
            await _dbContext.SaveChangesAsync(cancellationToken);

        }

        return null;
    }

    public async Task<IDbContextTransaction> GetTransactionAsync()
    {
        return await _dbContext.Database.BeginTransactionAsync();
    }

}