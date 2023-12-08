using WHO.BioHub.Models.Models.Abstractions;
using WHO.BioHub.Shared.Enums;

namespace WHO.BioHub.Models.Models;

public class BookingFormHistory : EntityBase
{
    public Guid? WorklistToBioHubHistoryItemId { get; set; }
    public virtual WorklistToBioHubHistoryItem WorklistToBioHubHistoryItem { get; set; }
    public Guid? WorklistFromBioHubHistoryItemId { get; set; }
    public virtual WorklistFromBioHubHistoryItem WorklistFromBioHubHistoryItem { get; set; }    
    public DateTime? Date { get; set; }
    public DateTime? RequestDateOfPickup { get; set; }
    public TemperatureTransportCondition? TemperatureTransportCondition { get; set; }
    public int? TotalNumberOfVials { get; set; }
    public decimal? TotalAmount { get; set; }
    public Guid? MaterialProductId { get; set; }
    public virtual MaterialProduct MaterialProduct { get; set; }
    public string NumberOfInnerPackagingAndSize { get; set; }
    public virtual ICollection<BookingFormPickupUserHistory> BookingFormPickupUsersHistory { get; set; }
    public Guid? CourierId { get; set; }
    public virtual Courier Courier { get; set; }
    public virtual ICollection<BookingFormCourierUserHistory> BookingFormCourierUsersHistory { get; set; }
    public DateTime? EstimateDateOfPickup { get; set; }
    public string ShipmentReferenceNumber { get; set; }
    public DateTime? DateOfPickup { get; set; }
    public DateTime? DateOfDelivery { get; set; }
    public Guid? TransportCategoryId { get; set; }
    public virtual TransportCategory TransportCategory { get; set; }
    public Guid? TransportModeId { get; set; }
    public virtual TransportMode TransportMode { get; set; }

}
