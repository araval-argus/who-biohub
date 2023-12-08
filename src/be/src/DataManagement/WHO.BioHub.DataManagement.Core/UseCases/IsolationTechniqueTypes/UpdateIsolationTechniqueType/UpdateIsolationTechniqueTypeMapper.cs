using WHO.BioHub.Models.Models;

namespace WHO.BioHub.DataManagement.Core.UseCases.IsolationTechniqueTypes.UpdateIsolationTechniqueType;

public interface IUpdateIsolationTechniqueTypeMapper
{
    IsolationTechniqueType Map(IsolationTechniqueType isolationtechniquetype, UpdateIsolationTechniqueTypeCommand command);
}

public class UpdateIsolationTechniqueTypeMapper : IUpdateIsolationTechniqueTypeMapper
{
    public IsolationTechniqueType Map(IsolationTechniqueType isolationtechniquetype, UpdateIsolationTechniqueTypeCommand command)
    {
        
        isolationtechniquetype.Id = command.Id;
        isolationtechniquetype.Name = command.Name;
        isolationtechniquetype.Description = command.Description;
        isolationtechniquetype.IsActive = command.IsActive;
        isolationtechniquetype.DeletedOn = null;

        return isolationtechniquetype;
    }
}