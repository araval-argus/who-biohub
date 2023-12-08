using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Linq;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.WorklistToBioHubItems;
using WHO.BioHub.Shared.Dto;
using WHO.BioHub.Shared.Enums;
using WHO.BioHub.Shared.SeedDataConstants;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DAL.Repositories;

public class SQLWorklistToBioHubItemWriteRepository : IWorklistToBioHubItemWriteRepository
{
    private readonly BioHubDbContext _dbContext;
    private DbSet<WorklistToBioHubItem> WorklistToBioHubItems => _dbContext.WorklistToBioHubItems;
    private DbSet<WorklistToBioHubItemDocument> WorklistToBioHubItemDocuments => _dbContext.WorklistToBioHubItemDocuments;
    private DbSet<MaterialShippingInformation> MaterialShippingInformations => _dbContext.MaterialShippingInformations;
    private DbSet<MaterialClinicalDetail> MaterialClinicalDetails => _dbContext.MaterialClinicalDetails;
    private DbSet<MaterialLaboratoryAnalysisInformation> MaterialLaboratoryAnalysisInformation => _dbContext.MaterialLaboratoryAnalysisInformation;
    private DbSet<CollectedSpecimenType> CollectedSpecimenTypes => _dbContext.CollectedSpecimenTypes;

    private DbSet<WorklistToBioHubItemLaboratoryFocalPoint> WorklistToBioHubItemLaboratoryFocalPoints => _dbContext.WorklistToBioHubItemLaboratoryFocalPoints;
    private DbSet<WorklistToBioHubItemBioHubFacilityFocalPoint> WorklistToBioHubItemBioHubFacilityFocalPoints => _dbContext.WorklistToBioHubItemBioHubFacilityFocalPoints;

    private DbSet<BookingForm> BookingForms => _dbContext.BookingForms;
    private DbSet<BookingFormPickupUser> BookingFormPickupUsers => _dbContext.BookingFormPickupUsers;
    private DbSet<BookingFormCourierUser> BookingFormCourierUsers => _dbContext.BookingFormCourierUsers;
    private DbSet<WorklistToBioHubItemFeedback> WorklistToBioHubItemFeedback => _dbContext.WorklistToBioHubItemFeedback;
    private DbSet<WorklistToBioHubItemMaterial> WorklistToBioHubItemMaterials => _dbContext.WorklistToBioHubItemMaterials;
    private DbSet<MaterialCollectedSpecimenType> MaterialCollectedSpecimenTypes => _dbContext.MaterialCollectedSpecimenTypes;

    private DbSet<Material> Materials => _dbContext.Materials;

