using WHO.BioHub.Models.Models;

namespace WHO.BioHub.DataManagement.Core.UseCases.MaterialClinicalDetailsHistory.CreateMaterialClinicalDetailHistory;

public interface ICreateMaterialClinicalDetailHistoryMapper
{
    MaterialClinicalDetailHistory Map(CreateMaterialClinicalDetailHistoryCommand command);
}

public class CreateMaterialClinicalDetailHistoryMapper : ICreateMaterialClinicalDetailHistoryMapper
{
    public MaterialClinicalDetailHistory Map(CreateMaterialClinicalDetailHistoryCommand command)
    {
        // TODO: Implement mapper

        MaterialClinicalDetailHistory materialclinicaldetailhistory = new()
        {
            Id = Guid.NewGuid(),
            CreationDate = DateTime.UtcNow,

            // ...

            DeletedOn = null,
        };

        return materialclinicaldetailhistory;
    }
}