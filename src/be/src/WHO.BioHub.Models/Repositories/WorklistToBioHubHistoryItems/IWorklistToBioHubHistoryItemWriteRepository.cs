using WHO.BioHub.Shared.Utils;
using WHO.BioHub.Models.Models;
using Microsoft.EntityFrameworkCore.Storage;

namespace WHO.BioHub.Models.Repositories.WorklistToBioHubHistoryItems;

public interface IWorklistToBioHubHistoryItemWriteRepository
{
    Task<Errors?> CopyLinkDocumentFromWorklistToBioHubItem(Guid worklisttobiohubitemId, Guid worklisttobiohubhistoryitemId, CancellationToken cancellationToken, IDbContextTransaction? transaction = null);
    Task<Either<WorklistToBioHubHistoryItem, Errors>> Create(WorklistToBioHubHistoryItem worklisttobiohubhistoryitem, CancellationToken cancellationToken, IDbContextTransaction? transaction = null);
    Task<Errors?> Delete(Guid id, CancellationToken cancellationToken);
    Task<IDbContextTransaction> GetTransactionAsync();
    Task<Errors?> LinkBioHubFacilityFocalPointsFromWorklistToBioHubItem(Guid worklisttobiohubitemId, Guid worklisttobiohubhistoryitemId, CancellationToken cancellationToken, IDbContextTransaction? transaction = null);
    Task<Errors?> LinkBookingFormsFromWorklistToBioHubItem(Guid worklisttobiohubitemId, Guid worklisttobiohubhistoryitemId, CancellationToken cancellationToken, IDbContextTransaction? transaction = null);
    Task<Errors?> LinkFeedbacksFromWorklistToBioHubItem(Guid worklisttobiohubitemId, Guid worklisttobiohubhistoryitemId, CancellationToken cancellationToken, IDbContextTransaction? transaction = null);
    Task<Errors?> LinkLaboratoryFocalPointsFromWorklistToBioHubItem(Guid worklisttobiohubitemId, Guid worklisttobiohubhistoryitemId, CancellationToken cancellationToken, IDbContextTransaction? transaction = null);
    Task<Errors?> LinkMaterialShippingInformationFromWorklistToBioHubItem(Guid worklisttobiohubitemId, Guid worklisttobiohubhistoryitemId, CancellationToken cancellationToken, IDbContextTransaction? transaction = null);
    Task<WorklistToBioHubHistoryItem> ReadForUpdate(Guid id, CancellationToken cancellationToken);
    Task<Errors?> Update(WorklistToBioHubHistoryItem worklisttobiohubhistoryitem, CancellationToken cancellationToken);
}
