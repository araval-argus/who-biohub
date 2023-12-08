namespace WHO.BioHub.DataManagement.Core.UseCases.GeneticSequenceDatas.CreateGeneticSequenceData;

public record struct CreateGeneticSequenceDataCommand(
    string Code,
    string Name,
    string Description,
    bool IsActive
);