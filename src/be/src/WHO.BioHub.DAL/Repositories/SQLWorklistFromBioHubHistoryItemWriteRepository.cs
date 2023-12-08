using Microsoft.EntityFrameworkCore;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.WorklistFromBioHubHistoryItems;
using WHO.BioHub.Shared.Utils;
using Microsoft.EntityFrameworkCore.Storage;

namespace WHO.BioHub.DAL.Repositories;

public class SQLWorklistFromBioHubHistoryItemWriteRepository : IWorklistFromBioHubHistoryItemWriteRepository
{
    private readonly BioHubDbContext _dbContext;
    private DbSet<WorklistFromBioHubHistoryItem> WorklistFromBioHubHistoryItems => _dbContext.WorklistFromBioHubHistoryItems;
    private DbSet<WorklistFromBioHubHistoryItemDocument> WorklistFromBioHubHistoryItemDocuments => _dbContext.WorklistFromBioHubHistoryItemDocuments;
    private DbSet<WorklistFromBioHubItemDocument> WorklistFromBioHubItemDocuments => _dbContext.WorklistFromBioHubItemDocuments;
    private DbSet<MaterialShippingInformation> MaterialShippingInformations => _dbContext.MaterialShippingInformations;
    private DbSet<MaterialClinicalDetail> MaterialClinicalDetails => _dbContext.MaterialClinicalDetails;
    private DbSet<MaterialShippingInformationHistory> MaterialShippingInformationsHistory => _dbContext.MaterialShippingInformationsHistory;
    private DbSet<MaterialClinicalDetailHistory> MaterialClinicalDetailsHistory => _dbContext.MaterialClinicalDetailsHistory;

    private DbSet<WorklistFromBioHubItemLaboratoryFocalPoint> WorklistFromBioHubItemLaboratoryFocalPoints => _dbContext.WorklistFromBioHubItemLaboratoryFocalPoints;
    private DbSet<WorklistFromBioHubHistoryItemLaboratoryFocalPoint> WorklistFromBioHubHistoryItemLaboratoryFocalPoints => _dbContext.WorklistFromBioHubHistoryItemLaboratoryFocalPoints;

    private DbSet<BookingFormHistory> BookingFormsHistory => _dbContext.BookingFormsHistory;
    private DbSet<BookingFormPickupUserHistory> BookingFormPickupUsersHistory => _dbContext.BookingFormPickupUsersHistory;
    private DbSet<BookingForm> BookingForms => _dbContext.BookingForms;
    private DbSet<BookingFormPickupUser> BookingFormPickupUsers => _dbContext.BookingFormPickupUsers;
    private DbSet<BookingFormCourierUser> BookingFormCourierUsers => _dbContext.BookingFormCourierUsers;

    private DbSet<BookingFormCourierUserHistory> BookingFormCourierUsersHistory => _dbContext.BookingFormCourierUsersHistory;

    private DbSet<WorklistFromBioHubHistoryItemFeedback> WorklistFromBioHubHistoryItemFeedback => _dbContext.WorklistFromBioHubHistoryItemFeedback;
    private DbSet<WorklistFromBioHubItemFeedback> WorklistFromBioHubItemFeedback => _dbContext.WorklistFromBioHubItemFeedback;

    private DbSet<WorklistFromBioHubItemMaterial> WorklistFromBioHubItemMaterials => _dbContext.WorklistFromBioHubItemMaterials;

    private DbSet<WorklistFromBioHubHistoryItemMaterial> WorklistFromBioHubHistoryItemMaterials => _dbContext.WorklistFromBioHubHistoryItemMaterials;
    private DbSet<WorklistFromBioHubItemAnnex2OfSMTA2Condition> WorklistFromBioHubItemAnnex2OfSMTA2Conditions => _dbContext.WorklistFromBioHubItemAnnex2OfSMTA2Conditions;

    private DbSet<WorklistFromBioHubHistoryItemBiosafetyChecklistOfSMTA2> WorklistFromBioHubHistoryItemBiosafetyChecklistOfSMTA2s => _dbContext.WorklistFromBioHubHistoryItemBiosafetyChecklistOfSMTA2s;

    private DbSet<WorklistFromBioHubHistoryItemAnnex2OfSMTA2Condition> WorklistFromBioHubHistoryItemAnnex2OfSMTA2Conditions => _dbContext.WorklistFromBioHubHistoryItemAnnex2OfSMTA2Conditions;

    private DbSet<WorklistFromBioHubItemBiosafetyChecklistOfSMTA2> WorklistFromBioHubItemBiosafetyChecklistOfSMTA2s => _dbContext.WorklistFromBioHubItemBiosafetyChecklistOfSMTA2s;

