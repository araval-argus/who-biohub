using WHO.BioHub.Models.Models.Abstractions;

namespace WHO.BioHub.Models.Models;

public class BookingFormCourierUser : EntityBase
{
    public Guid? BookingFormId { get; set; }
    public BookingForm BookingForm { get; set; }
    public Guid? UserId { get; set; }
    public User User { get; set; }
    public string Other { get; set; }

}
