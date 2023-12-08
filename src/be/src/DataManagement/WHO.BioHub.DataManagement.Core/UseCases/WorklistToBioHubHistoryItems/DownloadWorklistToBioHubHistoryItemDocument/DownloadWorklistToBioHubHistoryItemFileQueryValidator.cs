using FluentValidation;

namespace WHO.BioHub.DataManagement.Core.UseCases.WorklistToBioHubHistoryItems.DownloadWorklistToBioHubHistoryItemFile;

public class DownloadWorklistToBioHubHistoryItemFileQueryValidator : AbstractValidator<DownloadWorklistToBioHubHistoryItemFileQuery>
{
    public DownloadWorklistToBioHubHistoryItemFileQueryValidator()
    {
        RuleFor(cmd => cmd);
    }
}