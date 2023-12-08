namespace WHO.BioHub.Shared.Worklists
{
    public static class SMTA2WorkflowStatusNames
    {
        public const string RequestInitiationTitle = "Request Initiation";
        public const string RequestInitiationApprovedInfo = "Initiated Request";
        public const string RequestInitiationRejectedInfo = "";
        public const string RequestInitiationRejectedTitle = "";


        public const string SubmitSMTA2Title = "Submit signed SMTA 2";
        public const string SubmitSMTA2ApprovedInfo = "Submitted signed SMTA 2";
        public const string SubmitSMTA2RejectedInfo = "";
        public const string SubmitSMTA2RejectedTitle = "Resubmit signed SMTA 2";

        public const string WaitingForSMTA2SECsApprovalTitle = "Countersign submitted SMTA 2";
        public const string WaitingForSMTA2SECsApprovalApprovedInfo = "Countersigned submitted SMTA 2";
        public const string WaitingForSMTA2SECsApprovalRejectedInfo = "Returned submitted SMTA 2";
        public const string WaitingForSMTA2SECsApprovalRejectedTitle = "";

        public const string SMTA2WorkflowCompleteTitle = "Completed SMTA 2 process";
        public const string SMTA2WorkflowCompleteApprovedInfo = "SMTA 2 approved and signed by Secretariat";
        public const string SMTA2WorkflowCompleteRejectedInfo = "SMTA 2 Rejected";
        public const string SMTA2WorkflowCompleteRejectedTitle = "SMTA 2 Rejected";

    }
}
