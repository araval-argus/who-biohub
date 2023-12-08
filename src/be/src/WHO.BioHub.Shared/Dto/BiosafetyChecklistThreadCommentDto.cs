namespace WHO.BioHub.Shared.Dto
{
    public class BiosafetyChecklistThreadCommentDto
    {
        public string Text { get; set; }
        public string PostedBy { get; set; }
        public Guid? PostedById { get; set; }
        public DateTime? Date { get; set; }
    }
}
