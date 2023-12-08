using WHO.BioHub.Models.Models;
using WHO.BioHub.Shared.Enums;

namespace WHO.BioHub.DataManagement.Core.UseCases.Resources.CreateFolder;

public interface ICreateFolderMapper
{
    Resource Map(CreateFolderCommand command);
}

public class CreateFolderMapper : ICreateFolderMapper
{
    public Resource Map(CreateFolderCommand command)
    {        

        Resource resource = new()
        {
            Id = Guid.NewGuid(),
            CreationDate = DateTime.UtcNow,
            UploadTime = DateTime.UtcNow,
            Type = ResourceType.Folder,
            Name = command.Name,
            ParentId = command.ParentId,
            DeletedOn = null,
        };

        return resource;
    }
}