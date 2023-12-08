using WHO.BioHub.Models.Models;

namespace WHO.BioHub.DataManagement.Core.UseCases.IsolationTechniqueTypes.CreateIsolationTechniqueType;

public interface ICreateIsolationTechniqueTypeMapper
{
    IsolationTechniqueType Map(CreateIsolationTechniqueTypeCommand command);
}

public class CreateIsolationTechniqueTypeMapper : ICreateIsolationTechniqueTypeMapper
{
    public IsolationTechniqueType Map(CreateIsolationTechniqueTypeCommand command)
    {        

        IsolationTechniqueType isolationtechniquetype = new()
        {
            Id = Guid.NewGuid(),
            CreationDate = DateTime.UtcNow,
            Name = command.Name,
            Description = command.Description,
            IsActive = command.IsActive,
            DeletedOn = null,
        };

        return isolationtechniquetype;
    }
}