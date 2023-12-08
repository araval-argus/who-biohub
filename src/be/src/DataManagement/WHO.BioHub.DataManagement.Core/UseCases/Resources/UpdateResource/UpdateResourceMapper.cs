using WHO.BioHub.Models.Models;

namespace WHO.BioHub.DataManagement.Core.UseCases.Resources.UpdateResource;

public interface IUpdateResourceMapper
{
    Resource Map(Resource resource, UpdateResourceCommand command);
}

public class UpdateResourceMapper : IUpdateResourceMapper
{
    public Resource Map(Resource resource, UpdateResourceCommand command)
    {      
                
        resource.Name = command.Name;       
        resource.DeletedOn = null;

        return resource;
    }
}