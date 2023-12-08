using WHO.BioHub.Models.Models.Abstractions;

namespace WHO.BioHub.Models.Models;

public class BookingFormPickupUserHistory : EntityBase
{
    public Guid? BookingFormHistoryId { get; set; }
    public BookingFormHistory BookingFormHistory { get; set; }
    public Guid? UserId { get; set; }
    public User User { get; set; }
    public string Other { get; set; }

}