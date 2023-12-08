using Microsoft.EntityFrameworkCore;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.WorklistToBioHubHistoryItems;
using WHO.BioHub.Shared.Utils;
using Microsoft.EntityFrameworkCore.Storage;
using WHO.BioHub.Shared.SeedDataConstants;
using WHO.BioHub.Shared.Enums;

namespace WHO.BioHub.DAL.Repositories;

public class SQLWorklistToBioHubHistoryItemWriteRepository : IWorklistToBioHubHistoryItemWriteRepository
{
    private readonly BioHubDbContext _dbContext;
    private DbSet<WorklistToBioHubHistoryItem> WorklistToBioHubHistoryItems => _dbContext.WorklistToBioHubHistoryItems;
    private DbSet<WorklistToBioHubHistoryItemDocument> WorklistToBioHubHistoryItemDocuments => _dbContext.WorklistToBioHubHistoryItemDocuments;
    private DbSet<WorklistToBioHubItemDocument> WorklistToBioHubItemDocuments => _dbContext.WorklistToBioHubItemDocuments;
    private DbSet<MaterialShippingInformation> MaterialShippingInformations => _dbContext.MaterialShippingInformations;
    private DbSet<MaterialClinicalDetail> MaterialClinicalDetails => _dbContext.MaterialClinicalDetails;
    private DbSet<MaterialShippingInformationHistory> MaterialShippingInformationsHistory => _dbContext.MaterialShippingInformationsHistory;
    private DbSet<MaterialClinicalDetailHistory> MaterialClinicalDetailsHistory => _dbContext.MaterialClinicalDetailsHistory;
    private DbSet<MaterialLaboratoryAnalysisInformationHistory> MaterialLaboratoryAnalysisInformationHistory => _dbContext.MaterialLaboratoryAnalysisInformationHistory;

    private DbSet<WorklistToBioHubItemLaboratoryFocalPoint> WorklistToBioHubItemLaboratoryFocalPoints => _dbContext.WorklistToBioHubItemLaboratoryFocalPoints;
    private DbSet<WorklistToBioHubHistoryItemLaboratoryFocalPoint> WorklistToBioHubHistoryItemLaboratoryFocalPoints => _dbContext.WorklistToBioHubHistoryItemLaboratoryFocalPoints;

    private DbSet<BookingFormHistory> BookingFormsHistory => _dbContext.BookingFormsHistory;
    private DbSet<BookingFormPickupUserHistory> BookingFormPickupUsersHistory => _dbContext.BookingFormPickupUsersHistory;
    private DbSet<BookingForm> BookingForms => _dbContext.BookingForms;
    private DbSet<BookingFormPickupUser> BookingFormPickupUsers => _dbContext.BookingFormPickupUsers;
    private DbSet<BookingFormCourierUser> BookingFormCourierUsers => _dbContext.BookingFormCourierUsers;

    private DbSet<BookingFormCourierUserHistory> BookingFormCourierUsersHistory => _dbContext.BookingFormCourierUsersHistory;

    private DbSet<WorklistToBioHubHistoryItemFeedback> WorklistToBioHubHistoryItemFeedback => _dbContext.WorklistToBioHubHistoryItemFeedback;
    private DbSet<WorklistToBioHubItemFeedback> WorklistToBioHubItemFeedback => _dbContext.WorklistToBioHubItemFeedback;
    private DbSet<WorklistToBioHubHistoryItemBioHubFacilityFocalPoint> WorklistToBioHubHistoryItemBioHubFacilityFocalPoints => _dbContext.WorklistToBioHubHistoryItemBioHubFacilityFocalPoints;
    private DbSet<WorklistToBioHubItemBioHubFacilityFocalPoint> WorklistToBioHubItemBioHubFacilityFocalPoints => _dbContext.WorklistToBioHubItemBioHubFacilityFocalPoints;

