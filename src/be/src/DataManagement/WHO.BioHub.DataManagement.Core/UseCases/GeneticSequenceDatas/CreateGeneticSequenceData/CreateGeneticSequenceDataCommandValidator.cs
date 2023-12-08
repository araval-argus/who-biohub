using FluentValidation;

namespace WHO.BioHub.DataManagement.Core.UseCases.GeneticSequenceDatas.CreateGeneticSequenceData;

public class CreateGeneticSequenceDataCommandValidator : AbstractValidator<CreateGeneticSequenceDataCommand>
{
    public CreateGeneticSequenceDataCommandValidator()
    {
        RuleFor(cmd => cmd);
    }
}