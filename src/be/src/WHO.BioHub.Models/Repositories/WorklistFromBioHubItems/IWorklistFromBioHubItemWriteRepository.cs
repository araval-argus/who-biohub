using WHO.BioHub.Shared.Utils;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Shared.Enums;
using Microsoft.EntityFrameworkCore.Storage;
using WHO.BioHub.Shared.Dto;

namespace WHO.BioHub.Models.Repositories.WorklistFromBioHubItems;

public interface IWorklistFromBioHubItemWriteRepository
{
    Task<Either<WorklistFromBioHubItem, Errors>> Create(WorklistFromBioHubItem worklisttobiohubitem, CancellationToken cancellationToken, IDbContextTransaction? transaction = null);
    Task<Errors?> Delete(Guid id, CancellationToken cancellationToken);
    Task<IDbContextTransaction> BeginTransactionAsync();
    Task<Errors?> LinkDocument(Guid worklisttobiohubitemId, Guid? documentId, DocumentFileType type, CancellationToken cancellationToken, bool? isDocumentFile = true, IDbContextTransaction? transaction = null, bool? replaceExistingType = true);
    Task<WorklistFromBioHubItem> ReadForUpdate(Guid id, CancellationToken cancellationToken);
    Task<Errors?> Update(WorklistFromBioHubItem worklisttobiohubitem, CancellationToken cancellationToken, IDbContextTransaction? transaction = null);
    //Task<Errors?> LinkMaterialShippingInformation(Guid worklisttobiohubitemId, IEnumerable<MaterialShippingInformationDto>? materialShippingInformations, CancellationToken cancellationToken, IDbContextTransaction? transaction = null);
    //Task<Errors?> LinkLaboratoryFocalPoints(Guid worklisttobiohubitemId, IEnumerable<WorklistItemUserDto>? worklistFromBioHubItemLaboratoryFocalPoints, CancellationToken cancellationToken, IDbContextTransaction? transaction = null);
    Task<Errors?> LinkBookingForm(Guid worklisttobiohubitemId, IEnumerable<BookingFormOfSMTADto>? bookingForms, CancellationToken cancellationToken, IDbContextTransaction? transaction = null);
    Task<Errors?> LinkBookingFormPickupUsers(Guid bookingFormId, IEnumerable<WorklistItemUserDto>? bookingFormPickupUsers, CancellationToken cancellationToken, IDbContextTransaction? transaction = null);
    Task<Errors?> UnlinkDocument(Guid worklisttobiohubitemId, Guid? documentId, CancellationToken cancellationToken, bool? isDocumentFile = true, IDbContextTransaction? transaction = null);
    Task<Errors?> AddFeedback(Guid worklisttobiohubitemId, FeedbackDto? feedback, CancellationToken cancellationToken, IDbContextTransaction? transaction = null);
    Task<Errors?> UpdateBookingFormDeliveryProperties(Guid worklisttobiohubitemId, IEnumerable<BookingFormOfSMTADto>? bookingForms, CancellationToken cancellationToken, IDbContextTransaction? transaction = null);
    Task<Errors?> LinkLaboratoryFocalPoints(Guid worklisttobiohubitemId, IEnumerable<WorklistItemUserDto>? worklistFromBioHubItemLaboratoryFocalPoints, CancellationToken cancellationToken, IDbContextTransaction? transaction = null);
    Task<Errors?> LinkMaterials(Guid worklistfrombiohubitemId, IEnumerable<WorklistFromBioHubItemMaterialDto>? worklistFromBioHubItemMaterialsDto, CancellationToken cancellationToken, IDbContextTransaction? transaction = null);
    Task<Errors?> LinkAnnex2OfSMTA2Conditions(Guid worklistfrombiohubitemId, IEnumerable<WorklistFromBioHubItemAnnex2OfSMTA2ConditionDto>? worklistFromBioHubItemAnnex2OfSMTA2ConditionsDto, CancellationToken cancellationToken, IDbContextTransaction? transaction = null);
    Task<Errors?> LinkBiosafetyChecklistOfSMTA2(Guid worklistfrombiohubitemId, IEnumerable<WorklistFromBioHubItemBiosafetyChecklistOfSMTA2Dto>? worklistFromBioHubItemBiosafetyChecklistOfSMTA2sDto, CancellationToken cancellationToken, IDbContextTransaction? transaction = null);
    Task<Errors?> AddBiosafetyChecklistComment(Guid worklistfrombiohubitemId, BiosafetyChecklistThreadCommentDto? comment, CancellationToken cancellationToken, IDbContextTransaction? transaction = null);
    Task<Errors?> UpdateMaterialsCurrentNumberOfVials(Guid worklistfrombiohubitemId, CancellationToken cancellationToken, IDbContextTransaction? transaction = null);
}
