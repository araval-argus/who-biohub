using WHO.BioHub.Models.Models;
using WHO.BioHub.Shared.Enums;

namespace WHO.BioHub.Models.Repositories.Documents;

public interface IDocumentReadRepository
{
    Task<Document> Read(Guid id, CancellationToken cancellationToken);
    Task<IEnumerable<Document>> List(CancellationToken cancellationToken);
    Task<IEnumerable<Document>> ListByLaboratoryId(Guid laboratoryId, CancellationToken cancellationToken);
    Task<Document> ReadForDocumentMenu(Guid id, CancellationToken cancellationToken);
    Task<Document> ReadByLaboratoryIdForDocumentMenu(Guid id, Guid laboratoryId, CancellationToken cancellationToken);
    Task<bool> IsDocumentSignedByLaboratoryId(Guid laboratoryId, DocumentFileType type, CancellationToken cancellationToken);
    Task<Document?> GetCurrentDocument(Guid laboratoryId, DocumentFileType type, CancellationToken cancellationToken);
    Task<bool> CanNewSMTARequestBeStarted(Guid laboratoryId, DocumentFileType type, CancellationToken cancellationToken);
    Task<IEnumerable<Document>> ListByBioHubFacilityId(Guid bioHubFacilityId, CancellationToken cancellationToken);
    Task<Document> ReadByBioHubFacilityIdForDocumentMenu(Guid id, Guid bioHubFacilityId, CancellationToken cancellationToken);
    Task<Document> ReadWithSMTAInfo(Guid id, CancellationToken cancellationToken);
    Task<IEnumerable<Document>> ListSignedSMTA(Guid? laboratoryId, Guid? bioHubFacilityId, CancellationToken cancellationToken);
}
