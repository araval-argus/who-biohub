using FluentValidation;

namespace WHO.BioHub.DataManagement.Core.UseCases.GeneticSequenceDatas.DeleteGeneticSequenceData;

public class DeleteGeneticSequenceDataCommandValidator : AbstractValidator<DeleteGeneticSequenceDataCommand>
{
    public DeleteGeneticSequenceDataCommandValidator()
    {
        RuleFor(cmd => cmd);
    }
}