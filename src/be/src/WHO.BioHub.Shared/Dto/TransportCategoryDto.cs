namespace WHO.BioHub.Shared.Dto
{
    public class TransportCategoryDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string HexColor { get; set; }
        public bool IsActive { get; set; }
    }
}
