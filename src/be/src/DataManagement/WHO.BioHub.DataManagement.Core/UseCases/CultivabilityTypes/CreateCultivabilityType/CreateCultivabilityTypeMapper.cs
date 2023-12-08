using WHO.BioHub.Models.Models;

namespace WHO.BioHub.DataManagement.Core.UseCases.CultivabilityTypes.CreateCultivabilityType;

public interface ICreateCultivabilityTypeMapper
{
    CultivabilityType Map(CreateCultivabilityTypeCommand command);
}

public class CreateCultivabilityTypeMapper : ICreateCultivabilityTypeMapper
{
    public CultivabilityType Map(CreateCultivabilityTypeCommand command)
    {       

        CultivabilityType cultivabilitytype = new()
        {
            Id = Guid.NewGuid(),
            CreationDate = DateTime.UtcNow,
            Name = command.Name,
            Description = command.Description,
            IsActive = command.IsActive,
            DeletedOn = null,
        };

        return cultivabilitytype;
    }
}