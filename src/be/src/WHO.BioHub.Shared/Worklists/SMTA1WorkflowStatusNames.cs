namespace WHO.BioHub.Shared.Worklists
{
    public static class SMTA1WorkflowStatusNames
    {
        public const string RequestInitiationTitle = "Request Initiation";
        public const string RequestInitiationApprovedInfo = "Initiated Request";
        public const string RequestInitiationRejectedInfo = "";
        public const string RequestInitiationRejectedTitle = "";

        public const string SubmitSMTA1Title = "Submit signed SMTA 1";
        public const string SubmitSMTA1ApprovedInfo = "Submitted signed SMTA 1";
        public const string SubmitSMTA1RejectedInfo = "";
        public const string SubmitSMTA1RejectedTitle = "Resubmit signed SMTA 1";

        public const string WaitingForSMTA1SECsApprovalTitle = "Countersign submitted SMTA 1";
        public const string WaitingForSMTA1SECsApprovalApprovedInfo = "Countersigned submitted SMTA 1";
        public const string WaitingForSMTA1SECsApprovalRejectedInfo = "Returned submitted SMTA 1";
        public const string WaitingForSMTA1SECsApprovalRejectedTitle = "Returned submitted SMTA 1";

        public const string SMTA1WorkflowCompleteTitle = "Completed SMTA 1 process";
        public const string SMTA1WorkflowCompleteApprovedInfo = "SMTA 1 approved and signed by Secretariat";
        public const string SMTA1WorkflowCompleteRejectedInfo = "SMTA 1 Rejected";
        public const string SMTA1WorkflowCompleteRejectedTitle = "SMTA 1 Rejected";
    }
}
