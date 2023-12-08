using WHO.BioHub.Models.Models;

namespace WHO.BioHub.DataManagement.Core.UseCases.IsolationHostTypes.UpdateIsolationHostType;

public interface IUpdateIsolationHostTypeMapper
{
    IsolationHostType Map(IsolationHostType isolationhosttype, UpdateIsolationHostTypeCommand command);
}

public class UpdateIsolationHostTypeMapper : IUpdateIsolationHostTypeMapper
{
    public IsolationHostType Map(IsolationHostType isolationhosttype, UpdateIsolationHostTypeCommand command)
    {
        
        isolationhosttype.Id = command.Id;
        isolationhosttype.Name = command.Name;
        isolationhosttype.Description = command.Description;
        isolationhosttype.IsActive = command.IsActive;
        isolationhosttype.DeletedOn = null;

        return isolationhosttype;
    }
}