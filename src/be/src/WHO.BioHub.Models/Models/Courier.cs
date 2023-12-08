using WHO.BioHub.Models.Models.Abstractions;

namespace WHO.BioHub.Models.Models;

public class Courier : EntityBase
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string WHOAccountNumber { get; set; }
    public string Address { get; set; }
    public string Email { get; set; }
    public string BusinessPhone { get; set; }
    public double? Latitude { get; set; }
    public double? Longitude { get; set; }
    public bool IsActive { get; set; }
    public Guid? CountryId { get; set; }
    public virtual Country Country { get; set; }
    public Guid? LastOperationByUserId { get; set; }
    public DateTime? LastOperationDate { get; set; }
    public virtual ICollection<User> Users { get; set; }
    public virtual ICollection<BookingForm> BookingForms { get; set; }
    public virtual ICollection<BookingFormHistory> BookingFormsHistory { get; set; }
    public virtual ICollection<CourierHistory> CourierHistory { get; set; }
    public virtual ICollection<UserHistory> UsersHistory { get; set; }

    // public virtual ICollection<ShipmentRequest> ShipmentRequests { get; set; }
}
