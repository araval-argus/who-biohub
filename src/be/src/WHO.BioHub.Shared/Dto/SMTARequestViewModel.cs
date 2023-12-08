using WHO.BioHub.Shared.Enums;

namespace WHO.BioHub.Shared.Dto
{
    public class SMTARequestViewModel
    {
        public Guid Id { get; set; }
        public string WorkflowItemTitle { get; set; }
        public DateTime? OperationDate { get; set; }
        public string SendBy { get; set; }
        public string Institution { get; set; }
        public string SMTAType { get; set; }

    }
}
