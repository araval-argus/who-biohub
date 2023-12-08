namespace WHO.BioHub.Shared.Dto
{
    public class CourierViewModel
    {
        public Guid Id { get; set; }
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
    }
}