    private DbSet<WorklistFromBioHubHistoryItemBiosafetyChecklistThreadComment> WorklistFromBioHubHistoryItemBiosafetyChecklistThreadComments => _dbContext.WorklistFromBioHubHistoryItemBiosafetyChecklistThreadComments;
    private DbSet<WorklistFromBioHubItemBiosafetyChecklistThreadComment> WorklistFromBioHubItemBiosafetyChecklistThreadComments => _dbContext.WorklistFromBioHubItemBiosafetyChecklistThreadComments;



    public SQLWorklistFromBioHubHistoryItemWriteRepository(BioHubDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Either<WorklistFromBioHubHistoryItem, Errors>> Create(WorklistFromBioHubHistoryItem worklistfrombiohubhistoryitem, CancellationToken cancellationToken, IDbContextTransaction? transaction = null)
    {
        if (transaction != null)
        {
            _dbContext.Database.UseTransaction(transaction.GetDbTransaction());
        }
        await _dbContext.AddAsync(worklistfrombiohubhistoryitem, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return new(worklistfrombiohubhistoryitem);
    }

    public async Task<Errors?> Delete(Guid id, CancellationToken cancellationToken)
    {
        WorklistFromBioHubHistoryItem lab = await WorklistFromBioHubHistoryItems.FirstOrDefaultAsync(l => l.DeletedOn == null && l.Id == id, cancellationToken);
        lab.DeletedOn = DateTime.Now;
        WorklistFromBioHubHistoryItems.Update(lab);

        await _dbContext.SaveChangesAsync(cancellationToken);
        return null;
    }

    public async Task<WorklistFromBioHubHistoryItem> ReadForUpdate(Guid id, CancellationToken cancellationToken)
    {
        return await _dbContext.WorklistFromBioHubHistoryItems
            .FirstOrDefaultAsync(l => l.DeletedOn == null && l.Id == id, cancellationToken);
    }

    public async Task<Errors?> Update(WorklistFromBioHubHistoryItem worklistfrombiohubhistoryitem, CancellationToken cancellationToken)
    {
        WorklistFromBioHubHistoryItems.Update(worklistfrombiohubhistoryitem);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return null;
    }

    public async Task<Errors?> CopyLinkDocumentFromWorklistFromBioHubItem(Guid worklistfrombiohubitemId, Guid worklistfrombiohubhistoryitemId, CancellationToken cancellationToken, IDbContextTransaction? transaction = null)
    {
        if (transaction != null)
        {
            _dbContext.Database.UseTransaction(transaction.GetDbTransaction());
        }
        var documentsToCopy = WorklistFromBioHubItemDocuments.Where(x => x.WorklistFromBioHubItemId == worklistfrombiohubitemId);

        if (documentsToCopy.Any())
        {
            var newHistoryElements = new List<WorklistFromBioHubHistoryItemDocument>();
            foreach (var documentToCopy in documentsToCopy)
            {
                var newHistoryElement = new WorklistFromBioHubHistoryItemDocument();
                newHistoryElement.WorklistFromBioHubHistoryItemId = worklistfrombiohubhistoryitemId;
                newHistoryElement.DocumentId = documentToCopy.DocumentId;
                newHistoryElement.IsDocumentFile = documentToCopy.IsDocumentFile;
                newHistoryElements.Add(newHistoryElement);

            }
            WorklistFromBioHubHistoryItemDocuments.AddRange(newHistoryElements);
            await _dbContext.SaveChangesAsync(cancellationToken);

        }

        return null;
    }


    public async Task<Errors?> LinkLaboratoryFocalPointsFromWorklistFromBioHubItem(Guid worklistfrombiohubitemId, Guid worklistfrombiohubhistoryitemId, CancellationToken cancellationToken, IDbContextTransaction? transaction = null)
    {
        if (transaction != null)
        {
            _dbContext.Database.UseTransaction(transaction.GetDbTransaction());
        }
        var worklistFromBioHubItemLaboratoryFocalPointsToCopy = WorklistFromBioHubItemLaboratoryFocalPoints.Where(x => x.WorklistFromBioHubItemId == worklistfrombiohubitemId);

        if (worklistFromBioHubItemLaboratoryFocalPointsToCopy.Any())
        {
            var newWorklistFromBioHubHistoryItemLaboratoryFocalPointElements = new List<WorklistFromBioHubHistoryItemLaboratoryFocalPoint>();

            foreach (var worklistFromBioHubItemLaboratoryFocalPointToCopy in worklistFromBioHubItemLaboratoryFocalPointsToCopy)
            {
                var newMaterialShippingInformationHistoryElement = new WorklistFromBioHubHistoryItemLaboratoryFocalPoint();
                newMaterialShippingInformationHistoryElement.Id = Guid.NewGuid();
                newMaterialShippingInformationHistoryElement.WorklistFromBioHubHistoryItemId = worklistfrombiohubhistoryitemId;
                newMaterialShippingInformationHistoryElement.UserId = worklistFromBioHubItemLaboratoryFocalPointToCopy.UserId;
                newMaterialShippingInformationHistoryElement.Other = worklistFromBioHubItemLaboratoryFocalPointToCopy.Other;
                newMaterialShippingInformationHistoryElement.CreationDate = DateTime.UtcNow;
                newWorklistFromBioHubHistoryItemLaboratoryFocalPointElements.Add(newMaterialShippingInformationHistoryElement);


            }
            WorklistFromBioHubHistoryItemLaboratoryFocalPoints.AddRange(newWorklistFromBioHubHistoryItemLaboratoryFocalPointElements);


            await _dbContext.SaveChangesAsync(cancellationToken);

        }

        return null;
    }


    public async Task<Errors?> LinkMaterialsFromWorklistFromBioHubItem(Guid worklistfrombiohubitemId, Guid worklistfrombiohubhistoryitemId, CancellationToken cancellationToken, IDbContextTransaction? transaction = null)
    {
        if (transaction != null)
        {
            _dbContext.Database.UseTransaction(transaction.GetDbTransaction());
        }
        var worklistFromBioHubItemMaterialsToCopy = WorklistFromBioHubItemMaterials.Where(x => x.WorklistFromBioHubItemId == worklistfrombiohubitemId);

        if (worklistFromBioHubItemMaterialsToCopy.Any())
        {
            var newWorklistFromBioHubHistoryItemMaterialElements = new List<WorklistFromBioHubHistoryItemMaterial>();

            foreach (var worklistFromBioHubItemMaterialToCopy in worklistFromBioHubItemMaterialsToCopy)
            {
                var newMaterialShippingInformationHistoryElement = new WorklistFromBioHubHistoryItemMaterial();
                newMaterialShippingInformationHistoryElement.Id = Guid.NewGuid();
                newMaterialShippingInformationHistoryElement.WorklistFromBioHubHistoryItemId = worklistfrombiohubhistoryitemId;
                newMaterialShippingInformationHistoryElement.MaterialId = worklistFromBioHubItemMaterialToCopy.MaterialId;
                newMaterialShippingInformationHistoryElement.Quantity = worklistFromBioHubItemMaterialToCopy.Quantity;
                newMaterialShippingInformationHistoryElement.Amount = worklistFromBioHubItemMaterialToCopy.Amount;
                newMaterialShippingInformationHistoryElement.Condition = worklistFromBioHubItemMaterialToCopy.Condition;
                newMaterialShippingInformationHistoryElement.Note = worklistFromBioHubItemMaterialToCopy.Note;
                newMaterialShippingInformationHistoryElement.CreationDate = DateTime.UtcNow;
                newWorklistFromBioHubHistoryItemMaterialElements.Add(newMaterialShippingInformationHistoryElement);
            }
            WorklistFromBioHubHistoryItemMaterials.AddRange(newWorklistFromBioHubHistoryItemMaterialElements);


            await _dbContext.SaveChangesAsync(cancellationToken);

        }

        return null;
    }


    public async Task<Errors?> LinkAnnex2OfSMTA2ConditionsFromWorklistFromBioHubItem(Guid worklistfrombiohubitemId, Guid worklistfrombiohubhistoryitemId, CancellationToken cancellationToken, IDbContextTransaction? transaction = null)
    {
        if (transaction != null)
        {
            _dbContext.Database.UseTransaction(transaction.GetDbTransaction());
        }
        var worklistFromBioHubItemAnnex2OfSMTA2ConditionsToCopy = WorklistFromBioHubItemAnnex2OfSMTA2Conditions.Where(x => x.WorklistFromBioHubItemId == worklistfrombiohubitemId);

        if (worklistFromBioHubItemAnnex2OfSMTA2ConditionsToCopy.Any())
        {
            var newWorklistFromBioHubHistoryItemAnnex2OfSMTA2ConditionElements = new List<WorklistFromBioHubHistoryItemAnnex2OfSMTA2Condition>();

            foreach (var worklistFromBioHubItemAnnex2OfSMTA2ConditionToCopy in worklistFromBioHubItemAnnex2OfSMTA2ConditionsToCopy)
            {
                var newAnnex2OfSMTA2ConditionShippingInformationHistoryElement = new WorklistFromBioHubHistoryItemAnnex2OfSMTA2Condition();
                newAnnex2OfSMTA2ConditionShippingInformationHistoryElement.Id = Guid.NewGuid();
                newAnnex2OfSMTA2ConditionShippingInformationHistoryElement.WorklistFromBioHubHistoryItemId = worklistfrombiohubhistoryitemId;
                newAnnex2OfSMTA2ConditionShippingInformationHistoryElement.Annex2OfSMTA2ConditionId = worklistFromBioHubItemAnnex2OfSMTA2ConditionToCopy.Annex2OfSMTA2ConditionId;
                newAnnex2OfSMTA2ConditionShippingInformationHistoryElement.Comment = worklistFromBioHubItemAnnex2OfSMTA2ConditionToCopy.Comment;
                newAnnex2OfSMTA2ConditionShippingInformationHistoryElement.Flag = worklistFromBioHubItemAnnex2OfSMTA2ConditionToCopy.Flag;
                newAnnex2OfSMTA2ConditionShippingInformationHistoryElement.CreationDate = DateTime.UtcNow;
                newWorklistFromBioHubHistoryItemAnnex2OfSMTA2ConditionElements.Add(newAnnex2OfSMTA2ConditionShippingInformationHistoryElement);
            }
            WorklistFromBioHubHistoryItemAnnex2OfSMTA2Conditions.AddRange(newWorklistFromBioHubHistoryItemAnnex2OfSMTA2ConditionElements);


            await _dbContext.SaveChangesAsync(cancellationToken);

        }

        return null;
    }


    public async Task<Errors?> LinkBiosafetyChecklistOfSMTA2sFromWorklistFromBioHubItem(Guid worklistfrombiohubitemId, Guid worklistfrombiohubhistoryitemId, CancellationToken cancellationToken, IDbContextTransaction? transaction = null)
    {
        if (transaction != null)
        {
            _dbContext.Database.UseTransaction(transaction.GetDbTransaction());
        }
        var worklistFromBioHubItemBiosafetyChecklistOfSMTA2ConditionsToCopy = WorklistFromBioHubItemBiosafetyChecklistOfSMTA2s.Where(x => x.WorklistFromBioHubItemId == worklistfrombiohubitemId);

        if (worklistFromBioHubItemBiosafetyChecklistOfSMTA2ConditionsToCopy.Any())
        {
            var newWorklistFromBioHubHistoryItemBiosafetyChecklistOfSMTA2ConditionElements = new List<WorklistFromBioHubHistoryItemBiosafetyChecklistOfSMTA2>();

            foreach (var worklistFromBioHubItemBiosafetyChecklistOfSMTA2ConditionToCopy in worklistFromBioHubItemBiosafetyChecklistOfSMTA2ConditionsToCopy)
            {
                var newBiosafetyChecklistOfSMTA2ConditionShippingInformationHistoryElement = new WorklistFromBioHubHistoryItemBiosafetyChecklistOfSMTA2();
                newBiosafetyChecklistOfSMTA2ConditionShippingInformationHistoryElement.Id = Guid.NewGuid();
                newBiosafetyChecklistOfSMTA2ConditionShippingInformationHistoryElement.WorklistFromBioHubHistoryItemId = worklistfrombiohubhistoryitemId;
                newBiosafetyChecklistOfSMTA2ConditionShippingInformationHistoryElement.BiosafetyChecklistOfSMTA2Id = worklistFromBioHubItemBiosafetyChecklistOfSMTA2ConditionToCopy.BiosafetyChecklistOfSMTA2Id;

                newBiosafetyChecklistOfSMTA2ConditionShippingInformationHistoryElement.Flag = worklistFromBioHubItemBiosafetyChecklistOfSMTA2ConditionToCopy.Flag;
                newBiosafetyChecklistOfSMTA2ConditionShippingInformationHistoryElement.CreationDate = DateTime.UtcNow;
                newWorklistFromBioHubHistoryItemBiosafetyChecklistOfSMTA2ConditionElements.Add(newBiosafetyChecklistOfSMTA2ConditionShippingInformationHistoryElement);
            }
            WorklistFromBioHubHistoryItemBiosafetyChecklistOfSMTA2s.AddRange(newWorklistFromBioHubHistoryItemBiosafetyChecklistOfSMTA2ConditionElements);


            await _dbContext.SaveChangesAsync(cancellationToken);

        }

        return null;
    }



    public async Task<Errors?> LinkBookingFormsFromWorklistFromBioHubItem(Guid worklistfrombiohubitemId, Guid worklistfrombiohubhistoryitemId, CancellationToken cancellationToken, IDbContextTransaction? transaction = null)
    {
        if (transaction != null)
        {
            _dbContext.Database.UseTransaction(transaction.GetDbTransaction());
        }
        var bookingFormsToCopy = BookingForms.Where(x => x.WorklistFromBioHubItemId == worklistfrombiohubitemId).Include(x => x.BookingFormPickupUsers).Include(x => x.BookingFormCourierUsers);

        if (bookingFormsToCopy.Any())
        {
            var newBookingFormElements = new List<BookingFormHistory>();
            var newBookingFormPickupUserElements = new List<BookingFormPickupUserHistory>();
            var newBookingFormCourierUserElements = new List<BookingFormCourierUserHistory>();
            foreach (var bookingFormToCopy in bookingFormsToCopy)
            {
                var newBookingFormElement = new BookingFormHistory();

                newBookingFormElement.Id = Guid.NewGuid();
                newBookingFormElement.WorklistFromBioHubHistoryItemId = worklistfrombiohubhistoryitemId;
                newBookingFormElement.TransportCategoryId = bookingFormToCopy.TransportCategoryId;
                newBookingFormElement.NumberOfInnerPackagingAndSize = bookingFormToCopy.NumberOfInnerPackagingAndSize;
                newBookingFormElement.TotalNumberOfVials = bookingFormToCopy.TotalNumberOfVials;
                newBookingFormElement.TotalAmount = bookingFormToCopy.TotalAmount;
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


    public async Task<Errors?> LinkFeedbacksFromWorklistFromBioHubItem(Guid worklistfrombiohubitemId, Guid worklistfrombiohubhistoryitemId, CancellationToken cancellationToken, IDbContextTransaction? transaction = null)
    {
        if (transaction != null)
        {
            _dbContext.Database.UseTransaction(transaction.GetDbTransaction());
        }
        var feedbacksToCopy = WorklistFromBioHubItemFeedback.Where(x => x.WorklistFromBioHubItemId == worklistfrombiohubitemId).ToList();

        if (feedbacksToCopy.Any())
        {
            var newFeedbackElements = new List<WorklistFromBioHubHistoryItemFeedback>();

            foreach (var feedbackToCopy in feedbacksToCopy)
            {
                var newFeedbackElement = new WorklistFromBioHubHistoryItemFeedback();

                newFeedbackElement.Id = Guid.NewGuid();
                newFeedbackElement.WorklistFromBioHubHistoryItemId = worklistfrombiohubhistoryitemId;
                newFeedbackElement.Date = feedbackToCopy.Date;
                newFeedbackElement.PostedById = feedbackToCopy.PostedById;
                newFeedbackElement.Text = feedbackToCopy.Text;


                newFeedbackElements.Add(newFeedbackElement);
            }
            WorklistFromBioHubHistoryItemFeedback.AddRange(newFeedbackElements);


            await _dbContext.SaveChangesAsync(cancellationToken);

        }

        return null;
    }


    public async Task<Errors?> LinkBiosafetyChecklistCommentsFromWorklistFromBioHubItem(Guid worklistfrombiohubitemId, Guid worklistfrombiohubhistoryitemId, CancellationToken cancellationToken, IDbContextTransaction? transaction = null)
    {
        if (transaction != null)
        {
            _dbContext.Database.UseTransaction(transaction.GetDbTransaction());
        }
        var commentsToCopy = WorklistFromBioHubItemBiosafetyChecklistThreadComments.Where(x => x.WorklistFromBioHubItemId == worklistfrombiohubitemId).ToList();

        if (commentsToCopy.Any())
        {
            var newCommentElements = new List<WorklistFromBioHubHistoryItemBiosafetyChecklistThreadComment>();

            foreach (var commentToCopy in commentsToCopy)
            {
                var newCommentElement = new WorklistFromBioHubHistoryItemBiosafetyChecklistThreadComment();

                newCommentElement.Id = Guid.NewGuid();
                newCommentElement.WorklistFromBioHubHistoryItemId = worklistfrombiohubhistoryitemId;
                newCommentElement.Date = commentToCopy.Date;
                newCommentElement.PostedById = commentToCopy.PostedById;
                newCommentElement.Text = commentToCopy.Text;


                newCommentElements.Add(newCommentElement);
            }
            WorklistFromBioHubHistoryItemBiosafetyChecklistThreadComments.AddRange(newCommentElements);


            await _dbContext.SaveChangesAsync(cancellationToken);

        }

        return null;
    }

    public async Task<IDbContextTransaction> GetTransactionAsync()
    {
        return await _dbContext.Database.BeginTransactionAsync();
    }

}