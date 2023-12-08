using FluentValidation;

namespace WHO.BioHub.DataManagement.Core.UseCases.BSLLevels.ListBSLLevels;

public class ListBSLLevelsQueryValidator : AbstractValidator<ListBSLLevelsQuery>
{
    public ListBSLLevelsQueryValidator()
    {
        RuleFor(cmd => cmd);
    }
}