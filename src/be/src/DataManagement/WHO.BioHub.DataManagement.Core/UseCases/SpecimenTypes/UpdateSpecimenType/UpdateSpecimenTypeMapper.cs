using WHO.BioHub.Models.Models;

namespace WHO.BioHub.DataManagement.Core.UseCases.SpecimenTypes.UpdateSpecimenType;

public interface IUpdateSpecimenTypeMapper
{
    SpecimenType Map(SpecimenType specimenttype, UpdateSpecimenTypeCommand command);
}

public class UpdateSpecimenTypeMapper : IUpdateSpecimenTypeMapper
{
    public SpecimenType Map(SpecimenType specimenttype, UpdateSpecimenTypeCommand command)
    {
        // TODO: Implement mapper

        specimenttype.Id = command.Id;
        specimenttype.CreationDate = DateTime.UtcNow;

        // ...

        specimenttype.DeletedOn = null;

        return specimenttype;
    }
}