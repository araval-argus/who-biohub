using Microsoft.EntityFrameworkCore;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.DocumentTemplates;
using WHO.BioHub.Shared.Enums;

namespace WHO.BioHub.DAL.Repositories;

public class SQLDocumentTemplateReadRepository : IDocumentTemplateReadRepository
{
    private readonly BioHubDbContext _dbContext;

    public SQLDocumentTemplateReadRepository(BioHubDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<DocumentTemplate>> List(Guid id, CancellationToken cancellationToken)
    {
        return await _dbContext.DocumentTemplates
            .Include(x => x.UploadedBy)
            .Where(l => l.DeletedOn == null && l.ParentId == id)
            .ToArrayAsync(cancellationToken);
    }

    public async Task<IEnumerable<DocumentTemplate>> ListSMTA(CancellationToken cancellationToken)
    {
        var query = _dbContext.DocumentTemplates
            .Include(x => x.UploadedBy)
            .Where(l => l.DeletedOn == null && l.Type == DocumentTemplateType.File && (l.FileType == DocumentFileType.SMTA1 || l.FileType == DocumentFileType.SMTA2));

        return await query.ToArrayAsync(cancellationToken);
    }

    public async Task<DocumentTemplate> Read(Guid id, CancellationToken cancellationToken)
    {
        return await _dbContext.DocumentTemplates
            .AsNoTracking()
            .FirstOrDefaultAsync(l => l.DeletedOn == null && l.Id == id, cancellationToken);
    }


    public async Task<DocumentTemplate> ReadEFormTemplate(Guid id, CancellationToken cancellationToken)
    {
        return await _dbContext.DocumentTemplates
            .AsNoTracking()
            .FirstOrDefaultAsync(l => l.Id == id, cancellationToken);
    }


    public async Task<bool> IsOtherCurrentPresent(Guid id, DocumentFileType documentTemplateFileType, CancellationToken cancellationToken)
    {
        return await _dbContext.DocumentTemplates
            .AnyAsync(l => l.DeletedOn == null && l.Id != id && l.Type == DocumentTemplateType.File && l.FileType == documentTemplateFileType && l.Current == true, cancellationToken);
    }


    public async Task<List<Guid>> GetIdsForDeleteCheck(IEnumerable<Guid> parentIds, CancellationToken cancellationToken)
    {
        return await _dbContext.DocumentTemplates
            .Where(l => l.DeletedOn == null)
            .Where(l => l.ParentId != null)
            .Where(l => parentIds.Contains(l.ParentId.Value))
            .Select(x => x.Id)
            .ToListAsync(cancellationToken);
    }

    public async Task<bool> ContainsCurrent(IEnumerable<Guid> ids, CancellationToken cancellationToken)
    {
        return await _dbContext.DocumentTemplates
            .AnyAsync(l => l.DeletedOn == null && ids.Contains(l.Id) && l.Type == DocumentTemplateType.File && l.Current == true, cancellationToken);
    }

    public async Task<DocumentTemplate> GetCurrentDocumentTemplateByType(DocumentFileType type, CancellationToken cancellationToken)
    {
        return await _dbContext.DocumentTemplates
            .Include(x => x.UploadedBy)
            .Where(l => l.DeletedOn == null && l.FileType == type && l.Current == true)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<bool> TemplatesPresent(List<DocumentFileType?> typeList, CancellationToken cancellationToken)
    {
        var result = await _dbContext.DocumentTemplates
            .Where(l => l.DeletedOn == null && typeList.Contains(l.FileType) && l.Current == true)
            .ToListAsync(cancellationToken);

        return result.Count() == typeList.Count();
    }
}