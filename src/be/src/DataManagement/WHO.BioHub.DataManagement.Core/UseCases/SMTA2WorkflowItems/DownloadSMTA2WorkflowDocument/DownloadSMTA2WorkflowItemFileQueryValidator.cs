using FluentValidation;

namespace WHO.BioHub.DataManagement.Core.UseCases.SMTA2WorkflowItems.DownloadSMTA2WorkflowItemFile;

public class DownloadSMTA2WorkflowItemFileQueryValidator : AbstractValidator<DownloadSMTA2WorkflowItemFileQuery>
{
    public DownloadSMTA2WorkflowItemFileQueryValidator()
    {
        RuleFor(cmd => cmd);
    }
}