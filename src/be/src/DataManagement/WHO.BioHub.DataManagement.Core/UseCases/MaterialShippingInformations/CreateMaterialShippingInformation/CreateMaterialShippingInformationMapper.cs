using WHO.BioHub.Models.Models;

namespace WHO.BioHub.DataManagement.Core.UseCases.MaterialShippingInformations.CreateMaterialShippingInformation;

public interface ICreateMaterialShippingInformationMapper
{
    MaterialShippingInformation Map(CreateMaterialShippingInformationCommand command);
}

public class CreateMaterialShippingInformationMapper : ICreateMaterialShippingInformationMapper
{
    public MaterialShippingInformation Map(CreateMaterialShippingInformationCommand command)
    {
        // TODO: Implement mapper

        MaterialShippingInformation materialshippinginformation = new()
        {
            Id = Guid.NewGuid(),
            CreationDate = DateTime.UtcNow,

            // ...

            DeletedOn = null,
        };

        return materialshippinginformation;
    }
}