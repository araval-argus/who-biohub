using Microsoft.EntityFrameworkCore;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.WorklistToBioHubItems;
using WHO.BioHub.Shared.Dto;
using WHO.BioHub.Shared.Enums;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DAL.Repositories;

public class SQLWorklistToBioHubItemReadRepository : IWorklistToBioHubItemReadRepository
{
    private readonly BioHubDbContext _dbContext;

    public SQLWorklistToBioHubItemReadRepository(BioHubDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<WorklistToBioHubItem>> List(CancellationToken cancellationToken)
    {
        return await _dbContext.WorklistToBioHubItems
            .Include(x => x.RequestInitiationFromLaboratory)
            .ThenInclude(x => x.Country)
            .Include(x => x.RequestInitiationToBioHubFacility)
            .Include(x => x.LastOperationUser)
            .ThenInclude(x => x.Role)
            .Include(x => x.WorklistToBioHubItemDocuments)
            .Where(l => l.DeletedOn == null)
            .Where(l => l.Status < WorklistToBioHubStatus.ShipmentCompleted)
            .ToArrayAsync(cancellationToken);
    }

    public async Task<WorklistToBioHubItem> Read(Guid id, CancellationToken cancellationToken)
    {
        return await _dbContext.WorklistToBioHubItems
            .AsNoTracking()
            .FirstOrDefaultAsync(l => l.DeletedOn == null && l.Id == id, cancellationToken);
    }

    public async Task<int?> GetTotalRequestsOfTheYear(CancellationToken cancellationToken)
    {
        DateTime firstDateOfTheYear = new DateTime(DateTime.UtcNow.Year, 1, 1);

        var total = await _dbContext.WorklistItemUsedReferenceNumbers
            .CountAsync(l => l.DeletedOn == null && l.CreationDate >= firstDateOfTheYear, cancellationToken);

        return total;
    }

    public async Task<WorklistToBioHubItem> ReadByIdAndStatus(Guid id, WorklistToBioHubStatus status, CancellationToken cancellationToken)
    {
        return await _dbContext.WorklistToBioHubItems
            .AsNoTracking()
            .FirstOrDefaultAsync(l => l.DeletedOn == null && l.Id == id && l.Status == status, cancellationToken);
    }

    public async Task<WorklistToBioHubItem> ReadByIdWithExtraInfo(Guid id, CancellationToken cancellationToken)
    {
        var status = (await _dbContext.WorklistToBioHubItems.FirstOrDefaultAsync(l => l.DeletedOn == null && l.Id == id, cancellationToken))?.Status;

        if (status == null)
        {
            return null;
        }

        var query = _dbContext.WorklistToBioHubItems.AsNoTracking();


        switch (status)
        {

            case WorklistToBioHubStatus.SubmitAnnex2OfSMTA1:
            case WorklistToBioHubStatus.WaitingForAnnex2OfSMTA1SECsApproval:
                query = query
                    .Include(l => l.RequestInitiationFromLaboratory)
                    .ThenInclude(x => x.Country)
                    .Include(x => x.RequestInitiationToBioHubFacility)
                    .ThenInclude(x => x.Country)
                    .Include(x => x.LastOperationUser)
                    .ThenInclude(x => x.Role)
                    .Include(x => x.WorklistToBioHubItemDocuments)
                    .ThenInclude(x => x.Document)
                    .ThenInclude(x => x.UploadedBy)
                    .Include(x => x.MaterialShippingInformations)
                    .ThenInclude(x => x.MaterialClinicalDetails)
                    .Include(x => x.MaterialShippingInformations)
                    .ThenInclude(x => x.MaterialLaboratoryAnalysisInformation)
                    .ThenInclude(x => x.CollectedSpecimenTypes)
                    .ThenInclude(x => x.SpecimenType)
                    .Include(x => x.WorklistToBioHubItemLaboratoryFocalPoints)
                    .ThenInclude(x => x.User)
                    .ThenInclude(x => x.Laboratory)
                    .ThenInclude(x => x.Country);
                break;

            case WorklistToBioHubStatus.SubmitBookingFormOfSMTA1:

                query = query
                    .Include(x => x.WorklistToBioHubItemDocuments)
                    .ThenInclude(x => x.Document)
                    .ThenInclude(x => x.UploadedBy)
                    .Include(x => x.WorklistToBioHubItemLaboratoryFocalPoints)
                    .ThenInclude(x => x.User)
                    .Include(l => l.RequestInitiationFromLaboratory)
                    .ThenInclude(l => l.Country)
                    .Include(x => x.RequestInitiationToBioHubFacility)
                    .ThenInclude(l => l.Country)
                    .Include(x => x.RequestInitiationToBioHubFacility)
                    .ThenInclude(l => l.Users)
                    .ThenInclude(l => l.Role)
                    .Include(x => x.LastOperationUser)
                    .ThenInclude(x => x.Role)
                    .Include(x => x.BookingForms)
                    .ThenInclude(x => x.TransportCategory)
                    .Include(x => x.BookingForms)
                    .ThenInclude(x => x.BookingFormPickupUsers)
                    .ThenInclude(x => x.User)
                    .ThenInclude(x => x.Laboratory)
                    .ThenInclude(x => x.Country)
                    .Include(x => x.MaterialShippingInformations);
                break;

            case WorklistToBioHubStatus.WaitForBookingFormSMTA1OPSApproval:
                query = query
                    .Include(x => x.WorklistToBioHubItemDocuments)
                    .ThenInclude(x => x.Document)
                    .ThenInclude(x => x.UploadedBy)
                    .Include(x => x.WorklistToBioHubItemLaboratoryFocalPoints)
                    .ThenInclude(x => x.User)
                    .Include(l => l.RequestInitiationFromLaboratory)
                    .ThenInclude(l => l.Country)
                    .Include(x => x.RequestInitiationToBioHubFacility)
                    .ThenInclude(l => l.Country)
                    .Include(x => x.RequestInitiationToBioHubFacility)
                    .ThenInclude(l => l.Users)
                    .ThenInclude(l => l.Role)
                    .Include(x => x.LastOperationUser)
                    .ThenInclude(x => x.Role)
                    .Include(x => x.BookingForms)
                    .ThenInclude(x => x.TransportCategory)
                    .Include(x => x.BookingForms)
                    .ThenInclude(x => x.BookingFormPickupUsers)
                    .ThenInclude(x => x.User)
                    .ThenInclude(x => x.Laboratory)
                    .ThenInclude(x => x.Country)
                    .Include(x => x.BookingForms)
                    .ThenInclude(x => x.BookingFormCourierUsers)
                    .ThenInclude(x => x.User)
                    .ThenInclude(x => x.Courier)
                    .ThenInclude(x => x.Country)
                    .Include(x => x.MaterialShippingInformations);
                break;

            case WorklistToBioHubStatus.SubmitSMTA1ShipmentDocuments:
                query = query
                    .Include(x => x.LastOperationUser)
                    .ThenInclude(x => x.Role)
                    .Include(l => l.RequestInitiationFromLaboratory)
                    .Include(x => x.RequestInitiationToBioHubFacility)
                    .Include(x => x.WorklistToBioHubItemDocuments)
                    .ThenInclude(x => x.Document)
                    .ThenInclude(x => x.UploadedBy);
                break;

            case WorklistToBioHubStatus.WaitForPickUpCompleted:
                query = query
                    .Include(x => x.LastOperationUser)
                    .ThenInclude(x => x.Role)
                    .Include(l => l.RequestInitiationFromLaboratory)
                    .Include(x => x.RequestInitiationToBioHubFacility)
                    .Include(x => x.WorklistToBioHubItemDocuments)
                    .ThenInclude(x => x.Document)
                    .ThenInclude(x => x.UploadedBy)
                    .Include(x => x.BookingForms)
                    .ThenInclude(x => x.TransportCategory);
                break;

            case WorklistToBioHubStatus.WaitForDeliveryCompleted:
                query = query
                    .Include(x => x.LastOperationUser)
                    .ThenInclude(x => x.Role)
                    .Include(l => l.RequestInitiationFromLaboratory)
                    .Include(x => x.RequestInitiationToBioHubFacility)
                    .Include(x => x.WorklistToBioHubItemDocuments)
                    .ThenInclude(x => x.Document)
                    .ThenInclude(x => x.UploadedBy)
                    .Include(x => x.BookingForms)
                    .ThenInclude(x => x.TransportCategory)
                    .Include(x => x.BookingForms)
                    .ThenInclude(x => x.TransportMode);
                break;

            case WorklistToBioHubStatus.WaitForArrivalConditionCheck:
                query = query
                    .Include(x => x.LastOperationUser)
                    .ThenInclude(x => x.Role)
                    .Include(l => l.RequestInitiationFromLaboratory)
                    .Include(x => x.RequestInitiationToBioHubFacility)
                    .Include(x => x.WorklistToBioHubItemDocuments)
                    .ThenInclude(x => x.Document)
                    .ThenInclude(x => x.UploadedBy)
                    .Include(x => x.MaterialShippingInformations)
                    .ThenInclude(x => x.MaterialClinicalDetails)
                    .Include(x => x.MaterialShippingInformations)
                    .ThenInclude(x => x.MaterialLaboratoryAnalysisInformation)
                    .ThenInclude(x => x.CollectedSpecimenTypes)
                    .ThenInclude(x => x.SpecimenType);
                break;

            case WorklistToBioHubStatus.WaitForCommentBHFSendFeedback:
            case WorklistToBioHubStatus.WaitForFinalApproval:
                query = query
                    .Include(x => x.LastOperationUser)
                    .ThenInclude(x => x.Role)
                    .Include(x => x.WorklistToBioHubItemDocuments)
                    .ThenInclude(x => x.Document)
                    .ThenInclude(x => x.UploadedBy)
                    .Include(l => l.RequestInitiationFromLaboratory)
                    .Include(x => x.RequestInitiationToBioHubFacility)
                    .Include(x => x.MaterialShippingInformations)
                    .ThenInclude(x => x.MaterialClinicalDetails)
                    .Include(x => x.MaterialShippingInformations)
                    .ThenInclude(x => x.MaterialLaboratoryAnalysisInformation)
                    .ThenInclude(x => x.CollectedSpecimenTypes)
                    .ThenInclude(x => x.SpecimenType)
                    .Include(x => x.WorklistToBioHubItemFeedbacks)
                    .ThenInclude(x => x.PostedBy);
                break;

            case WorklistToBioHubStatus.ShipmentCompleted:
                query = query
                    .Include(x => x.LastOperationUser)
                    .ThenInclude(x => x.Role)
                    .Include(x => x.WorklistToBioHubItemMaterials)
                    .ThenInclude(x => x.Material)
                    .Include(x => x.WorklistToBioHubItemDocuments)
                    .ThenInclude(x => x.Document)
                    .ThenInclude(x => x.UploadedBy)
                    .Include(l => l.RequestInitiationFromLaboratory)
                    .Include(x => x.RequestInitiationToBioHubFacility)
                    .Include(x => x.MaterialShippingInformations)
                    .ThenInclude(x => x.MaterialClinicalDetails)
                    .Include(x => x.MaterialShippingInformations)
                    .ThenInclude(x => x.MaterialLaboratoryAnalysisInformation)
                    .ThenInclude(x => x.CollectedSpecimenTypes)
                    .ThenInclude(x => x.SpecimenType);
                break;
        }

        query = query.AsSplitQuery();

        return await query.FirstOrDefaultAsync(l => l.DeletedOn == null && l.Id == id, cancellationToken);

    }



    public async Task<WorkflowEmailInfoDto> ReadInfoForEmail(Guid id, WorklistToBioHubStatus status, WorklistToBioHubStatus previousStatus, CancellationToken cancellationToken)
    {
        WorklistToBioHubItem? entity;
        WorklistToBioHubHistoryItem? entityHistory;
        WorkflowEmailInfoDto emailInfo = new WorkflowEmailInfoDto();
        WorklistToBioHubItemFeedback feedback;


        var query = _dbContext.WorklistToBioHubItems.AsNoTracking().AsSplitQuery();
        var queryHistory = _dbContext.WorklistToBioHubHistoryItems.AsNoTracking().AsSplitQuery();

        query = query
            .Include(x => x.LastOperationUser)
            .Include(x => x.RequestInitiationToBioHubFacility)
            .ThenInclude(x => x.Country)
            .Include(x => x.RequestInitiationFromLaboratory)
            .ThenInclude(x => x.Country);

        queryHistory = queryHistory
            .Include(x => x.LastOperationUser)
            .Include(x => x.RequestInitiationToBioHubFacility)
            .ThenInclude(x => x.Country)
            .Include(x => x.RequestInitiationFromLaboratory)
            .ThenInclude(x => x.Country);

        switch (status)
        {

            case WorklistToBioHubStatus.SubmitAnnex2OfSMTA1:

                if (previousStatus == WorklistToBioHubStatus.WaitingForAnnex2OfSMTA1SECsApproval)
                {

                    entityHistory = await (queryHistory.Where(l => l.DeletedOn == null && l.WorklistToBioHubItemId == id && l.Status == WorklistToBioHubStatus.WaitingForAnnex2OfSMTA1SECsApproval && l.LastSubmissionApproved == true).OrderByDescending(x => x.CreationDate).FirstOrDefaultAsync(cancellationToken));
                    emailInfo.Id = id;
                    emailInfo.LaboratoryName = entityHistory.RequestInitiationFromLaboratory.Name;
                    emailInfo.LaboratoryCountry = entityHistory.RequestInitiationFromLaboratory.Country.Name;
                    emailInfo.LaboratoryUserFirstName = entityHistory.LastOperationUser.FirstName;
                    emailInfo.LaboratoryUserLastName = entityHistory.LastOperationUser.LastName;
                    emailInfo.LaboratoryUserEmail = entityHistory.LastOperationUser.Email;
                    emailInfo.BioHubFacilityName = entityHistory.RequestInitiationToBioHubFacility != null ? entityHistory.RequestInitiationToBioHubFacility.Name : string.Empty;

                }

                else
                {

                    entityHistory = await (queryHistory.Where(l => l.DeletedOn == null && l.WorklistToBioHubItemId == id && l.LastSubmissionApproved == true).OrderByDescending(x => x.CreationDate).FirstOrDefaultAsync(cancellationToken));
                    emailInfo.Id = id;
                    if (entityHistory != null)
                    {
                        emailInfo.LaboratoryName = entityHistory.RequestInitiationFromLaboratory.Name;
                        emailInfo.LaboratoryCountry = entityHistory.RequestInitiationFromLaboratory.Country.Name;
                        emailInfo.LaboratoryUserFirstName = entityHistory.LastOperationUser.FirstName;
                        emailInfo.LaboratoryUserLastName = entityHistory.LastOperationUser.LastName;
                        emailInfo.LaboratoryUserEmail = entityHistory.LastOperationUser.Email;
                        emailInfo.BioHubFacilityName = entityHistory.RequestInitiationToBioHubFacility != null ? entityHistory.RequestInitiationToBioHubFacility.Name : String.Empty;
                    }
                }
                break;

            case WorklistToBioHubStatus.WaitingForAnnex2OfSMTA1SECsApproval:

                entity = await (query.Where(l => l.DeletedOn == null && l.Id == id).FirstOrDefaultAsync(cancellationToken));

                emailInfo.Id = id;
                emailInfo.LaboratoryName = entity.RequestInitiationFromLaboratory.Name;
                emailInfo.LaboratoryCountry = entity.RequestInitiationFromLaboratory.Country.Name;
                emailInfo.LaboratoryUserFirstName = entity.LastOperationUser.FirstName;
                emailInfo.LaboratoryUserLastName = entity.LastOperationUser.LastName;
                emailInfo.LaboratoryUserEmail = entity.LastOperationUser.Email;
                emailInfo.BioHubFacilityName = entity.RequestInitiationToBioHubFacility != null ? entity.RequestInitiationToBioHubFacility.Name : string.Empty;

                break;

            case WorklistToBioHubStatus.SubmitBookingFormOfSMTA1:

                if (previousStatus == WorklistToBioHubStatus.WaitForBookingFormSMTA1OPSApproval)
                {


                    entityHistory = await (queryHistory.Where(l => l.DeletedOn == null && l.WorklistToBioHubItemId == id && l.Status == WorklistToBioHubStatus.WaitForBookingFormSMTA1OPSApproval && l.LastSubmissionApproved == true).OrderByDescending(x => x.CreationDate).FirstOrDefaultAsync(cancellationToken));

                    emailInfo.Id = id;
                    emailInfo.LaboratoryName = entityHistory.RequestInitiationFromLaboratory.Name;
                    emailInfo.LaboratoryCountry = entityHistory.RequestInitiationFromLaboratory.Country.Name;
                    emailInfo.LaboratoryUserFirstName = entityHistory.LastOperationUser.FirstName;
                    emailInfo.LaboratoryUserLastName = entityHistory.LastOperationUser.LastName;
                    emailInfo.LaboratoryUserEmail = entityHistory.LastOperationUser.Email;
                    emailInfo.BioHubFacilityName = entityHistory.RequestInitiationToBioHubFacility != null ? entityHistory.RequestInitiationToBioHubFacility.Name : string.Empty;
                }

                else
                {
                    queryHistory = queryHistory
                    .Include(x => x.WorklistToBioHubHistoryItemDocuments)
                    .ThenInclude(x => x.Document);

                    entityHistory = await (queryHistory.Where(l => l.DeletedOn == null && l.WorklistToBioHubItemId == id && l.Status == WorklistToBioHubStatus.WaitingForAnnex2OfSMTA1SECsApproval && l.LastSubmissionApproved == true).OrderByDescending(x => x.CreationDate).FirstOrDefaultAsync(cancellationToken));

                    entity = await (query.Where(l => l.DeletedOn == null && l.Id == id && l.LastSubmissionApproved == true).OrderByDescending(x => x.CreationDate).FirstOrDefaultAsync(cancellationToken));


                    emailInfo.Id = id;
                    emailInfo.LaboratoryName = entity.RequestInitiationFromLaboratory.Name;
                    emailInfo.LaboratoryCountry = entity.RequestInitiationFromLaboratory.Country.Name;
                    emailInfo.LaboratoryUserFirstName = entityHistory.LastOperationUser.FirstName;
                    emailInfo.LaboratoryUserLastName = entityHistory.LastOperationUser.LastName;
                    emailInfo.LaboratoryUserEmail = entityHistory.LastOperationUser.Email;
                    emailInfo.BioHubFacilityName = entity.RequestInitiationToBioHubFacility != null ? entity.RequestInitiationToBioHubFacility.Name : string.Empty;

                    //# 54317
                    //emailInfo.LaboratoryUserSignature = entityHistory.WorklistToBioHubHistoryItemDocuments.Select(x => x.Document).Where(x => x.Type == DocumentFileType.Annex2OfSMTA1).FirstOrDefault()?.Base64String ?? String.Empty;
                    emailInfo.LaboratoryUserSignature = entity.Annex2OfSMTA1SignatureText ?? String.Empty;
                    //////////////////////
                }
                break;

            case WorklistToBioHubStatus.WaitForBookingFormSMTA1OPSApproval:

                entity = await (query.Where(l => l.DeletedOn == null && l.Id == id).FirstOrDefaultAsync(cancellationToken));

                emailInfo.Id = id;
                emailInfo.LaboratoryName = entity.RequestInitiationFromLaboratory.Name;
                emailInfo.LaboratoryCountry = entity.RequestInitiationFromLaboratory.Country.Name;
                emailInfo.LaboratoryUserFirstName = entity.LastOperationUser.FirstName;
                emailInfo.LaboratoryUserLastName = entity.LastOperationUser.LastName;
                emailInfo.LaboratoryUserEmail = entity.LastOperationUser.Email;
                emailInfo.BioHubFacilityName = entity.RequestInitiationToBioHubFacility.Name;
                break;


            case WorklistToBioHubStatus.SubmitSMTA1ShipmentDocuments:


                entityHistory = await (queryHistory.Where(l => l.DeletedOn == null && l.WorklistToBioHubItemId == id && l.Status == WorklistToBioHubStatus.WaitForBookingFormSMTA1OPSApproval && l.LastSubmissionApproved == true).OrderByDescending(x => x.CreationDate).FirstOrDefaultAsync(cancellationToken));

                query = query
                    .Include(x => x.WorklistToBioHubItemBioHubFacilityFocalPoints)
                    .ThenInclude(x => x.User)
                    .Include(x => x.BookingForms)
                    .ThenInclude(x => x.TransportCategory)
                    .Include(x => x.BookingForms)
                    .ThenInclude(x => x.Courier)
                    .Include(x => x.BookingForms)
                    .ThenInclude(x => x.BookingFormPickupUsers)
                    .ThenInclude(x => x.User)
                    .ThenInclude(x => x.Laboratory)
                    .ThenInclude(x => x.Country)
                    .Include(x => x.BookingForms)
                    .ThenInclude(x => x.BookingFormCourierUsers)
                    .ThenInclude(x => x.User)
                    .Include(x => x.WorklistToBioHubItemBioHubFacilityFocalPoints)
                    .ThenInclude(x => x.User)
                    .ThenInclude(x => x.Laboratory)
                    .ThenInclude(x => x.Country)
                    .Include(x => x.WorklistToBioHubItemLaboratoryFocalPoints)
                    .ThenInclude(x => x.User)
                    .ThenInclude(x => x.Laboratory)
                    .ThenInclude(x => x.Country)
                    .Include(x => x.WorklistToBioHubItemDocuments)
                    .ThenInclude(x => x.Document);

                entity = await (query.Where(l => l.DeletedOn == null && l.Id == id).FirstOrDefaultAsync(cancellationToken));

                emailInfo.Id = id;
                emailInfo.LaboratoryName = entityHistory.RequestInitiationFromLaboratory.Name;
                emailInfo.LaboratoryAddress = entityHistory.RequestInitiationFromLaboratory.Address;
                emailInfo.LaboratoryCountry = entityHistory.RequestInitiationFromLaboratory.Country.Name;
                emailInfo.LaboratoryUserFirstName = entityHistory.LastOperationUser.FirstName;
                emailInfo.LaboratoryUserLastName = entityHistory.LastOperationUser.LastName;
                emailInfo.LaboratoryUserEmail = entityHistory.LastOperationUser.Email;
                emailInfo.LaboratoryUserBusinessPhone = entityHistory.LastOperationUser.BusinessPhone;
                emailInfo.LaboratoryUserMobilePhone = entityHistory.LastOperationUser.MobilePhone;
                emailInfo.LaboratoryUserJobTitle = entityHistory.LastOperationUser.JobTitle;


                //# 54317
                //emailInfo.LaboratoryUserSignature = entity.WorklistToBioHubItemDocuments.Select(x => x.Document).Where(x => x.Type == DocumentFileType.BookingFormOfSMTA1).FirstOrDefault()?.Base64String ?? String.Empty;
                emailInfo.LaboratoryUserSignature = entityHistory.BookingFormOfSMTA1SignatureText ?? String.Empty;
                //////////////////////


                emailInfo.BioHubFacilityName = entity.RequestInitiationToBioHubFacility.Name;
                emailInfo.BioHubFacilityAddress = entity.RequestInitiationToBioHubFacility.Address;
                emailInfo.BioHubFacilityCountry = entity.RequestInitiationToBioHubFacility.Country.Name;
                emailInfo.WHOOperationalFocalPointName = entity.LastOperationUser.FirstName;
                emailInfo.WHOOperationalFocalPointLastname = entity.LastOperationUser.LastName;
                emailInfo.WHOOperationalFocalPointEmail = entity.LastOperationUser.Email;
                emailInfo.WHOOperationalFocalPointRoleId = entity.LastOperationUser.RoleId.GetValueOrDefault();

                emailInfo.WHOAccountNumber = entity.WHODocumentRegistrationNumber;



                emailInfo.BioHubFacilityFocalPoints = new List<ContactUserInfoForEmailDto>();
                foreach (var bioHubFacilityFocalPoint in entity.WorklistToBioHubItemBioHubFacilityFocalPoints)
                {
                    ContactUserInfoForEmailDto contactUserInfoForEmailDto = new ContactUserInfoForEmailDto();
                    contactUserInfoForEmailDto.Name = bioHubFacilityFocalPoint.User.FirstName + " " + bioHubFacilityFocalPoint.User.LastName;
                    contactUserInfoForEmailDto.Email = bioHubFacilityFocalPoint.User.Email;
                    contactUserInfoForEmailDto.Phone = bioHubFacilityFocalPoint.User.BusinessPhone;
                    emailInfo.BioHubFacilityFocalPoints.Add(contactUserInfoForEmailDto);
                }

                emailInfo.LaboratoryFocalPoints = new List<ContactUserInfoForEmailDto>();
                foreach (var laboratoryFocalPoint in entity.WorklistToBioHubItemLaboratoryFocalPoints)
                {
                    ContactUserInfoForEmailDto contactUserInfoForEmailDto = new ContactUserInfoForEmailDto();
                    contactUserInfoForEmailDto.Name = laboratoryFocalPoint.User.FirstName + " " + laboratoryFocalPoint.User.LastName;
                    contactUserInfoForEmailDto.Email = laboratoryFocalPoint.User.Email;
                    contactUserInfoForEmailDto.Phone = laboratoryFocalPoint.User.BusinessPhone;
                    emailInfo.LaboratoryFocalPoints.Add(contactUserInfoForEmailDto);
                }

                emailInfo.BookingForms = new List<BookingFormEmailInfoDto>();

                foreach (var bookingForm in entity.BookingForms)
                {
                    var bookingFormEmailInfoDto = new BookingFormEmailInfoDto();

                    bookingFormEmailInfoDto.CourierEmail = bookingForm.Courier.Email;
                    bookingFormEmailInfoDto.TemperatureTransportCondition = bookingForm.TemperatureTransportCondition.ToString();
                    bookingFormEmailInfoDto.Date = bookingForm.Date;
                    bookingFormEmailInfoDto.RequestDateOfPickup = bookingForm.RequestDateOfPickup;
                    bookingFormEmailInfoDto.EstimateDateOfPickup = bookingForm.EstimateDateOfPickup;
                    bookingFormEmailInfoDto.TransportCategoryName = bookingForm.TransportCategory.Name;
                    bookingFormEmailInfoDto.TransportCategoryDescription = bookingForm.TransportCategory.Description;
                    bookingFormEmailInfoDto.NumberOfInnerPackagingAndSize = bookingForm.NumberOfInnerPackagingAndSize;
                    bookingFormEmailInfoDto.Quantity = bookingForm.TotalNumberOfVials.GetValueOrDefault();
                    bookingFormEmailInfoDto.Amount = bookingForm.TotalAmount.GetValueOrDefault();



                    bookingFormEmailInfoDto.BookingFormCourierUsers = new List<ContactUserInfoForEmailDto>();
                    foreach (var courierUser in bookingForm.BookingFormCourierUsers)
                    {
                        ContactUserInfoForEmailDto contactUserInfoForEmailDto = new ContactUserInfoForEmailDto();
                        contactUserInfoForEmailDto.Name = courierUser.User.FirstName + " " + courierUser.User.LastName;
                        contactUserInfoForEmailDto.Email = courierUser.User.Email;
                        contactUserInfoForEmailDto.Phone = courierUser.User.BusinessPhone;
                        bookingFormEmailInfoDto.BookingFormCourierUsers.Add(contactUserInfoForEmailDto);
                    }

                    bookingFormEmailInfoDto.BookingFormPickupUsers = new List<ContactUserInfoForEmailDto>();
                    foreach (var pickupUser in bookingForm.BookingFormPickupUsers)
                    {
                        ContactUserInfoForEmailDto contactUserInfoForEmailDto = new ContactUserInfoForEmailDto();
                        contactUserInfoForEmailDto.Name = pickupUser.User.FirstName + " " + pickupUser.User.LastName;
                        contactUserInfoForEmailDto.Email = pickupUser.User.Email;
                        contactUserInfoForEmailDto.Phone = pickupUser.User.BusinessPhone;
                        bookingFormEmailInfoDto.BookingFormPickupUsers.Add(contactUserInfoForEmailDto);
                    }

                    bookingFormEmailInfoDto.BookingFormDeliveryUsers = new List<ContactUserInfoForEmailDto>();
                    foreach (var bioHubFacilityFocalPoint in entity.WorklistToBioHubItemBioHubFacilityFocalPoints)
                    {
                        ContactUserInfoForEmailDto contactUserInfoForEmailDto = new ContactUserInfoForEmailDto();
                        contactUserInfoForEmailDto.Name = bioHubFacilityFocalPoint.User.FirstName + " " + bioHubFacilityFocalPoint.User.LastName;
                        contactUserInfoForEmailDto.Email = bioHubFacilityFocalPoint.User.Email;
                        contactUserInfoForEmailDto.Phone = bioHubFacilityFocalPoint.User.BusinessPhone;
                        bookingFormEmailInfoDto.BookingFormDeliveryUsers.Add(contactUserInfoForEmailDto);
                    }



                    emailInfo.BookingForms.Add(bookingFormEmailInfoDto);

                }

                break;

            case WorklistToBioHubStatus.WaitForPickUpCompleted:

                entityHistory = await (queryHistory.Where(l => l.DeletedOn == null && l.WorklistToBioHubItemId == id && l.Status == WorklistToBioHubStatus.WaitForBookingFormSMTA1OPSApproval && l.LastSubmissionApproved == true).OrderByDescending(x => x.CreationDate).FirstOrDefaultAsync(cancellationToken));

                query = query
                    .Include(x => x.WorklistToBioHubItemBioHubFacilityFocalPoints)
                    .ThenInclude(x => x.User)
                    .Include(x => x.BookingForms)
                    .ThenInclude(x => x.TransportCategory)
                    .Include(x => x.BookingForms)
                    .ThenInclude(x => x.Courier)
                    .Include(x => x.BookingForms)
                    .ThenInclude(x => x.BookingFormPickupUsers)
                    .ThenInclude(x => x.User)
                    .ThenInclude(x => x.Laboratory)
                    .ThenInclude(x => x.Country)
                    .Include(x => x.BookingForms)
                    .ThenInclude(x => x.BookingFormCourierUsers)
                    .ThenInclude(x => x.User)
                    .Include(x => x.WorklistToBioHubItemBioHubFacilityFocalPoints)
                    .ThenInclude(x => x.User)
                    .ThenInclude(x => x.Laboratory)
                    .ThenInclude(x => x.Country)
                    .Include(x => x.WorklistToBioHubItemLaboratoryFocalPoints)
                    .ThenInclude(x => x.User)
                    .ThenInclude(x => x.Laboratory)
                    .ThenInclude(x => x.Country)
                    .Include(x => x.WorklistToBioHubItemDocuments)
                    .ThenInclude(x => x.Document);

                entity = await (query.Where(l => l.DeletedOn == null && l.Id == id).FirstOrDefaultAsync(cancellationToken));

                emailInfo.Id = id;
                emailInfo.LaboratoryName = entityHistory.RequestInitiationFromLaboratory.Name;
                emailInfo.LaboratoryAddress = entityHistory.RequestInitiationFromLaboratory.Address;
                emailInfo.LaboratoryCountry = entityHistory.RequestInitiationFromLaboratory.Country.Name;
                emailInfo.LaboratoryUserFirstName = entityHistory.LastOperationUser.FirstName;
                emailInfo.LaboratoryUserLastName = entityHistory.LastOperationUser.LastName;
                emailInfo.LaboratoryUserEmail = entityHistory.LastOperationUser.Email;
                emailInfo.LaboratoryUserBusinessPhone = entityHistory.LastOperationUser.BusinessPhone;
                emailInfo.LaboratoryUserMobilePhone = entityHistory.LastOperationUser.MobilePhone;
                emailInfo.LaboratoryUserJobTitle = entityHistory.LastOperationUser.JobTitle;


                //# 54317
                //emailInfo.LaboratoryUserSignature = entity.WorklistToBioHubItemDocuments.Select(x => x.Document).Where(x => x.Type == DocumentFileType.BookingFormOfSMTA1).FirstOrDefault()?.Base64String ?? String.Empty;
                emailInfo.LaboratoryUserSignature = entityHistory.BookingFormOfSMTA1SignatureText ?? String.Empty;
                //////////////////////


                emailInfo.BioHubFacilityName = entity.RequestInitiationToBioHubFacility.Name;
                emailInfo.BioHubFacilityAddress = entity.RequestInitiationToBioHubFacility.Address;
                emailInfo.BioHubFacilityCountry = entity.RequestInitiationToBioHubFacility.Country.Name;
                emailInfo.WHOOperationalFocalPointName = entity.LastOperationUser.FirstName;
                emailInfo.WHOOperationalFocalPointLastname = entity.LastOperationUser.LastName;
                emailInfo.WHOOperationalFocalPointEmail = entity.LastOperationUser.Email;
                emailInfo.WHOOperationalFocalPointRoleId = entity.LastOperationUser.RoleId.GetValueOrDefault();

                emailInfo.WHOAccountNumber = entity.WHODocumentRegistrationNumber;



                emailInfo.BioHubFacilityFocalPoints = new List<ContactUserInfoForEmailDto>();
                foreach (var bioHubFacilityFocalPoint in entity.WorklistToBioHubItemBioHubFacilityFocalPoints)
                {
                    ContactUserInfoForEmailDto contactUserInfoForEmailDto = new ContactUserInfoForEmailDto();
                    contactUserInfoForEmailDto.Name = bioHubFacilityFocalPoint.User.FirstName + " " + bioHubFacilityFocalPoint.User.LastName;
                    contactUserInfoForEmailDto.Email = bioHubFacilityFocalPoint.User.Email;
                    contactUserInfoForEmailDto.Phone = bioHubFacilityFocalPoint.User.BusinessPhone;
                    emailInfo.BioHubFacilityFocalPoints.Add(contactUserInfoForEmailDto);
                }

                emailInfo.LaboratoryFocalPoints = new List<ContactUserInfoForEmailDto>();
                foreach (var laboratoryFocalPoint in entity.WorklistToBioHubItemLaboratoryFocalPoints)
                {
                    ContactUserInfoForEmailDto contactUserInfoForEmailDto = new ContactUserInfoForEmailDto();
                    contactUserInfoForEmailDto.Name = laboratoryFocalPoint.User.FirstName + " " + laboratoryFocalPoint.User.LastName;
                    contactUserInfoForEmailDto.Email = laboratoryFocalPoint.User.Email;
                    contactUserInfoForEmailDto.Phone = laboratoryFocalPoint.User.BusinessPhone;
                    emailInfo.LaboratoryFocalPoints.Add(contactUserInfoForEmailDto);
                }

                emailInfo.BookingForms = new List<BookingFormEmailInfoDto>();

                foreach (var bookingForm in entity.BookingForms)
                {
                    var bookingFormEmailInfoDto = new BookingFormEmailInfoDto();

                    bookingFormEmailInfoDto.CourierEmail = bookingForm.Courier.Email;
                    bookingFormEmailInfoDto.TemperatureTransportCondition = bookingForm.TemperatureTransportCondition.ToString();
                    bookingFormEmailInfoDto.Date = bookingForm.Date;
                    bookingFormEmailInfoDto.RequestDateOfPickup = bookingForm.RequestDateOfPickup;
                    bookingFormEmailInfoDto.EstimateDateOfPickup = bookingForm.EstimateDateOfPickup;
                    bookingFormEmailInfoDto.TransportCategoryName = bookingForm.TransportCategory.Name;
                    bookingFormEmailInfoDto.TransportCategoryDescription = bookingForm.TransportCategory.Description;
                    bookingFormEmailInfoDto.NumberOfInnerPackagingAndSize = bookingForm.NumberOfInnerPackagingAndSize;
                    bookingFormEmailInfoDto.Quantity = bookingForm.TotalNumberOfVials.GetValueOrDefault();
                    bookingFormEmailInfoDto.Amount = bookingForm.TotalAmount.GetValueOrDefault();



                    bookingFormEmailInfoDto.BookingFormCourierUsers = new List<ContactUserInfoForEmailDto>();
                    foreach (var courierUser in bookingForm.BookingFormCourierUsers)
                    {
                        ContactUserInfoForEmailDto contactUserInfoForEmailDto = new ContactUserInfoForEmailDto();
                        contactUserInfoForEmailDto.Name = courierUser.User.FirstName + " " + courierUser.User.LastName;
                        contactUserInfoForEmailDto.Email = courierUser.User.Email;
                        contactUserInfoForEmailDto.Phone = courierUser.User.BusinessPhone;
                        bookingFormEmailInfoDto.BookingFormCourierUsers.Add(contactUserInfoForEmailDto);
                    }

                    bookingFormEmailInfoDto.BookingFormPickupUsers = new List<ContactUserInfoForEmailDto>();
                    foreach (var pickupUser in bookingForm.BookingFormPickupUsers)
                    {
                        ContactUserInfoForEmailDto contactUserInfoForEmailDto = new ContactUserInfoForEmailDto();
                        contactUserInfoForEmailDto.Name = pickupUser.User.FirstName + " " + pickupUser.User.LastName;
                        contactUserInfoForEmailDto.Email = pickupUser.User.Email;
                        contactUserInfoForEmailDto.Phone = pickupUser.User.BusinessPhone;
                        bookingFormEmailInfoDto.BookingFormPickupUsers.Add(contactUserInfoForEmailDto);
                    }

                    bookingFormEmailInfoDto.BookingFormDeliveryUsers = new List<ContactUserInfoForEmailDto>();
                    foreach (var bioHubFacilityFocalPoint in entity.WorklistToBioHubItemBioHubFacilityFocalPoints)
                    {
                        ContactUserInfoForEmailDto contactUserInfoForEmailDto = new ContactUserInfoForEmailDto();
                        contactUserInfoForEmailDto.Name = bioHubFacilityFocalPoint.User.FirstName + " " + bioHubFacilityFocalPoint.User.LastName;
                        contactUserInfoForEmailDto.Email = bioHubFacilityFocalPoint.User.Email;
                        contactUserInfoForEmailDto.Phone = bioHubFacilityFocalPoint.User.BusinessPhone;
                        bookingFormEmailInfoDto.BookingFormDeliveryUsers.Add(contactUserInfoForEmailDto);
                    }



                    emailInfo.BookingForms.Add(bookingFormEmailInfoDto);

                }

                break;


                //query = query
                //  .Include(x => x.WorklistToBioHubItemDocuments)
                //  .ThenInclude(x => x.Document);

                //entity = await (query.Where(l => l.DeletedOn == null && l.Id == id).FirstOrDefaultAsync(cancellationToken));

                //emailInfo.Id = id;
                //emailInfo.LaboratoryName = entity.RequestInitiationFromLaboratory.Name;
                //emailInfo.LaboratoryCountry = entity.RequestInitiationFromLaboratory.Country.Name;
                //emailInfo.LaboratoryUserFirstName = entity.LastOperationUser.FirstName;
                //emailInfo.LaboratoryUserLastName = entity.LastOperationUser.LastName;
                //emailInfo.LaboratoryUserEmail = entity.LastOperationUser.Email;
                //emailInfo.BioHubFacilityName = entity.RequestInitiationToBioHubFacility.Name;

                //emailInfo.ShipmentDocuments = new List<ShipmentDocumentInfoForEmailDto>();

                //var documents = entity.WorklistToBioHubItemDocuments
                //    .Where(x => x.Document.Type == DocumentFileType.ImportPermit ||
                //    x.Document.Type == DocumentFileType.ExportPermit ||
                //    x.Document.Type == DocumentFileType.NonCommercialInvoiceCatA ||
                //    x.Document.Type == DocumentFileType.NonCommercialInvoiceCatB ||
                //    x.Document.Type == DocumentFileType.PackagingList ||
                //    x.Document.Type == DocumentFileType.DangerousGoodsDeclaration ||
                //    x.Document.Type == DocumentFileType.Other);

                //foreach (var document in documents)
                //{
                //    ShipmentDocumentInfoForEmailDto newDocument = new ShipmentDocumentInfoForEmailDto();
                //    newDocument.DocumentName = document.Document.Name;
                //    newDocument.DocumentExtension = document.Document.Extension;
                //    newDocument.DocumentType = document.Document.Type.Value.AttributeName();
                //    emailInfo.ShipmentDocuments.Add(newDocument);

                //}

                //break;


            case WorklistToBioHubStatus.WaitForDeliveryCompleted:
                query = query
                    .Include(x => x.BookingForms)
                    .ThenInclude(x => x.TransportCategory);


                entity = await (query.Where(l => l.DeletedOn == null && l.Id == id).FirstOrDefaultAsync(cancellationToken));

                emailInfo.Id = id;
                emailInfo.LaboratoryName = entity.RequestInitiationFromLaboratory.Name;
                emailInfo.LaboratoryCountry = entity.RequestInitiationFromLaboratory.Country.Name;
                emailInfo.BioHubFacilityName = entity.RequestInitiationToBioHubFacility.Name;

                emailInfo.BookingForms = new List<BookingFormEmailInfoDto>();

                foreach (var bookingForm in entity.BookingForms)
                {
                    var bookingFormEmailInfoDto = new BookingFormEmailInfoDto();

                    bookingFormEmailInfoDto.DateOfPickup = bookingForm.DateOfPickup;
                    bookingFormEmailInfoDto.TransportCategoryName = bookingForm.TransportCategory.Name;
                    bookingFormEmailInfoDto.TransportCategoryDescription = bookingForm.TransportCategory.Description;
                    bookingFormEmailInfoDto.ShipmentReferenceNumber = bookingForm.ShipmentReferenceNumber;
                    emailInfo.BookingForms.Add(bookingFormEmailInfoDto);

                }
                break;

            case WorklistToBioHubStatus.WaitForArrivalConditionCheck:
                query = query
                    .Include(x => x.BookingForms)
                    .ThenInclude(x => x.TransportCategory);

                entity = await (query.Where(l => l.DeletedOn == null && l.Id == id).FirstOrDefaultAsync(cancellationToken));

                emailInfo.Id = id;
                emailInfo.LaboratoryName = entity.RequestInitiationFromLaboratory.Name;
                emailInfo.LaboratoryCountry = entity.RequestInitiationFromLaboratory.Country.Name;
                emailInfo.BioHubFacilityName = entity.RequestInitiationToBioHubFacility.Name;
                emailInfo.BioHubFacilityCountry = entity.RequestInitiationToBioHubFacility.Country.Name;
                emailInfo.Comment = entity.WaitForArrivalConditionCheckApprovalComment;

                emailInfo.BookingForms = new List<BookingFormEmailInfoDto>();

                foreach (var bookingForm in entity.BookingForms)
                {
                    var bookingFormEmailInfoDto = new BookingFormEmailInfoDto();

                    bookingFormEmailInfoDto.DateOfDelivery = bookingForm.DateOfDelivery;
                    bookingFormEmailInfoDto.TransportCategoryName = bookingForm.TransportCategory.Name;
                    bookingFormEmailInfoDto.TransportCategoryDescription = bookingForm.TransportCategory.Description;
                    bookingFormEmailInfoDto.ShipmentReferenceNumber = bookingForm.ShipmentReferenceNumber;
                    emailInfo.BookingForms.Add(bookingFormEmailInfoDto);

                }
                break;

            case WorklistToBioHubStatus.WaitForCommentBHFSendFeedback:

                query = query
                    .Include(x => x.WorklistToBioHubItemFeedbacks)
                    .ThenInclude(x => x.PostedBy);

                entity = await (query.Where(l => l.DeletedOn == null && l.Id == id).FirstOrDefaultAsync(cancellationToken));

                emailInfo.Id = id;
                emailInfo.LaboratoryName = entity.RequestInitiationFromLaboratory.Name;
                emailInfo.LaboratoryCountry = entity.RequestInitiationFromLaboratory.Country.Name;
                emailInfo.BioHubFacilityName = entity.RequestInitiationToBioHubFacility.Name;
                emailInfo.Feedback = new FeedbackDto();

                feedback = entity.WorklistToBioHubItemFeedbacks.OrderByDescending(x => x.Date).FirstOrDefault();

                if (feedback != null)
                {
                    emailInfo.Feedback.PostedBy = feedback.PostedBy.FirstName + " " + feedback.PostedBy.LastName;
                    emailInfo.Feedback.Text = feedback.Text;
                }


                break;

            case WorklistToBioHubStatus.WaitForFinalApproval:
                query = query
                    .Include(x => x.WorklistToBioHubItemFeedbacks)
                    .ThenInclude(x => x.PostedBy);


                entity = await (query.Where(l => l.DeletedOn == null && l.Id == id).FirstOrDefaultAsync(cancellationToken));

                emailInfo.Id = id;
                emailInfo.LaboratoryName = entity.RequestInitiationFromLaboratory.Name;
                emailInfo.LaboratoryCountry = entity.RequestInitiationFromLaboratory.Country.Name;
                emailInfo.BioHubFacilityName = entity.RequestInitiationToBioHubFacility.Name;
                emailInfo.Feedback = new FeedbackDto();

                feedback = entity.WorklistToBioHubItemFeedbacks.OrderByDescending(x => x.Date).FirstOrDefault();

                if (feedback != null)
                {

                    emailInfo.Feedback.PostedBy = feedback.PostedBy.FirstName + " " + feedback.PostedBy.LastName;
                    emailInfo.Feedback.Text = feedback.Text;

                }
                break;

            case WorklistToBioHubStatus.ShipmentCompleted:

                entity = await (query.Where(l => l.DeletedOn == null && l.Id == id).FirstOrDefaultAsync(cancellationToken));

                if (entity.PreviousStatus == WorklistToBioHubStatus.WaitForArrivalConditionCheck)
                {
                    emailInfo.Id = id;
                    emailInfo.LaboratoryName = entity.RequestInitiationFromLaboratory.Name;
                    emailInfo.LaboratoryCountry = entity.RequestInitiationFromLaboratory.Country.Name;
                    emailInfo.BioHubFacilityName = entity.RequestInitiationToBioHubFacility.Name;

                    emailInfo.WaitForArrivalConditionCheckApprovalComment = entity.WaitForArrivalConditionCheckApprovalComment;
                }

                break;
        }

        return emailInfo;
    }

    public async Task<WorklistToBioHubItem> ReadWithHistory(Guid id, CancellationToken cancellationToken)
    {
        return await _dbContext.WorklistToBioHubItems
            .AsNoTracking()
            .AsSplitQuery()
            .Include(x => x.BookingForms)
            .ThenInclude(x => x.TransportCategory)
            .Include(x => x.WorklistToBioHubHistoryItems)
            .Where(l => l.DeletedOn == null)
            .Where(x => x.Id == id)
            .FirstOrDefaultAsync(cancellationToken);
    }


    public async Task<IEnumerable<WorklistToBioHubItem>> ReadForEformList(CancellationToken cancellationToken)
    {
        return await _dbContext.WorklistToBioHubItems
            .AsNoTracking()
            .AsSplitQuery()          
            .Include(x => x.RequestInitiationFromLaboratory)
            .Include(x => x.RequestInitiationToBioHubFacility)
            .Where(l => l.DeletedOn == null && l.Status > WorklistToBioHubStatus.WaitingForAnnex2OfSMTA1SECsApproval)  
            .AsNoTracking()            
            .ToArrayAsync(cancellationToken);
    }

    public async Task<IEnumerable<WorklistToBioHubItem>> ReadForEformListForBioHubFacility(Guid bioHubFacilityId, CancellationToken cancellationToken)
    {
        return await _dbContext.WorklistToBioHubItems
            .AsNoTracking()
            .AsSplitQuery()
            .Include(x => x.RequestInitiationFromLaboratory)
            .Include(x => x.RequestInitiationToBioHubFacility)
            .Where(l => l.DeletedOn == null && l.RequestInitiationToBioHubFacilityId == bioHubFacilityId && l.Status > WorklistToBioHubStatus.WaitingForAnnex2OfSMTA1SECsApproval)
            .AsNoTracking()
            .ToArrayAsync(cancellationToken);
    }

    public async Task<IEnumerable<WorklistToBioHubItem>> ReadForEformListForLaboratory(Guid laboratoryId, CancellationToken cancellationToken)
    {
        return await _dbContext.WorklistToBioHubItems
            .AsNoTracking()
            .AsSplitQuery()
            .Include(x => x.RequestInitiationFromLaboratory)
            .Include(x => x.RequestInitiationToBioHubFacility)
            .Where(l => l.DeletedOn == null && l.RequestInitiationFromLaboratoryId == laboratoryId && l.Status > WorklistToBioHubStatus.WaitingForAnnex2OfSMTA1SECsApproval)
            .AsNoTracking()
            .ToArrayAsync(cancellationToken);
    }


    public async Task<WorklistToBioHubItem> ReadAnnex2OfSMTA1Info(Guid id, CancellationToken cancellationToken)
    {        
                var query = _dbContext.WorklistToBioHubItems
                    .Include(x => x.WorklistToBioHubItemDocuments)
                    .ThenInclude(x => x.Document)
                    .Include(x => x.WorklistToBioHubHistoryItems)
                    .Include(l => l.RequestInitiationFromLaboratory)
                    .ThenInclude(x => x.Country)
                    .Include(x => x.RequestInitiationToBioHubFacility)
                    .ThenInclude(x => x.Country)
                    .Include(x => x.LastOperationUser)
                    .ThenInclude(x => x.Role)                    
                    .Include(x => x.MaterialShippingInformations)
                    .ThenInclude(x => x.MaterialClinicalDetails)
                    .Include(x => x.MaterialShippingInformations)
                    .ThenInclude(x => x.MaterialLaboratoryAnalysisInformation)
                    .ThenInclude(x => x.CollectedSpecimenTypes)
                    .ThenInclude(x => x.SpecimenType)
                    .Include(x => x.WorklistToBioHubItemLaboratoryFocalPoints)
                    .ThenInclude(x => x.User)
                    .ThenInclude(x => x.Laboratory)
                    .AsNoTracking()
                    .AsSplitQuery();


        return await query.FirstOrDefaultAsync(l => l.DeletedOn == null && l.Id == id && l.Status > WorklistToBioHubStatus.WaitingForAnnex2OfSMTA1SECsApproval, cancellationToken);

    }


    public async Task<WorklistToBioHubItem> ReadAnnex2OfSMTA1InfoForBioHubFacility(Guid id, Guid bioHubFacilityId, CancellationToken cancellationToken)
    {
        var query = _dbContext.WorklistToBioHubItems
            .Include(x => x.WorklistToBioHubItemDocuments)
            .ThenInclude(x => x.Document)
            .Include(x => x.WorklistToBioHubHistoryItems)
            .Include(l => l.RequestInitiationFromLaboratory)
            .ThenInclude(x => x.Country)
            .Include(x => x.RequestInitiationToBioHubFacility)
            .ThenInclude(x => x.Country)
            .Include(x => x.LastOperationUser)
            .ThenInclude(x => x.Role)           
            .Include(x => x.MaterialShippingInformations)
            .ThenInclude(x => x.MaterialClinicalDetails)
            .Include(x => x.MaterialShippingInformations)
            .ThenInclude(x => x.MaterialLaboratoryAnalysisInformation)
            .ThenInclude(x => x.CollectedSpecimenTypes)
            .ThenInclude(x => x.SpecimenType)
            .Include(x => x.WorklistToBioHubItemLaboratoryFocalPoints)
            .ThenInclude(x => x.User)
            .ThenInclude(x => x.Laboratory)
            .AsNoTracking()
            .AsSplitQuery();


        return await query.FirstOrDefaultAsync(l => l.DeletedOn == null && l.Id == id && l.RequestInitiationToBioHubFacilityId == bioHubFacilityId && l.Status > WorklistToBioHubStatus.WaitingForAnnex2OfSMTA1SECsApproval, cancellationToken);

    }


    public async Task<WorklistToBioHubItem> ReadAnnex2OfSMTA1InfoForLaboratory(Guid id, Guid laboratoryId, CancellationToken cancellationToken)
    {
        var query = _dbContext.WorklistToBioHubItems
            .Include(x => x.WorklistToBioHubItemDocuments)
            .ThenInclude(x => x.Document)
            .Include(x => x.WorklistToBioHubHistoryItems)
            .Include(l => l.RequestInitiationFromLaboratory)
            .ThenInclude(x => x.Country)
            .Include(x => x.RequestInitiationToBioHubFacility)
            .ThenInclude(x => x.Country)
            .Include(x => x.LastOperationUser)
            .ThenInclude(x => x.Role)            
            .Include(x => x.MaterialShippingInformations)
            .ThenInclude(x => x.MaterialClinicalDetails)
            .Include(x => x.MaterialShippingInformations)
            .ThenInclude(x => x.MaterialLaboratoryAnalysisInformation)
            .ThenInclude(x => x.CollectedSpecimenTypes)
            .ThenInclude(x => x.SpecimenType)
            .Include(x => x.WorklistToBioHubItemLaboratoryFocalPoints)
            .ThenInclude(x => x.User)
            .ThenInclude(x => x.Laboratory)
            .AsNoTracking()
            .AsSplitQuery();


        return await query.FirstOrDefaultAsync(l => l.DeletedOn == null && l.Id == id && l.RequestInitiationFromLaboratoryId == laboratoryId && l.Status > WorklistToBioHubStatus.WaitingForAnnex2OfSMTA1SECsApproval, cancellationToken);

    }


    public async Task<WorklistToBioHubItem> ReadBookingFormOfSMTA1Info(Guid id, CancellationToken cancellationToken)
    {
        var query = _dbContext.WorklistToBioHubItems
                    .Include(x => x.WorklistToBioHubItemDocuments)
                    .ThenInclude(x => x.Document)
                    .Include(x => x.WorklistToBioHubHistoryItems)
                    .ThenInclude(x => x.LastOperationUser)                                     
                    .Include(x => x.WorklistToBioHubItemBioHubFacilityFocalPoints)
                    .ThenInclude(x => x.User)
                    .Include(l => l.RequestInitiationFromLaboratory)
                    .ThenInclude(l => l.Country)
                    .Include(x => x.RequestInitiationToBioHubFacility)
                    .ThenInclude(l => l.Country)
                    .Include(x => x.RequestInitiationToBioHubFacility)
                    .ThenInclude(l => l.Users)
                    .Include(x => x.LastOperationUser)
                    .ThenInclude(x => x.Role)
                    .Include(x => x.BookingForms)
                    .ThenInclude(x => x.TransportCategory)
                    .Include(x => x.BookingForms)
                    .ThenInclude(x => x.TransportMode)
                    .Include(x => x.BookingForms)
                    .ThenInclude(x => x.BookingFormPickupUsers)
                    .ThenInclude(x => x.User)
                    .Include(x => x.BookingForms)
                    .ThenInclude(x => x.Courier)
                    .Include(x => x.BookingForms)
                    .ThenInclude(x => x.BookingFormCourierUsers)
                    .ThenInclude(x => x.User)
                    .ThenInclude(x => x.Courier)
                    .ThenInclude(x => x.Country)
                    .Include(x => x.MaterialShippingInformations)
                    .AsNoTracking()
                    .AsSplitQuery();


        return await query.FirstOrDefaultAsync(l => l.DeletedOn == null && l.Id == id && l.Status > WorklistToBioHubStatus.WaitForBookingFormSMTA1OPSApproval, cancellationToken);

    }

    public async Task<WorklistToBioHubItem> ReadBookingFormOfSMTA1InfoForBioHubFacility(Guid id, Guid bioHubFacilityId, CancellationToken cancellationToken)
    {
        var query = _dbContext.WorklistToBioHubItems
                    .Include(x => x.WorklistToBioHubItemDocuments)
                    .ThenInclude(x => x.Document)
                    .Include(x => x.WorklistToBioHubHistoryItems)
                    .ThenInclude(x => x.LastOperationUser)
                    .Include(x => x.WorklistToBioHubItemBioHubFacilityFocalPoints)
                    .ThenInclude(x => x.User)
                    .Include(l => l.RequestInitiationFromLaboratory)
                    .ThenInclude(l => l.Country)
                    .Include(x => x.RequestInitiationToBioHubFacility)
                    .ThenInclude(l => l.Country)
                    .Include(x => x.RequestInitiationToBioHubFacility)
                    .ThenInclude(l => l.Users)
                    .Include(x => x.LastOperationUser)
                    .ThenInclude(x => x.Role)
                    .Include(x => x.BookingForms)
                    .ThenInclude(x => x.TransportCategory)
                    .Include(x => x.BookingForms)
                    .ThenInclude(x => x.TransportMode)
                    .Include(x => x.BookingForms)
                    .ThenInclude(x => x.BookingFormPickupUsers)
                    .ThenInclude(x => x.User)
                    .Include(x => x.BookingForms)
                    .ThenInclude(x => x.Courier)
                    .Include(x => x.BookingForms)
                    .ThenInclude(x => x.BookingFormCourierUsers)
                    .ThenInclude(x => x.User)
                    .ThenInclude(x => x.Courier)
                    .ThenInclude(x => x.Country)
                    .Include(x => x.MaterialShippingInformations)
                    .AsNoTracking()
                    .AsSplitQuery();


        return await query.FirstOrDefaultAsync(l => l.DeletedOn == null && l.Id == id && l.RequestInitiationToBioHubFacilityId == bioHubFacilityId && l.Status > WorklistToBioHubStatus.WaitForBookingFormSMTA1OPSApproval, cancellationToken);

    }

    public async Task<WorklistToBioHubItem> ReadBookingFormOfSMTA1InfoForLaboratory(Guid id, Guid laboratoryId, CancellationToken cancellationToken)
    {
        var query = _dbContext.WorklistToBioHubItems
                    .Include(x => x.WorklistToBioHubItemDocuments)
                    .ThenInclude(x => x.Document)
                    .Include(x => x.WorklistToBioHubHistoryItems)
                    .ThenInclude(x => x.LastOperationUser)
                    .Include(x => x.WorklistToBioHubItemBioHubFacilityFocalPoints)
                    .ThenInclude(x => x.User)
                    .Include(l => l.RequestInitiationFromLaboratory)
                    .ThenInclude(l => l.Country)
                    .Include(x => x.RequestInitiationToBioHubFacility)
                    .ThenInclude(l => l.Country)
                    .Include(x => x.RequestInitiationToBioHubFacility)
                    .ThenInclude(l => l.Users)
                    .Include(x => x.LastOperationUser)
                    .ThenInclude(x => x.Role)
                    .Include(x => x.BookingForms)
                    .ThenInclude(x => x.TransportCategory)
                    .Include(x => x.BookingForms)
                    .ThenInclude(x => x.TransportMode)
                    .Include(x => x.BookingForms)
                    .ThenInclude(x => x.BookingFormPickupUsers)
                    .ThenInclude(x => x.User)
                    .Include(x => x.BookingForms)
                    .ThenInclude(x => x.Courier)
                    .Include(x => x.BookingForms)
                    .ThenInclude(x => x.BookingFormCourierUsers)
                    .ThenInclude(x => x.User)
                    .ThenInclude(x => x.Courier)
                    .ThenInclude(x => x.Country)
                    .Include(x => x.MaterialShippingInformations)
                    .AsNoTracking()
                    .AsSplitQuery();


        return await query.FirstOrDefaultAsync(l => l.DeletedOn == null && l.Id == id && l.RequestInitiationFromLaboratoryId == laboratoryId && l.Status > WorklistToBioHubStatus.WaitForBookingFormSMTA1OPSApproval, cancellationToken);

    }

    public async Task<IEnumerable<WorklistToBioHubItem>> ListByIds(List<Guid> ids, CancellationToken cancellationToken)
    {
        return await _dbContext.WorklistToBioHubItems            
            .Where(l => ids.Contains(l.Id))
            .ToArrayAsync(cancellationToken);
    }

}