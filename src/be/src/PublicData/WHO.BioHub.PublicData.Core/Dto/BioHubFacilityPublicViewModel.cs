namespace WHO.BioHub.PublicData.Core.Dto
{
    public class BioHubFacilityPublicViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Abbreviation { get; set; }
        public string Address { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public Guid? CountryId { get; set; }
    }
}
