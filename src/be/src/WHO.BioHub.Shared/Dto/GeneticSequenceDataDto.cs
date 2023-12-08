namespace WHO.BioHub.Shared.Dto
{
    public class GeneticSequenceDataDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
    }
}
