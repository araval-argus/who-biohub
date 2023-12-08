using WHO.BioHub.Models.Models;

namespace WHO.BioHub.DataManagement.Core.UseCases.GeneticSequenceDatas.CreateGeneticSequenceData;

public interface ICreateGeneticSequenceDataMapper
{
    GeneticSequenceData Map(CreateGeneticSequenceDataCommand command);
}

public class CreateGeneticSequenceDataMapper : ICreateGeneticSequenceDataMapper
{
    public GeneticSequenceData Map(CreateGeneticSequenceDataCommand command)
    {        

        GeneticSequenceData geneticsequencedata = new()
        {
            Id = Guid.NewGuid(),
            CreationDate = DateTime.UtcNow,
            Code = command.Code,
            Name = command.Name,
            Description = command.Description,
            IsActive = command.IsActive,
            DeletedOn = null,
        };

        return geneticsequencedata;
    }
}