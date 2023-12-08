using WHO.BioHub.Shared.Utils;
using WHO.BioHub.Shared.Worklists;

namespace WHO.BioHub.Shared.Enums
{
    public enum SMTA1WorkflowStatus
    {

        [StatusDescription(
        SMTA1WorkflowStatusNames.RequestInitiationTitle,
        SMTA1WorkflowStatusNames.RequestInitiationApprovedInfo,
        SMTA1WorkflowStatusNames.RequestInitiationRejectedInfo,
        SMTA1WorkflowStatusNames.RequestInitiationRejectedTitle)]
        RequestInitiation = 0,

        [StatusDescription(
            SMTA1WorkflowStatusNames.SubmitSMTA1Title,
            SMTA1WorkflowStatusNames.SubmitSMTA1ApprovedInfo,
            SMTA1WorkflowStatusNames.SubmitSMTA1RejectedInfo,
            SMTA1WorkflowStatusNames.SubmitSMTA1RejectedTitle)]
        SubmitSMTA1 = 1,

        [StatusDescription(
            SMTA1WorkflowStatusNames.WaitingForSMTA1SECsApprovalTitle,
            SMTA1WorkflowStatusNames.WaitingForSMTA1SECsApprovalApprovedInfo,
            SMTA1WorkflowStatusNames.WaitingForSMTA1SECsApprovalRejectedInfo,
            SMTA1WorkflowStatusNames.WaitingForSMTA1SECsApprovalRejectedTitle)]
        WaitingForSMTA1SECsApproval = 2,

        [StatusDescription(
            SMTA1WorkflowStatusNames.SMTA1WorkflowCompleteTitle,
            SMTA1WorkflowStatusNames.SubmitSMTA1ApprovedInfo,
            SMTA1WorkflowStatusNames.SMTA1WorkflowCompleteRejectedInfo,
            SMTA1WorkflowStatusNames.SMTA1WorkflowCompleteRejectedTitle)]
        SMTA1WorkflowComplete = 3,
    }
}
