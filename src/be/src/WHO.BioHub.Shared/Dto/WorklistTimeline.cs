namespace WHO.BioHub.Shared.Dto
{
    public class WorklistTimeline
    {
        public string TimelineTitle { get; set; }
        public List<WorklistTimelineEventsDayDto> Events { get; set; }
    }
}
