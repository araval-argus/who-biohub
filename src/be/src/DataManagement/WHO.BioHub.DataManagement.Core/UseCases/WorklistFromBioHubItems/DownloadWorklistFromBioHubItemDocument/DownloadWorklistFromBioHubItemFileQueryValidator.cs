using FluentValidation;

namespace WHO.BioHub.DataManagement.Core.UseCases.WorklistFromBioHubItems.DownloadWorklistFromBioHubItemFile;

public class DownloadWorklistFromBioHubItemFileQueryValidator : AbstractValidator<DownloadWorklistFromBioHubItemFileQuery>
{
    public DownloadWorklistFromBioHubItemFileQueryValidator()
    {
        RuleFor(cmd => cmd);
    }
}