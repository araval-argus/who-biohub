using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.WorklistFromBioHubItems;
using WHO.BioHub.Shared.Dto;
using WHO.BioHub.Shared.Enums;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DAL.Repositories;

public class SQLWorklistFromBioHubItemWriteRepository : IWorklistFromBioHubItemWriteRepository
{
    private readonly BioHubDbContext _dbContext;
    private DbSet<WorklistFromBioHubItem> WorklistFromBioHubItems => _dbContext.WorklistFromBioHubItems;
    private DbSet<WorklistFromBioHubItemDocument> WorklistFromBioHubItemDocuments => _dbContext.WorklistFromBioHubItemDocuments;
    private DbSet<WorklistFromBioHubItemMaterial> WorklistFromBioHubItemMaterials => _dbContext.WorklistFromBioHubItemMaterials;

    private DbSet<BookingForm> BookingForms => _dbContext.BookingForms;
    private DbSet<BookingFormPickupUser> BookingFormPickupUsers => _dbContext.BookingFormPickupUsers;
    private DbSet<BookingFormCourierUser> BookingFormCourierUsers => _dbContext.BookingFormCourierUsers;
    private DbSet<WorklistFromBioHubItemFeedback> WorklistFromBioHubItemFeedback => _dbContext.WorklistFromBioHubItemFeedback;

    private DbSet<WorklistFromBioHubItemLaboratoryFocalPoint> WorklistFromBioHubItemLaboratoryFocalPoints => _dbContext.WorklistFromBioHubItemLaboratoryFocalPoints;
    private DbSet<WorklistFromBioHubItemAnnex2OfSMTA2Condition> WorklistFromBioHubItemAnnex2OfSMTA2Conditions => _dbContext.WorklistFromBioHubItemAnnex2OfSMTA2Conditions;

    private DbSet<WorklistFromBioHubItemBiosafetyChecklistOfSMTA2> WorklistFromBioHubItemBiosafetyChecklistOfSMTA2s => _dbContext.WorklistFromBioHubItemBiosafetyChecklistOfSMTA2s;
    private DbSet<WorklistFromBioHubItemBiosafetyChecklistThreadComment> WorklistFromBioHubItemBiosafetyChecklistOfSMTA2Comments => _dbContext.WorklistFromBioHubItemBiosafetyChecklistThreadComments;
    private DbSet<Material> Materials => _dbContext.Materials;

