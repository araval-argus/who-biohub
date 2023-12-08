using FluentValidation;

namespace WHO.BioHub.DataManagement.Core.UseCases.BioHubFacilities.UpdateBioHubFacility;

public class UpdateBioHubFacilityCommandValidator : AbstractValidator<UpdateBioHubFacilityCommand>
{
    public UpdateBioHubFacilityCommandValidator()
    {
        RuleFor(cmd => cmd.Name)
            .NotEmpty().WithMessage("'Name' is required");

        RuleFor(cmd => cmd.Abbreviation)
            .NotEmpty().WithMessage("'Abbreviation' is required");

        RuleFor(cmd => cmd.Description)
            .NotEmpty().WithMessage("'Description' is required");

        RuleFor(cmd => cmd.Address)
            .NotEmpty().WithMessage("'Address' is required");

        RuleFor(cmd => cmd.Latitude)
            .NotNull().WithMessage("'Latitude' is required");

        RuleFor(cmd => cmd.Longitude)
            .NotNull().WithMessage("'Longitude' is required");
    }
}