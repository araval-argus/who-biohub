using FluentValidation;

namespace WHO.BioHub.PublicData.Core.UseCases.GeneticSequenceDatas.ReadGeneticSequenceData;

public class ReadGeneticSequenceDataQueryValidator : AbstractValidator<ReadGeneticSequenceDataQuery>
{
    public ReadGeneticSequenceDataQueryValidator()
    {
        RuleFor(cmd => cmd);
    }
}