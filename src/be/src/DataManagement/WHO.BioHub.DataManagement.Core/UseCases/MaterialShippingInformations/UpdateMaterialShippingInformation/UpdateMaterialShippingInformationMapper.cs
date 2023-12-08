using WHO.BioHub.Models.Models;

namespace WHO.BioHub.DataManagement.Core.UseCases.MaterialShippingInformations.UpdateMaterialShippingInformation;

public interface IUpdateMaterialShippingInformationMapper
{
    MaterialShippingInformation Map(MaterialShippingInformation materialshippinginformation, UpdateMaterialShippingInformationCommand command);
}

public class UpdateMaterialShippingInformationMapper : IUpdateMaterialShippingInformationMapper
{
    public MaterialShippingInformation Map(MaterialShippingInformation materialshippinginformation, UpdateMaterialShippingInformationCommand command)
    {
        // TODO: Implement mapper

        materialshippinginformation.Id = command.Id;
        materialshippinginformation.CreationDate = DateTime.UtcNow;

        // ...

        materialshippinginformation.DeletedOn = null;

        return materialshippinginformation;
    }
}