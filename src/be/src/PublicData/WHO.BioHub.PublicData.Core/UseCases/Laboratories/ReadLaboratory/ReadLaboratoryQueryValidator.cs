using FluentValidation;

namespace WHO.BioHub.PublicData.Core.UseCases.Laboratories.ReadLaboratory;

public class ReadLaboratoryQueryValidator : AbstractValidator<ReadLaboratoryQuery>
{
    public ReadLaboratoryQueryValidator()
    {
        RuleFor(cmd => cmd);
    }
}