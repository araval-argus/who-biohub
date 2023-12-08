namespace WHO.BioHub.Shared.Dto
{
    public class BookingFormPickupUserDto
    {
        public Guid Id { get; set; }
        public Guid? BookingFormId { get; set; }
        public Guid? UserId { get; set; }
        public string UserName { get; set; }
        public string Country { get; set; }
        public string Laboratory { get; set; }
        public string MobilePhone { get; set; }
        public string Email { get; set; }
        public string JobTitle { get; set; }
        public string Other { get; set; }
    }
}
