using Microsoft.EntityFrameworkCore;
using System.Linq;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.DocumentTemplates;
using WHO.BioHub.Shared.Enums;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DAL.Repositories;

public class SQLDocumentTemplateWriteRepository : IDocumentTemplateWriteRepository
{
    private readonly BioHubDbContext _dbContext;
    private DbSet<DocumentTemplate> DocumentTemplates => _dbContext.DocumentTemplates;

    public SQLDocumentTemplateWriteRepository(BioHubDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Either<DocumentTemplate, Errors>> Create(DocumentTemplate documenttemplate, CancellationToken cancellationToken)
    {
        await _dbContext.AddAsync(documenttemplate, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return new(documenttemplate);
    }

    public async Task<Errors?> Delete(Guid id, CancellationToken cancellationToken)
    {
        DocumentTemplate lab = await DocumentTemplates.FirstOrDefaultAsync(l => l.DeletedOn == null && l.Id == id, cancellationToken);
        lab.DeletedOn = DateTime.Now;
        DocumentTemplates.Update(lab);

        await _dbContext.SaveChangesAsync(cancellationToken);
        return null;
    }

    public async Task<Errors?> DeleteRange(IEnumerable<Guid> ids, CancellationToken cancellationToken)
    {
        List<DocumentTemplate> documentTemplates = await DocumentTemplates.Where(l => l.DeletedOn == null && ids.Contains(l.Id)).ToListAsync(cancellationToken);
        foreach (var documenttemplate in documentTemplates)
        {
            documenttemplate.DeletedOn = DateTime.UtcNow;
        }

        DocumentTemplates.UpdateRange(documentTemplates);

        await _dbContext.SaveChangesAsync(cancellationToken);
        return null;
    }

    public async Task<DocumentTemplate> ReadForUpdate(Guid id, CancellationToken cancellationToken)
    {
        return await _dbContext.DocumentTemplates
            .FirstOrDefaultAsync(l => l.DeletedOn == null && l.Id == id, cancellationToken);
    }

    public async Task<List<Guid>> GetIdsForDelete(IEnumerable<Guid> parentIds, CancellationToken cancellationToken)
    {
        return await _dbContext.DocumentTemplates
            .Where(l => l.DeletedOn == null)
            .Where(l => l.ParentId != null)
            .Where(l => parentIds.Contains(l.ParentId.Value))
            .Select(x => x.Id)
            .ToListAsync(cancellationToken);

    }

    public async Task<Errors?> Update(DocumentTemplate documenttemplate, CancellationToken cancellationToken)
    {
        DocumentTemplates.Update(documenttemplate);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return null;
    }

    public async Task<bool> IsCurrentForUpload(DocumentFileType documentTemplateFileType, CancellationToken cancellationToken)
    {
        return !(await _dbContext.DocumentTemplates
            .AnyAsync(l => l.DeletedOn == null && l.Type == DocumentTemplateType.File && documentTemplateFileType == l.FileType && l.Current == true, cancellationToken));
    }

    public async Task<Errors?> SetOffOtherCurrents(Guid id, DocumentFileType documentTemplateFileType, CancellationToken cancellationToken)
    {
        var toBeSetOff = await _dbContext.DocumentTemplates
            .Where(l => l.DeletedOn == null && l.Id != id && l.Type == DocumentTemplateType.File && l.FileType == documentTemplateFileType && l.Current == true)
            .ToListAsync(cancellationToken);

        foreach (var documentTemplate in toBeSetOff)
        {
            documentTemplate.Current = false;
        }
        DocumentTemplates.UpdateRange(toBeSetOff);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return null;
    }
}