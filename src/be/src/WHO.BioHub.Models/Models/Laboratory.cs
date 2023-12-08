using WHO.BioHub.Models.Models.Abstractions;

namespace WHO.BioHub.Models.Models;

// This class is used by Entity Framework Core which needs empty constructor

#pragma warning disable CS8618

public class Laboratory : EntityBase
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string Abbreviation { get; set; }
    public string Address { get; set; }
    public double? Latitude { get; set; }
    public double? Longitude { get; set; }
    public bool IsActive { get; set; }
    public Guid? BSLLevelId { get; set; }
    public virtual BSLLevel? BSLLevel { get; set; }
    public Guid? CountryId { get; set; }
    public virtual Country Country { get; set; }
    public bool IsPublicFacing { get; set; }
    public Guid? LastOperationByUserId { get; set; }
    public DateTime? LastOperationDate { get; set; }
    public virtual ICollection<Material> Materials { get; set; }
    public virtual ICollection<Shipment> Shipments { get; set; }
    public virtual ICollection<User> Users { get; set; }
    public virtual ICollection<UserRequest> UserRequests { get; set; }
    public virtual ICollection<WorklistToBioHubItem> WorklistToBioHubItems { get; set; }
    public virtual ICollection<WorklistToBioHubHistoryItem> WorklistToBioHubHistoryItems { get; set; }
    public virtual ICollection<WorklistFromBioHubItem> WorklistFromBioHubItems { get; set; }
    public virtual ICollection<WorklistFromBioHubHistoryItem> WorklistFromBioHubHistoryItems { get; set; }
    public virtual ICollection<Document> Documents { get; set; }
    public virtual ICollection<MaterialHistory> MaterialsHistory { get; set; }
    public virtual ICollection<SMTA1WorkflowItem> SMTA1WorkflowItems { get; set; }
    public virtual ICollection<SMTA1WorkflowHistoryItem> SMTA1WorkflowHistoryItems { get; set; }
    public virtual ICollection<SMTA2WorkflowItem> SMTA2WorkflowItems { get; set; }
    public virtual ICollection<SMTA2WorkflowHistoryItem> SMTA2WorkflowHistoryItems { get; set; }
    public virtual ICollection<UserHistory> UsersHistory { get; set; }
    public virtual ICollection<LaboratoryHistory> LaboratoriesHistory { get; set; }
    /**
     * TODO: try to understand 
     * - SMTA
     * - QE biosafety pre-assessment
     * - QE meeting biosafety criteria
     */
    // public virtual ICollection<User> StaffMembers { get; set; }
    // public virtual ICollection<ShipmentRequest> ShipmentRequests { get; set; }
}
