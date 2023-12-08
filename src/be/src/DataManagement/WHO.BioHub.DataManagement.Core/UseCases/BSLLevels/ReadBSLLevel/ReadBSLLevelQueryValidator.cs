using FluentValidation;

namespace WHO.BioHub.DataManagement.Core.UseCases.BSLLevels.ReadBSLLevel;

public class ReadBSLLevelQueryValidator : AbstractValidator<ReadBSLLevelQuery>
{
    public ReadBSLLevelQueryValidator()
    {
        RuleFor(cmd => cmd);
    }
}