using WHO.BioHub.Models.Models;

namespace WHO.BioHub.DataManagement.Core.UseCases.GeneticSequenceDatas.UpdateGeneticSequenceData;

public interface IUpdateGeneticSequenceDataMapper
{
    GeneticSequenceData Map(GeneticSequenceData geneticsequencedata, UpdateGeneticSequenceDataCommand command);
}

public class UpdateGeneticSequenceDataMapper : IUpdateGeneticSequenceDataMapper
{
    public GeneticSequenceData Map(GeneticSequenceData geneticsequencedata, UpdateGeneticSequenceDataCommand command)
    {              

        geneticsequencedata.Id = command.Id;
        geneticsequencedata.Code = command.Code;
        geneticsequencedata.Name = command.Name;
        geneticsequencedata.Description = command.Description;
        geneticsequencedata.IsActive = command.IsActive;
        geneticsequencedata.DeletedOn = null;

        return geneticsequencedata;
    }
}