using WHO.BioHub.Shared.Utils;
using WHO.BioHub.Models.Models;
using Microsoft.EntityFrameworkCore.Storage;

namespace WHO.BioHub.Models.Repositories.WorklistFromBioHubHistoryItems;

public interface IWorklistFromBioHubHistoryItemWriteRepository
{
    Task<Errors?> CopyLinkDocumentFromWorklistFromBioHubItem(Guid worklisttobiohubitemId, Guid worklisttobiohubhistoryitemId, CancellationToken cancellationToken, IDbContextTransaction? transaction = null);
    Task<Either<WorklistFromBioHubHistoryItem, Errors>> Create(WorklistFromBioHubHistoryItem worklisttobiohubhistoryitem, CancellationToken cancellationToken, IDbContextTransaction? transaction = null);
    Task<Errors?> Delete(Guid id, CancellationToken cancellationToken);
    Task<IDbContextTransaction> GetTransactionAsync();
    Task<Errors?> LinkAnnex2OfSMTA2ConditionsFromWorklistFromBioHubItem(Guid worklistfrombiohubitemId, Guid worklistfrombiohubhistoryitemId, CancellationToken cancellationToken, IDbContextTransaction? transaction = null);
    Task<Errors?> LinkBiosafetyChecklistCommentsFromWorklistFromBioHubItem(Guid worklistfrombiohubitemId, Guid worklistfrombiohubhistoryitemId, CancellationToken cancellationToken, IDbContextTransaction? transaction = null);
    Task<Errors?> LinkBiosafetyChecklistOfSMTA2sFromWorklistFromBioHubItem(Guid worklistfrombiohubitemId, Guid worklistfrombiohubhistoryitemId, CancellationToken cancellationToken, IDbContextTransaction? transaction = null);
    Task<Errors?> LinkBookingFormsFromWorklistFromBioHubItem(Guid worklisttobiohubitemId, Guid worklisttobiohubhistoryitemId, CancellationToken cancellationToken, IDbContextTransaction? transaction = null);
    Task<Errors?> LinkFeedbacksFromWorklistFromBioHubItem(Guid worklisttobiohubitemId, Guid worklisttobiohubhistoryitemId, CancellationToken cancellationToken, IDbContextTransaction? transaction = null);
    Task<Errors?> LinkLaboratoryFocalPointsFromWorklistFromBioHubItem(Guid worklisttobiohubitemId, Guid worklisttobiohubhistoryitemId, CancellationToken cancellationToken, IDbContextTransaction? transaction = null);
    Task<Errors?> LinkMaterialsFromWorklistFromBioHubItem(Guid worklistfrombiohubitemId, Guid worklistfrombiohubhistoryitemId, CancellationToken cancellationToken, IDbContextTransaction? transaction = null);

    //Task<Errors?> LinkMaterialShippingInformationFromWorklistFromBioHubItem(Guid worklisttobiohubitemId, Guid worklisttobiohubhistoryitemId, CancellationToken cancellationToken, IDbContextTransaction? transaction = null);
    Task<WorklistFromBioHubHistoryItem> ReadForUpdate(Guid id, CancellationToken cancellationToken);
    Task<Errors?> Update(WorklistFromBioHubHistoryItem worklisttobiohubhistoryitem, CancellationToken cancellationToken);
}
