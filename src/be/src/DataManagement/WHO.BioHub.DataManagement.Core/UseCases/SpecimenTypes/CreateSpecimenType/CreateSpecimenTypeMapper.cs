using WHO.BioHub.Models.Models;

namespace WHO.BioHub.DataManagement.Core.UseCases.SpecimenTypes.CreateSpecimenType;

public interface ICreateSpecimenTypeMapper
{
    SpecimenType Map(CreateSpecimenTypeCommand command);
}

public class CreateSpecimenTypeMapper : ICreateSpecimenTypeMapper
{
    public SpecimenType Map(CreateSpecimenTypeCommand command)
    {
        // TODO: Implement mapper

        SpecimenType specimenttype = new()
        {
            Id = Guid.NewGuid(),
            CreationDate = DateTime.UtcNow,

            // ...

            DeletedOn = null,
        };

        return specimenttype;
    }
}