using FluentValidation;

namespace WHO.BioHub.PublicData.Core.UseCases.GeneticSequenceDatas.ListGeneticSequenceDatas;

public class ListGeneticSequenceDatasQueryValidator : AbstractValidator<ListGeneticSequenceDatasQuery>
{
    public ListGeneticSequenceDatasQueryValidator()
    {
        RuleFor(cmd => cmd);
    }
}