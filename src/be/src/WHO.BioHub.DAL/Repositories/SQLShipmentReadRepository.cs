using Microsoft.EntityFrameworkCore;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.Shipments;
using WHO.BioHub.Shared.Dto;
using WHO.BioHub.Shared.Enums;

namespace WHO.BioHub.DAL.Repositories;

public class SQLShipmentReadRepository : IShipmentReadRepository
{
    private readonly BioHubDbContext _dbContext;

    public SQLShipmentReadRepository(BioHubDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<Shipment>> List(CancellationToken cancellationToken)
    {
        return await _dbContext.Shipments
            .Include(x => x.WorklistFromBioHubItem)
            .ThenInclude(x => x.RequestInitiationFromBioHubFacility)
            .ThenInclude(x => x.Country)
            .Include(x => x.WorklistFromBioHubItem)
            .ThenInclude(x => x.RequestInitiationToLaboratory)
            .ThenInclude(x => x.Country)
            .Include(x => x.WorklistToBioHubItem)
            .ThenInclude(x => x.RequestInitiationToBioHubFacility)
            .ThenInclude(x => x.Country)
            .Include(x => x.WorklistToBioHubItem)
            .ThenInclude(x => x.RequestInitiationFromLaboratory)
            .ThenInclude(x => x.Country)
            .Where(l => l.DeletedOn == null)
            .ToArrayAsync(cancellationToken);
    }

    public async Task<IEnumerable<Shipment>> ListForLaboratoryUser(Guid userLaboratoryId, CancellationToken cancellationToken)
    {
        return await _dbContext.Shipments
            .Include(x => x.WorklistFromBioHubItem)
            .ThenInclude(x => x.RequestInitiationFromBioHubFacility)
            .ThenInclude(x => x.Country)
            .Include(x => x.WorklistFromBioHubItem)
            .ThenInclude(x => x.RequestInitiationToLaboratory)
            .ThenInclude(x => x.Country)
            .Include(x => x.WorklistToBioHubItem)
            .ThenInclude(x => x.RequestInitiationToBioHubFacility)
            .ThenInclude(x => x.Country)
            .Include(x => x.WorklistToBioHubItem)
            .ThenInclude(x => x.RequestInitiationFromLaboratory)
            .ThenInclude(x => x.Country)
            .Where(l => l.DeletedOn == null)
            .Where(l => l.QELaboratoryId == userLaboratoryId ||
                (l.QELaboratory.IsPublicFacing == true && l.WorklistFromBioHubItem != null &&
                l.WorklistFromBioHubItem.WorklistFromBioHubItemMaterials != null &&
                l.WorklistFromBioHubItem.WorklistFromBioHubItemMaterials.Any() &&
                l.WorklistFromBioHubItem.WorklistFromBioHubItemMaterials
                .Select(x => x.Material)
                .Where(x => x.PublicShare == YesNoOption.Yes && x.Status == MaterialStatus.Completed).Any())
            ||
            (l.QELaboratory.IsPublicFacing == true && l.WorklistToBioHubItem != null &&
                l.WorklistToBioHubItem.WorklistToBioHubItemMaterials != null &&
                l.WorklistToBioHubItem.WorklistToBioHubItemMaterials.Any() &&
                l.WorklistToBioHubItem.WorklistToBioHubItemMaterials
                .Select(x => x.Material)
                .Where(x => x.PublicShare == YesNoOption.Yes && x.Status == MaterialStatus.Completed).Any()))
            .ToArrayAsync(cancellationToken);
    }

    public async Task<IEnumerable<Shipment>> ListForBioHubFacilityUser(Guid userBioHubFacilityId, CancellationToken cancellationToken)
    {
        return await _dbContext.Shipments
            .Include(x => x.WorklistFromBioHubItem)
            .ThenInclude(x => x.RequestInitiationFromBioHubFacility)
            .ThenInclude(x => x.Country)
            .Include(x => x.WorklistFromBioHubItem)
            .ThenInclude(x => x.RequestInitiationToLaboratory)
            .ThenInclude(x => x.Country)
            .Include(x => x.WorklistToBioHubItem)
            .ThenInclude(x => x.RequestInitiationToBioHubFacility)
            .ThenInclude(x => x.Country)
            .Include(x => x.WorklistToBioHubItem)
            .ThenInclude(x => x.RequestInitiationFromLaboratory)
            .ThenInclude(x => x.Country)
            .Where(l => l.DeletedOn == null)
            .Where(l => l.BioHubFacilityId == userBioHubFacilityId ||
                (l.WorklistFromBioHubItem != null &&
                l.WorklistFromBioHubItem.WorklistFromBioHubItemMaterials != null &&
                l.WorklistFromBioHubItem.WorklistFromBioHubItemMaterials.Any() &&
                l.WorklistFromBioHubItem.WorklistFromBioHubItemMaterials
                .Select(x => x.Material)
                .Where(x => x.Status == MaterialStatus.Completed).Any())
            ||
            (l.WorklistToBioHubItem != null &&
                l.WorklistToBioHubItem.WorklistToBioHubItemMaterials != null &&
                l.WorklistToBioHubItem.WorklistToBioHubItemMaterials.Any() &&
                l.WorklistToBioHubItem.WorklistToBioHubItemMaterials
                .Select(x => x.Material)
                .Where(x => x.Status == MaterialStatus.Completed).Any()))
            .ToArrayAsync(cancellationToken);
    }

    public async Task<Shipment> Read(Guid id, CancellationToken cancellationToken)
    {
        return await _dbContext.Shipments
            .AsNoTracking()
            .Include(x => x.WorklistFromBioHubItem)
            .ThenInclude(x => x.RequestInitiationFromBioHubFacility)
            .Include(x => x.WorklistFromBioHubItem)
            .ThenInclude(x => x.RequestInitiationToLaboratory)
            .Include(x => x.WorklistToBioHubItem)
            .ThenInclude(x => x.RequestInitiationToBioHubFacility)
            .Include(x => x.WorklistToBioHubItem)
            .ThenInclude(x => x.RequestInitiationFromLaboratory)
            .Include(x => x.WorklistFromBioHubItem)
            .ThenInclude(x => x.WorklistFromBioHubItemMaterials)
            .ThenInclude(x => x.Material)
            .Include(x => x.WorklistToBioHubItem)
            .ThenInclude(x => x.WorklistToBioHubItemMaterials)
            .ThenInclude(x => x.Material)
            .Include(x => x.WorklistFromBioHubItem)
            .ThenInclude(x => x.WorklistFromBioHubItemDocuments)
            .ThenInclude(x => x.Document)
            .ThenInclude(x => x.UploadedBy)
            .Include(x => x.WorklistToBioHubItem)
            .ThenInclude(x => x.WorklistToBioHubItemDocuments)
            .ThenInclude(x => x.Document)
            .ThenInclude(x => x.UploadedBy)
            .Include(x => x.WorklistFromBioHubItem)
            .ThenInclude(x => x.BookingForms)
            .ThenInclude(x => x.TransportCategory)
            .Include(x => x.WorklistToBioHubItem)
            .ThenInclude(x => x.BookingForms)
            .ThenInclude(x => x.TransportCategory)
            .Include(x => x.WorklistFromBioHubItem)
            .ThenInclude(x => x.BookingForms)
            .ThenInclude(x => x.TransportMode)
            .Include(x => x.WorklistToBioHubItem)
            .ThenInclude(x => x.BookingForms)
            .ThenInclude(x => x.TransportMode)
            .AsSplitQuery()
            .FirstOrDefaultAsync(l => l.DeletedOn == null && l.Id == id, cancellationToken);
    }

    public async Task<Shipment> ReadForLaboratoryUser(Guid id, Guid userLaboratoryId, CancellationToken cancellationToken)
    {
        return await _dbContext.Shipments
            .AsNoTracking()
            .Include(x => x.WorklistFromBioHubItem)
            .ThenInclude(x => x.RequestInitiationFromBioHubFacility)
            .Include(x => x.WorklistFromBioHubItem)
            .ThenInclude(x => x.RequestInitiationToLaboratory)
            .Include(x => x.WorklistToBioHubItem)
            .ThenInclude(x => x.RequestInitiationToBioHubFacility)
            .Include(x => x.WorklistToBioHubItem)
            .ThenInclude(x => x.RequestInitiationFromLaboratory)
            .Include(x => x.WorklistFromBioHubItem)
            .ThenInclude(x => x.WorklistFromBioHubItemMaterials)
            .ThenInclude(x => x.Material)
            .Include(x => x.WorklistToBioHubItem)
            .ThenInclude(x => x.WorklistToBioHubItemMaterials)
            .ThenInclude(x => x.Material)
            .Include(x => x.WorklistFromBioHubItem)
            .ThenInclude(x => x.WorklistFromBioHubItemDocuments)
            .ThenInclude(x => x.Document)
            .ThenInclude(x => x.UploadedBy)
            .Include(x => x.WorklistToBioHubItem)
            .ThenInclude(x => x.WorklistToBioHubItemDocuments)
            .ThenInclude(x => x.Document)
            .ThenInclude(x => x.UploadedBy)
            .Include(x => x.WorklistFromBioHubItem)
            .ThenInclude(x => x.BookingForms)
            .ThenInclude(x => x.TransportCategory)
            .Include(x => x.WorklistToBioHubItem)
            .ThenInclude(x => x.BookingForms)
            .ThenInclude(x => x.TransportCategory)
            .Include(x => x.WorklistFromBioHubItem)
            .ThenInclude(x => x.BookingForms)
            .ThenInclude(x => x.TransportMode)
            .Include(x => x.WorklistToBioHubItem)
            .ThenInclude(x => x.BookingForms)
            .ThenInclude(x => x.TransportMode)
            .AsSplitQuery()
            .Where(l => l.DeletedOn == null &&
            l.Id == id &&
            (l.QELaboratoryId == userLaboratoryId ||
                (l.QELaboratory.IsPublicFacing == true && l.WorklistFromBioHubItem != null &&
                l.WorklistFromBioHubItem.WorklistFromBioHubItemMaterials != null &&
                l.WorklistFromBioHubItem.WorklistFromBioHubItemMaterials.Any() &&
                l.WorklistFromBioHubItem.WorklistFromBioHubItemMaterials
                .Select(x => x.Material)
                .Where(x => x.PublicShare == YesNoOption.Yes && x.Status == MaterialStatus.Completed).Any())
            ||
            (l.QELaboratory.IsPublicFacing == true && l.WorklistToBioHubItem != null &&
                l.WorklistToBioHubItem.WorklistToBioHubItemMaterials != null &&
                l.WorklistToBioHubItem.WorklistToBioHubItemMaterials.Any() &&
                l.WorklistToBioHubItem.WorklistToBioHubItemMaterials
                .Select(x => x.Material)
                .Where(x => x.PublicShare == YesNoOption.Yes && x.Status == MaterialStatus.Completed).Any())))
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<Shipment> ReadForBioHubFacilityUser(Guid id, Guid userBioHubFacilityId, CancellationToken cancellationToken)
    {
        return await _dbContext.Shipments
            .AsNoTracking()
            .Include(x => x.WorklistFromBioHubItem)
            .ThenInclude(x => x.RequestInitiationFromBioHubFacility)
            .Include(x => x.WorklistFromBioHubItem)
            .ThenInclude(x => x.RequestInitiationToLaboratory)
            .Include(x => x.WorklistToBioHubItem)
            .ThenInclude(x => x.RequestInitiationToBioHubFacility)
            .Include(x => x.WorklistToBioHubItem)
            .ThenInclude(x => x.RequestInitiationFromLaboratory)
            .Include(x => x.WorklistFromBioHubItem)
            .ThenInclude(x => x.WorklistFromBioHubItemMaterials)
            .ThenInclude(x => x.Material)
            .Include(x => x.WorklistToBioHubItem)
            .ThenInclude(x => x.WorklistToBioHubItemMaterials)
            .ThenInclude(x => x.Material)
            .Include(x => x.WorklistFromBioHubItem)
            .ThenInclude(x => x.WorklistFromBioHubItemDocuments)
            .ThenInclude(x => x.Document)
            .ThenInclude(x => x.UploadedBy)
            .Include(x => x.WorklistToBioHubItem)
            .ThenInclude(x => x.WorklistToBioHubItemDocuments)
            .ThenInclude(x => x.Document)
            .ThenInclude(x => x.UploadedBy)
            .Include(x => x.WorklistFromBioHubItem)
            .ThenInclude(x => x.BookingForms)
            .ThenInclude(x => x.TransportCategory)
            .Include(x => x.WorklistToBioHubItem)
            .ThenInclude(x => x.BookingForms)
            .ThenInclude(x => x.TransportCategory)
            .Include(x => x.WorklistFromBioHubItem)
            .ThenInclude(x => x.BookingForms)
            .ThenInclude(x => x.TransportMode)
            .Include(x => x.WorklistToBioHubItem)
            .ThenInclude(x => x.BookingForms)
            .ThenInclude(x => x.TransportMode)
            .AsSplitQuery()
            .Where(l => l.DeletedOn == null &&
            l.Id == id &&
            (l.BioHubFacilityId == userBioHubFacilityId ||
                (l.QELaboratory.IsPublicFacing == true && l.WorklistFromBioHubItem != null &&
                l.WorklistFromBioHubItem.WorklistFromBioHubItemMaterials != null &&
                l.WorklistFromBioHubItem.WorklistFromBioHubItemMaterials.Any() &&
                l.WorklistFromBioHubItem.WorklistFromBioHubItemMaterials
                .Select(x => x.Material)
                .Where(x => x.Status == MaterialStatus.Completed).Any())
            ||
            (l.QELaboratory.IsPublicFacing == true && l.WorklistToBioHubItem != null &&
                l.WorklistToBioHubItem.WorklistToBioHubItemMaterials != null &&
                l.WorklistToBioHubItem.WorklistToBioHubItemMaterials.Any() &&
                l.WorklistToBioHubItem.WorklistToBioHubItemMaterials
                .Select(x => x.Material)
                .Where(x => x.Status == MaterialStatus.Completed).Any())))
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<KpiData> IncomingShipmentsKPIData(CancellationToken cancellationToken)
    {

        KpiData kpiViewModel = new KpiData();

        int daysBetweenIncomingRequestAndPickup = 0;

        int daysBetweenRequestAndSMTASigning = 0;

        int daysBetweenRequestAndBookingFormSigning = 0;

        int daysBetweenBookingFormCourierReceiptAndPickup = 0;

        int totalTransportDaysOfSamples = 0;

        int totalDaysFromRequestToDelivery = 0;

        int totalDaysFromDeliveryToGSDApproval = 0;

        DateTime? shipmentRequestDate = null;

        var bookingForms = await _dbContext.BookingForms
            .Include(x => x.WorklistToBioHubItem)
            .ThenInclude(x => x.WorklistToBioHubHistoryItems)
            .Include(x => x.WorklistToBioHubItem)
            .ThenInclude(x => x.WorklistToBioHubItemMaterials)
            .ThenInclude(x => x.Material)
            .ThenInclude(x => x.MaterialsHistory)
            .AsNoTracking()
            .Where(x => x.DeletedOn == null)
            .Where(x => x.WorklistToBioHubItemId != null)
            .Where(x => x.WorklistToBioHubItem.DeletedOn == null)
            .Where(x => x.WorklistToBioHubItem.Status == WorklistToBioHubStatus.ShipmentCompleted)
             .ToListAsync(cancellationToken);

        int bookingFormsTotalNumber = bookingForms.Count;

        int materialsNumber = 0;



        foreach (var bookingForm in bookingForms)
        {
            shipmentRequestDate = bookingForm.WorklistToBioHubItem.WorklistToBioHubHistoryItems
                .OrderBy(x => x.OperationDate)
                .FirstOrDefault().OperationDate;

            var pickupDate = bookingForm.DateOfPickup;

            if (shipmentRequestDate != null && pickupDate != null)
            {
                daysBetweenIncomingRequestAndPickup += (pickupDate.Value - shipmentRequestDate.Value).Days;
            }

            var bookingFormApprovalDate = bookingForm.WorklistToBioHubItem.WorklistToBioHubHistoryItems
               .OrderByDescending(x => x.OperationDate)
               .Where(x => x.PreviousStatus == WorklistToBioHubStatus.WaitForBookingFormSMTA1OPSApproval)
               .Where(x => x.LastSubmissionApproved == true)
               .FirstOrDefault().OperationDate;

            if (shipmentRequestDate != null && bookingFormApprovalDate != null)
            {
                daysBetweenRequestAndBookingFormSigning += (bookingFormApprovalDate.Value - shipmentRequestDate.Value).Days;
            }
                                   

            if (bookingFormApprovalDate != null && pickupDate != null)
            {
                daysBetweenBookingFormCourierReceiptAndPickup += (pickupDate.Value - bookingFormApprovalDate.Value).Days;
            }

            var deliveryDate = bookingForm.DateOfDelivery;


            if (deliveryDate != null && pickupDate != null)
            {
                totalTransportDaysOfSamples += (deliveryDate.Value - pickupDate.Value).Days;
            }

            if (deliveryDate != null && shipmentRequestDate != null)
            {
                totalDaysFromRequestToDelivery += (deliveryDate.Value - shipmentRequestDate.Value).Days;
            }


            var materials = bookingForm.WorklistToBioHubItem.WorklistToBioHubItemMaterials
                .Select(x => x.Material)
                .Where(x => x.DeletedOn == null)
                .Where(x => x.ManualCreation == false)
                .Where(x => x.GeneticSequenceDataValidation == MaterialValidationSelection.Verified)
                .Where(x => x.Status == MaterialStatus.Completed)
                .Where(x => x.TransportCategoryId == bookingForm.TransportCategoryId);


            materialsNumber += materials.Count();

            foreach (var material in materials)
            {               
                //TODO: Double check if this date fits for the kpi calculation. No feedback received by the business
                var firstGSDUploadingDate = material.MaterialsHistory.Where(x => x.GSDUploadingDate != null).OrderBy(x => x.LastOperationDate).FirstOrDefault()?.GSDUploadingDate;

                if (firstGSDUploadingDate == null)
                {
                    firstGSDUploadingDate = material.GSDUploadingDate;
                }

                if (deliveryDate != null && firstGSDUploadingDate != null)
                {
                    totalDaysFromDeliveryToGSDApproval += (firstGSDUploadingDate.Value - deliveryDate.Value).Days;
                }
            }
        }

        kpiViewModel.AverageDaysBetweenRequestAndPickup = bookingFormsTotalNumber == 0 ? 0 : Math.Round(((decimal)daysBetweenIncomingRequestAndPickup / (decimal)bookingFormsTotalNumber), 2);
        kpiViewModel.AverageDaysBetweenRequestAndBookingFormSigning = bookingFormsTotalNumber == 0 ? 0 : Math.Round(((decimal)daysBetweenRequestAndBookingFormSigning / (decimal)bookingFormsTotalNumber), 2);
        kpiViewModel.AverageDaysBetweenBookingFormCourierReceiptAndPickup = bookingFormsTotalNumber == 0 ? 0 : Math.Round(((decimal)daysBetweenBookingFormCourierReceiptAndPickup / (decimal)bookingFormsTotalNumber), 2);
        kpiViewModel.AverageTotalTransportDaysOfSamples = bookingFormsTotalNumber == 0 ? 0 : Math.Round(((decimal)totalTransportDaysOfSamples / (decimal)bookingFormsTotalNumber), 2);
        kpiViewModel.AverageTotalDaysFromRequestToDelivery = bookingFormsTotalNumber == 0 ? 0 : Math.Round(((decimal)totalDaysFromRequestToDelivery / (decimal)bookingFormsTotalNumber), 2);

        kpiViewModel.AverageGSDUploadingTime = materialsNumber == 0 ? 0 : Math.Round(((decimal)totalDaysFromDeliveryToGSDApproval / (decimal)materialsNumber), 2);



        var smta1CompletedRequests = await _dbContext.SMTA1WorkflowItems
            .Include(x => x.SMTA1WorkflowHistoryItems)
            .AsNoTracking()
            .Where(x => x.DeletedOn == null)
            .Where(x => x.Status == SMTA1WorkflowStatus.SMTA1WorkflowComplete)
            .ToListAsync(cancellationToken);

        int smta1CompletedRequestsTotalNumber = 0;

        foreach (var smta1CompletedRequest in smta1CompletedRequests)
        {
            var smta1RequestDate = smta1CompletedRequest.SMTA1WorkflowHistoryItems
                .OrderBy(x => x.OperationDate)
                .FirstOrDefault()?.OperationDate;


            var smta1CompletionDate = smta1CompletedRequest.OperationDate;

            if (smta1RequestDate != null && smta1CompletionDate != null)
            {
                daysBetweenRequestAndSMTASigning += (smta1CompletionDate.Value - smta1RequestDate.Value).Days;
                smta1CompletedRequestsTotalNumber++;
            }
        }

        kpiViewModel.AverageDaysBetweenRequestAndSMTASigning = smta1CompletedRequestsTotalNumber == 0 ? 0 : Math.Round(((decimal)daysBetweenRequestAndSMTASigning / (decimal)smta1CompletedRequestsTotalNumber), 2);

        return kpiViewModel;

    }


    public async Task<KpiData> OutgoingShipmentsKPIData(CancellationToken cancellationToken)
    {

        KpiData kpiViewModel = new KpiData();

        int daysBetweenIncomingRequestAndPickup = 0;

        int daysBetweenRequestAndSMTASigning = 0;

        int daysBetweenRequestAndBookingFormSigning = 0;

        int daysBetweenBookingFormCourierReceiptAndPickup = 0;

        int totalTransportDaysOfSamples = 0;

        int totalDaysFromRequestToDelivery = 0;

        int GSDUploadingTime = 0;

        DateTime? shipmentRequestDate = null;

        var bookingForms = await _dbContext.BookingForms
            .Include(x => x.WorklistFromBioHubItem)
            .ThenInclude(x => x.WorklistFromBioHubHistoryItems)
            .AsNoTracking()
            .Where(x => x.DeletedOn == null)
            .Where(x => x.WorklistFromBioHubItemId != null)
            .Where(x => x.WorklistFromBioHubItem.DeletedOn == null)
            .Where(x => x.WorklistFromBioHubItem.Status == WorklistFromBioHubStatus.ShipmentCompleted)
             .ToListAsync(cancellationToken);

        int bookingFormsTotalNumber = bookingForms.Count;

        foreach (var bookingForm in bookingForms)
        {
            shipmentRequestDate = bookingForm.WorklistFromBioHubItem.WorklistFromBioHubHistoryItems
                .OrderBy(x => x.OperationDate)
                .FirstOrDefault().OperationDate;

            var pickupDate = bookingForm.DateOfPickup;

            if (shipmentRequestDate != null && pickupDate != null)
            {
                daysBetweenIncomingRequestAndPickup += (pickupDate.Value - shipmentRequestDate.Value).Days;
            }

            var bookingFormApprovalDate = bookingForm.WorklistFromBioHubItem.WorklistFromBioHubHistoryItems
              .OrderByDescending(x => x.OperationDate)
              .Where(x => x.PreviousStatus == WorklistFromBioHubStatus.WaitForBookingFormSMTA2OPSsApproval)
              .Where(x => x.LastSubmissionApproved == true)
              .FirstOrDefault().OperationDate;

            if (shipmentRequestDate != null && bookingFormApprovalDate != null)
            {
                daysBetweenRequestAndBookingFormSigning += (bookingFormApprovalDate.Value - shipmentRequestDate.Value).Days;
            }            

            if (bookingFormApprovalDate != null && pickupDate != null)
            {
                daysBetweenBookingFormCourierReceiptAndPickup += (pickupDate.Value - bookingFormApprovalDate.Value).Days;
            }

            var deliveryDate = bookingForm.DateOfDelivery;


            if (deliveryDate != null && pickupDate != null)
            {
                totalTransportDaysOfSamples += (deliveryDate.Value - pickupDate.Value).Days;
            }

            if (deliveryDate != null && shipmentRequestDate != null)
            {
                totalDaysFromRequestToDelivery += (deliveryDate.Value - shipmentRequestDate.Value).Days;
            }

        }

        kpiViewModel.AverageDaysBetweenRequestAndPickup = bookingFormsTotalNumber == 0 ? 0 : Math.Round(((decimal)daysBetweenIncomingRequestAndPickup / (decimal)bookingFormsTotalNumber), 2);
        kpiViewModel.AverageDaysBetweenRequestAndBookingFormSigning = bookingFormsTotalNumber == 0 ? 0 : Math.Round(((decimal)daysBetweenRequestAndBookingFormSigning / (decimal)bookingFormsTotalNumber), 2);
        kpiViewModel.AverageDaysBetweenBookingFormCourierReceiptAndPickup = bookingFormsTotalNumber == 0 ? 0 : Math.Round(((decimal)daysBetweenBookingFormCourierReceiptAndPickup / (decimal)bookingFormsTotalNumber), 2);
        kpiViewModel.AverageTotalTransportDaysOfSamples = bookingFormsTotalNumber == 0 ? 0 : Math.Round(((decimal)totalTransportDaysOfSamples / (decimal)bookingFormsTotalNumber), 2);
        kpiViewModel.AverageTotalDaysFromRequestToDelivery = bookingFormsTotalNumber == 0 ? 0 : Math.Round(((decimal)totalDaysFromRequestToDelivery / (decimal)bookingFormsTotalNumber), 2);


        var smta2CompletedRequests = await _dbContext.SMTA2WorkflowItems
            .Include(x => x.SMTA2WorkflowHistoryItems)
            .AsNoTracking()
            .Where(x => x.DeletedOn == null)
            .Where(x => x.Status == SMTA2WorkflowStatus.SMTA2WorkflowComplete)
            .ToListAsync(cancellationToken);


        int smta2CompletedRequestsTotalNumber = 0;
        foreach (var smta2CompletedRequest in smta2CompletedRequests)
        {
            var smta2RequestDate = smta2CompletedRequest.SMTA2WorkflowHistoryItems
                .OrderBy(x => x.OperationDate)
                .FirstOrDefault()?.OperationDate;

            var smta2CompletionDate = smta2CompletedRequest.OperationDate;

            if (smta2RequestDate != null && smta2CompletionDate != null)
            {
                daysBetweenRequestAndSMTASigning += (smta2CompletionDate.Value - smta2RequestDate.Value).Days;
                smta2CompletedRequestsTotalNumber++;
            }
        }

        kpiViewModel.AverageDaysBetweenRequestAndSMTASigning = smta2CompletedRequestsTotalNumber == 0 ? 0 : Math.Round(((decimal)daysBetweenRequestAndSMTASigning / (decimal)smta2CompletedRequestsTotalNumber), 2);


        return kpiViewModel;

    }
}