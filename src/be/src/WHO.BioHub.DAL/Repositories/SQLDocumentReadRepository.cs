using Microsoft.EntityFrameworkCore;
using System.Linq;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.Documents;
using WHO.BioHub.Shared.Enums;

namespace WHO.BioHub.DAL.Repositories;

public class SQLDocumentReadRepository : IDocumentReadRepository
{
    private readonly BioHubDbContext _dbContext;

    public SQLDocumentReadRepository(BioHubDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<Document>> List(CancellationToken cancellationToken)
    {
        return await _dbContext.Documents
            .AsNoTracking()            
            .Include(x => x.Laboratory)
            .Include(x => x.BioHubFacility)
            .Include(x => x.WorklistFromBioHubItemDocuments)
            //.ThenInclude(x => x.WorklistFromBioHubItem)
            .Include(x => x.WorklistToBioHubItemDocuments)
            //.ThenInclude(x => x.WorklistToBioHubItem)            
            .Include(x => x.SMTA1WorkflowItemDocuments)           
            .Include(x => x.SMTA2WorkflowItemDocuments)            
            .Include(x => x.UploadedBy)
            .Where(l => l.DeletedOn == null)        
            .AsSplitQuery()
            .ToArrayAsync(cancellationToken);
    }

    public async Task<IEnumerable<Document>> ListByLaboratoryId(Guid laboratoryId, CancellationToken cancellationToken)
    {
        return await _dbContext.Documents
            .AsNoTracking()
            .AsSplitQuery()
            .Include(x => x.Laboratory)
            .Include(x => x.WorklistFromBioHubItemDocuments)
            .ThenInclude(x => x.WorklistFromBioHubItem)
            .Include(x => x.WorklistToBioHubItemDocuments)
            .ThenInclude(x => x.WorklistToBioHubItem)            
            .Include(x => x.SMTA1WorkflowItemDocuments)           
            .Include(x => x.SMTA2WorkflowItemDocuments)         
            .Include(x => x.UploadedBy)
            .Where(l => l.DeletedOn == null && l.LaboratoryId == laboratoryId)
            .ToArrayAsync(cancellationToken);
    }

    public async Task<IEnumerable<Document>> ListByBioHubFacilityId(Guid bioHubFacilityId, CancellationToken cancellationToken)
    {
        return await _dbContext.Documents
            .AsNoTracking()
            .AsSplitQuery()
            .Include(x => x.BioHubFacility)
            .Include(x => x.WorklistFromBioHubItemDocuments)
            .ThenInclude(x => x.WorklistFromBioHubItem)
            .Include(x => x.WorklistToBioHubItemDocuments)
            .ThenInclude(x => x.WorklistToBioHubItem)          
            .Include(x => x.SMTA1WorkflowItemDocuments)
            .Include(x => x.SMTA2WorkflowItemDocuments)
            .Include(x => x.UploadedBy)
            .Where(l => l.DeletedOn == null && l.BioHubFacilityId == bioHubFacilityId)
            .ToArrayAsync(cancellationToken);
    }

    public async Task<Document> Read(Guid id, CancellationToken cancellationToken)
    {
        return await _dbContext.Documents
            .AsNoTracking()
            .FirstOrDefaultAsync(l => l.DeletedOn == null && l.Id == id, cancellationToken);
    }

    public async Task<Document> ReadWithSMTAInfo(Guid id, CancellationToken cancellationToken)
    {
        return await _dbContext.Documents
            .Include(x => x.SMTA1WorkflowItemDocuments)
            .ThenInclude(x => x.SMTA1WorkflowItem)
            .Include(x => x.SMTA2WorkflowItemDocuments)
            .ThenInclude(x => x.SMTA2WorkflowItem)
            .AsNoTracking()
            .AsSplitQuery()
            .FirstOrDefaultAsync(l => l.DeletedOn == null && l.Id == id, cancellationToken);
    }


    public async Task<Document> ReadForDocumentMenu(Guid id, CancellationToken cancellationToken)
    {
        return await _dbContext.Documents
            .AsNoTracking()
            .Include(x => x.WorklistFromBioHubItemDocuments)
            .ThenInclude(x => x.WorklistFromBioHubItem)
            .Include(x => x.WorklistToBioHubItemDocuments)
            .ThenInclude(x => x.WorklistToBioHubItem)
            .AsSplitQuery()
            .FirstOrDefaultAsync(l => l.DeletedOn == null && l.Id == id, cancellationToken);
    }

    public async Task<Document> ReadByLaboratoryIdForDocumentMenu(Guid id, Guid laboratoryId, CancellationToken cancellationToken)
    {
        return await _dbContext.Documents
            .AsNoTracking()
            .Include(x => x.WorklistFromBioHubItemDocuments)
            .ThenInclude(x => x.WorklistFromBioHubItem)
            .Include(x => x.WorklistToBioHubItemDocuments)
            .ThenInclude(x => x.WorklistToBioHubItem)
            .AsSplitQuery()
            .FirstOrDefaultAsync(l => l.DeletedOn == null && l.Id == id && l.LaboratoryId == laboratoryId, cancellationToken);
    }

    public async Task<Document> ReadByBioHubFacilityIdForDocumentMenu(Guid id, Guid bioHubFacilityId, CancellationToken cancellationToken)
    {
        return await _dbContext.Documents
            .AsNoTracking()
            .Include(x => x.WorklistFromBioHubItemDocuments)
            .ThenInclude(x => x.WorklistFromBioHubItem)
            .Include(x => x.WorklistToBioHubItemDocuments)
            .ThenInclude(x => x.WorklistToBioHubItem)
            .AsSplitQuery()
            .FirstOrDefaultAsync(l => l.DeletedOn == null && l.Id == id && l.BioHubFacilityId == bioHubFacilityId, cancellationToken);
    }

    public async Task<bool> IsDocumentSignedByLaboratoryId(Guid laboratoryId, DocumentFileType type, CancellationToken cancellationToken)
    {
        var query = _dbContext.Documents
            .AsNoTracking()
            .Where(l => l.DeletedOn == null && l.OriginalDocumentTemplate.DeletedOn == null && l.LaboratoryId == laboratoryId && l.Type == type && l.Approved == true && l.OriginalDocumentTemplate.Current == true);


        return await query
          .AnyAsync(cancellationToken);
    }

    public async Task<bool> CanNewSMTARequestBeStarted(Guid laboratoryId, DocumentFileType type, CancellationToken cancellationToken)
    {
        var documentAlreadySigned = await IsDocumentSignedByLaboratoryId(laboratoryId, type, cancellationToken);

        if (!documentAlreadySigned)
        {
            if (type == DocumentFileType.SMTA1)
            {
                var SMTA1flowAlreadyStarted = await _dbContext.SMTA1WorkflowItems
                .Where(l => l.DeletedOn == null)
                .Where(l => l.LaboratoryId == laboratoryId)
                .Where(l => l.Status < SMTA1WorkflowStatus.SMTA1WorkflowComplete)
                .AnyAsync(cancellationToken);

                return !SMTA1flowAlreadyStarted;
            }
            else if (type == DocumentFileType.SMTA2)
            {
                var SMTA2flowAlreadyStarted = await _dbContext.SMTA2WorkflowItems
                .Where(l => l.DeletedOn == null)
                .Where(l => l.LaboratoryId == laboratoryId)
                .Where(l => l.Status < SMTA2WorkflowStatus.SMTA2WorkflowComplete)
                .AnyAsync(cancellationToken);

                return !SMTA2flowAlreadyStarted;
            }

            return false;
        }

        return !documentAlreadySigned;
    }

    public async Task<Document?> GetCurrentDocument(Guid laboratoryId, DocumentFileType type, CancellationToken cancellationToken)
    {
        var query = _dbContext.Documents
            .Include(x => x.SMTA1WorkflowItemDocuments)
            .ThenInclude(x => x.SMTA1WorkflowItem)
            .Include(x => x.SMTA2WorkflowItemDocuments)
            .ThenInclude(x => x.SMTA2WorkflowItem)            
            .AsNoTracking()
            .AsSplitQuery()
            .Where(l => l.DeletedOn == null && l.OriginalDocumentTemplate.DeletedOn == null &&  l.LaboratoryId == laboratoryId && l.Type == type && l.OriginalDocumentTemplate.Current == true)
            .OrderByDescending(x => x.CreationDate);

        return await query
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<IEnumerable<Document>> ListSignedSMTA(Guid? laboratoryId, Guid? bioHubFacilityId, CancellationToken cancellationToken)
    {
        var query = _dbContext.Documents
            .Include(x => x.SMTA1WorkflowItemDocuments)
            .ThenInclude(x => x.SMTA1WorkflowItem)
            .Include(x => x.SMTA2WorkflowItemDocuments)
            .ThenInclude(x => x.SMTA2WorkflowItem)
            .AsNoTracking()
            .AsSplitQuery()
            .Where(l => l.DeletedOn == null)
            .Where(l => l.Approved == true)
            .Where(l => l.IsDocumentFile == true)
            .Where(l => l.Type == DocumentFileType.SMTA1 || l.Type == DocumentFileType.SMTA2);

        if (laboratoryId != null)
        {
            query = query
                .Where(l => l.LaboratoryId == laboratoryId);
        }

        if (bioHubFacilityId != null)
        {
            query = query
                .Where(l => l.BioHubFacilityId == bioHubFacilityId);
        }


        return await query
            .ToArrayAsync(cancellationToken);
    }    
}