namespace WHO.BioHub.DataManagement.Core.UseCases.GeneticSequenceDatas.UpdateGeneticSequenceData;

public record struct UpdateGeneticSequenceDataCommand(Guid Id,
    string Code,
    string Name,
    string Description,
    bool IsActive);