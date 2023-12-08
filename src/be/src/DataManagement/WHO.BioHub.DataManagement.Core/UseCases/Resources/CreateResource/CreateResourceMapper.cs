using WHO.BioHub.Models.Models;
using WHO.BioHub.Shared.Enums;

namespace WHO.BioHub.DataManagement.Core.UseCases.Resources.CreateResource;

public interface ICreateResourceMapper
{
    Resource Map(CreateResourceCommand command);
}

public class CreateResourceMapper : ICreateResourceMapper
{
    public Resource Map(CreateResourceCommand command)
    {        
        Resource resource = new()
        {
            Id = command.Id.GetValueOrDefault(),
            CreationDate = DateTime.UtcNow,
            UploadTime = DateTime.UtcNow,           
            Name = GetFileName(command.FileCompleteName),
            Extension = GetFileExtension(command.FileCompleteName),
            UploadedById = command.UploadedById,
            Type = ResourceType.File,
            FileType = command.FileType,          
            ParentId = command.ParentId,
            DeletedOn = null,
        };

        return resource;
    }

    

    private string GetFileExtension(string fileCompleteName)
    {
        var nameParts = fileCompleteName.Split(".");

        return nameParts.LastOrDefault() ?? string.Empty;
    }

    private string GetFileName(string fileCompleteName)
    {
        var nameParts = fileCompleteName.Split(".");        

        string fileName = string.Join(".", nameParts.SkipLast(1).ToArray());

        return fileName;
    }
}