    public SQLWorklistToBioHubHistoryItemWriteRepository(BioHubDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Either<WorklistToBioHubHistoryItem, Errors>> Create(WorklistToBioHubHistoryItem worklisttobiohubhistoryitem, CancellationToken cancellationToken, IDbContextTransaction? transaction = null)
    {
        if (transaction != null)
        {
            _dbContext.Database.UseTransaction(transaction.GetDbTransaction());
        }
        await _dbContext.AddAsync(worklisttobiohubhistoryitem, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return new(worklisttobiohubhistoryitem);
    }

    public async Task<Errors?> Delete(Guid id, CancellationToken cancellationToken)
    {
        WorklistToBioHubHistoryItem lab = await WorklistToBioHubHistoryItems.FirstOrDefaultAsync(l => l.DeletedOn == null && l.Id == id, cancellationToken);
        lab.DeletedOn = DateTime.Now;
        WorklistToBioHubHistoryItems.Update(lab);

        await _dbContext.SaveChangesAsync(cancellationToken);
        return null;
    }

    public async Task<WorklistToBioHubHistoryItem> ReadForUpdate(Guid id, CancellationToken cancellationToken)
    {
        return await _dbContext.WorklistToBioHubHistoryItems
            .FirstOrDefaultAsync(l => l.DeletedOn == null && l.Id == id, cancellationToken);
    }

    public async Task<Errors?> Update(WorklistToBioHubHistoryItem worklisttobiohubhistoryitem, CancellationToken cancellationToken)
    {
        WorklistToBioHubHistoryItems.Update(worklisttobiohubhistoryitem);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return null;
    }

    public async Task<Errors?> CopyLinkDocumentFromWorklistToBioHubItem(Guid worklisttobiohubitemId, Guid worklisttobiohubhistoryitemId, CancellationToken cancellationToken, IDbContextTransaction? transaction = null)
    {
        if (transaction != null)
        {
            _dbContext.Database.UseTransaction(transaction.GetDbTransaction());
        }
        var documentsToCopy = WorklistToBioHubItemDocuments.Where(x => x.WorklistToBioHubItemId == worklisttobiohubitemId);

        if (documentsToCopy.Any())
        {
            var newHistoryElements = new List<WorklistToBioHubHistoryItemDocument>();
            foreach (var documentToCopy in documentsToCopy)
            {
                var newHistoryElement = new WorklistToBioHubHistoryItemDocument();
                newHistoryElement.WorklistToBioHubHistoryItemId = worklisttobiohubhistoryitemId;
                newHistoryElement.DocumentId = documentToCopy.DocumentId;
                newHistoryElement.IsDocumentFile = documentToCopy.IsDocumentFile;
                newHistoryElements.Add(newHistoryElement);

            }
            WorklistToBioHubHistoryItemDocuments.AddRange(newHistoryElements);
            await _dbContext.SaveChangesAsync(cancellationToken);

        }

        return null;
    }


    public async Task<Errors?> LinkMaterialShippingInformationFromWorklistToBioHubItem(Guid worklisttobiohubitemId, Guid worklisttobiohubhistoryitemId, CancellationToken cancellationToken, IDbContextTransaction? transaction = null)
    {
        if (transaction != null)
        {
            _dbContext.Database.UseTransaction(transaction.GetDbTransaction());
        }
        var materialShippingInformationsToCopy = MaterialShippingInformations.Where(x => x.WorklistToBioHubItemId == worklisttobiohubitemId)
            .Include(x => x.MaterialLaboratoryAnalysisInformation)
            .ThenInclude(x => x.CollectedSpecimenTypes)
            .Include(x => x.MaterialClinicalDetails);

        if (materialShippingInformationsToCopy.Any())
        {
            var newMaterialShippingInformationHistoryElements = new List<MaterialShippingInformationHistory>();
            var newMaterialClinicalDetailsHistoryElements = new List<MaterialClinicalDetailHistory>();
            var newMaterialLaboratoryAnalysisInformationHistoryElements = new List<MaterialLaboratoryAnalysisInformationHistory>();

            foreach (var materialShippingInformationToCopy in materialShippingInformationsToCopy)
            {
                var newMaterialShippingInformationHistoryElement = new MaterialShippingInformationHistory();
                newMaterialShippingInformationHistoryElement.Id = Guid.NewGuid();
                newMaterialShippingInformationHistoryElement.WorklistToBioHubHistoryItemId = worklisttobiohubhistoryitemId;
                newMaterialShippingInformationHistoryElement.MaterialNumber = materialShippingInformationToCopy.MaterialNumber;
                newMaterialShippingInformationHistoryElement.MaterialProductId = materialShippingInformationToCopy.MaterialProductId;
                newMaterialShippingInformationHistoryElement.TransportCategoryId = materialShippingInformationToCopy.TransportCategoryId;
                newMaterialShippingInformationHistoryElement.Quantity = materialShippingInformationToCopy.Quantity;
                newMaterialShippingInformationHistoryElement.Amount = materialShippingInformationToCopy.Amount;
                newMaterialShippingInformationHistoryElement.Condition = materialShippingInformationToCopy.Condition;
                newMaterialShippingInformationHistoryElement.AdditionalInformation = materialShippingInformationToCopy.AdditionalInformation;
                newMaterialShippingInformationHistoryElement.CreationDate = DateTime.UtcNow;
                newMaterialShippingInformationHistoryElements.Add(newMaterialShippingInformationHistoryElement);

                foreach (var materialLaboratoryAnalysisInformation in materialShippingInformationToCopy.MaterialLaboratoryAnalysisInformation)
                {
                    var newMaterialLaboratoryAnalysisInformationHistory = new MaterialLaboratoryAnalysisInformationHistory();
                    newMaterialLaboratoryAnalysisInformationHistory.Id = Guid.NewGuid();
                    newMaterialLaboratoryAnalysisInformationHistory.MaterialShippingInformationHistoryId = newMaterialShippingInformationHistoryElement.Id;
                    newMaterialLaboratoryAnalysisInformationHistory.UnitOfMeasureId = materialLaboratoryAnalysisInformation.UnitOfMeasureId;
                    newMaterialLaboratoryAnalysisInformationHistory.Temperature = materialLaboratoryAnalysisInformation.Temperature;
                    newMaterialLaboratoryAnalysisInformationHistory.VirusConcentration = materialLaboratoryAnalysisInformation.VirusConcentration;
                    newMaterialLaboratoryAnalysisInformationHistory.CreationDate = DateTime.UtcNow;
                    newMaterialLaboratoryAnalysisInformationHistory.MaterialNumber = materialLaboratoryAnalysisInformation.MaterialNumber;
                    newMaterialLaboratoryAnalysisInformationHistory.FreezingDate = materialLaboratoryAnalysisInformation.FreezingDate;

                    newMaterialLaboratoryAnalysisInformationHistory.CollectedSpecimenTypes = new List<CollectedSpecimenTypeHistory>();

                    if (materialShippingInformationToCopy.MaterialProductId == Guid.Parse(SeedDataConstants.CulturedIsolateProductId))
                    {
                        newMaterialLaboratoryAnalysisInformationHistory.CulturingCellLine = materialLaboratoryAnalysisInformation.CulturingCellLine;
                        newMaterialLaboratoryAnalysisInformationHistory.CulturingPassagesNumber = materialLaboratoryAnalysisInformation.CulturingPassagesNumber;
                    }

                    else if (materialShippingInformationToCopy.MaterialProductId == Guid.Parse(SeedDataConstants.ClinicalSpecimenProductId))
                    {
                        
                        foreach (var specimenType in materialLaboratoryAnalysisInformation.CollectedSpecimenTypes)
                        {
                            CollectedSpecimenTypeHistory newCollectedSpecimenTypesElement = new CollectedSpecimenTypeHistory();
                            newCollectedSpecimenTypesElement.SpecimenTypeId = specimenType.SpecimenTypeId;
                            newCollectedSpecimenTypesElement.MaterialLaboratoryAnalysisInformationHistoryId = newMaterialLaboratoryAnalysisInformationHistory.Id;
                            newMaterialLaboratoryAnalysisInformationHistory.CollectedSpecimenTypes.Append(newCollectedSpecimenTypesElement);
                        }
                        newMaterialLaboratoryAnalysisInformationHistory.BrandOfTransportMedium = materialLaboratoryAnalysisInformation.BrandOfTransportMedium;
                        newMaterialLaboratoryAnalysisInformationHistory.TypeOfTransportMedium = materialLaboratoryAnalysisInformation.TypeOfTransportMedium;
                    }

                    newMaterialLaboratoryAnalysisInformationHistory.GSDUploadedToDatabase = materialLaboratoryAnalysisInformation.GSDUploadedToDatabase;

                    if (materialLaboratoryAnalysisInformation.GSDUploadedToDatabase == YesNoOption.Yes)
                    {
                        newMaterialLaboratoryAnalysisInformationHistory.DatabaseUsedForGSDUploadingId = materialLaboratoryAnalysisInformation.DatabaseUsedForGSDUploadingId;
                        newMaterialLaboratoryAnalysisInformationHistory.AccessionNumberInGSDDatabase = materialLaboratoryAnalysisInformation.AccessionNumberInGSDDatabase;
                    }

                    newMaterialLaboratoryAnalysisInformationHistoryElements.Add(newMaterialLaboratoryAnalysisInformationHistory);
                }


                foreach (var materialClinicalDetail in materialShippingInformationToCopy.MaterialClinicalDetails)
                {
                    var newMaterialClinicalDetailHistory = new MaterialClinicalDetailHistory();
                    newMaterialClinicalDetailHistory.Id = Guid.NewGuid();
                    newMaterialClinicalDetailHistory.MaterialShippingInformationHistoryId = newMaterialShippingInformationHistoryElement.Id;
                    newMaterialClinicalDetailHistory.Age = materialClinicalDetail.Age;
                    newMaterialClinicalDetailHistory.Gender = materialClinicalDetail.Gender;
                    newMaterialClinicalDetailHistory.CreationDate = DateTime.UtcNow;
                    newMaterialClinicalDetailHistory.MaterialNumber = materialClinicalDetail.MaterialNumber;
                    newMaterialClinicalDetailHistory.CollectionDate = materialClinicalDetail.CollectionDate;
                    newMaterialClinicalDetailHistory.IsolationHostTypeId = materialClinicalDetail.IsolationHostTypeId;
                    newMaterialClinicalDetailHistory.Location = materialClinicalDetail.Location;
                    newMaterialClinicalDetailHistory.PatientStatus = materialClinicalDetail.PatientStatus;
                    newMaterialClinicalDetailHistory.Note = materialClinicalDetail.Note;
                    newMaterialClinicalDetailHistory.Condition = materialClinicalDetail.Condition;
                    newMaterialClinicalDetailsHistoryElements.Add(newMaterialClinicalDetailHistory);

                }

            }
            MaterialShippingInformationsHistory.AddRange(newMaterialShippingInformationHistoryElements);
            MaterialLaboratoryAnalysisInformationHistory.AddRange(newMaterialLaboratoryAnalysisInformationHistoryElements);
            MaterialClinicalDetailsHistory.AddRange(newMaterialClinicalDetailsHistoryElements);

            await _dbContext.SaveChangesAsync(cancellationToken);

        }

        return null;
    }

    public async Task<Errors?> LinkLaboratoryFocalPointsFromWorklistToBioHubItem(Guid worklisttobiohubitemId, Guid worklisttobiohubhistoryitemId, CancellationToken cancellationToken, IDbContextTransaction? transaction = null)
    {
        if (transaction != null)
        {
            _dbContext.Database.UseTransaction(transaction.GetDbTransaction());
        }
        var worklistToBioHubItemLaboratoryFocalPointsToCopy = WorklistToBioHubItemLaboratoryFocalPoints.Where(x => x.WorklistToBioHubItemId == worklisttobiohubitemId);

        if (worklistToBioHubItemLaboratoryFocalPointsToCopy.Any())
        {
            var newWorklistToBioHubHistoryItemLaboratoryFocalPointElements = new List<WorklistToBioHubHistoryItemLaboratoryFocalPoint>();

            foreach (var worklistToBioHubItemLaboratoryFocalPointToCopy in worklistToBioHubItemLaboratoryFocalPointsToCopy)
            {
                var newMaterialShippingInformationHistoryElement = new WorklistToBioHubHistoryItemLaboratoryFocalPoint();
                newMaterialShippingInformationHistoryElement.Id = Guid.NewGuid();
                newMaterialShippingInformationHistoryElement.WorklistToBioHubHistoryItemId = worklisttobiohubhistoryitemId;
                newMaterialShippingInformationHistoryElement.UserId = worklistToBioHubItemLaboratoryFocalPointToCopy.UserId;
                newMaterialShippingInformationHistoryElement.Other = worklistToBioHubItemLaboratoryFocalPointToCopy.Other;
                newMaterialShippingInformationHistoryElement.CreationDate = DateTime.UtcNow;
                newWorklistToBioHubHistoryItemLaboratoryFocalPointElements.Add(newMaterialShippingInformationHistoryElement);


            }
            WorklistToBioHubHistoryItemLaboratoryFocalPoints.AddRange(newWorklistToBioHubHistoryItemLaboratoryFocalPointElements);


            await _dbContext.SaveChangesAsync(cancellationToken);

        }

        return null;
    }


    public async Task<Errors?> LinkBioHubFacilityFocalPointsFromWorklistToBioHubItem(Guid worklisttobiohubitemId, Guid worklisttobiohubhistoryitemId, CancellationToken cancellationToken, IDbContextTransaction? transaction = null)
    {
        if (transaction != null)
        {
            _dbContext.Database.UseTransaction(transaction.GetDbTransaction());
        }
        var worklistToBioHubItemBioHubFacilityFocalPointsToCopy = WorklistToBioHubItemBioHubFacilityFocalPoints.Where(x => x.WorklistToBioHubItemId == worklisttobiohubitemId);

        if (worklistToBioHubItemBioHubFacilityFocalPointsToCopy.Any())
        {
            var newWorklistToBioHubHistoryItemBioHubFacilityFocalPointElements = new List<WorklistToBioHubHistoryItemBioHubFacilityFocalPoint>();

            foreach (var worklistToBioHubItemBioHubFacilityFocalPointToCopy in worklistToBioHubItemBioHubFacilityFocalPointsToCopy)
            {
                var newMaterialShippingInformationHistoryElement = new WorklistToBioHubHistoryItemBioHubFacilityFocalPoint();
                newMaterialShippingInformationHistoryElement.Id = Guid.NewGuid();
                newMaterialShippingInformationHistoryElement.WorklistToBioHubHistoryItemId = worklisttobiohubhistoryitemId;
                newMaterialShippingInformationHistoryElement.UserId = worklistToBioHubItemBioHubFacilityFocalPointToCopy.UserId;
                newMaterialShippingInformationHistoryElement.Other = worklistToBioHubItemBioHubFacilityFocalPointToCopy.Other;
                newMaterialShippingInformationHistoryElement.CreationDate = DateTime.UtcNow;
                newWorklistToBioHubHistoryItemBioHubFacilityFocalPointElements.Add(newMaterialShippingInformationHistoryElement);


            }
            WorklistToBioHubHistoryItemBioHubFacilityFocalPoints.AddRange(newWorklistToBioHubHistoryItemBioHubFacilityFocalPointElements);


            await _dbContext.SaveChangesAsync(cancellationToken);

        }

        return null;
    }



    public async Task<Errors?> LinkBookingFormsFromWorklistToBioHubItem(Guid worklisttobiohubitemId, Guid worklisttobiohubhistoryitemId, CancellationToken cancellationToken, IDbContextTransaction? transaction = null)
    {
        if (transaction != null)
        {
            _dbContext.Database.UseTransaction(transaction.GetDbTransaction());
        }
        var bookingFormsToCopy = BookingForms.Where(x => x.WorklistToBioHubItemId == worklisttobiohubitemId).Include(x => x.BookingFormPickupUsers).Include(x => x.BookingFormCourierUsers);

        if (bookingFormsToCopy.Any())
        {
            var newBookingFormElements = new List<BookingFormHistory>();
            var newBookingFormPickupUserElements = new List<BookingFormPickupUserHistory>();
            var newBookingFormCourierUserElements = new List<BookingFormCourierUserHistory>();
            foreach (var bookingFormToCopy in bookingFormsToCopy)
            {
                var newBookingFormElement = new BookingFormHistory();

                newBookingFormElement.Id = Guid.NewGuid();
                newBookingFormElement.WorklistToBioHubHistoryItemId = worklisttobiohubhistoryitemId;
                newBookingFormElement.TransportCategoryId = bookingFormToCopy.TransportCategoryId;
                newBookingFormElement.NumberOfInnerPackagingAndSize = bookingFormToCopy.NumberOfInnerPackagingAndSize;
                newBookingFormElement.TotalAmount = bookingFormToCopy.TotalAmount;
                newBookingFormElement.TotalNumberOfVials = bookingFormToCopy.TotalNumberOfVials;
                newBookingFormElement.TemperatureTransportCondition = bookingFormToCopy.TemperatureTransportCondition;
                newBookingFormElement.Date = bookingFormToCopy.Date;
                newBookingFormElement.RequestDateOfPickup = bookingFormToCopy.RequestDateOfPickup;
                newBookingFormElement.CourierId = bookingFormToCopy.CourierId;
                newBookingFormElement.CreationDate = DateTime.UtcNow;
                newBookingFormElement.EstimateDateOfPickup = bookingFormToCopy.EstimateDateOfPickup;
                newBookingFormElement.DateOfPickup = bookingFormToCopy.DateOfPickup;
                newBookingFormElement.DateOfDelivery = bookingFormToCopy.DateOfDelivery;
                newBookingFormElement.ShipmentReferenceNumber = bookingFormToCopy.ShipmentReferenceNumber;
                newBookingFormElement.TransportModeId = bookingFormToCopy.TransportModeId;
                newBookingFormElements.Add(newBookingFormElement);

                foreach (var bookingFormPickupUserToCopy in bookingFormToCopy.BookingFormPickupUsers)
                {

                    var newBookingFormPickupUserElement = new BookingFormPickupUserHistory();

                    newBookingFormPickupUserElement.Id = Guid.NewGuid();
                    newBookingFormPickupUserElement.BookingFormHistoryId = newBookingFormElement.Id;
                    newBookingFormPickupUserElement.UserId = bookingFormPickupUserToCopy.UserId;
                    newBookingFormPickupUserElement.Other = bookingFormPickupUserToCopy.Other;
                    newBookingFormPickupUserElement.CreationDate = DateTime.UtcNow;
                    newBookingFormPickupUserElements.Add(newBookingFormPickupUserElement);
                }

                foreach (var bookingFormCourierUserToCopy in bookingFormToCopy.BookingFormCourierUsers)
                {

                    var newBookingFormCourierUserElement = new BookingFormCourierUserHistory();

                    newBookingFormCourierUserElement.Id = Guid.NewGuid();
                    newBookingFormCourierUserElement.BookingFormHistoryId = newBookingFormElement.Id;
                    newBookingFormCourierUserElement.UserId = bookingFormCourierUserToCopy.UserId;
                    newBookingFormCourierUserElement.Other = bookingFormCourierUserToCopy.Other;
                    newBookingFormCourierUserElement.CreationDate = DateTime.UtcNow;
                    newBookingFormCourierUserElements.Add(newBookingFormCourierUserElement);
                }


                newBookingFormElements.Add(newBookingFormElement);
            }
            BookingFormsHistory.AddRange(newBookingFormElements);

            BookingFormPickupUsersHistory.AddRange(newBookingFormPickupUserElements);

            BookingFormCourierUsersHistory.AddRange(newBookingFormCourierUserElements);

            await _dbContext.SaveChangesAsync(cancellationToken);

        }

        return null;
    }


    public async Task<Errors?> LinkFeedbacksFromWorklistToBioHubItem(Guid worklisttobiohubitemId, Guid worklisttobiohubhistoryitemId, CancellationToken cancellationToken, IDbContextTransaction? transaction = null)
    {
        if (transaction != null)
        {
            _dbContext.Database.UseTransaction(transaction.GetDbTransaction());
        }
        var feedbacksToCopy = WorklistToBioHubItemFeedback.Where(x => x.WorklistToBioHubItemId == worklisttobiohubitemId).ToList();

        if (feedbacksToCopy.Any())
        {
            var newFeedbackElements = new List<WorklistToBioHubHistoryItemFeedback>();

            foreach (var feedbackToCopy in feedbacksToCopy)
            {
                var newFeedbackElement = new WorklistToBioHubHistoryItemFeedback();

                newFeedbackElement.Id = Guid.NewGuid();
                newFeedbackElement.WorklistToBioHubHistoryItemId = worklisttobiohubhistoryitemId;
                newFeedbackElement.Date = feedbackToCopy.Date;
                newFeedbackElement.PostedById = feedbackToCopy.PostedById;
                newFeedbackElement.Text = feedbackToCopy.Text;


                newFeedbackElements.Add(newFeedbackElement);
            }
            WorklistToBioHubHistoryItemFeedback.AddRange(newFeedbackElements);


            await _dbContext.SaveChangesAsync(cancellationToken);

        }

        return null;
    }

    public async Task<IDbContextTransaction> GetTransactionAsync()
    {
        return await _dbContext.Database.BeginTransactionAsync();
    }

}