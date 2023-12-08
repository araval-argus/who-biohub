using FluentValidation;

namespace WHO.BioHub.DataManagement.Core.UseCases.BioHubFacilities.DeleteBioHubFacility;

public class DeleteBioHubFacilityCommandValidator : AbstractValidator<DeleteBioHubFacilityCommand>
{
    public DeleteBioHubFacilityCommandValidator()
    {
        RuleFor(cmd => cmd);
    }
}