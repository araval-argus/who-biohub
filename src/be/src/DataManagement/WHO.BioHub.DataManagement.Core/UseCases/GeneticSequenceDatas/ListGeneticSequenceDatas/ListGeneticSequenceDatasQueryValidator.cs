using FluentValidation;

namespace WHO.BioHub.DataManagement.Core.UseCases.GeneticSequenceDatas.ListGeneticSequenceDatas;

public class ListGeneticSequenceDatasQueryValidator : AbstractValidator<ListGeneticSequenceDatasQuery>
{
    public ListGeneticSequenceDatasQueryValidator()
    {
        RuleFor(cmd => cmd);
    }
}