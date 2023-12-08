using WHO.BioHub.Models.Models;

namespace WHO.BioHub.DataManagement.Core.UseCases.MaterialTypes.CreateMaterialType;

public interface ICreateMaterialTypeMapper
{
    MaterialType Map(CreateMaterialTypeCommand command);
}

public class CreateMaterialTypeMapper : ICreateMaterialTypeMapper
{
    public MaterialType Map(CreateMaterialTypeCommand command)
    {      

        MaterialType materialtype = new()
        {
            Id = Guid.NewGuid(),
            CreationDate = DateTime.UtcNow,
            Name = command.Name,
            Description = command.Description,
            IsActive = command.IsActive,
            DeletedOn = null,
        };

        return materialtype;
    }
}