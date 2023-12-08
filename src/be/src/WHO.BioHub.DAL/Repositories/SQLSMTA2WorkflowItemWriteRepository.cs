using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.SMTA2WorkflowItems;
using WHO.BioHub.Shared.Dto;
using WHO.BioHub.Shared.Enums;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DAL.Repositories;

public class SQLSMTA2WorkflowItemWriteRepository : ISMTA2WorkflowItemWriteRepository
{
    private readonly BioHubDbContext _dbContext;
    private DbSet<SMTA2WorkflowItem> SMTA2WorkflowItems => _dbContext.SMTA2WorkflowItems;
    private DbSet<SMTA2WorkflowItemDocument> SMTA2WorkflowItemDocuments => _dbContext.SMTA2WorkflowItemDocuments;


    public SQLSMTA2WorkflowItemWriteRepository(BioHubDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Either<SMTA2WorkflowItem, Errors>> Create(SMTA2WorkflowItem SMTA2WorkflowItem, CancellationToken cancellationToken, IDbContextTransaction? transaction = null)
    {
        if (transaction != null)
        {
            _dbContext.Database.UseTransaction(transaction.GetDbTransaction());
        }
        await _dbContext.AddAsync(SMTA2WorkflowItem, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return new(SMTA2WorkflowItem);
    }

    public async Task<Errors?> Delete(Guid id, CancellationToken cancellationToken)
    {
        SMTA2WorkflowItem lab = await SMTA2WorkflowItems.FirstOrDefaultAsync(l => l.DeletedOn == null && l.Id == id, cancellationToken);
        lab.DeletedOn = DateTime.Now;
        SMTA2WorkflowItems.Update(lab);

        await _dbContext.SaveChangesAsync(cancellationToken);
        return null;
    }

    public async Task<SMTA2WorkflowItem> ReadForUpdate(Guid id, CancellationToken cancellationToken)
    {
        return await _dbContext.SMTA2WorkflowItems
            .FirstOrDefaultAsync(l => l.DeletedOn == null && l.Id == id, cancellationToken);
    }

    public async Task<Errors?> Update(SMTA2WorkflowItem SMTA2WorkflowItem, CancellationToken cancellationToken, IDbContextTransaction? transaction = null)
    {
        if (transaction != null)
        {
            _dbContext.Database.UseTransaction(transaction.GetDbTransaction());
        }
        SMTA2WorkflowItems.Update(SMTA2WorkflowItem);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return null;
    }

    public async Task<Errors?> LinkDocument(Guid SMTA2WorkflowItemId, Guid? documentId, DocumentFileType type, CancellationToken cancellationToken, bool? isDocumentFile = true, IDbContextTransaction? transaction = null, bool? replaceExistingType = true)
    {
        if (transaction != null)
        {
            _dbContext.Database.UseTransaction(transaction.GetDbTransaction());
        }

        if (replaceExistingType == true)
        {
            var elementToRemove = SMTA2WorkflowItemDocuments.Where(x => x.SMTA2WorkflowItemId == SMTA2WorkflowItemId && x.Type == type && x.IsDocumentFile == isDocumentFile).FirstOrDefault();

            if (elementToRemove != default)
            {
                SMTA2WorkflowItemDocuments.Remove(elementToRemove);
            }
        }

        if (documentId != null)
        {
            var newElement = new SMTA2WorkflowItemDocument();
            newElement.SMTA2WorkflowItemId = SMTA2WorkflowItemId;
            newElement.DocumentId = documentId;
            newElement.Type = type;
            newElement.IsDocumentFile = isDocumentFile;
            await _dbContext.AddAsync(newElement, cancellationToken);
        }
        await _dbContext.SaveChangesAsync(cancellationToken);

        return null;
    }

    public async Task<Errors?> UnlinkDocument(Guid SMTA2WorkflowItemId, Guid? documentId, CancellationToken cancellationToken, bool? isDocumentFile = true, IDbContextTransaction? transaction = null)
    {
        if (transaction != null)
        {
            _dbContext.Database.UseTransaction(transaction.GetDbTransaction());
        }
        var elementToRemove = SMTA2WorkflowItemDocuments.Where(x => x.SMTA2WorkflowItemId == SMTA2WorkflowItemId && x.IsDocumentFile == isDocumentFile && x.DocumentId == documentId).FirstOrDefault();

        if (elementToRemove != default)
        {
            SMTA2WorkflowItemDocuments.Remove(elementToRemove);
        }

        await _dbContext.SaveChangesAsync(cancellationToken);

        return null;
    }

    public async Task<IDbContextTransaction> BeginTransactionAsync()
    {
        return await _dbContext.Database.BeginTransactionAsync();
    }

}
