using FluentValidation;

namespace WHO.BioHub.DataManagement.Core.UseCases.SMTA2WorkflowHistoryItems.DownloadSMTA2WorkflowHistoryItemFile;

public class DownloadSMTA2WorkflowHistoryItemFileQueryValidator : AbstractValidator<DownloadSMTA2WorkflowHistoryItemFileQuery>
{
    public DownloadSMTA2WorkflowHistoryItemFileQueryValidator()
    {
        RuleFor(cmd => cmd);
    }
}