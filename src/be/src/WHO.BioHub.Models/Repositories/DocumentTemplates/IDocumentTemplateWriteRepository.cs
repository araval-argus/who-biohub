using WHO.BioHub.Shared.Utils;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Shared.Enums;

namespace WHO.BioHub.Models.Repositories.DocumentTemplates;

public interface IDocumentTemplateWriteRepository
{
    Task<Either<DocumentTemplate, Errors>> Create(DocumentTemplate documenttemplate, CancellationToken cancellationToken);
    Task<Errors?> Delete(Guid id, CancellationToken cancellationToken);
    Task<Errors?> DeleteRange(IEnumerable<Guid> ids, CancellationToken cancellationToken);
    Task<List<Guid>> GetIdsForDelete(IEnumerable<Guid> parentIds, CancellationToken cancellationToken);
    Task<bool> IsCurrentForUpload(DocumentFileType documentTemplateFileType, CancellationToken cancellationToken);
    Task<DocumentTemplate> ReadForUpdate(Guid id, CancellationToken cancellationToken);
    Task<Errors?> SetOffOtherCurrents(Guid id, DocumentFileType documentTemplateFileType, CancellationToken cancellationToken);
    Task<Errors?> Update(DocumentTemplate documenttemplate, CancellationToken cancellationToken);
}
