using FluentValidation;

namespace WHO.BioHub.DataManagement.Core.UseCases.GeneticSequenceDatas.UpdateGeneticSequenceData;

public class UpdateGeneticSequenceDataCommandValidator : AbstractValidator<UpdateGeneticSequenceDataCommand>
{
    public UpdateGeneticSequenceDataCommandValidator()
    {
        RuleFor(cmd => cmd);
    }
}