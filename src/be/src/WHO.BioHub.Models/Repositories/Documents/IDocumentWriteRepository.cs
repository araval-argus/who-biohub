using WHO.BioHub.Shared.Utils;
using WHO.BioHub.Models.Models;
using Microsoft.EntityFrameworkCore.Storage;
using WHO.BioHub.Shared.Enums;

namespace WHO.BioHub.Models.Repositories.Documents;

public interface IDocumentWriteRepository
{
    Task<Errors?> ApproveWorklistFromBioHubItemDocument(Guid worklistFromBioHubItemId, DocumentFileType type, CancellationToken cancellationToken, IDbContextTransaction? transaction = null);
    Task<Errors?> ApproveWorklistToBioHubItemDocument(Guid worklistToBioHubItemId, DocumentFileType type, CancellationToken cancellationToken, IDbContextTransaction? transaction = null);
    Task<Either<Document, Errors>> Create(Document document, CancellationToken cancellationToken, IDbContextTransaction? transaction = null);
    Task<Errors?> Delete(Guid id, CancellationToken cancellationToken, IDbContextTransaction? transaction = null);
    Task<Document> ReadForUpdate(Guid id, CancellationToken cancellationToken);
    Task<Errors?> Update(Document document, CancellationToken cancellationToken, IDbContextTransaction? transaction = null);
}
