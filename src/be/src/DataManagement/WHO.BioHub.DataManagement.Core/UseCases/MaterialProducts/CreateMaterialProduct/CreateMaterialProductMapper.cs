using WHO.BioHub.Models.Models;

namespace WHO.BioHub.DataManagement.Core.UseCases.MaterialProducts.CreateMaterialProduct;

public interface ICreateMaterialProductMapper
{
    MaterialProduct Map(CreateMaterialProductCommand command);
}

public class CreateMaterialProductMapper : ICreateMaterialProductMapper
{
    public MaterialProduct Map(CreateMaterialProductCommand command)
    {      

        MaterialProduct materialproduct = new()
        {
            Id = Guid.NewGuid(),
            CreationDate = DateTime.UtcNow,
            Name = command.Name,
            Description = command.Description,
            IsActive = command.IsActive,
            DeletedOn = null,
        };

        return materialproduct;
    }
}