    public SQLWorklistToBioHubItemWriteRepository(BioHubDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Either<WorklistToBioHubItem, Errors>> Create(WorklistToBioHubItem worklisttobiohubitem, CancellationToken cancellationToken, IDbContextTransaction? transaction = null)
    {
        if (transaction != null)
        {
            _dbContext.Database.UseTransaction(transaction.GetDbTransaction());
        }
        await _dbContext.AddAsync(worklisttobiohubitem, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return new(worklisttobiohubitem);
    }

    public async Task<Errors?> Delete(Guid id, CancellationToken cancellationToken)
    {
        WorklistToBioHubItem lab = await WorklistToBioHubItems.FirstOrDefaultAsync(l => l.DeletedOn == null && l.Id == id, cancellationToken);
        lab.DeletedOn = DateTime.Now;
        WorklistToBioHubItems.Update(lab);

        await _dbContext.SaveChangesAsync(cancellationToken);
        return null;
    }

    public async Task<WorklistToBioHubItem> ReadForUpdate(Guid id, CancellationToken cancellationToken)
    {
        return await _dbContext.WorklistToBioHubItems
            .FirstOrDefaultAsync(l => l.DeletedOn == null && l.Id == id, cancellationToken);
    }

    public async Task<Errors?> Update(WorklistToBioHubItem worklisttobiohubitem, CancellationToken cancellationToken, IDbContextTransaction? transaction = null)
    {
        if (transaction != null)
        {
            _dbContext.Database.UseTransaction(transaction.GetDbTransaction());
        }
        WorklistToBioHubItems.Update(worklisttobiohubitem);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return null;
    }

    public async Task<Errors?> LinkDocument(Guid worklisttobiohubitemId, Guid? documentId, DocumentFileType type, CancellationToken cancellationToken, bool? isDocumentFile = true, IDbContextTransaction? transaction = null, bool? replaceExistingType = true)
    {
        if (transaction != null)
        {
            _dbContext.Database.UseTransaction(transaction.GetDbTransaction());
        }

        if (replaceExistingType == true)
        {
            var elementToRemove = WorklistToBioHubItemDocuments.Where(x => x.WorklistToBioHubItemId == worklisttobiohubitemId && x.Type == type && x.IsDocumentFile == isDocumentFile).FirstOrDefault();

            if (elementToRemove != default)
            {
                WorklistToBioHubItemDocuments.Remove(elementToRemove);
            }
        }

        if (documentId != null && documentId != Guid.Empty)
        {
            var newElement = new WorklistToBioHubItemDocument();
            newElement.WorklistToBioHubItemId = worklisttobiohubitemId;
            newElement.DocumentId = documentId;
            newElement.Type = type;
            newElement.IsDocumentFile = isDocumentFile;
            await _dbContext.AddAsync(newElement, cancellationToken);
        }
        await _dbContext.SaveChangesAsync(cancellationToken);

        return null;
    }

    public async Task<Errors?> UnlinkDocument(Guid worklisttobiohubitemId, Guid? documentId, CancellationToken cancellationToken, bool? isDocumentFile = true, IDbContextTransaction? transaction = null)
    {
        if (transaction != null)
        {
            _dbContext.Database.UseTransaction(transaction.GetDbTransaction());
        }
        var elementToRemove = WorklistToBioHubItemDocuments.Where(x => x.WorklistToBioHubItemId == worklisttobiohubitemId && x.IsDocumentFile == isDocumentFile && x.DocumentId == documentId).FirstOrDefault();

        if (elementToRemove != default)
        {
            WorklistToBioHubItemDocuments.Remove(elementToRemove);
        }

        await _dbContext.SaveChangesAsync(cancellationToken);

        return null;
    }


    public async Task<Errors?> LinkMaterialShippingInformation(Guid worklisttobiohubitemId, IEnumerable<MaterialShippingInformationDto>? materialShippingInformations, CancellationToken cancellationToken, IDbContextTransaction? transaction = null)
    {
        if (transaction != null)
        {
            _dbContext.Database.UseTransaction(transaction.GetDbTransaction());
        }
        var materialShippingInformationsToRemove = MaterialShippingInformations.Where(x => x.WorklistToBioHubItemId == worklisttobiohubitemId).ToList();

        if (materialShippingInformationsToRemove.Any())
        {
            foreach (var materialShippingInformationToRemove in materialShippingInformationsToRemove)
            {
                var materialClinicalDetailsToRemove = MaterialClinicalDetails.Where(x => x.MaterialShippingInformationId == materialShippingInformationToRemove.Id).ToList();
                if (materialClinicalDetailsToRemove.Any())
                {
                    MaterialClinicalDetails.RemoveRange(materialClinicalDetailsToRemove);
                }

                var materialLaboratoryAnalysisInformationToRemove = MaterialLaboratoryAnalysisInformation.Where(x => x.MaterialShippingInformationId == materialShippingInformationToRemove.Id).ToList();
                if (materialLaboratoryAnalysisInformationToRemove.Any())
                {
                    foreach (var elem in materialLaboratoryAnalysisInformationToRemove)
                    {
                        var collectedSpecimenTypes = CollectedSpecimenTypes.Where(x => elem.Id == x.MaterialLaboratoryAnalysisInformationId);
                        if (collectedSpecimenTypes.Any())
                        {
                            CollectedSpecimenTypes.RemoveRange(collectedSpecimenTypes);
                        }
                    }

                    MaterialLaboratoryAnalysisInformation.RemoveRange(materialLaboratoryAnalysisInformationToRemove);
                }
            }
            MaterialShippingInformations.RemoveRange(materialShippingInformationsToRemove);
        }


        if (materialShippingInformations != null && materialShippingInformations.Any())
        {
            var newMaterialShippingInformationElements = new List<MaterialShippingInformation>();
            var newMaterialClinicalDetailsElements = new List<MaterialClinicalDetail>();
            var newMaterialLaboratoryAnalysisInformationElements = new List<MaterialLaboratoryAnalysisInformation>();
            var newCollectedSpecimenTypesElements = new List<CollectedSpecimenType>();

            foreach (var materialShippingInformation in materialShippingInformations)
            {
                var newMaterialShippingInformationElement = new MaterialShippingInformation();
                newMaterialShippingInformationElement.Id = Guid.NewGuid();
                newMaterialShippingInformationElement.WorklistToBioHubItemId = worklisttobiohubitemId;
                newMaterialShippingInformationElement.MaterialNumber = materialShippingInformation.MaterialNumber;
                newMaterialShippingInformationElement.MaterialProductId = materialShippingInformation.MaterialProductId;
                newMaterialShippingInformationElement.TransportCategoryId = materialShippingInformation.TransportCategoryId;
                newMaterialShippingInformationElement.Quantity = materialShippingInformation.Quantity;
                newMaterialShippingInformationElement.Amount = materialShippingInformation.Amount;
                newMaterialShippingInformationElement.Condition = materialShippingInformation.Condition;
                newMaterialShippingInformationElement.AdditionalInformation = materialShippingInformation.AdditionalInformation;
                newMaterialShippingInformationElement.CreationDate = DateTime.UtcNow;
                newMaterialShippingInformationElements.Add(newMaterialShippingInformationElement);
                foreach (var materialClinicalDetail in materialShippingInformation.MaterialClinicalDetails)
                {
                    var newMaterialClinicalDetail = new MaterialClinicalDetail();
                    newMaterialClinicalDetail.Id = Guid.NewGuid();
                    newMaterialClinicalDetail.MaterialShippingInformationId = newMaterialShippingInformationElement.Id;
                    newMaterialClinicalDetail.Age = materialClinicalDetail.Age;
                    newMaterialClinicalDetail.Gender = materialClinicalDetail.Gender;
                    newMaterialClinicalDetail.CreationDate = DateTime.UtcNow;
                    newMaterialClinicalDetail.MaterialNumber = materialClinicalDetail.MaterialNumber;
                    newMaterialClinicalDetail.CollectionDate = materialClinicalDetail.CollectionDate;
                    newMaterialClinicalDetail.IsolationHostTypeId = materialClinicalDetail.IsolationHostTypeId;
                    newMaterialClinicalDetail.Location = materialClinicalDetail.Location;
                    newMaterialClinicalDetail.PatientStatus = materialClinicalDetail.PatientStatus;
                    newMaterialClinicalDetail.Note = materialClinicalDetail.Note;
                    newMaterialClinicalDetail.Condition = materialClinicalDetail.Condition;
                    newMaterialClinicalDetailsElements.Add(newMaterialClinicalDetail);
                }

                foreach (var materialLaboratoryAnalysisInformationDtoElement in materialShippingInformation.MaterialLaboratoryAnalysisInformation)
                {
                    var newMaterialLaboratoryAnalysisInformation = new MaterialLaboratoryAnalysisInformation();
                    newMaterialLaboratoryAnalysisInformation.Id = Guid.NewGuid();
                    newMaterialLaboratoryAnalysisInformation.MaterialShippingInformationId = newMaterialShippingInformationElement.Id;
                    newMaterialLaboratoryAnalysisInformation.UnitOfMeasureId = materialLaboratoryAnalysisInformationDtoElement.UnitOfMeasureId;
                    newMaterialLaboratoryAnalysisInformation.Temperature = materialLaboratoryAnalysisInformationDtoElement.Temperature;
                    newMaterialLaboratoryAnalysisInformation.VirusConcentration = materialLaboratoryAnalysisInformationDtoElement.VirusConcentration;
                    newMaterialLaboratoryAnalysisInformation.CreationDate = DateTime.UtcNow;
                    newMaterialLaboratoryAnalysisInformation.MaterialNumber = materialLaboratoryAnalysisInformationDtoElement.MaterialNumber;
                    newMaterialLaboratoryAnalysisInformation.FreezingDate = materialLaboratoryAnalysisInformationDtoElement.FreezingDate;
                    
                    newMaterialLaboratoryAnalysisInformation.CollectedSpecimenTypes = new List<CollectedSpecimenType>();

                    if (newMaterialShippingInformationElement.MaterialProductId == Guid.Parse(SeedDataConstants.CulturedIsolateProductId))
                    {
                        newMaterialLaboratoryAnalysisInformation.CulturingCellLine = materialLaboratoryAnalysisInformationDtoElement.CulturingCellLine;
                        newMaterialLaboratoryAnalysisInformation.CulturingPassagesNumber = materialLaboratoryAnalysisInformationDtoElement.CulturingPassagesNumber;
                    }

                    else if (newMaterialShippingInformationElement.MaterialProductId == Guid.Parse(SeedDataConstants.ClinicalSpecimenProductId))
                    {
                        foreach (var specimenTypeId in materialLaboratoryAnalysisInformationDtoElement.CollectedSpecimenTypes)
                        {
                            CollectedSpecimenType newCollectedSpecimenTypesElement = new CollectedSpecimenType();
                            newCollectedSpecimenTypesElement.SpecimenTypeId = specimenTypeId;
                            newCollectedSpecimenTypesElement.MaterialLaboratoryAnalysisInformationId = newMaterialLaboratoryAnalysisInformation.Id;
                            newCollectedSpecimenTypesElements.Add(newCollectedSpecimenTypesElement);
                        }
                        newMaterialLaboratoryAnalysisInformation.BrandOfTransportMedium = materialLaboratoryAnalysisInformationDtoElement.BrandOfTransportMedium;
                        newMaterialLaboratoryAnalysisInformation.TypeOfTransportMedium = materialLaboratoryAnalysisInformationDtoElement.TypeOfTransportMedium;
                    }

                    newMaterialLaboratoryAnalysisInformation.GSDUploadedToDatabase = materialLaboratoryAnalysisInformationDtoElement.GSDUploadedToDatabase;

                    if (materialLaboratoryAnalysisInformationDtoElement.GSDUploadedToDatabase == YesNoOption.Yes)
                    {
                        newMaterialLaboratoryAnalysisInformation.DatabaseUsedForGSDUploadingId = materialLaboratoryAnalysisInformationDtoElement.DatabaseUsedForGSDUploadingId;
                        newMaterialLaboratoryAnalysisInformation.AccessionNumberInGSDDatabase = materialLaboratoryAnalysisInformationDtoElement.AccessionNumberInGSDDatabase;
                    }

                    newMaterialLaboratoryAnalysisInformationElements.Add(newMaterialLaboratoryAnalysisInformation);
                }

            }
            MaterialShippingInformations.AddRange(newMaterialShippingInformationElements);
            MaterialClinicalDetails.AddRange(newMaterialClinicalDetailsElements);
            MaterialLaboratoryAnalysisInformation.AddRange(newMaterialLaboratoryAnalysisInformationElements);
            CollectedSpecimenTypes.AddRange(newCollectedSpecimenTypesElements);
        }
        await _dbContext.SaveChangesAsync(cancellationToken);

        return null;
    }



    public async Task<Errors?> LinkLaboratoryFocalPoints(Guid worklisttobiohubitemId, IEnumerable<WorklistItemUserDto>? worklistToBioHubItemLaboratoryFocalPoints, CancellationToken cancellationToken, IDbContextTransaction? transaction = null)
    {
        if (transaction != null)
        {
            _dbContext.Database.UseTransaction(transaction.GetDbTransaction());
        }

        var worklistToBioHubItemLaboratoryFocalPointsToRemove = WorklistToBioHubItemLaboratoryFocalPoints.Where(x => x.WorklistToBioHubItemId == worklisttobiohubitemId).ToList();

        if (worklistToBioHubItemLaboratoryFocalPointsToRemove.Any())
        {
            WorklistToBioHubItemLaboratoryFocalPoints.RemoveRange(worklistToBioHubItemLaboratoryFocalPointsToRemove);
        }


        if (worklistToBioHubItemLaboratoryFocalPoints != null && worklistToBioHubItemLaboratoryFocalPoints.Any())
        {
            var newWorklistToBioHubHistoryItemLaboratoryFocalPointElements = new List<WorklistToBioHubItemLaboratoryFocalPoint>();

            foreach (var worklistToBioHubItemLaboratoryFocalPoint in worklistToBioHubItemLaboratoryFocalPoints)
            {
                var newWorklistToBioHubItemLaboratoryFocalPoint = new WorklistToBioHubItemLaboratoryFocalPoint();
                newWorklistToBioHubItemLaboratoryFocalPoint.Id = Guid.NewGuid();
                newWorklistToBioHubItemLaboratoryFocalPoint.WorklistToBioHubItemId = worklisttobiohubitemId;
                newWorklistToBioHubItemLaboratoryFocalPoint.UserId = worklistToBioHubItemLaboratoryFocalPoint.UserId;
                newWorklistToBioHubItemLaboratoryFocalPoint.Other = worklistToBioHubItemLaboratoryFocalPoint.Other;
                newWorklistToBioHubItemLaboratoryFocalPoint.CreationDate = DateTime.UtcNow;
                newWorklistToBioHubHistoryItemLaboratoryFocalPointElements.Add(newWorklistToBioHubItemLaboratoryFocalPoint);

            }
            WorklistToBioHubItemLaboratoryFocalPoints.AddRange(newWorklistToBioHubHistoryItemLaboratoryFocalPointElements);


            await _dbContext.SaveChangesAsync(cancellationToken);

        }

        return null;
    }


    public async Task<Errors?> LinkBioHubFacilityFocalPoints(Guid worklisttobiohubitemId, IEnumerable<WorklistItemUserDto>? worklistToBioHubItemBioHubFacilityFocalPoints, CancellationToken cancellationToken, IDbContextTransaction? transaction = null)
    {
        if (transaction != null)
        {
            _dbContext.Database.UseTransaction(transaction.GetDbTransaction());
        }

        var worklistToBioHubItemBioHubFacilityFocalPointsToRemove = WorklistToBioHubItemBioHubFacilityFocalPoints.Where(x => x.WorklistToBioHubItemId == worklisttobiohubitemId).ToList();

        if (worklistToBioHubItemBioHubFacilityFocalPointsToRemove.Any())
        {
            WorklistToBioHubItemBioHubFacilityFocalPoints.RemoveRange(worklistToBioHubItemBioHubFacilityFocalPointsToRemove);
        }


        if (worklistToBioHubItemBioHubFacilityFocalPoints != null && worklistToBioHubItemBioHubFacilityFocalPoints.Any())
        {
            var newWorklistToBioHubHistoryItemBioHubFacilityFocalPointElements = new List<WorklistToBioHubItemBioHubFacilityFocalPoint>();

            foreach (var worklistToBioHubItemBioHubFacilityFocalPoint in worklistToBioHubItemBioHubFacilityFocalPoints)
            {
                var newWorklistToBioHubItemBioHubFacilityFocalPointElement = new WorklistToBioHubItemBioHubFacilityFocalPoint();
                newWorklistToBioHubItemBioHubFacilityFocalPointElement.Id = Guid.NewGuid();
                newWorklistToBioHubItemBioHubFacilityFocalPointElement.WorklistToBioHubItemId = worklisttobiohubitemId;
                newWorklistToBioHubItemBioHubFacilityFocalPointElement.UserId = worklistToBioHubItemBioHubFacilityFocalPoint.UserId;
                newWorklistToBioHubItemBioHubFacilityFocalPointElement.Other = worklistToBioHubItemBioHubFacilityFocalPoint.Other;
                newWorklistToBioHubItemBioHubFacilityFocalPointElement.CreationDate = DateTime.UtcNow;
                newWorklistToBioHubHistoryItemBioHubFacilityFocalPointElements.Add(newWorklistToBioHubItemBioHubFacilityFocalPointElement);

            }
            WorklistToBioHubItemBioHubFacilityFocalPoints.AddRange(newWorklistToBioHubHistoryItemBioHubFacilityFocalPointElements);


            await _dbContext.SaveChangesAsync(cancellationToken);

        }

        return null;
    }

    public async Task<Errors?> UpdateBookingFormDeliveryProperties(Guid worklisttobiohubitemId, IEnumerable<BookingFormOfSMTADto>? bookingForms, CancellationToken cancellationToken, IDbContextTransaction? transaction = null)
    {
        if (transaction != null)
        {
            _dbContext.Database.UseTransaction(transaction.GetDbTransaction());
        }
        var bookingFormsToUpdate = BookingForms.Where(x => x.WorklistToBioHubItemId == worklisttobiohubitemId).ToList();
        if (bookingFormsToUpdate.Any())
        {
            foreach (var bookingFormToUpdate in bookingFormsToUpdate)
            {
                var newBookingForm = bookingForms.Where(x => x.Id == bookingFormToUpdate.Id).FirstOrDefault();
                if (newBookingForm != null)
                {
                    bookingFormToUpdate.DateOfPickup = newBookingForm.DateOfPickup;
                    bookingFormToUpdate.DateOfDelivery = newBookingForm.DateOfDelivery;
                    bookingFormToUpdate.ShipmentReferenceNumber = newBookingForm.ShipmentReferenceNumber;
                    bookingFormToUpdate.TransportModeId = newBookingForm.TransportModeId;
                    BookingForms.Update(bookingFormToUpdate);
                }
            }


        }
        await _dbContext.SaveChangesAsync(cancellationToken);

        return null;
    }

    public async Task<Errors?> LinkBookingForm(Guid worklisttobiohubitemId, IEnumerable<BookingFormOfSMTADto>? bookingForms, CancellationToken cancellationToken, IDbContextTransaction? transaction = null)
    {
        if (transaction != null)
        {
            _dbContext.Database.UseTransaction(transaction.GetDbTransaction());
        }
        var bookingFormsToRemove = BookingForms.Where(x => x.WorklistToBioHubItemId == worklisttobiohubitemId).ToList();

        if (bookingFormsToRemove.Any())
        {
            foreach (var bookingFormToRemove in bookingFormsToRemove)
            {
                var bookingFormPickupUsersToRemove = BookingFormPickupUsers.Where(x => x.BookingFormId == bookingFormToRemove.Id).ToList();
                if (bookingFormPickupUsersToRemove.Any())
                {
                    BookingFormPickupUsers.RemoveRange(bookingFormPickupUsersToRemove);
                }
                var bookingFormCourierUsersToRemove = BookingFormCourierUsers.Where(x => x.BookingFormId == bookingFormToRemove.Id).ToList();
                if (bookingFormCourierUsersToRemove.Any())
                {
                    BookingFormCourierUsers.RemoveRange(bookingFormCourierUsersToRemove);
                }
            }
            BookingForms.RemoveRange(bookingFormsToRemove);
        }

        if (bookingForms != null && bookingForms.Any())
        {
            var newBookingFormElements = new List<BookingForm>();
            var newBookingFormPickupUsersElements = new List<BookingFormPickupUser>();
            var newBookingFormCourierUsersElements = new List<BookingFormCourierUser>();

            foreach (var bookingForm in bookingForms)
            {
                var newBookingFormElement = new BookingForm();
                newBookingFormElement.Id = Guid.NewGuid();
                newBookingFormElement.WorklistToBioHubItemId = worklisttobiohubitemId;
                //newBookingFormElement.MaterialProductId = bookingForm.MaterialProductId;
                newBookingFormElement.TransportCategoryId = bookingForm.TransportCategoryId;
                newBookingFormElement.NumberOfInnerPackagingAndSize = bookingForm.NumberOfInnerPackagingAndSize;
                newBookingFormElement.TotalAmount = bookingForm.TotalAmount;
                newBookingFormElement.TotalNumberOfVials = bookingForm.TotalNumberOfVials;
                newBookingFormElement.TemperatureTransportCondition = bookingForm.TemperatureTransportCondition;
                newBookingFormElement.Date = bookingForm.Date;
                newBookingFormElement.RequestDateOfPickup = bookingForm.RequestDateOfPickup;
                newBookingFormElement.CreationDate = DateTime.UtcNow;
                newBookingFormElement.CourierId = bookingForm.CourierId;
                newBookingFormElement.EstimateDateOfPickup = bookingForm.EstimateDateOfPickup;
                newBookingFormElement.DateOfPickup = bookingForm.DateOfPickup;
                newBookingFormElement.DateOfDelivery = bookingForm.DateOfDelivery;
                newBookingFormElement.ShipmentReferenceNumber = bookingForm.ShipmentReferenceNumber;

                newBookingFormElements.Add(newBookingFormElement);

                if (bookingForm.BookingFormPickupUsers != null)
                {

                    foreach (var bookingFormPickupUser in bookingForm.BookingFormPickupUsers)
                    {
                        var newBookingFormPickupUserElement = new BookingFormPickupUser();
                        newBookingFormPickupUserElement.Id = Guid.NewGuid();
                        newBookingFormPickupUserElement.BookingFormId = newBookingFormElement.Id;
                        newBookingFormPickupUserElement.UserId = bookingFormPickupUser.UserId;
                        newBookingFormPickupUserElement.Other = bookingFormPickupUser.Other;
                        newBookingFormPickupUserElement.CreationDate = DateTime.UtcNow;
                        newBookingFormPickupUsersElements.Add(newBookingFormPickupUserElement);

                    }
                }

                if (bookingForm.BookingFormCourierUsers != null)
                {

                    foreach (var bookingFormCourierUser in bookingForm.BookingFormCourierUsers)
                    {
                        var newBookingFormCourierUserElement = new BookingFormCourierUser();
                        newBookingFormCourierUserElement.Id = Guid.NewGuid();
                        newBookingFormCourierUserElement.BookingFormId = newBookingFormElement.Id;
                        newBookingFormCourierUserElement.UserId = bookingFormCourierUser.UserId;
                        newBookingFormCourierUserElement.Other = bookingFormCourierUser.Other;
                        newBookingFormCourierUserElement.CreationDate = DateTime.UtcNow;
                        newBookingFormCourierUsersElements.Add(newBookingFormCourierUserElement);

                    }
                }
            }
            BookingForms.AddRange(newBookingFormElements);

            if (newBookingFormPickupUsersElements.Any())
            {
                BookingFormPickupUsers.AddRange(newBookingFormPickupUsersElements);
            }
            if (newBookingFormCourierUsersElements.Any())
            {
                BookingFormCourierUsers.AddRange(newBookingFormCourierUsersElements);
            }
        }
        await _dbContext.SaveChangesAsync(cancellationToken);

        return null;
    }

    public async Task<Errors?> LinkBookingFormPickupUsers(Guid bookingFormId, IEnumerable<WorklistItemUserDto>? bookingFormPickupUsers, CancellationToken cancellationToken, IDbContextTransaction? transaction = null)
    {
        if (transaction != null)
        {
            _dbContext.Database.UseTransaction(transaction.GetDbTransaction());
        }

        var bookingFormPickupUsersToRemove = BookingFormPickupUsers.Where(x => x.BookingFormId == bookingFormId).ToList();

        if (bookingFormPickupUsersToRemove.Any())
        {
            BookingFormPickupUsers.RemoveRange(bookingFormPickupUsersToRemove);
        }


        if (bookingFormPickupUsers != null && bookingFormPickupUsers.Any())
        {
            var newBookingFormPickupUsersElements = new List<BookingFormPickupUser>();

            foreach (var bookingFormPickupUser in bookingFormPickupUsers)
            {
                var newBookingFormPickupUserElement = new BookingFormPickupUser();
                newBookingFormPickupUserElement.Id = Guid.NewGuid();
                newBookingFormPickupUserElement.BookingFormId = bookingFormId;
                newBookingFormPickupUserElement.UserId = bookingFormPickupUser.UserId;
                newBookingFormPickupUserElement.Other = bookingFormPickupUser.Other;
                newBookingFormPickupUserElement.CreationDate = DateTime.UtcNow;
                newBookingFormPickupUsersElements.Add(newBookingFormPickupUserElement);

            }
            BookingFormPickupUsers.AddRange(newBookingFormPickupUsersElements);


            await _dbContext.SaveChangesAsync(cancellationToken);

        }

        return null;
    }

    public async Task<Errors?> AddFeedback(Guid worklisttobiohubitemId, FeedbackDto? feedback, CancellationToken cancellationToken, IDbContextTransaction? transaction = null)
    {
        if (transaction != null)
        {
            _dbContext.Database.UseTransaction(transaction.GetDbTransaction());
        }


        if (feedback != null)
        {
            var newFeedback = new WorklistToBioHubItemFeedback();

            newFeedback.Id = Guid.NewGuid();
            newFeedback.WorklistToBioHubItemId = worklisttobiohubitemId;
            newFeedback.Text = feedback.Text;
            newFeedback.Date = DateTime.UtcNow;
            newFeedback.PostedById = feedback.PostedById;

            WorklistToBioHubItemFeedback.Add(newFeedback);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        return null;
    }

    public async Task<Errors?> LinkMaterials(Guid worklisttobiohubitemId, IEnumerable<WorklistToBioHubItemMaterialDto>? worklistToBioHubItemMaterialsDto, CancellationToken cancellationToken, IDbContextTransaction? transaction = null)
    {
        if (transaction != null)
        {
            _dbContext.Database.UseTransaction(transaction.GetDbTransaction());
        }
        var worklistToBioHubItemMaterialsToRemove = WorklistToBioHubItemMaterials.Where(x => x.WorklistToBioHubItemId == worklisttobiohubitemId).ToList();

        if (worklistToBioHubItemMaterialsToRemove.Any())
        {
            WorklistToBioHubItemMaterials.RemoveRange(worklistToBioHubItemMaterialsToRemove);
        }

        if (worklistToBioHubItemMaterialsDto != null && worklistToBioHubItemMaterialsDto.Any())
        {
            var newWorklistToBioHubItemElements = new List<WorklistToBioHubItemMaterial>();
            foreach (var worklistToBioHubItemMaterialDto in worklistToBioHubItemMaterialsDto)
            {
                var newWorklistToBioHubItemMaterialElement = new WorklistToBioHubItemMaterial();
                newWorklistToBioHubItemMaterialElement.Id = Guid.NewGuid();
                newWorklistToBioHubItemMaterialElement.WorklistToBioHubItemId = worklisttobiohubitemId;
                newWorklistToBioHubItemMaterialElement.MaterialId = worklistToBioHubItemMaterialDto.MaterialId;
                newWorklistToBioHubItemMaterialElement.CreationDate = DateTime.UtcNow;
                newWorklistToBioHubItemElements.Add(newWorklistToBioHubItemMaterialElement);
            }
            WorklistToBioHubItemMaterials.AddRange(newWorklistToBioHubItemElements);
        }
        await _dbContext.SaveChangesAsync(cancellationToken);

        return null;
    }

    public async Task<Errors?> CreateBMEPPMaterialFromWorklistToBioHubItem(Guid worklisttobiohubitemId, DateTime startingDate, CancellationToken cancellationToken, IDbContextTransaction? transaction = null)
    {
        if (transaction != null)
        {
            _dbContext.Database.UseTransaction(transaction.GetDbTransaction());
        }

        var workListToBioHubItem = await WorklistToBioHubItems
            .AsNoTracking()
            .AsSplitQuery()
            .Include(x => x.RequestInitiationToBioHubFacility)
            .Include(x => x.BookingForms)
            .FirstOrDefaultAsync(x => x.Id == worklisttobiohubitemId, cancellationToken);

        var ownerBioHubFacilityId = workListToBioHubItem.RequestInitiationToBioHubFacilityId;

        var isPast = workListToBioHubItem.IsPast ?? false;

        var abbreviation = workListToBioHubItem.RequestInitiationToBioHubFacility.Abbreviation;

        DateTime firstDateOfTheYear = new DateTime(DateTime.UtcNow.Year, 1, 1);

        var totalMaterials = await Materials.Select(x => new { x.ReferenceNumber, x.OwnerBioHubFacilityId, x.CreationDate, x.IsPast }).ToListAsync(cancellationToken);

        int nextReferenceNumber = totalMaterials.Where(x => x.OwnerBioHubFacilityId == ownerBioHubFacilityId && x.CreationDate >= firstDateOfTheYear && x.IsPast == isPast).Count();
      
        var materialShippingInformations = await MaterialShippingInformations
            .AsNoTracking()
            .AsSplitQuery()
            .Include(x => x.MaterialLaboratoryAnalysisInformation)
            .Include(x => x.MaterialClinicalDetails)
            .Where(x => x.WorklistToBioHubItemId == worklisttobiohubitemId)
            .ToListAsync(cancellationToken);

        var newBMEPPMaterialList = new List<Material>();
        var newWorklistToBioHubItemElements = new List<WorklistToBioHubItemMaterial>();

        var newMaterialCollectedSpecimenTypes = new List<MaterialCollectedSpecimenType>();

        foreach (var materialShippingInformation in materialShippingInformations)
        {
            foreach (var materialClinicalDetail in materialShippingInformation.MaterialClinicalDetails)
            {

                nextReferenceNumber += 1;

                var materialLaboratoryAnalysisInformationElement = materialShippingInformation.MaterialLaboratoryAnalysisInformation.Where(x => x.MaterialNumber == materialClinicalDetail.MaterialNumber).FirstOrDefault();
                             

                var referenceNumber = $"{DateTime.UtcNow.Year.ToString()}-WHO-{abbreviation}-{nextReferenceNumber.ToString("D3")}";

                if (isPast)
                {
                    referenceNumber += "-PAST";
                }

                while (totalMaterials.Select(x => x.ReferenceNumber).Contains(referenceNumber))
                {
                    nextReferenceNumber += 1;

                    referenceNumber = $"{DateTime.UtcNow.Year.ToString()}-WHO-{abbreviation}-{nextReferenceNumber.ToString("D3")}";
                    
                    if (isPast)
                    {
                        referenceNumber += "-PAST";
                    }
                }              


                var newBMEPPMaterial = new Material();

                newBMEPPMaterial.Id = Guid.NewGuid();
                newBMEPPMaterial.Gender = materialClinicalDetail.Gender;
                newBMEPPMaterial.Age = materialClinicalDetail.Age;
                newBMEPPMaterial.CollectionDate = materialClinicalDetail.CollectionDate;
                newBMEPPMaterial.IsolationHostTypeId = materialClinicalDetail.IsolationHostTypeId;
                newBMEPPMaterial.Location = materialClinicalDetail.Location;
                newBMEPPMaterial.PatientStatus = materialClinicalDetail.PatientStatus;
                newBMEPPMaterial.ReferenceNumber = referenceNumber;
                newBMEPPMaterial.Name = referenceNumber;
                newBMEPPMaterial.CreationDate = DateTime.UtcNow;
                newBMEPPMaterial.StartingDate = startingDate;
                newBMEPPMaterial.ProviderLaboratoryId = workListToBioHubItem.RequestInitiationFromLaboratoryId;
                newBMEPPMaterial.OriginalProductTypeId = materialShippingInformation.MaterialProductId;                
                newBMEPPMaterial.TransportCategoryId = materialShippingInformation.TransportCategoryId;
                newBMEPPMaterial.Status = MaterialStatus.WaitingForBioHubFacilityCompletion;
                newBMEPPMaterial.OwnerBioHubFacilityId = workListToBioHubItem.RequestInitiationToBioHubFacilityId;
                newBMEPPMaterial.CurrentNumberOfVials = 0;
                newBMEPPMaterial.ShipmentNumberOfVials = materialShippingInformation.Quantity;
                newBMEPPMaterial.ShipmentAmount = materialShippingInformation.Amount;
                newBMEPPMaterial.SampleId = materialShippingInformation.MaterialNumber;
                newBMEPPMaterial.WarningEmailCurrentNumberOfVialsThreshold = 10;

                var bookingForm = workListToBioHubItem.BookingForms.Where(x => x.TransportCategoryId == materialShippingInformation.TransportCategoryId).FirstOrDefault();
                newBMEPPMaterial.DateOfBMEPPReceipt = bookingForm.DateOfDelivery;

                newBMEPPMaterial.ShipmentMaterialCondition = materialClinicalDetail.Condition;
                newBMEPPMaterial.ShipmentMaterialConditionNote = materialClinicalDetail.Note;



                newBMEPPMaterial.GeneticSequenceDataId = materialLaboratoryAnalysisInformationElement.DatabaseUsedForGSDUploadingId;
                newBMEPPMaterial.DatabaseAccessionId = materialLaboratoryAnalysisInformationElement.AccessionNumberInGSDDatabase;
                newBMEPPMaterial.LastOperationDate = workListToBioHubItem.OperationDate;

                newBMEPPMaterial.VirusConcentration = materialLaboratoryAnalysisInformationElement.VirusConcentration;
                newBMEPPMaterial.FreezingDate = materialLaboratoryAnalysisInformationElement.FreezingDate;
                newBMEPPMaterial.ShipmentTemperature = materialLaboratoryAnalysisInformationElement.Temperature;
                newBMEPPMaterial.ShipmentUnitOfMeasureId = materialLaboratoryAnalysisInformationElement.UnitOfMeasureId;
                newBMEPPMaterial.CulturingCellLine = materialLaboratoryAnalysisInformationElement.CulturingCellLine;
                newBMEPPMaterial.CulturingPassagesNumber = materialLaboratoryAnalysisInformationElement.CulturingPassagesNumber;
                newBMEPPMaterial.TypeOfTransportMedium = materialLaboratoryAnalysisInformationElement.TypeOfTransportMedium;
                newBMEPPMaterial.BrandOfTransportMedium = materialLaboratoryAnalysisInformationElement.BrandOfTransportMedium;

                newBMEPPMaterial.IsPast = isPast;

                if (materialLaboratoryAnalysisInformationElement.CollectedSpecimenTypes != null && materialLaboratoryAnalysisInformationElement.CollectedSpecimenTypes.Any())
                {
                    foreach (var elem in materialLaboratoryAnalysisInformationElement.CollectedSpecimenTypes)
                    {
                        var newMaterialSpecimenType = new MaterialCollectedSpecimenType();
                        newMaterialSpecimenType.Id = Guid.NewGuid();
                        newMaterialSpecimenType.MaterialId = newBMEPPMaterial.Id;
                        newMaterialSpecimenType.SpecimenTypeId = elem.SpecimenTypeId;
                        newMaterialCollectedSpecimenTypes.Add(newMaterialSpecimenType);

                    }
                }

                newBMEPPMaterialList.Add(newBMEPPMaterial);


                var newWorklistToBioHubItemMaterialElement = new WorklistToBioHubItemMaterial();
                newWorklistToBioHubItemMaterialElement.Id = Guid.NewGuid();
                newWorklistToBioHubItemMaterialElement.WorklistToBioHubItemId = worklisttobiohubitemId;
                newWorklistToBioHubItemMaterialElement.MaterialId = newBMEPPMaterial.Id;
                newWorklistToBioHubItemMaterialElement.CreationDate = DateTime.UtcNow;
                newWorklistToBioHubItemElements.Add(newWorklistToBioHubItemMaterialElement);

            }
        }

        Materials.AddRange(newBMEPPMaterialList);
        WorklistToBioHubItemMaterials.AddRange(newWorklistToBioHubItemElements);
        MaterialCollectedSpecimenTypes.AddRange(newMaterialCollectedSpecimenTypes);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return null;
    }

    public async Task<IDbContextTransaction> BeginTransactionAsync()
    {
        return await _dbContext.Database.BeginTransactionAsync();
    }

}