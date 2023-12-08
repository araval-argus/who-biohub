using WHO.BioHub.Models.Models.Abstractions;

namespace WHO.BioHub.Models.Models;

// This class is used by Entity Framework Core which needs empty constructor

#pragma warning disable CS8618

public class Shipment : EntityBase
{
    /// <summary>
    /// Shipment reference number
    /// </summary>
    public string ReferenceNumber { get; set; }

    //public virtual ICollection<ShipmentToMaterial> Materials { get; set; }
    //public Guid? MaterialId { get; set; }
    //public virtual Material Material { get; set; }

    //public DateTime? WHONotifiedDate { get; set; }

    //public double Temperature { get; set; }


    //public Guid? UnitOfMeasureId { get; set; }
    //public virtual TemperatureUnitOfMeasure UnitOfMeasure { get; set; }

    // public virtual MaterialUsageRequest MaterialUsageRequest { get; set; }
    //public Guid? PriorityRequestTypeId { get; set; }
    //public virtual PriorityRequestType PriorityRequestType { get; set; }

    /// <summary>
    /// Transport category
    /// </summary>
    //public virtual TransportCategory TransportCategory { get; set; }
    //public Guid? TransportModeId { get; set; }
    //public virtual TransportMode TransportMode { get; set; }

    //public int NumberOfVials { get; set; }

    public string StatusOfRequest { get; set; }
    /// <summary>
    /// Date of sending booking form
    /// </summary>
    //public DateTime? SendingBookingDateForm { get; set; }

    /// <summary>
    /// Date receiving booking form
    /// </summary>
    //public DateTime? ReceivingBookingDateForm { get; set; }

    //public DateTime? CourierRequestDate { get; set; }

    //public DateTime? MaterialDepartingDate { get; set; }

    //public DateTime? MaterialArrivingDate { get; set; }

    //public DateTime? RecipientLaboratoryDate { get; set; }

    //public string Comments { get; set; }


    //public virtual PurchaseOrder PurchaseOrder { get; set; }

    //public virtual PaymentStatus PaymentCurrentStatus { get; set; }

    /**
     * TODO: missing all document part and relationship
     */
    public Guid? QELaboratoryId { get; set; }
    public virtual Laboratory QELaboratory { get; set; }

    public Guid? BioHubFacilityId { get; set; }
    public virtual BioHubFacility BioHubFacility { get; set; }

    public Guid? WorklistToBioHubItemId { get; set; }
    public virtual WorklistToBioHubItem WorklistToBioHubItem { get; set; }

    public Guid? WorklistFromBioHubItemId { get; set; }
    public virtual WorklistFromBioHubItem WorklistFromBioHubItem { get; set; }
    public DateTime? CompletedOn { get; set; }
}
