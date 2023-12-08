using WHO.BioHub.Models.Models;
using WHO.BioHub.Shared.Enums;

namespace WHO.BioHub.Models.Repositories.DocumentTemplates;

public interface IDocumentTemplateReadRepository
{
    Task<DocumentTemplate> Read(Guid id, CancellationToken cancellationToken);
    Task<IEnumerable<DocumentTemplate>> List(Guid id, CancellationToken cancellationToken);
    Task<List<Guid>> GetIdsForDeleteCheck(IEnumerable<Guid> parentIds, CancellationToken cancellationToken);
    Task<bool> ContainsCurrent(IEnumerable<Guid> ids, CancellationToken cancellationToken);
    Task<bool> IsOtherCurrentPresent(Guid id, DocumentFileType documentTemplateFileType, CancellationToken cancellationToken);
    Task<DocumentTemplate> GetCurrentDocumentTemplateByType(DocumentFileType type, CancellationToken cancellationToken);
    Task<bool> TemplatesPresent(List<DocumentFileType?> typeList, CancellationToken cancellationToken);
    Task<IEnumerable<DocumentTemplate>> ListSMTA(CancellationToken cancellationToken);
    Task<DocumentTemplate> ReadEFormTemplate(Guid id, CancellationToken cancellationToken);
}
