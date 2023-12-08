using WHO.BioHub.Models.Models;

namespace WHO.BioHub.DataManagement.Core.UseCases.MaterialProducts.UpdateMaterialProduct;

public interface IUpdateMaterialProductMapper
{
    MaterialProduct Map(MaterialProduct materialproduct, UpdateMaterialProductCommand command);
}

public class UpdateMaterialProductMapper : IUpdateMaterialProductMapper
{
    public MaterialProduct Map(MaterialProduct materialproduct, UpdateMaterialProductCommand command)
    {        

        materialproduct.Id = command.Id;
        materialproduct.Name = command.Name;
        materialproduct.Description = command.Description;
        materialproduct.IsActive = command.IsActive;
        materialproduct.DeletedOn = null;

        materialproduct.DeletedOn = null;

        return materialproduct;
    }
}