using FluentValidation;

namespace WHO.BioHub.PublicData.Core.UseCases.BSLLevels.ReadBSLLevel;

public class ReadBSLLevelQueryValidator : AbstractValidator<ReadBSLLevelQuery>
{
    public ReadBSLLevelQueryValidator()
    {
        RuleFor(cmd => cmd);
    }
}