using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.SMTA1WorkflowItems;
using WHO.BioHub.Shared.Dto;
using WHO.BioHub.Shared.Enums;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DAL.Repositories;

public class SQLSMTA1WorkflowItemWriteRepository : ISMTA1WorkflowItemWriteRepository
{
    private readonly BioHubDbContext _dbContext;
    private DbSet<SMTA1WorkflowItem> SMTA1WorkflowItems => _dbContext.SMTA1WorkflowItems;
    private DbSet<SMTA1WorkflowItemDocument> SMTA1WorkflowItemDocuments => _dbContext.SMTA1WorkflowItemDocuments;


    public SQLSMTA1WorkflowItemWriteRepository(BioHubDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Either<SMTA1WorkflowItem, Errors>> Create(SMTA1WorkflowItem SMTA1WorkflowItem, CancellationToken cancellationToken, IDbContextTransaction? transaction = null)
    {
        if (transaction != null)
        {
            _dbContext.Database.UseTransaction(transaction.GetDbTransaction());
        }
        await _dbContext.AddAsync(SMTA1WorkflowItem, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return new(SMTA1WorkflowItem);
    }

    public async Task<Errors?> Delete(Guid id, CancellationToken cancellationToken)
    {
        SMTA1WorkflowItem lab = await SMTA1WorkflowItems.FirstOrDefaultAsync(l => l.DeletedOn == null && l.Id == id, cancellationToken);
        lab.DeletedOn = DateTime.Now;
        SMTA1WorkflowItems.Update(lab);

        await _dbContext.SaveChangesAsync(cancellationToken);
        return null;
    }

    public async Task<SMTA1WorkflowItem> ReadForUpdate(Guid id, CancellationToken cancellationToken)
    {
        return await _dbContext.SMTA1WorkflowItems
            .FirstOrDefaultAsync(l => l.DeletedOn == null && l.Id == id, cancellationToken);
    }

    public async Task<Errors?> Update(SMTA1WorkflowItem SMTA1WorkflowItem, CancellationToken cancellationToken, IDbContextTransaction? transaction = null)
    {
        if (transaction != null)
        {
            _dbContext.Database.UseTransaction(transaction.GetDbTransaction());
        }
        SMTA1WorkflowItems.Update(SMTA1WorkflowItem);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return null;
    }

    public async Task<Errors?> LinkDocument(Guid SMTA1WorkflowItemId, Guid? documentId, DocumentFileType type, CancellationToken cancellationToken, bool? isDocumentFile = true, IDbContextTransaction? transaction = null, bool? replaceExistingType = true)
    {
        if (transaction != null)
        {
            _dbContext.Database.UseTransaction(transaction.GetDbTransaction());
        }

        if (replaceExistingType == true)
        {
            var elementToRemove = SMTA1WorkflowItemDocuments.Where(x => x.SMTA1WorkflowItemId == SMTA1WorkflowItemId && x.Type == type && x.IsDocumentFile == isDocumentFile).FirstOrDefault();

            if (elementToRemove != default)
            {
                SMTA1WorkflowItemDocuments.Remove(elementToRemove);
            }
        }

        if (documentId != null)
        {
            var newElement = new SMTA1WorkflowItemDocument();
            newElement.SMTA1WorkflowItemId = SMTA1WorkflowItemId;
            newElement.DocumentId = documentId;
            newElement.Type = type;
            newElement.IsDocumentFile = isDocumentFile;
            await _dbContext.AddAsync(newElement, cancellationToken);
        }
        await _dbContext.SaveChangesAsync(cancellationToken);

        return null;
    }

    public async Task<Errors?> UnlinkDocument(Guid SMTA1WorkflowItemId, Guid? documentId, CancellationToken cancellationToken, bool? isDocumentFile = true, IDbContextTransaction? transaction = null)
    {
        if (transaction != null)
        {
            _dbContext.Database.UseTransaction(transaction.GetDbTransaction());
        }
        var elementToRemove = SMTA1WorkflowItemDocuments.Where(x => x.SMTA1WorkflowItemId == SMTA1WorkflowItemId && x.IsDocumentFile == isDocumentFile && x.DocumentId == documentId).FirstOrDefault();

        if (elementToRemove != default)
        {
            SMTA1WorkflowItemDocuments.Remove(elementToRemove);
        }

        await _dbContext.SaveChangesAsync(cancellationToken);

        return null;
    }

    public async Task<IDbContextTransaction> BeginTransactionAsync()
    {
        return await _dbContext.Database.BeginTransactionAsync();
    }

}
