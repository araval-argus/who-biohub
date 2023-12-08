using WHO.BioHub.Models.Models;

namespace WHO.BioHub.DataManagement.Core.UseCases.CultivabilityTypes.UpdateCultivabilityType;

public interface IUpdateCultivabilityTypeMapper
{
    CultivabilityType Map(CultivabilityType cultivabilitytype, UpdateCultivabilityTypeCommand command);
}

public class UpdateCultivabilityTypeMapper : IUpdateCultivabilityTypeMapper
{
    public CultivabilityType Map(CultivabilityType cultivabilitytype, UpdateCultivabilityTypeCommand command)
    {      

        cultivabilitytype.Id = command.Id;
        cultivabilitytype.Name = command.Name;
        cultivabilitytype.Description = command.Description;
        cultivabilitytype.IsActive = command.IsActive;
        cultivabilitytype.DeletedOn = null;

        return cultivabilitytype;
    }
}