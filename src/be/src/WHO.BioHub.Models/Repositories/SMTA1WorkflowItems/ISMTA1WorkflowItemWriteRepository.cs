using Microsoft.EntityFrameworkCore.Storage;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Shared.Enums;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.Models.Repositories.SMTA1WorkflowItems
{
    public interface ISMTA1WorkflowItemWriteRepository
    {
        Task<IDbContextTransaction> BeginTransactionAsync();
        Task<Either<SMTA1WorkflowItem, Errors>> Create(SMTA1WorkflowItem worklisttobiohubitem, CancellationToken cancellationToken, IDbContextTransaction transaction = null);
        Task<Errors?> Delete(Guid id, CancellationToken cancellationToken);
        Task<Errors?> LinkDocument(Guid worklisttobiohubitemId, Guid? documentId, DocumentFileType type, CancellationToken cancellationToken, bool? isDocumentFile = true, IDbContextTransaction transaction = null, bool? replaceExistingType = true);
        Task<SMTA1WorkflowItem> ReadForUpdate(Guid id, CancellationToken cancellationToken);
        Task<Errors?> UnlinkDocument(Guid worklisttobiohubitemId, Guid? documentId, CancellationToken cancellationToken, bool? isDocumentFile = true, IDbContextTransaction transaction = null);
        Task<Errors?> Update(SMTA1WorkflowItem worklisttobiohubitem, CancellationToken cancellationToken, IDbContextTransaction transaction = null);
    }
}