    public SQLWorklistFromBioHubItemWriteRepository(BioHubDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Either<WorklistFromBioHubItem, Errors>> Create(WorklistFromBioHubItem worklistfrombiohubitem, CancellationToken cancellationToken, IDbContextTransaction? transaction = null)
    {
        if (transaction != null)
        {
            _dbContext.Database.UseTransaction(transaction.GetDbTransaction());
        }
        await _dbContext.AddAsync(worklistfrombiohubitem, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return new(worklistfrombiohubitem);
    }

    public async Task<Either<WorklistItemUsedReferenceNumber, Errors>> CreateReferenceNumber(WorklistItemUsedReferenceNumber worklistItemUsedReferenceNumber, CancellationToken cancellationToken, IDbContextTransaction? transaction = null)
    {
        if (transaction != null)
        {
            _dbContext.Database.UseTransaction(transaction.GetDbTransaction());
        }
        await _dbContext.AddAsync(worklistItemUsedReferenceNumber, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return new(worklistItemUsedReferenceNumber);
    }

    public async Task<Errors?> Delete(Guid id, CancellationToken cancellationToken)
    {
        WorklistFromBioHubItem lab = await WorklistFromBioHubItems.FirstOrDefaultAsync(l => l.DeletedOn == null && l.Id == id, cancellationToken);
        lab.DeletedOn = DateTime.Now;
        WorklistFromBioHubItems.Update(lab);

        await _dbContext.SaveChangesAsync(cancellationToken);
        return null;
    }

    public async Task<WorklistFromBioHubItem> ReadForUpdate(Guid id, CancellationToken cancellationToken)
    {
        return await _dbContext.WorklistFromBioHubItems
            .FirstOrDefaultAsync(l => l.DeletedOn == null && l.Id == id, cancellationToken);
    }

    public async Task<Errors?> Update(WorklistFromBioHubItem worklistfrombiohubitem, CancellationToken cancellationToken, IDbContextTransaction? transaction = null)
    {
        if (transaction != null)
        {
            _dbContext.Database.UseTransaction(transaction.GetDbTransaction());
        }
        WorklistFromBioHubItems.Update(worklistfrombiohubitem);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return null;
    }

    public async Task<Errors?> LinkDocument(Guid worklistfrombiohubitemId, Guid? documentId, DocumentFileType type, CancellationToken cancellationToken, bool? isDocumentFile = true, IDbContextTransaction? transaction = null, bool? replaceExistingType = true)
    {
        if (transaction != null)
        {
            _dbContext.Database.UseTransaction(transaction.GetDbTransaction());
        }

        if (replaceExistingType == true)
        {
            var elementToRemove = WorklistFromBioHubItemDocuments.Where(x => x.WorklistFromBioHubItemId == worklistfrombiohubitemId && x.Type == type && x.IsDocumentFile == isDocumentFile).FirstOrDefault();

            if (elementToRemove != default)
            {
                WorklistFromBioHubItemDocuments.Remove(elementToRemove);
            }
        }

        if (documentId != null && documentId != Guid.Empty)
        {
            var newElement = new WorklistFromBioHubItemDocument();
            newElement.WorklistFromBioHubItemId = worklistfrombiohubitemId;
            newElement.DocumentId = documentId;
            newElement.Type = type;
            newElement.IsDocumentFile = isDocumentFile;
            await _dbContext.AddAsync(newElement, cancellationToken);
        }
        await _dbContext.SaveChangesAsync(cancellationToken);

        return null;
    }

    public async Task<Errors?> UnlinkDocument(Guid worklistfrombiohubitemId, Guid? documentId, CancellationToken cancellationToken, bool? isDocumentFile = true, IDbContextTransaction? transaction = null)
    {
        if (transaction != null)
        {
            _dbContext.Database.UseTransaction(transaction.GetDbTransaction());
        }
        var elementToRemove = WorklistFromBioHubItemDocuments.Where(x => x.WorklistFromBioHubItemId == worklistfrombiohubitemId && x.IsDocumentFile == isDocumentFile && x.DocumentId == documentId).FirstOrDefault();

        if (elementToRemove != default)
        {
            WorklistFromBioHubItemDocuments.Remove(elementToRemove);
        }

        await _dbContext.SaveChangesAsync(cancellationToken);

        return null;
    }


    public async Task<Errors?> LinkMaterials(Guid worklistfrombiohubitemId, IEnumerable<WorklistFromBioHubItemMaterialDto>? worklistFromBioHubItemMaterialsDto, CancellationToken cancellationToken, IDbContextTransaction? transaction = null)
    {
        if (transaction != null)
        {
            _dbContext.Database.UseTransaction(transaction.GetDbTransaction());
        }
        var worklistFromBioHubItemMaterialsToRemove = WorklistFromBioHubItemMaterials.Where(x => x.WorklistFromBioHubItemId == worklistfrombiohubitemId).ToList();

        if (worklistFromBioHubItemMaterialsToRemove.Any())
        {
            WorklistFromBioHubItemMaterials.RemoveRange(worklistFromBioHubItemMaterialsToRemove);
        }

        if (worklistFromBioHubItemMaterialsDto != null && worklistFromBioHubItemMaterialsDto.Any())
        {
            var newWorklistFromBioHubItemElements = new List<WorklistFromBioHubItemMaterial>();
            foreach (var worklistFromBioHubItemMaterialDto in worklistFromBioHubItemMaterialsDto)
            {
                var newWorklistFromBioHubItemMaterialElement = new WorklistFromBioHubItemMaterial();
                newWorklistFromBioHubItemMaterialElement.Id = Guid.NewGuid();
                newWorklistFromBioHubItemMaterialElement.WorklistFromBioHubItemId = worklistfrombiohubitemId;
                newWorklistFromBioHubItemMaterialElement.MaterialId = worklistFromBioHubItemMaterialDto.MaterialId;
                newWorklistFromBioHubItemMaterialElement.Quantity = worklistFromBioHubItemMaterialDto.Quantity;
                newWorklistFromBioHubItemMaterialElement.Amount = worklistFromBioHubItemMaterialDto.Amount;
                newWorklistFromBioHubItemMaterialElement.Condition = worklistFromBioHubItemMaterialDto.Condition;
                newWorklistFromBioHubItemMaterialElement.Note = worklistFromBioHubItemMaterialDto.Note;
                newWorklistFromBioHubItemMaterialElement.CreationDate = DateTime.UtcNow;
                newWorklistFromBioHubItemElements.Add(newWorklistFromBioHubItemMaterialElement);
            }
            WorklistFromBioHubItemMaterials.AddRange(newWorklistFromBioHubItemElements);
        }
        await _dbContext.SaveChangesAsync(cancellationToken);

        return null;
    }



    public async Task<Errors?> LinkLaboratoryFocalPoints(Guid worklistfrombiohubitemId, IEnumerable<WorklistItemUserDto>? worklistFromBioHubItemLaboratoryFocalPoints, CancellationToken cancellationToken, IDbContextTransaction? transaction = null)
    {
        if (transaction != null)
        {
            _dbContext.Database.UseTransaction(transaction.GetDbTransaction());
        }

        var worklistFromBioHubItemLaboratoryFocalPointsToRemove = WorklistFromBioHubItemLaboratoryFocalPoints.Where(x => x.WorklistFromBioHubItemId == worklistfrombiohubitemId).ToList();

        if (worklistFromBioHubItemLaboratoryFocalPointsToRemove.Any())
        {
            WorklistFromBioHubItemLaboratoryFocalPoints.RemoveRange(worklistFromBioHubItemLaboratoryFocalPointsToRemove);
        }


        if (worklistFromBioHubItemLaboratoryFocalPoints != null && worklistFromBioHubItemLaboratoryFocalPoints.Any())
        {
            var newWorklistFromBioHubHistoryItemLaboratoryFocalPointElements = new List<WorklistFromBioHubItemLaboratoryFocalPoint>();

            foreach (var worklistFromBioHubItemLaboratoryFocalPoint in worklistFromBioHubItemLaboratoryFocalPoints)
            {
                var newMaterialShippingInformationElement = new WorklistFromBioHubItemLaboratoryFocalPoint();
                newMaterialShippingInformationElement.Id = Guid.NewGuid();
                newMaterialShippingInformationElement.WorklistFromBioHubItemId = worklistfrombiohubitemId;
                newMaterialShippingInformationElement.UserId = worklistFromBioHubItemLaboratoryFocalPoint.UserId;
                newMaterialShippingInformationElement.Other = worklistFromBioHubItemLaboratoryFocalPoint.Other;
                newMaterialShippingInformationElement.CreationDate = DateTime.UtcNow;
                newWorklistFromBioHubHistoryItemLaboratoryFocalPointElements.Add(newMaterialShippingInformationElement);

            }
            WorklistFromBioHubItemLaboratoryFocalPoints.AddRange(newWorklistFromBioHubHistoryItemLaboratoryFocalPointElements);


            await _dbContext.SaveChangesAsync(cancellationToken);

        }

        return null;
    }


    public async Task<Errors?> LinkAnnex2OfSMTA2Conditions(Guid worklistfrombiohubitemId, IEnumerable<WorklistFromBioHubItemAnnex2OfSMTA2ConditionDto>? worklistFromBioHubItemAnnex2OfSMTA2ConditionsDto, CancellationToken cancellationToken, IDbContextTransaction? transaction = null)
    {
        if (transaction != null)
        {
            _dbContext.Database.UseTransaction(transaction.GetDbTransaction());
        }
        var worklistFromBioHubItemAnnex2OfSMTA2ConditionsToRemove = WorklistFromBioHubItemAnnex2OfSMTA2Conditions.Where(x => x.WorklistFromBioHubItemId == worklistfrombiohubitemId).ToList();

        if (worklistFromBioHubItemAnnex2OfSMTA2ConditionsToRemove.Any())
        {
            WorklistFromBioHubItemAnnex2OfSMTA2Conditions.RemoveRange(worklistFromBioHubItemAnnex2OfSMTA2ConditionsToRemove);
        }

        if (worklistFromBioHubItemAnnex2OfSMTA2ConditionsDto != null && worklistFromBioHubItemAnnex2OfSMTA2ConditionsDto.Any())
        {
            var newWorklistFromBioHubItemElements = new List<WorklistFromBioHubItemAnnex2OfSMTA2Condition>();
            foreach (var worklistFromBioHubItemAnnex2OfSMTA2ConditionDto in worklistFromBioHubItemAnnex2OfSMTA2ConditionsDto)
            {
                var newWorklistFromBioHubItemAnnex2OfSMTA2ConditionElement = new WorklistFromBioHubItemAnnex2OfSMTA2Condition();
                newWorklistFromBioHubItemAnnex2OfSMTA2ConditionElement.Id = Guid.NewGuid();
                newWorklistFromBioHubItemAnnex2OfSMTA2ConditionElement.WorklistFromBioHubItemId = worklistfrombiohubitemId;
                newWorklistFromBioHubItemAnnex2OfSMTA2ConditionElement.Annex2OfSMTA2ConditionId = worklistFromBioHubItemAnnex2OfSMTA2ConditionDto.Annex2OfSMTA2ConditionId;
                newWorklistFromBioHubItemAnnex2OfSMTA2ConditionElement.Comment = worklistFromBioHubItemAnnex2OfSMTA2ConditionDto.Comment;
                newWorklistFromBioHubItemAnnex2OfSMTA2ConditionElement.Flag = worklistFromBioHubItemAnnex2OfSMTA2ConditionDto.Flag;

                newWorklistFromBioHubItemAnnex2OfSMTA2ConditionElement.CreationDate = DateTime.UtcNow;
                newWorklistFromBioHubItemElements.Add(newWorklistFromBioHubItemAnnex2OfSMTA2ConditionElement);
            }
            WorklistFromBioHubItemAnnex2OfSMTA2Conditions.AddRange(newWorklistFromBioHubItemElements);
        }
        await _dbContext.SaveChangesAsync(cancellationToken);

        return null;
    }

    public async Task<Errors?> LinkBiosafetyChecklistOfSMTA2(Guid worklistfrombiohubitemId, IEnumerable<WorklistFromBioHubItemBiosafetyChecklistOfSMTA2Dto>? worklistFromBioHubItemBiosafetyChecklistOfSMTA2sDto, CancellationToken cancellationToken, IDbContextTransaction? transaction = null)
    {
        if (transaction != null)
        {
            _dbContext.Database.UseTransaction(transaction.GetDbTransaction());
        }
        var worklistFromBioHubItemBiosafetyChecklistOfSMTA2sToRemove = WorklistFromBioHubItemBiosafetyChecklistOfSMTA2s.Where(x => x.WorklistFromBioHubItemId == worklistfrombiohubitemId).ToList();

        if (worklistFromBioHubItemBiosafetyChecklistOfSMTA2sToRemove.Any())
        {
            WorklistFromBioHubItemBiosafetyChecklistOfSMTA2s.RemoveRange(worklistFromBioHubItemBiosafetyChecklistOfSMTA2sToRemove);
        }

        if (worklistFromBioHubItemBiosafetyChecklistOfSMTA2sDto != null && worklistFromBioHubItemBiosafetyChecklistOfSMTA2sDto.Any())
        {
            var newWorklistFromBioHubItemElements = new List<WorklistFromBioHubItemBiosafetyChecklistOfSMTA2>();
            foreach (var worklistFromBioHubItemBiosafetyChecklistOfSMTA2Dto in worklistFromBioHubItemBiosafetyChecklistOfSMTA2sDto)
            {
                var newWorklistFromBioHubItemBiosafetyChecklistOfSMTA2Element = new WorklistFromBioHubItemBiosafetyChecklistOfSMTA2();
                newWorklistFromBioHubItemBiosafetyChecklistOfSMTA2Element.Id = Guid.NewGuid();
                newWorklistFromBioHubItemBiosafetyChecklistOfSMTA2Element.WorklistFromBioHubItemId = worklistfrombiohubitemId;
                newWorklistFromBioHubItemBiosafetyChecklistOfSMTA2Element.BiosafetyChecklistOfSMTA2Id = worklistFromBioHubItemBiosafetyChecklistOfSMTA2Dto.BiosafetyChecklistId;
                newWorklistFromBioHubItemBiosafetyChecklistOfSMTA2Element.Flag = worklistFromBioHubItemBiosafetyChecklistOfSMTA2Dto.Flag != null ? worklistFromBioHubItemBiosafetyChecklistOfSMTA2Dto.Flag : (worklistFromBioHubItemBiosafetyChecklistOfSMTA2Dto.IsParentCondition == true ? null : false);
                newWorklistFromBioHubItemBiosafetyChecklistOfSMTA2Element.CreationDate = DateTime.UtcNow;
                newWorklistFromBioHubItemElements.Add(newWorklistFromBioHubItemBiosafetyChecklistOfSMTA2Element);
            }
            WorklistFromBioHubItemBiosafetyChecklistOfSMTA2s.AddRange(newWorklistFromBioHubItemElements);
        }
        await _dbContext.SaveChangesAsync(cancellationToken);

        return null;
    }

    public async Task<Errors?> UpdateBookingFormDeliveryProperties(Guid worklistfrombiohubitemId, IEnumerable<BookingFormOfSMTADto>? bookingForms, CancellationToken cancellationToken, IDbContextTransaction? transaction = null)
    {
        if (transaction != null)
        {
            _dbContext.Database.UseTransaction(transaction.GetDbTransaction());
        }
        var bookingFormsToUpdate = BookingForms.Where(x => x.WorklistFromBioHubItemId == worklistfrombiohubitemId).ToList();
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

    public async Task<Errors?> LinkBookingForm(Guid worklistfrombiohubitemId, IEnumerable<BookingFormOfSMTADto>? bookingForms, CancellationToken cancellationToken, IDbContextTransaction? transaction = null)
    {
        if (transaction != null)
        {
            _dbContext.Database.UseTransaction(transaction.GetDbTransaction());
        }
        var bookingFormsToRemove = BookingForms.Where(x => x.WorklistFromBioHubItemId == worklistfrombiohubitemId).ToList();

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
                newBookingFormElement.WorklistFromBioHubItemId = worklistfrombiohubitemId;
                newBookingFormElement.TransportCategoryId = bookingForm.TransportCategoryId;
                newBookingFormElement.NumberOfInnerPackagingAndSize = bookingForm.NumberOfInnerPackagingAndSize;
                newBookingFormElement.TotalNumberOfVials = bookingForm.TotalNumberOfVials;
                newBookingFormElement.TotalAmount = bookingForm.TotalAmount;
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

    public async Task<Errors?> UpdateMaterialsCurrentNumberOfVials(Guid worklistfrombiohubitemId, CancellationToken cancellationToken, IDbContextTransaction? transaction = null)
    {
        if (transaction != null)
        {
            _dbContext.Database.UseTransaction(transaction.GetDbTransaction());
        }
        var worklistFromBioHubItemMaterials = WorklistFromBioHubItemMaterials.Where(x => x.WorklistFromBioHubItemId == worklistfrombiohubitemId).ToList();

        if (worklistFromBioHubItemMaterials.Any())
        {
            foreach (var worklistFromBioHubItemMaterial in worklistFromBioHubItemMaterials)
            {
                var material = await Materials.FirstOrDefaultAsync(x => x.Id == worklistFromBioHubItemMaterial.MaterialId, cancellationToken);
                material.CurrentNumberOfVials -= worklistFromBioHubItemMaterial.Quantity;
                Materials.Update(material);
            }                
        }
        
        await _dbContext.SaveChangesAsync(cancellationToken);

        return null;
    }

    public async Task<Errors?> AddFeedback(Guid worklistfrombiohubitemId, FeedbackDto? feedback, CancellationToken cancellationToken, IDbContextTransaction? transaction = null)
    {
        if (transaction != null)
        {
            _dbContext.Database.UseTransaction(transaction.GetDbTransaction());
        }


        if (feedback != null)
        {
            var newFeedback = new WorklistFromBioHubItemFeedback();

            newFeedback.Id = Guid.NewGuid();
            newFeedback.WorklistFromBioHubItemId = worklistfrombiohubitemId;
            newFeedback.Text = feedback.Text;
            newFeedback.Date = feedback.Date;
            newFeedback.PostedById = feedback.PostedById;

            WorklistFromBioHubItemFeedback.Add(newFeedback);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        return null;
    }


    public async Task<Errors?> AddBiosafetyChecklistComment(Guid worklistfrombiohubitemId, BiosafetyChecklistThreadCommentDto? comment, CancellationToken cancellationToken, IDbContextTransaction? transaction = null)
    {
        if (transaction != null)
        {
            _dbContext.Database.UseTransaction(transaction.GetDbTransaction());
        }


        if (comment != null)
        {
            var newComment = new WorklistFromBioHubItemBiosafetyChecklistThreadComment();

            newComment.Id = Guid.NewGuid();
            newComment.WorklistFromBioHubItemId = worklistfrombiohubitemId;
            newComment.Text = comment.Text;
            newComment.Date = comment.Date;
            newComment.PostedById = comment.PostedById;

            WorklistFromBioHubItemBiosafetyChecklistOfSMTA2Comments.Add(newComment);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        return null;
    }

    public async Task<IDbContextTransaction> BeginTransactionAsync()
    {
        return await _dbContext.Database.BeginTransactionAsync();
    }

}