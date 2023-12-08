namespace WHO.BioHub.Shared.Dto
{
    public class BioHubFacilityViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Abbreviation { get; set; }
        public string Address { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public Guid? CountryId { get; set; }
        public Guid? BSLLevelId { get; set; }
        public bool IsPublicFacing { get; set; }
        public bool IsActive { get; set; }
        public string Description { get; set; }
    }
}
