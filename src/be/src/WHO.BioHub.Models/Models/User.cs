using WHO.BioHub.Models.Models.Abstractions;

namespace WHO.BioHub.Models.Models;

// This class is used by Entity Framework Core which needs empty constructor

#pragma warning disable CS8618

public class User : EntityBase
{

    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string JobTitle { get; set; }
    public string Email { get; set; }
    public string BusinessPhone { get; set; }
    public string MobilePhone { get; set; }
    public bool IsActive { get; set; }
    public string Notes { get; set; }
    public Guid? RoleId { get; set; }
    public virtual Role Role { get; set; }
    public bool OperationalFocalPoint { get; set; }
    public virtual Laboratory Laboratory { get; set; }
    public Guid? LaboratoryId { get; set; }
    public virtual BioHubFacility BioHubFacility { get; set; }
    public Guid? BioHubFacilityId { get; set; }
    public virtual Courier Courier { get; set; }
    public Guid? CourierId { get; set; }
    public Guid? ExternalId { get; set; }
    public Guid? LastOperationByUserId { get; set; }
    public DateTime? LastOperationDate { get; set; }
    public virtual ICollection<UserHistory> UsersHistory { get; set; }

    public virtual ICollection<DocumentTemplate> DocumentTemplates { get; set; }
    public virtual ICollection<WorklistToBioHubItem> WorklistToBioHubItems { get; set; }
    public virtual ICollection<WorklistToBioHubHistoryItem> WorklistToBioHubHistoryItems { get; set; }
    public virtual ICollection<WorklistToBioHubItemLaboratoryFocalPoint> WorklistToBioHubItemLaboratoryFocalPoints { get; set; }
    public virtual ICollection<WorklistToBioHubHistoryItemLaboratoryFocalPoint> WorklistToBioHubHistoryItemLaboratoryFocalPoints { get; set; }
    public virtual ICollection<BookingFormPickupUser> BookingFormPickupUsers { get; set; }
    public virtual ICollection<BookingFormPickupUserHistory> BookingFormPickupUsersHistory { get; set; }

    public virtual ICollection<BookingFormCourierUser> BookingFormCourierUsers { get; set; }
    public virtual ICollection<BookingFormCourierUserHistory> BookingFormCourierUsersHistory { get; set; }
    public virtual ICollection<WorklistToBioHubItemFeedback> WorklistToBioHubItemFeedbacks { get; set; }
    public virtual ICollection<WorklistToBioHubHistoryItemFeedback> WorklistToBioHubHistoryItemFeedbacks { get; set; }

    public virtual ICollection<WorklistFromBioHubItemFeedback> WorklistFromBioHubItemFeedbacks { get; set; }
    public virtual ICollection<WorklistFromBioHubHistoryItemFeedback> WorklistFromBioHubHistoryItemFeedbacks { get; set; }

    public virtual ICollection<WorklistFromBioHubItem> WorklistFromBioHubItems { get; set; }
    public virtual ICollection<WorklistFromBioHubHistoryItem> WorklistFromBioHubHistoryItems { get; set; }
    public virtual ICollection<WorklistFromBioHubItemLaboratoryFocalPoint> WorklistFromBioHubItemLaboratoryFocalPoints { get; set; }
    public virtual ICollection<WorklistFromBioHubHistoryItemLaboratoryFocalPoint> WorklistFromBioHubHistoryItemLaboratoryFocalPoints { get; set; }
    public virtual ICollection<WorklistToBioHubItemBioHubFacilityFocalPoint> WorklistToBioHubItemBioHubFacilityFocalPoints { get; set; }
    public virtual ICollection<WorklistToBioHubHistoryItemBioHubFacilityFocalPoint> WorklistToBioHubHistoryItemBioHubFacilityFocalPoints { get; set; }
    public virtual ICollection<Material> Materials { get; set; }
    public virtual ICollection<MaterialHistory> MaterialsHistory { get; set; }
    public virtual ICollection<SMTA1WorkflowItem> SMTA1WorkflowItems { get; set; }
    public virtual ICollection<SMTA1WorkflowHistoryItem> SMTA1WorkflowHistoryItems { get; set; }

    public virtual ICollection<SMTA2WorkflowItem> SMTA2WorkflowItems { get; set; }
    public virtual ICollection<SMTA2WorkflowHistoryItem> SMTA2WorkflowHistoryItems { get; set; }

    public virtual ICollection<WorklistFromBioHubItemBiosafetyChecklistThreadComment> WorklistFromBioHubItemBiosafetyChecklistOfSMTA2Comments { get; set; }
    public virtual ICollection<WorklistFromBioHubHistoryItemBiosafetyChecklistThreadComment> WorklistFromBioHubHistoryItemBiosafetyChecklistOfSMTA2Comments { get; set; }

    public virtual ICollection<Resource> Resources { get; set; }
}
