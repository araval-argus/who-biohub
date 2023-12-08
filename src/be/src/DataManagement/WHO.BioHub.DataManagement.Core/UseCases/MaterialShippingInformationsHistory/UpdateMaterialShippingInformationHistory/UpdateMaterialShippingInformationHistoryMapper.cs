using WHO.BioHub.Models.Models;

namespace WHO.BioHub.DataManagement.Core.UseCases.MaterialShippingInformationsHistory.UpdateMaterialShippingInformationHistory;

public interface IUpdateMaterialShippingInformationHistoryMapper
{
    MaterialShippingInformationHistory Map(MaterialShippingInformationHistory materialshippinginformationHistory, UpdateMaterialShippingInformationHistoryCommand command);
}

public class UpdateMaterialShippingInformationHistoryMapper : IUpdateMaterialShippingInformationHistoryMapper
{
    public MaterialShippingInformationHistory Map(MaterialShippingInformationHistory materialshippinginformationHistory, UpdateMaterialShippingInformationHistoryCommand command)
    {
        // TODO: Implement mapper

        materialshippinginformationHistory.Id = command.Id;
        materialshippinginformationHistory.CreationDate = DateTime.UtcNow;

        // ...

        materialshippinginformationHistory.DeletedOn = null;

        return materialshippinginformationHistory;
    }
}