namespace WHO.BioHub.Shared.Dto
{
    public class WorklistTimelineEventsTypeDto
    {
        public Guid Id { get; set; }
        public string Label { get; set; } 
        public string Position { get; set; }        
        public int EventsNumber { get; set; }
        public WorklistEventType EventType { get; set; }
        public string Title { get; set; }
        public string Date { get; set; }
        public List<WorklistEventDetailItemDto> EventDetailItems { get; set; }
    }    

    public class WorklistEventDetailItemDto
    {
        public string Time { get; set; }
        public string Event { get; set; }
        public DateTime? EventDateTime { get; set; }
    }
    

    public enum WorklistEventType
    {
        Minor = 0,
        Milestone = 1,
        None = 2
    }

    public class WorklistTimelineEventsDayDto
    {
        public int DateZeroBased { get; set; }
        public DateTime Date { get; set; }
        public string DateString { get; set; }
        public string DateDD { get; set; }
        public string DateMM { get; set; }
        public string DateMMM { get; set; }
        public string DateYYYY { get; set; }
        public string Stage { get; set; }
        public List<WorklistTimelineEventsTypeDto> WorklistTimelineEventsTypes { get; set; }
    }

}
