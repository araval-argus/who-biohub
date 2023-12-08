namespace WHO.BioHub.Shared.Dto
{
    public class CountryDto
    {
        public Guid Id { get; set; }
        public string Uncode { get; set; }
        public string Name { get; set; }
        public string FullName { get; set; }
        public string Iso2 { get; set; }
        public string Iso3 { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public int? GmtHour { get; set; }
        public int? GmtMin { get; set; }
        public bool IsActive { get; set; }
    }

}
