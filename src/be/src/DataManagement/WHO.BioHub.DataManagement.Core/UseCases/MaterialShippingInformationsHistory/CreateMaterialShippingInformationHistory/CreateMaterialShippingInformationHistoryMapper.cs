using WHO.BioHub.Models.Models;

namespace WHO.BioHub.DataManagement.Core.UseCases.MaterialShippingInformationsHistory.CreateMaterialShippingInformationHistory;

public interface ICreateMaterialShippingInformationHistoryMapper
{
    MaterialShippingInformationHistory Map(CreateMaterialShippingInformationHistoryCommand command);
}

public class CreateMaterialShippingInformationHistoryMapper : ICreateMaterialShippingInformationHistoryMapper
{
    public MaterialShippingInformationHistory Map(CreateMaterialShippingInformationHistoryCommand command)
    {
        // TODO: Implement mapper

        MaterialShippingInformationHistory materialshippinginformationHistory = new()
        {
            Id = Guid.NewGuid(),
            CreationDate = DateTime.UtcNow,

            // ...

            DeletedOn = null,
        };

        return materialshippinginformationHistory;
    }
}