using FluentValidation;

namespace WHO.BioHub.DataManagement.Core.UseCases.SMTA1WorkflowItems.DownloadSMTA1WorkflowItemFile;

public class DownloadSMTA1WorkflowItemFileQueryValidator : AbstractValidator<DownloadSMTA1WorkflowItemFileQuery>
{
    public DownloadSMTA1WorkflowItemFileQueryValidator()
    {
        RuleFor(cmd => cmd);
    }
}