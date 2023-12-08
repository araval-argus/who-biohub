using WHO.BioHub.Shared.Utils;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Shared.Enums;
using Microsoft.EntityFrameworkCore.Storage;
using WHO.BioHub.Shared.Dto;

namespace WHO.BioHub.Models.Repositories.WorklistToBioHubItems;

public interface IWorklistToBioHubItemWriteRepository
{
    Task<Either<WorklistToBioHubItem, Errors>> Create(WorklistToBioHubItem worklisttobiohubitem, CancellationToken cancellationToken, IDbContextTransaction? transaction = null);
    Task<Errors?> Delete(Guid id, CancellationToken cancellationToken);
    Task<IDbContextTransaction> BeginTransactionAsync();
    Task<Errors?> LinkDocument(Guid worklisttobiohubitemId, Guid? documentId, DocumentFileType type, CancellationToken cancellationToken, bool? isDocumentFile = true, IDbContextTransaction? transaction = null, bool? replaceExistingType = true);
    Task<WorklistToBioHubItem> ReadForUpdate(Guid id, CancellationToken cancellationToken);
    Task<Errors?> Update(WorklistToBioHubItem worklisttobiohubitem, CancellationToken cancellationToken, IDbContextTransaction? transaction = null);
    Task<Errors?> LinkMaterialShippingInformation(Guid worklisttobiohubitemId, IEnumerable<MaterialShippingInformationDto>? materialShippingInformations, CancellationToken cancellationToken, IDbContextTransaction? transaction = null);
    Task<Errors?> LinkLaboratoryFocalPoints(Guid worklisttobiohubitemId, IEnumerable<WorklistItemUserDto>? worklistToBioHubItemLaboratoryFocalPoints, CancellationToken cancellationToken, IDbContextTransaction? transaction = null);
    Task<Errors?> LinkBookingForm(Guid worklisttobiohubitemId, IEnumerable<BookingFormOfSMTADto>? bookingForms, CancellationToken cancellationToken, IDbContextTransaction? transaction = null);
    Task<Errors?> LinkBookingFormPickupUsers(Guid bookingFormId, IEnumerable<WorklistItemUserDto>? bookingFormPickupUsers, CancellationToken cancellationToken, IDbContextTransaction? transaction = null);
    Task<Errors?> UnlinkDocument(Guid worklisttobiohubitemId, Guid? documentId, CancellationToken cancellationToken, bool? isDocumentFile = true, IDbContextTransaction? transaction = null);
    Task<Errors?> AddFeedback(Guid worklisttobiohubitemId, FeedbackDto? feedback, CancellationToken cancellationToken, IDbContextTransaction? transaction = null);
    Task<Errors?> UpdateBookingFormDeliveryProperties(Guid worklisttobiohubitemId, IEnumerable<BookingFormOfSMTADto>? bookingForms, CancellationToken cancellationToken, IDbContextTransaction? transaction = null);
    Task<Errors?> LinkMaterials(Guid worklisttobiohubitemId, IEnumerable<WorklistToBioHubItemMaterialDto>? worklistToBioHubItemMaterialsDto, CancellationToken cancellationToken, IDbContextTransaction? transaction = null);
    Task<Errors?> LinkBioHubFacilityFocalPoints(Guid worklisttobiohubitemId, IEnumerable<WorklistItemUserDto>? worklistToBioHubItemBioHubFacilityFocalPoints, CancellationToken cancellationToken, IDbContextTransaction? transaction = null);
    Task<Errors?> CreateBMEPPMaterialFromWorklistToBioHubItem(Guid worklisttobiohubitemId, DateTime startingDate, CancellationToken cancellationToken, IDbContextTransaction? transaction = null);
}
