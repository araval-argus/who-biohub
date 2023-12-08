using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.Documents;
using WHO.BioHub.Shared.Enums;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DAL.Repositories;

public class SQLDocumentWriteRepository : IDocumentWriteRepository
{
    private readonly BioHubDbContext _dbContext;
    private DbSet<Document> Documents => _dbContext.Documents;

    public SQLDocumentWriteRepository(BioHubDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Either<Document, Errors>> Create(Document document, CancellationToken cancellationToken, IDbContextTransaction? transaction = null)
    {
        if (transaction != null)
        {
            _dbContext.Database.UseTransaction(transaction.GetDbTransaction());
        }
        await _dbContext.AddAsync(document, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return new(document);
    }

    public async Task<Errors?> Delete(Guid id, CancellationToken cancellationToken, IDbContextTransaction? transaction = null)
    {
        if (transaction != null)
        {
            _dbContext.Database.UseTransaction(transaction.GetDbTransaction());
        }
        Document lab = await Documents.FirstOrDefaultAsync(l => l.DeletedOn == null && l.Id == id, cancellationToken);
        lab.DeletedOn = DateTime.Now;
        Documents.Update(lab);

        await _dbContext.SaveChangesAsync(cancellationToken);
        return null;
    }

    public async Task<Document> ReadForUpdate(Guid id, CancellationToken cancellationToken)
    {
        return await _dbContext.Documents
            .FirstOrDefaultAsync(l => l.DeletedOn == null && l.Id == id, cancellationToken);
    }

    public async Task<Errors?> Update(Document document, CancellationToken cancellationToken, IDbContextTransaction? transaction = null)
    {
        if (transaction != null)
        {
            _dbContext.Database.UseTransaction(transaction.GetDbTransaction());
        }
        Documents.Update(document);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return null;
    }


    public async Task<Errors?> ApproveWorklistToBioHubItemDocument(Guid worklistToBioHubItemId, DocumentFileType type, CancellationToken cancellationToken, IDbContextTransaction? transaction = null)
    {
        if (transaction != null)
        {
            _dbContext.Database.UseTransaction(transaction.GetDbTransaction());
        }

        var document = await _dbContext.Documents
            .Where(x => x.WorklistToBioHubItemDocuments.Select(x => x.WorklistToBioHubItemId).Contains(worklistToBioHubItemId))
            .Where(x => x.WorklistToBioHubItemDocuments.Select(x => x.Type).Contains(type))
            .FirstOrDefaultAsync(cancellationToken);

        if (document != null)
        {
            document.Approved = true;
            Documents.Update(document);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        return null;
    }

    public async Task<Errors?> ApproveWorklistFromBioHubItemDocument(Guid worklistFromBioHubItemId, DocumentFileType type, CancellationToken cancellationToken, IDbContextTransaction? transaction = null)
    {
        if (transaction != null)
        {
            _dbContext.Database.UseTransaction(transaction.GetDbTransaction());
        }

        var document = await _dbContext.Documents
            .Where(x => x.WorklistFromBioHubItemDocuments.Select(x => x.WorklistFromBioHubItemId).Contains(worklistFromBioHubItemId))
            .Where(x => x.WorklistFromBioHubItemDocuments.Select(x => x.Type).Contains(type))
            .FirstOrDefaultAsync(cancellationToken);

        if (document != null)
        {
            document.Approved = true;
            Documents.Update(document);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        return null;
    }

}