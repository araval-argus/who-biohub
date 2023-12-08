using FluentValidation;

namespace WHO.BioHub.DataManagement.Core.UseCases.SpecimenTypes.ReadSpecimenType;

public class ReadSpecimenTypeQueryValidator : AbstractValidator<ReadSpecimenTypeQuery>
{
    public ReadSpecimenTypeQueryValidator()
    {
        RuleFor(cmd => cmd);
    }
}