using FluentValidation;

namespace WHO.BioHub.DataManagement.Core.UseCases.WorklistToBioHubItems.DownloadWorklistToBioHubItemFile;

public class DownloadWorklistToBioHubItemFileQueryValidator : AbstractValidator<DownloadWorklistToBioHubItemFileQuery>
{
    public DownloadWorklistToBioHubItemFileQueryValidator()
    {
        RuleFor(cmd => cmd);
    }
}