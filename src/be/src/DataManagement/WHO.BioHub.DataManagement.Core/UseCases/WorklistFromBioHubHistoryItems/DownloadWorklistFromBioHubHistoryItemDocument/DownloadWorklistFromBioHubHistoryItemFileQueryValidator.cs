using FluentValidation;

namespace WHO.BioHub.DataManagement.Core.UseCases.WorklistFromBioHubHistoryItems.DownloadWorklistFromBioHubHistoryItemFile;

public class DownloadWorklistFromBioHubHistoryItemFileQueryValidator : AbstractValidator<DownloadWorklistFromBioHubHistoryItemFileQuery>
{
    public DownloadWorklistFromBioHubHistoryItemFileQueryValidator()
    {
        RuleFor(cmd => cmd);
    }
}