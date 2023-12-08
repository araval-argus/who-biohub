using FluentValidation;

namespace WHO.BioHub.DataManagement.Core.UseCases.SMTA1WorkflowHistoryItems.DownloadSMTA1WorkflowHistoryItemFile;

public class DownloadSMTA1WorkflowHistoryItemFileQueryValidator : AbstractValidator<DownloadSMTA1WorkflowHistoryItemFileQuery>
{
    public DownloadSMTA1WorkflowHistoryItemFileQueryValidator()
    {
        RuleFor(cmd => cmd);
    }
}