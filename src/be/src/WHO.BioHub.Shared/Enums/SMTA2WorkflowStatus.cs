using WHO.BioHub.Shared.Utils;
using WHO.BioHub.Shared.Worklists;

namespace WHO.BioHub.Shared.Enums
{
    public enum SMTA2WorkflowStatus
    {
        [StatusDescription(
                SMTA2WorkflowStatusNames.RequestInitiationTitle,
                SMTA2WorkflowStatusNames.RequestInitiationApprovedInfo,
                SMTA2WorkflowStatusNames.RequestInitiationRejectedInfo,
                SMTA2WorkflowStatusNames.RequestInitiationRejectedTitle)]
        RequestInitiation = 0,

        [StatusDescription(
            SMTA2WorkflowStatusNames.SubmitSMTA2Title,
            SMTA2WorkflowStatusNames.SubmitSMTA2ApprovedInfo,
            SMTA2WorkflowStatusNames.SubmitSMTA2RejectedInfo,
            SMTA2WorkflowStatusNames.SubmitSMTA2RejectedTitle)]
        SubmitSMTA2 = 1,

        [StatusDescription(
            SMTA2WorkflowStatusNames.WaitingForSMTA2SECsApprovalTitle,
            SMTA2WorkflowStatusNames.WaitingForSMTA2SECsApprovalApprovedInfo,
            SMTA2WorkflowStatusNames.WaitingForSMTA2SECsApprovalRejectedInfo,
            SMTA2WorkflowStatusNames.WaitingForSMTA2SECsApprovalRejectedTitle)]
        WaitingForSMTA2SECsApproval = 2,

        [StatusDescription(
            SMTA2WorkflowStatusNames.SMTA2WorkflowCompleteTitle,
            SMTA2WorkflowStatusNames.SubmitSMTA2ApprovedInfo,
            SMTA2WorkflowStatusNames.SMTA2WorkflowCompleteRejectedInfo,
            SMTA2WorkflowStatusNames.SMTA2WorkflowCompleteRejectedTitle)]
        SMTA2WorkflowComplete = 3,
    }
}

