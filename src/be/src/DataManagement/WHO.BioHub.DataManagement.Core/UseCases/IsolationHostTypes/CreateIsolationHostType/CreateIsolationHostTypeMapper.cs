using WHO.BioHub.Models.Models;

namespace WHO.BioHub.DataManagement.Core.UseCases.IsolationHostTypes.CreateIsolationHostType;

public interface ICreateIsolationHostTypeMapper
{
    IsolationHostType Map(CreateIsolationHostTypeCommand command);
}

public class CreateIsolationHostTypeMapper : ICreateIsolationHostTypeMapper
{
    public IsolationHostType Map(CreateIsolationHostTypeCommand command)
    {       

        IsolationHostType isolationhosttype = new()
        {
            Id = Guid.NewGuid(),
            CreationDate = DateTime.UtcNow,
            Name = command.Name,
            Description = command.Description,
            IsActive = command.IsActive,
            DeletedOn = null,
        };

        return isolationhosttype;
    }
}