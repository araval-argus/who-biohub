using Microsoft.EntityFrameworkCore;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.WorklistFromBioHubItems;
using WHO.BioHub.Shared.Dto;
using WHO.BioHub.Shared.Enums;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DAL.Repositories;

public class SQLWorklistFromBioHubItemReadRepository : IWorklistFromBioHubItemReadRepository
{
    private readonly BioHubDbContext _dbContext;

    public SQLWorklistFromBioHubItemReadRepository(BioHubDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<WorklistFromBioHubItem>> List(CancellationToken cancellationToken)
    {
        return await _dbContext.WorklistFromBioHubItems
            .Include(x => x.RequestInitiationToLaboratory)
            .ThenInclude(x => x.Country)
            .Include(x => x.RequestInitiationFromBioHubFacility)
            .Include(x => x.LastOperationUser)
            .ThenInclude(x => x.Role)
            .Include(x => x.WorklistFromBioHubItemDocuments)
            .Where(l => l.DeletedOn == null)
            .Where(l => l.Status < WorklistFromBioHubStatus.ShipmentCompleted)
            .ToArrayAsync(cancellationToken);
    }

    public async Task<WorklistFromBioHubItem> Read(Guid id, CancellationToken cancellationToken)
    {
        return await _dbContext.WorklistFromBioHubItems
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

    public async Task<WorklistFromBioHubItem> ReadByIdAndStatus(Guid id, WorklistFromBioHubStatus status, CancellationToken cancellationToken)
    {
        return await _dbContext.WorklistFromBioHubItems
            .AsNoTracking()
            .FirstOrDefaultAsync(l => l.DeletedOn == null && l.Id == id && l.Status == status, cancellationToken);
    }

    public async Task<WorklistFromBioHubItem> ReadByIdWithExtraInfo(Guid id, CancellationToken cancellationToken)
    {
        var status = (await _dbContext.WorklistFromBioHubItems.FirstOrDefaultAsync(l => l.DeletedOn == null && l.Id == id, cancellationToken))?.Status;

        if (status == null)
        {
            return null;
        }

        var query = _dbContext.WorklistFromBioHubItems.AsNoTracking();


        switch (status)
        {

            case WorklistFromBioHubStatus.SubmitAnnex2OfSMTA2:
            case WorklistFromBioHubStatus.WaitingForAnnex2OfSMTA2SECsApproval:
                query = query
                    .Include(l => l.RequestInitiationToLaboratory)
                    .ThenInclude(x => x.Country)
                    .Include(x => x.RequestInitiationFromBioHubFacility)
                    .ThenInclude(x => x.Country)
                    .Include(x => x.LastOperationUser)
                    .ThenInclude(x => x.Role)
                    .Include(x => x.WorklistFromBioHubItemDocuments)
                    .ThenInclude(x => x.Document)
                    .ThenInclude(x => x.UploadedBy)
                    .Include(x => x.WorklistFromBioHubItemMaterials)
                    .ThenInclude(x => x.Material)
                    .Include(x => x.WorklistFromBioHubItemLaboratoryFocalPoints)
                    .ThenInclude(x => x.User)
                    .ThenInclude(x => x.Laboratory)
                    .ThenInclude(x => x.Country)
                    .Include(x => x.WorklistFromBioHubItemAnnex2OfSMTA2Conditions)
                    .ThenInclude(x => x.Annex2OfSMTA2Condition);
                break;


            case WorklistFromBioHubStatus.SubmitBiosafetyChecklistFormOfSMTA2:
            case WorklistFromBioHubStatus.WaitForBiosafetyChecklistFormSMTA2BSFsApproval:
                query = query
                    .Include(l => l.RequestInitiationToLaboratory)
                    .ThenInclude(x => x.Country)
                    .Include(x => x.RequestInitiationFromBioHubFacility)
                    .Include(x => x.LastOperationUser)
                    .ThenInclude(x => x.Role)
                    .Include(x => x.WorklistFromBioHubItemDocuments)
                    .ThenInclude(x => x.Document)
                    .ThenInclude(x => x.UploadedBy)
                    .Include(x => x.WorklistFromBioHubItemBiosafetyChecklistOfSMTA2s)
                    .ThenInclude(x => x.BiosafetyChecklistOfSMTA2)
                    .Include(x => x.WorklistFromBioHubItemBiosafetyChecklistOfSMTA2Comments)
                    .ThenInclude(x => x.PostedBy);
                break;

            case WorklistFromBioHubStatus.SubmitBookingFormOfSMTA2:

                query = query
                    .Include(x => x.WorklistFromBioHubItemDocuments)
                    .ThenInclude(x => x.Document)
                    .ThenInclude(x => x.UploadedBy)
                    .Include(x => x.WorklistFromBioHubItemMaterials)
                    .ThenInclude(x => x.Material)
                    .Include(x => x.WorklistFromBioHubItemLaboratoryFocalPoints)
                    .ThenInclude(x => x.User)
                    .ThenInclude(x => x.Laboratory)
                    .ThenInclude(x => x.Country)
                    .Include(l => l.RequestInitiationToLaboratory)
                    .ThenInclude(l => l.Country)
                    .Include(x => x.RequestInitiationFromBioHubFacility)
                    .ThenInclude(l => l.Country)
                    .Include(x => x.LastOperationUser)
                    .ThenInclude(x => x.Role)
                    .Include(x => x.BookingForms)
                    .ThenInclude(x => x.TransportCategory)
                    .Include(x => x.BookingForms)
                    .ThenInclude(x => x.BookingFormPickupUsers)
                    .ThenInclude(x => x.User);                   


                break;

            case WorklistFromBioHubStatus.WaitForBookingFormSMTA2OPSsApproval:
                query = query
                    .Include(x => x.WorklistFromBioHubItemDocuments)
                    .ThenInclude(x => x.Document)
                    .ThenInclude(x => x.UploadedBy)
                    .Include(x => x.WorklistFromBioHubItemMaterials)
                    .ThenInclude(x => x.Material)
                    .Include(x => x.WorklistFromBioHubItemLaboratoryFocalPoints)
                    .ThenInclude(x => x.User)
                    .ThenInclude(x => x.Laboratory)
                    .ThenInclude(x => x.Country)
                    .Include(l => l.RequestInitiationToLaboratory)
                    .ThenInclude(l => l.Country)
                    .Include(x => x.RequestInitiationFromBioHubFacility)
                    .ThenInclude(l => l.Country)
                    .Include(x => x.LastOperationUser)
                    .ThenInclude(x => x.Role)
                    .Include(x => x.BookingForms)
                    .ThenInclude(x => x.TransportCategory)
                    .Include(x => x.BookingForms)
                    .ThenInclude(x => x.BookingFormPickupUsers)
                    .ThenInclude(x => x.User)                   
                    .Include(x => x.BookingForms)
                    .ThenInclude(x => x.BookingFormCourierUsers)
                    .ThenInclude(x => x.User)
                    .ThenInclude(x => x.Courier)
                    .ThenInclude(x => x.Country);                    
                break;

            //case WorklistFromBioHubStatus.SubmitBHFSMTA2ShipmentDocuments:
            //case WorklistFromBioHubStatus.SubmitQESMTA2ShipmentDocuments:
            //    query = query
            //        .Include(x => x.LastOperationUser)
            //        .ThenInclude(x => x.Role)
            //        .Include(l => l.RequestInitiationToLaboratory)
            //        .Include(x => x.RequestInitiationFromBioHubFacility)
            //        .Include(x => x.WorklistFromBioHubItemDocuments)
            //        .ThenInclude(x => x.Document)
            //        .ThenInclude(x => x.UploadedBy)
            //        .ThenInclude(x => x.Role);
            //    break;

            case WorklistFromBioHubStatus.WaitForPickUpCompleted:
                query = query
                    .Include(x => x.LastOperationUser)
                    .ThenInclude(x => x.Role)
                    .Include(l => l.RequestInitiationToLaboratory)
                    .Include(x => x.RequestInitiationFromBioHubFacility)
                    .Include(x => x.WorklistFromBioHubItemDocuments)
                    .ThenInclude(x => x.Document)
                    .ThenInclude(x => x.UploadedBy)
                    .ThenInclude(x => x.Role)
                    .Include(x => x.BookingForms)
                    .ThenInclude(x => x.TransportCategory);
                break;

            case WorklistFromBioHubStatus.WaitForDeliveryCompleted:
                query = query
                    .Include(x => x.LastOperationUser)
                    .ThenInclude(x => x.Role)
                    .Include(l => l.RequestInitiationToLaboratory)
                    .Include(x => x.RequestInitiationFromBioHubFacility)
                    .Include(x => x.WorklistFromBioHubItemDocuments)
                    .ThenInclude(x => x.Document)
                    .ThenInclude(x => x.UploadedBy)
                    .ThenInclude(x => x.Role)
                    .Include(x => x.BookingForms)
                    .ThenInclude(x => x.TransportCategory)
                    .Include(x => x.BookingForms)
                    .ThenInclude(x => x.TransportMode);
                break;

            case WorklistFromBioHubStatus.WaitForArrivalConditionCheck:
                query = query
                    .Include(x => x.LastOperationUser)
                    .ThenInclude(x => x.Role)
                    .Include(l => l.RequestInitiationToLaboratory)
                    .Include(x => x.RequestInitiationFromBioHubFacility)
                    .Include(x => x.WorklistFromBioHubItemDocuments)
                    .ThenInclude(x => x.Document)
                    .ThenInclude(x => x.UploadedBy)
                    .ThenInclude(x => x.Role)
                    .Include(x => x.WorklistFromBioHubItemMaterials)
                    .ThenInclude(x => x.Material);
                break;

            case WorklistFromBioHubStatus.WaitForCommentQESendFeedback:
            case WorklistFromBioHubStatus.WaitForFinalApproval:
                query = query
                    .Include(x => x.LastOperationUser)
                    .ThenInclude(x => x.Role)
                    .Include(l => l.RequestInitiationToLaboratory)
                    .Include(x => x.RequestInitiationFromBioHubFacility)
                    .Include(x => x.WorklistFromBioHubItemDocuments)
                    .ThenInclude(x => x.Document)
                    .ThenInclude(x => x.UploadedBy)
                    .ThenInclude(x => x.Role)
                    .Include(x => x.WorklistFromBioHubItemMaterials)
                    .ThenInclude(x => x.Material)
                    .Include(x => x.WorklistFromBioHubItemFeedbacks)
                    .ThenInclude(x => x.PostedBy);
                break;

            case WorklistFromBioHubStatus.ShipmentCompleted:
                query = query
                    .Include(x => x.LastOperationUser)
                    .ThenInclude(x => x.Role)
                    .Include(x => x.WorklistFromBioHubItemMaterials)
                    .ThenInclude(x => x.Material)
                    .Include(l => l.RequestInitiationToLaboratory)
                    .Include(x => x.RequestInitiationFromBioHubFacility)
                    .Include(x => x.WorklistFromBioHubItemDocuments)
                    .ThenInclude(x => x.Document)
                    .ThenInclude(x => x.UploadedBy)
                    .ThenInclude(x => x.Role); 
                break;

        }

        query = query.AsSplitQuery();

        return await query.FirstOrDefaultAsync(l => l.DeletedOn == null && l.Id == id, cancellationToken);

    }

    public async Task<IEnumerable<Annex2OfSMTA2Condition>> GetAnnex2OfSMTA2ConditionList(CancellationToken cancellationToken)
    {
        return await _dbContext.Annex2OfSMTA2Conditions.Where(l => l.DeletedOn == null && l.Current == true).ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Annex2OfSMTA2Condition>> GetAllAnnex2OfSMTA2ConditionList(CancellationToken cancellationToken)
    {
        return await _dbContext.Annex2OfSMTA2Conditions.ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<BiosafetyChecklistOfSMTA2>> GetBiosafetyChecklistOfSMTA2List(CancellationToken cancellationToken)
    {
        return await _dbContext.BiosafetyChecklistOfSMTA2s.Where(l => l.DeletedOn == null && l.Current == true).ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<BiosafetyChecklistOfSMTA2>> GetAllBiosafetyChecklistOfSMTA2List(CancellationToken cancellationToken)
    {
        return await _dbContext.BiosafetyChecklistOfSMTA2s.ToListAsync(cancellationToken);
    }

    public async Task<WorkflowEmailInfoDto> ReadInfoForEmail(Guid id, WorklistFromBioHubStatus status, WorklistFromBioHubStatus previousStatus, CancellationToken cancellationToken)
    {
        WorklistFromBioHubItem? entity;
        WorklistFromBioHubHistoryItem? entityHistory;
        WorkflowEmailInfoDto emailInfo = new WorkflowEmailInfoDto();
        WorklistFromBioHubItemFeedback feedback;


        var query = _dbContext.WorklistFromBioHubItems.AsNoTracking().AsSplitQuery();
        var queryHistory = _dbContext.WorklistFromBioHubHistoryItems.AsNoTracking().AsSplitQuery();

        query = query
            .Include(x => x.LastOperationUser)
            .Include(x => x.RequestInitiationFromBioHubFacility)
            .ThenInclude(x => x.Country)
            .Include(x => x.RequestInitiationToLaboratory)
            .ThenInclude(x => x.Country);


        queryHistory = queryHistory
            .Include(x => x.LastOperationUser)
            .Include(x => x.RequestInitiationFromBioHubFacility)
            .ThenInclude(x => x.Country)
            .Include(x => x.RequestInitiationToLaboratory)
            .ThenInclude(x => x.Country);


        switch (status)
        {

            case WorklistFromBioHubStatus.SubmitAnnex2OfSMTA2:

                if (previousStatus == WorklistFromBioHubStatus.WaitingForAnnex2OfSMTA2SECsApproval)
                {

                    entityHistory = await (queryHistory.Where(l => l.DeletedOn == null && l.WorklistFromBioHubItemId == id && l.Status == WorklistFromBioHubStatus.WaitingForAnnex2OfSMTA2SECsApproval && l.LastSubmissionApproved == true).OrderByDescending(x => x.CreationDate).FirstOrDefaultAsync(cancellationToken));
                    emailInfo.Id = id;
                    if (entityHistory != null)
                    {
                        emailInfo.LaboratoryName = entityHistory.RequestInitiationToLaboratory.Name;
                        emailInfo.LaboratoryCountry = entityHistory.RequestInitiationToLaboratory.Country.Name;
                        emailInfo.LaboratoryUserFirstName = entityHistory.LastOperationUser.FirstName;
                        emailInfo.LaboratoryUserLastName = entityHistory.LastOperationUser.LastName;
                        emailInfo.LaboratoryUserEmail = entityHistory.LastOperationUser.Email;
                        emailInfo.BioHubFacilityName = entityHistory.RequestInitiationFromBioHubFacility.Name;
                    }
                }

                else
                {

                    entityHistory = await (queryHistory.Where(l => l.DeletedOn == null && l.WorklistFromBioHubItemId == id && l.Status == WorklistFromBioHubStatus.RequestInitiation && l.LastSubmissionApproved == true).OrderByDescending(x => x.CreationDate).FirstOrDefaultAsync(cancellationToken));
                    emailInfo.Id = id;
                    emailInfo.LaboratoryName = entityHistory.RequestInitiationToLaboratory.Name;
                    emailInfo.LaboratoryCountry = entityHistory.RequestInitiationToLaboratory.Country.Name;
                    emailInfo.LaboratoryUserFirstName = entityHistory.LastOperationUser.FirstName;
                    emailInfo.LaboratoryUserLastName = entityHistory.LastOperationUser.LastName;
                    emailInfo.LaboratoryUserEmail = entityHistory.LastOperationUser.Email;
                    emailInfo.BioHubFacilityName = entityHistory.RequestInitiationFromBioHubFacility.Name;
                }
                break;

            case WorklistFromBioHubStatus.WaitingForAnnex2OfSMTA2SECsApproval:

                entity = await (query.Where(l => l.DeletedOn == null && l.Id == id).FirstOrDefaultAsync(cancellationToken));

                emailInfo.Id = id;
                emailInfo.LaboratoryName = entity.RequestInitiationToLaboratory.Name;
                emailInfo.LaboratoryCountry = entity.RequestInitiationToLaboratory.Country.Name;
                emailInfo.LaboratoryUserFirstName = entity.LastOperationUser.FirstName;
                emailInfo.LaboratoryUserLastName = entity.LastOperationUser.LastName;
                emailInfo.LaboratoryUserEmail = entity.LastOperationUser.Email;
                emailInfo.BioHubFacilityName = entity.RequestInitiationFromBioHubFacility != null ? entity.RequestInitiationFromBioHubFacility.Name : string.Empty;

                break;



            case WorklistFromBioHubStatus.SubmitBiosafetyChecklistFormOfSMTA2:

                if (previousStatus == WorklistFromBioHubStatus.WaitForBiosafetyChecklistFormSMTA2BSFsApproval)
                {

                    entityHistory = await (queryHistory.Where(l => l.DeletedOn == null && l.WorklistFromBioHubItemId == id && l.Status == WorklistFromBioHubStatus.WaitingForAnnex2OfSMTA2SECsApproval && l.LastSubmissionApproved == true).OrderByDescending(x => x.CreationDate).FirstOrDefaultAsync(cancellationToken));
                    emailInfo.Id = id;
                    emailInfo.LaboratoryName = entityHistory.RequestInitiationToLaboratory.Name;
                    emailInfo.LaboratoryCountry = entityHistory.RequestInitiationToLaboratory.Country.Name;
                    emailInfo.LaboratoryUserFirstName = entityHistory.LastOperationUser.FirstName;
                    emailInfo.LaboratoryUserLastName = entityHistory.LastOperationUser.LastName;
                    emailInfo.LaboratoryUserEmail = entityHistory.LastOperationUser.Email;
                    emailInfo.BioHubFacilityName = entityHistory.RequestInitiationFromBioHubFacility.Name;

                }

                else
                {
                    entity = await (query.Where(l => l.DeletedOn == null && l.Id == id && l.LastSubmissionApproved == true).OrderByDescending(x => x.CreationDate).FirstOrDefaultAsync(cancellationToken));

                    entityHistory = await (queryHistory.Where(l => l.DeletedOn == null && l.WorklistFromBioHubItemId == id && l.Status == WorklistFromBioHubStatus.WaitingForAnnex2OfSMTA2SECsApproval && l.LastSubmissionApproved == true).OrderByDescending(x => x.CreationDate).FirstOrDefaultAsync(cancellationToken));
                    emailInfo.Id = id;
                    emailInfo.LaboratoryName = entity.RequestInitiationToLaboratory.Name;
                    emailInfo.LaboratoryCountry = entity.RequestInitiationToLaboratory.Country.Name;
                    emailInfo.LaboratoryUserFirstName = entityHistory.LastOperationUser.FirstName;
                    emailInfo.LaboratoryUserLastName = entityHistory.LastOperationUser.LastName;
                    emailInfo.LaboratoryUserEmail = entityHistory.LastOperationUser.Email;
                    emailInfo.BioHubFacilityName = entity.RequestInitiationFromBioHubFacility.Name;
                }
                break;

            case WorklistFromBioHubStatus.WaitForBiosafetyChecklistFormSMTA2BSFsApproval:

                entity = await (query.Where(l => l.DeletedOn == null && l.Id == id).FirstOrDefaultAsync(cancellationToken));

                emailInfo.Id = id;
                emailInfo.LaboratoryName = entity.RequestInitiationToLaboratory.Name;
                emailInfo.LaboratoryCountry = entity.RequestInitiationToLaboratory.Country.Name;
                emailInfo.LaboratoryUserFirstName = entity.LastOperationUser.FirstName;
                emailInfo.LaboratoryUserLastName = entity.LastOperationUser.LastName;
                emailInfo.LaboratoryUserEmail = entity.LastOperationUser.Email;
                emailInfo.BioHubFacilityName = entity.RequestInitiationFromBioHubFacility.Name;

                break;

            case WorklistFromBioHubStatus.SubmitBookingFormOfSMTA2:

                if (previousStatus == WorklistFromBioHubStatus.WaitForBookingFormSMTA2OPSsApproval)
                {

                    entityHistory = await (queryHistory.Where(l => l.DeletedOn == null && l.WorklistFromBioHubItemId == id && l.Status == WorklistFromBioHubStatus.WaitForBookingFormSMTA2OPSsApproval && l.LastSubmissionApproved == true).OrderByDescending(x => x.CreationDate).FirstOrDefaultAsync(cancellationToken));

                    entity = await (query.Where(l => l.DeletedOn == null && l.Id == id).FirstOrDefaultAsync(cancellationToken));

                    emailInfo.Id = id;
                    emailInfo.BioHubFacilityName = entityHistory.RequestInitiationFromBioHubFacility.Name;
                    emailInfo.BioHubFacilityCountry = entityHistory.RequestInitiationFromBioHubFacility.Country.Name;
                    emailInfo.BioHubFacilityUserFirstName = entityHistory.LastOperationUser.FirstName;
                    emailInfo.BioHubFacilityUserLastName = entityHistory.LastOperationUser.LastName;
                    emailInfo.BioHubFacilityUserEmail = entityHistory.LastOperationUser.Email;
                    emailInfo.LaboratoryName = entity.RequestInitiationToLaboratory.Name;
                    emailInfo.LaboratoryCountry = entity.RequestInitiationToLaboratory.Country.Name;
                }

                else
                {
                    queryHistory = queryHistory
                    .Include(x => x.WorklistFromBioHubHistoryItemDocuments)
                    .ThenInclude(x => x.Document);

                    entityHistory = await (queryHistory.Where(l => l.DeletedOn == null && l.WorklistFromBioHubItemId == id && l.Status == WorklistFromBioHubStatus.WaitForBiosafetyChecklistFormSMTA2BSFsApproval && l.LastSubmissionApproved == true).OrderByDescending(x => x.CreationDate).FirstOrDefaultAsync(cancellationToken));

                    emailInfo.Id = id;
                    emailInfo.LaboratoryName = entityHistory.RequestInitiationToLaboratory.Name;
                    emailInfo.LaboratoryCountry = entityHistory.RequestInitiationToLaboratory.Country.Name;
                    emailInfo.LaboratoryUserFirstName = entityHistory.LastOperationUser.FirstName;
                    emailInfo.LaboratoryUserLastName = entityHistory.LastOperationUser.LastName;
                    emailInfo.LaboratoryUserEmail = entityHistory.LastOperationUser.Email;
                    emailInfo.BioHubFacilityName = entityHistory.RequestInitiationFromBioHubFacility.Name;

                    //# 54317
                    //emailInfo.LaboratoryUserSignature = entityHistory.WorklistFromBioHubHistoryItemDocuments.Select(x => x.Document).Where(x => x.Type == DocumentFileType.BookingFormOfSMTA2).FirstOrDefault()?.Base64String ?? String.Empty;
                    emailInfo.LaboratoryUserSignature = entityHistory.BookingFormOfSMTA2SignatureText ?? String.Empty;
                    //////////////////////
                }
                break;

            case WorklistFromBioHubStatus.WaitForBookingFormSMTA2OPSsApproval:

                entity = await (query.Where(l => l.DeletedOn == null && l.Id == id).FirstOrDefaultAsync(cancellationToken));

                emailInfo.Id = id;
                emailInfo.BioHubFacilityName = entity.RequestInitiationFromBioHubFacility.Name;
                emailInfo.BioHubFacilityCountry = entity.RequestInitiationFromBioHubFacility.Country.Name;
                emailInfo.BioHubFacilityUserFirstName = entity.LastOperationUser.FirstName;
                emailInfo.BioHubFacilityUserLastName = entity.LastOperationUser.LastName;
                emailInfo.BioHubFacilityUserEmail = entity.LastOperationUser.Email;
                emailInfo.LaboratoryName = entity.RequestInitiationToLaboratory.Name;
                emailInfo.LaboratoryCountry = entity.RequestInitiationToLaboratory.Country.Name;
                break;
            


            case WorklistFromBioHubStatus.WaitForPickUpCompleted:

                entityHistory = await (queryHistory.Where(l => l.DeletedOn == null && l.WorklistFromBioHubItemId == id && l.Status == WorklistFromBioHubStatus.WaitForBookingFormSMTA2OPSsApproval && l.LastSubmissionApproved == true).OrderByDescending(x => x.CreationDate).FirstOrDefaultAsync(cancellationToken));

                query = query
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
                    .Include(x => x.WorklistFromBioHubItemLaboratoryFocalPoints)
                    .ThenInclude(x => x.User)
                    .ThenInclude(x => x.Laboratory)
                    .ThenInclude(x => x.Country)
                    .Include(x => x.WorklistFromBioHubItemDocuments)
                    .ThenInclude(x => x.Document);

                entity = await (query.Where(l => l.DeletedOn == null && l.Id == id).FirstOrDefaultAsync(cancellationToken));

                emailInfo.Id = id;
                emailInfo.BioHubFacilityName = entityHistory.RequestInitiationFromBioHubFacility.Name;
                emailInfo.BioHubFacilityAddress = entityHistory.RequestInitiationFromBioHubFacility.Address;
                emailInfo.BioHubFacilityCountry = entityHistory.RequestInitiationFromBioHubFacility.Country.Name;
                emailInfo.BioHubFacilityUserFirstName = entityHistory.LastOperationUser.FirstName;
                emailInfo.BioHubFacilityUserLastName = entityHistory.LastOperationUser.LastName;
                emailInfo.BioHubFacilityUserEmail = entityHistory.LastOperationUser.Email;
                emailInfo.BioHubFacilityUserBusinessPhone = entityHistory.LastOperationUser.BusinessPhone;
                emailInfo.BioHubFacilityUserMobilePhone = entityHistory.LastOperationUser.MobilePhone;
                emailInfo.BioHubFacilityUserJobTitle = entityHistory.LastOperationUser.JobTitle;

                //# 54317
                //emailInfo.BioHubFacilityUserSignature = entity.WorklistFromBioHubItemDocuments.Select(x => x.Document).Where(x => x.Type == DocumentFileType.BookingFormOfSMTA2).FirstOrDefault()?.Base64String ?? String.Empty;
                emailInfo.BioHubFacilityUserSignature = entityHistory.BookingFormOfSMTA2SignatureText ?? String.Empty;
                //////////////////////


                emailInfo.WHOOperationalFocalPointName = entity.LastOperationUser.FirstName;
                emailInfo.WHOOperationalFocalPointLastname = entity.LastOperationUser.LastName;
                emailInfo.WHOOperationalFocalPointEmail = entity.LastOperationUser.Email;
                emailInfo.WHOOperationalFocalPointRoleId = entity.LastOperationUser.RoleId.GetValueOrDefault();

                emailInfo.WHOAccountNumber = entity.WHODocumentRegistrationNumber;

                emailInfo.LaboratoryName = entity.RequestInitiationToLaboratory.Name;
                emailInfo.LaboratoryAddress = entity.RequestInitiationToLaboratory.Address;
                emailInfo.LaboratoryCountry = entity.RequestInitiationToLaboratory.Country.Name;


                emailInfo.LaboratoryFocalPoints = new List<ContactUserInfoForEmailDto>();
                foreach (var laboratoryFocalPoint in entity.WorklistFromBioHubItemLaboratoryFocalPoints)
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
                    foreach (var laboratoryFocalPoint in entity.WorklistFromBioHubItemLaboratoryFocalPoints)
                    {
                        ContactUserInfoForEmailDto contactUserInfoForEmailDto = new ContactUserInfoForEmailDto();
                        contactUserInfoForEmailDto.Name = laboratoryFocalPoint.User.FirstName + " " + laboratoryFocalPoint.User.LastName;
                        contactUserInfoForEmailDto.Email = laboratoryFocalPoint.User.Email;
                        contactUserInfoForEmailDto.Phone = laboratoryFocalPoint.User.BusinessPhone;
                        bookingFormEmailInfoDto.BookingFormDeliveryUsers.Add(contactUserInfoForEmailDto);
                    }

                    emailInfo.BookingForms.Add(bookingFormEmailInfoDto);

                }

                break;

            case WorklistFromBioHubStatus.WaitForDeliveryCompleted:
                query = query
                    .Include(x => x.BookingForms)
                    .ThenInclude(x => x.TransportCategory)
                    .Include(x => x.WorklistFromBioHubItemMaterials)
                    .ThenInclude(x => x.Material)
                    .ThenInclude(x => x.MaterialsHistory);
                    


                entity = await (query.Where(l => l.DeletedOn == null && l.Id == id).FirstOrDefaultAsync(cancellationToken));

                emailInfo.Id = id;
                emailInfo.LaboratoryName = entity.RequestInitiationToLaboratory.Name;
                emailInfo.LaboratoryCountry = entity.RequestInitiationToLaboratory.Country.Name;
                emailInfo.BioHubFacilityName = entity.RequestInitiationFromBioHubFacility.Name;
                emailInfo.BioHubFacilityCountry = entity.RequestInitiationFromBioHubFacility.Country.Name;

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

                emailInfo.MaterialsCurrentNumberOfInfo = new List<MaterialsCurrentNumberOfVialsInfo>();
                var materials = entity.WorklistFromBioHubItemMaterials.Select(x => x.Material).ToList();
                foreach (var material in materials)
                {
                    var materialsCurrentNumberOfVialsInfo = new MaterialsCurrentNumberOfVialsInfo();
                    materialsCurrentNumberOfVialsInfo.NewNumberOfVials = material.CurrentNumberOfVials;
                    materialsCurrentNumberOfVialsInfo.WarningEmailCurrentNumberOfVialsThreshold = material.WarningEmailCurrentNumberOfVialsThreshold;
                    materialsCurrentNumberOfVialsInfo.ReferenceNumber = material.ReferenceNumber;

                    if (material.MaterialsHistory != null && material.MaterialsHistory.Any())
                    {
                        var previousMaterial = material.MaterialsHistory.OrderByDescending(x => x.CreationDate).FirstOrDefault();
                        materialsCurrentNumberOfVialsInfo.PreviousNumberOfVials = previousMaterial.CurrentNumberOfVials;
                    }
                    else
                    {
                        materialsCurrentNumberOfVialsInfo.PreviousNumberOfVials = material.CurrentNumberOfVials;
                    }
                    emailInfo.MaterialsCurrentNumberOfInfo.Add(materialsCurrentNumberOfVialsInfo);

                }

                break;

            case WorklistFromBioHubStatus.WaitForArrivalConditionCheck:
                query = query
                    .Include(x => x.BookingForms)
                    .ThenInclude(x => x.TransportCategory);


                entity = await (query.Where(l => l.DeletedOn == null && l.Id == id).FirstOrDefaultAsync(cancellationToken));

                emailInfo.Id = id;
                emailInfo.LaboratoryName = entity.RequestInitiationToLaboratory.Name;
                emailInfo.LaboratoryCountry = entity.RequestInitiationToLaboratory.Country.Name;
                emailInfo.BioHubFacilityName = entity.RequestInitiationFromBioHubFacility.Name;
                emailInfo.BioHubFacilityCountry = entity.RequestInitiationFromBioHubFacility.Country.Name;
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

            case WorklistFromBioHubStatus.WaitForCommentQESendFeedback:

                query = query
                    .Include(x => x.WorklistFromBioHubItemFeedbacks)
                    .ThenInclude(x => x.PostedBy);


                entity = await (query.Where(l => l.DeletedOn == null && l.Id == id).FirstOrDefaultAsync(cancellationToken));

                emailInfo.Id = id;
                emailInfo.LaboratoryName = entity.RequestInitiationToLaboratory.Name;
                emailInfo.LaboratoryCountry = entity.RequestInitiationToLaboratory.Country.Name;
                emailInfo.LaboratoryUserFirstName = entity.LastOperationUser.FirstName;
                emailInfo.LaboratoryUserLastName = entity.LastOperationUser.LastName;
                emailInfo.BioHubFacilityName = entity.RequestInitiationFromBioHubFacility.Name;
                emailInfo.BioHubFacilityCountry = entity.RequestInitiationFromBioHubFacility.Country.Name;
                emailInfo.Feedback = new FeedbackDto();

                feedback = entity.WorklistFromBioHubItemFeedbacks.OrderByDescending(x => x.Date).FirstOrDefault();

                if (feedback != null)
                {
                    emailInfo.Feedback.PostedBy = feedback.PostedBy.FirstName + " " + feedback.PostedBy.LastName;
                    emailInfo.Feedback.Text = feedback.Text;
                }


                break;

            case WorklistFromBioHubStatus.WaitForFinalApproval:
                query = query
                    .Include(x => x.WorklistFromBioHubItemFeedbacks)
                    .ThenInclude(x => x.PostedBy);


                entity = await (query.Where(l => l.DeletedOn == null && l.Id == id).FirstOrDefaultAsync(cancellationToken));

                emailInfo.Id = id;
                emailInfo.LaboratoryName = entity.RequestInitiationToLaboratory.Name;
                emailInfo.LaboratoryCountry = entity.RequestInitiationToLaboratory.Country.Name;
                emailInfo.BioHubFacilityName = entity.RequestInitiationFromBioHubFacility.Name;
                emailInfo.BioHubFacilityUserFirstName = entity.LastOperationUser.FirstName;
                emailInfo.BioHubFacilityUserLastName = entity.LastOperationUser.LastName;
                emailInfo.BioHubFacilityCountry = entity.RequestInitiationFromBioHubFacility.Country.Name;
                emailInfo.Feedback = new FeedbackDto();

                feedback = entity.WorklistFromBioHubItemFeedbacks.OrderByDescending(x => x.Date).FirstOrDefault();

                if (feedback != null)
                {

                    emailInfo.Feedback.PostedBy = feedback.PostedBy.FirstName + " " + feedback.PostedBy.LastName;
                    emailInfo.Feedback.Text = feedback.Text;

                }
                break;

            case WorklistFromBioHubStatus.ShipmentCompleted:

                entity = await (query.Where(l => l.DeletedOn == null && l.Id == id).FirstOrDefaultAsync(cancellationToken));

                if (entity.PreviousStatus == WorklistFromBioHubStatus.WaitForArrivalConditionCheck)
                {
                    emailInfo.Id = id;
                    emailInfo.LaboratoryName = entity.RequestInitiationToLaboratory.Name;
                    emailInfo.LaboratoryCountry = entity.RequestInitiationToLaboratory.Country.Name;
                    emailInfo.BioHubFacilityName = entity.RequestInitiationFromBioHubFacility.Name;
                    emailInfo.LaboratoryUserFirstName = entity.LastOperationUser.FirstName;
                    emailInfo.LaboratoryUserLastName = entity.LastOperationUser.LastName;
                    emailInfo.BioHubFacilityCountry = entity.RequestInitiationFromBioHubFacility.Country.Name;

                    emailInfo.WaitForArrivalConditionCheckApprovalComment = entity.WaitForArrivalConditionCheckApprovalComment;
                }

                break;
        }

        return emailInfo;
    }

    public async Task<WorklistFromBioHubItem> ReadWithHistory(Guid id, CancellationToken cancellationToken)
    {
        return await _dbContext.WorklistFromBioHubItems
            .AsNoTracking()
            .AsSplitQuery()
            .Include(x => x.BookingForms)
            .ThenInclude(x => x.TransportCategory)
            .Include(x => x.WorklistFromBioHubHistoryItems)
            .Where(l => l.DeletedOn == null)
            .Where(x => x.Id == id)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<IEnumerable<WorklistFromBioHubItem>> ReadForEformList(CancellationToken cancellationToken)
    {
        return await _dbContext.WorklistFromBioHubItems
            .AsNoTracking()
            .AsSplitQuery()
            .Include(x => x.RequestInitiationToLaboratory)
            .Include(x => x.RequestInitiationFromBioHubFacility)
            .Where(l => l.DeletedOn == null && l.Status > WorklistFromBioHubStatus.WaitingForAnnex2OfSMTA2SECsApproval)
            .AsNoTracking()
            .ToArrayAsync(cancellationToken);
    }

    public async Task<IEnumerable<WorklistFromBioHubItem>> ReadForEformListForBioHubFacility(Guid bioHubFacilityId, CancellationToken cancellationToken)
    {
        return await _dbContext.WorklistFromBioHubItems
            .AsNoTracking()
            .AsSplitQuery()
            .Include(x => x.RequestInitiationToLaboratory)
            .Include(x => x.RequestInitiationFromBioHubFacility)
            .Where(l => l.DeletedOn == null && l.RequestInitiationFromBioHubFacilityId == bioHubFacilityId && l.Status > WorklistFromBioHubStatus.WaitingForAnnex2OfSMTA2SECsApproval)
            .AsNoTracking()
            .ToArrayAsync(cancellationToken);
    }

    public async Task<IEnumerable<WorklistFromBioHubItem>> ReadForEformListForLaboratory(Guid laboratoryId, CancellationToken cancellationToken)
    {
        return await _dbContext.WorklistFromBioHubItems
            .AsNoTracking()
            .AsSplitQuery()
            .Include(x => x.RequestInitiationToLaboratory)
            .Include(x => x.RequestInitiationFromBioHubFacility)
            .Where(l => l.DeletedOn == null && l.RequestInitiationToLaboratoryId == laboratoryId && l.Status > WorklistFromBioHubStatus.WaitingForAnnex2OfSMTA2SECsApproval)
            .AsNoTracking()
            .ToArrayAsync(cancellationToken);
    }


    public async Task<WorklistFromBioHubItem> ReadAnnex2OfSMTA2Info(Guid id, CancellationToken cancellationToken)
    {
        var query = _dbContext.WorklistFromBioHubItems
            .Include(x => x.WorklistFromBioHubItemDocuments)
            .ThenInclude(x => x.Document)
            .Include(x => x.WorklistFromBioHubHistoryItems)
            .ThenInclude(x => x.WorklistFromBioHubHistoryItemMaterials)
            .ThenInclude(x => x.Material)
            .Include(l => l.RequestInitiationToLaboratory)
            .ThenInclude(x => x.Country)
            .Include(x => x.RequestInitiationFromBioHubFacility)
            .ThenInclude(x => x.Country)
            .Include(x => x.LastOperationUser)
            .ThenInclude(x => x.Role)
            .Include(x => x.WorklistFromBioHubItemAnnex2OfSMTA2Conditions)
            .ThenInclude(x => x.Annex2OfSMTA2Condition)
            .Include(x => x.WorklistFromBioHubItemMaterials)
            .ThenInclude(x => x.Material)
            .Include(x => x.WorklistFromBioHubItemLaboratoryFocalPoints)
            .ThenInclude(x => x.User)
            .ThenInclude(x => x.Laboratory)
            .AsNoTracking()
            .AsSplitQuery();


        return await query.FirstOrDefaultAsync(l => l.DeletedOn == null && l.Id == id && l.Status > WorklistFromBioHubStatus.WaitingForAnnex2OfSMTA2SECsApproval, cancellationToken);

    }


    public async Task<WorklistFromBioHubItem> ReadAnnex2OfSMTA2InfoForBioHubFacility(Guid id, Guid bioHubFacilityId, CancellationToken cancellationToken)
    {
        var query = _dbContext.WorklistFromBioHubItems
            .Include(x => x.WorklistFromBioHubItemDocuments)
            .ThenInclude(x => x.Document)
            .Include(x => x.WorklistFromBioHubHistoryItems)
            .ThenInclude(x => x.WorklistFromBioHubHistoryItemMaterials)
            .ThenInclude(x => x.Material)
            .Include(l => l.RequestInitiationToLaboratory)
            .ThenInclude(x => x.Country)
            .Include(x => x.RequestInitiationFromBioHubFacility)
            .ThenInclude(x => x.Country)
            .Include(x => x.LastOperationUser)
            .ThenInclude(x => x.Role)
            .Include(x => x.WorklistFromBioHubItemAnnex2OfSMTA2Conditions)
            .ThenInclude(x => x.Annex2OfSMTA2Condition)
            .Include(x => x.WorklistFromBioHubItemMaterials)
            .ThenInclude(x => x.Material)
            .Include(x => x.WorklistFromBioHubItemLaboratoryFocalPoints)
            .ThenInclude(x => x.User)
            .ThenInclude(x => x.Laboratory)
            .AsNoTracking()
            .AsSplitQuery();


        return await query.FirstOrDefaultAsync(l => l.DeletedOn == null && l.Id == id && l.RequestInitiationFromBioHubFacilityId == bioHubFacilityId && l.Status > WorklistFromBioHubStatus.WaitingForAnnex2OfSMTA2SECsApproval, cancellationToken);

    }


    public async Task<WorklistFromBioHubItem> ReadAnnex2OfSMTA2InfoForLaboratory(Guid id, Guid laboratoryId, CancellationToken cancellationToken)
    {
        var query = _dbContext.WorklistFromBioHubItems
            .Include(x => x.WorklistFromBioHubItemDocuments)
            .ThenInclude(x => x.Document)
            .Include(x => x.WorklistFromBioHubHistoryItems)
            .ThenInclude(x => x.WorklistFromBioHubHistoryItemMaterials)
            .ThenInclude(x => x.Material)
            .Include(l => l.RequestInitiationToLaboratory)
            .ThenInclude(x => x.Country)
            .Include(x => x.RequestInitiationFromBioHubFacility)
            .ThenInclude(x => x.Country)
            .Include(x => x.LastOperationUser)
            .ThenInclude(x => x.Role)
            .Include(x => x.WorklistFromBioHubItemAnnex2OfSMTA2Conditions)
            .ThenInclude(x => x.Annex2OfSMTA2Condition)
            .Include(x => x.WorklistFromBioHubItemMaterials)
            .ThenInclude(x => x.Material)
            .Include(x => x.WorklistFromBioHubItemLaboratoryFocalPoints)
            .ThenInclude(x => x.User)
            .ThenInclude(x => x.Laboratory)
            .AsNoTracking()
            .AsSplitQuery();


        return await query.FirstOrDefaultAsync(l => l.DeletedOn == null && l.Id == id && l.RequestInitiationToLaboratoryId == laboratoryId && l.Status > WorklistFromBioHubStatus.WaitingForAnnex2OfSMTA2SECsApproval, cancellationToken);

    }


    public async Task<WorklistFromBioHubItem> ReadBookingFormOfSMTA2Info(Guid id, CancellationToken cancellationToken)
    {
        var query = _dbContext.WorklistFromBioHubItems
                    .Include(x => x.WorklistFromBioHubItemDocuments)
                    .ThenInclude(x => x.Document)
                    .Include(x => x.WorklistFromBioHubHistoryItems)
                    .ThenInclude(x => x.LastOperationUser)
                    .Include(x => x.WorklistFromBioHubItemLaboratoryFocalPoints)
                    .ThenInclude(x => x.User)
                    .Include(l => l.RequestInitiationToLaboratory)
                    .ThenInclude(l => l.Country)
                    .Include(x => x.RequestInitiationFromBioHubFacility)
                    .ThenInclude(l => l.Country)
                    .Include(x => x.RequestInitiationFromBioHubFacility)
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
                    .Include(x => x.WorklistFromBioHubItemMaterials)
                    .ThenInclude(x => x.Material)
                    .AsNoTracking()
                    .AsSplitQuery();


        return await query.FirstOrDefaultAsync(l => l.DeletedOn == null && l.Id == id && l.Status > WorklistFromBioHubStatus.WaitForBookingFormSMTA2OPSsApproval, cancellationToken);

    }

    public async Task<WorklistFromBioHubItem> ReadBookingFormOfSMTA2InfoForBioHubFacility(Guid id, Guid bioHubFacilityId, CancellationToken cancellationToken)
    {
        var query = _dbContext.WorklistFromBioHubItems
                    .Include(x => x.WorklistFromBioHubItemDocuments)
                    .ThenInclude(x => x.Document)
                    .Include(x => x.WorklistFromBioHubHistoryItems)
                    .ThenInclude(x => x.LastOperationUser)
                    .Include(x => x.WorklistFromBioHubItemLaboratoryFocalPoints)
                    .ThenInclude(x => x.User)
                    .Include(l => l.RequestInitiationToLaboratory)
                    .ThenInclude(l => l.Country)
                    .Include(x => x.RequestInitiationFromBioHubFacility)
                    .ThenInclude(l => l.Country)
                    .Include(x => x.RequestInitiationFromBioHubFacility)
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
                    .Include(x => x.WorklistFromBioHubItemMaterials)
                    .ThenInclude(x => x.Material)
                    .AsNoTracking()
                    .AsSplitQuery();


        return await query.FirstOrDefaultAsync(l => l.DeletedOn == null && l.Id == id && l.RequestInitiationFromBioHubFacilityId == bioHubFacilityId && l.Status > WorklistFromBioHubStatus.WaitForBookingFormSMTA2OPSsApproval, cancellationToken);

    }

    public async Task<WorklistFromBioHubItem> ReadBookingFormOfSMTA2InfoForLaboratory(Guid id, Guid laboratoryId, CancellationToken cancellationToken)
    {
        var query = _dbContext.WorklistFromBioHubItems
                    .Include(x => x.WorklistFromBioHubItemDocuments)
                    .ThenInclude(x => x.Document)
                    .Include(x => x.WorklistFromBioHubHistoryItems)
                    .ThenInclude(x => x.LastOperationUser)
                    .Include(x => x.WorklistFromBioHubItemLaboratoryFocalPoints)
                    .ThenInclude(x => x.User)
                    .Include(l => l.RequestInitiationToLaboratory)
                    .ThenInclude(l => l.Country)
                    .Include(x => x.RequestInitiationFromBioHubFacility)
                    .ThenInclude(l => l.Country)
                    .Include(x => x.RequestInitiationFromBioHubFacility)
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
                    .Include(x => x.WorklistFromBioHubItemMaterials)
                    .ThenInclude(x => x.Material)
                    .AsNoTracking()
                    .AsSplitQuery();

        return await query.FirstOrDefaultAsync(l => l.DeletedOn == null && l.Id == id && l.RequestInitiationToLaboratoryId == laboratoryId && l.Status > WorklistFromBioHubStatus.WaitForBookingFormSMTA2OPSsApproval, cancellationToken);

    }


    public async Task<WorklistFromBioHubItem> ReadBiosafetyChecklistOfSMTA2Info(Guid id, CancellationToken cancellationToken)
    {
        var query = _dbContext.WorklistFromBioHubItems
                    .Include(x => x.WorklistFromBioHubHistoryItems)
                    .Include(l => l.RequestInitiationToLaboratory)
                    .ThenInclude(l => l.Country)
                    .Include(x => x.RequestInitiationFromBioHubFacility)
                    .ThenInclude(l => l.Country)
                    .Include(x => x.LastOperationUser)
                    .ThenInclude(x => x.Role)
                    .Include(x => x.WorklistFromBioHubItemDocuments)
                    .ThenInclude(x => x.Document)
                    .ThenInclude(x => x.UploadedBy)
                    .Include(x => x.WorklistFromBioHubItemBiosafetyChecklistOfSMTA2s)
                    .ThenInclude(x => x.BiosafetyChecklistOfSMTA2)
                    .Include(x => x.WorklistFromBioHubItemBiosafetyChecklistOfSMTA2Comments)
                    .ThenInclude(x => x.PostedBy)
                    .AsNoTracking()
                    .AsSplitQuery();


        return await query.FirstOrDefaultAsync(l => l.DeletedOn == null && l.Id == id && l.Status > WorklistFromBioHubStatus.WaitForBiosafetyChecklistFormSMTA2BSFsApproval, cancellationToken);

    }


    public async Task<WorklistFromBioHubItem> ReadBiosafetyChecklistOfSMTA2InfoForBioHubFacility(Guid id, Guid bioHubFacilityId, CancellationToken cancellationToken)
    {
        var query = _dbContext.WorklistFromBioHubItems
                    .Include(x => x.WorklistFromBioHubHistoryItems)
                    .Include(l => l.RequestInitiationToLaboratory)
                    .ThenInclude(l => l.Country)
                    .Include(x => x.RequestInitiationFromBioHubFacility)
                    .ThenInclude(l => l.Country)
                    .Include(x => x.LastOperationUser)
                    .ThenInclude(x => x.Role)
                    .Include(x => x.WorklistFromBioHubItemDocuments)
                    .ThenInclude(x => x.Document)
                    .ThenInclude(x => x.UploadedBy)
                    .Include(x => x.WorklistFromBioHubItemBiosafetyChecklistOfSMTA2s)
                    .ThenInclude(x => x.BiosafetyChecklistOfSMTA2)
                    .Include(x => x.WorklistFromBioHubItemBiosafetyChecklistOfSMTA2Comments)
                    .ThenInclude(x => x.PostedBy)
                    .AsNoTracking()
                    .AsSplitQuery();


        return await query.FirstOrDefaultAsync(l => l.DeletedOn == null && l.Id == id && l.RequestInitiationFromBioHubFacilityId == bioHubFacilityId && l.Status > WorklistFromBioHubStatus.WaitForBiosafetyChecklistFormSMTA2BSFsApproval, cancellationToken);

    }


    public async Task<WorklistFromBioHubItem> ReadBiosafetyChecklistOfSMTA2InfoForLaboratory(Guid id, Guid laboratoryId, CancellationToken cancellationToken)
    {
        var query = _dbContext.WorklistFromBioHubItems
                    .Include(x => x.WorklistFromBioHubHistoryItems)
                    .Include(l => l.RequestInitiationToLaboratory)
                    .ThenInclude(l => l.Country)
                    .Include(x => x.RequestInitiationFromBioHubFacility)
                    .ThenInclude(l => l.Country)
                    .Include(x => x.LastOperationUser)
                    .ThenInclude(x => x.Role)
                    .Include(x => x.WorklistFromBioHubItemDocuments)
                    .ThenInclude(x => x.Document)
                    .ThenInclude(x => x.UploadedBy)
                    .Include(x => x.WorklistFromBioHubItemBiosafetyChecklistOfSMTA2s)
                    .ThenInclude(x => x.BiosafetyChecklistOfSMTA2)
                    .Include(x => x.WorklistFromBioHubItemBiosafetyChecklistOfSMTA2Comments)
                    .ThenInclude(x => x.PostedBy)
                    .AsNoTracking()
                    .AsSplitQuery();


        return await query.FirstOrDefaultAsync(l => l.DeletedOn == null && l.Id == id && l.RequestInitiationToLaboratoryId == laboratoryId && l.Status > WorklistFromBioHubStatus.WaitForBiosafetyChecklistFormSMTA2BSFsApproval, cancellationToken);

    }


    public async Task<IEnumerable<WorklistFromBioHubItem>> ListByIds(List<Guid> ids, CancellationToken cancellationToken)
    {
        return await _dbContext.WorklistFromBioHubItems
            .Where(l => ids.Contains(l.Id))
            .ToArrayAsync(cancellationToken);
    }
}