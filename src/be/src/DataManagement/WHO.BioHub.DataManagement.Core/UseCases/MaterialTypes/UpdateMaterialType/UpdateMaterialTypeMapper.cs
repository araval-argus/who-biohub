using WHO.BioHub.DataManagement.Core.UseCases.MaterialTypes.UpdateMaterialType;
using WHO.BioHub.Models.Models;

namespace WHO.BioHub.DataManagement.Core.UseCases.MaterialTypes.UpdateMaterialType;

public interface IUpdateMaterialTypeMapper
{
    MaterialType Map(MaterialType materialtype, UpdateMaterialTypeCommand command);
}

public class UpdateMaterialTypeMapper : IUpdateMaterialTypeMapper
{
    public MaterialType Map(MaterialType materialtype, UpdateMaterialTypeCommand command)
    {        

        materialtype.Id = command.Id;
        materialtype.Name = command.Name;
        materialtype.Description = command.Description;
        materialtype.IsActive = command.IsActive;
        materialtype.DeletedOn = null;
        return materialtype;
    }
}