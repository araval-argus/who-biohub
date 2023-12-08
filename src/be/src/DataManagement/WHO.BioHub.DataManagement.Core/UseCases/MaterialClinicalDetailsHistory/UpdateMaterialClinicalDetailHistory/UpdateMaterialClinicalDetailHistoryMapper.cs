using WHO.BioHub.Models.Models;

namespace WHO.BioHub.DataManagement.Core.UseCases.MaterialClinicalDetailsHistory.UpdateMaterialClinicalDetailHistory;

public interface IUpdateMaterialClinicalDetailHistoryMapper
{
    MaterialClinicalDetailHistory Map(MaterialClinicalDetailHistory materialclinicaldetailhistory, UpdateMaterialClinicalDetailHistoryCommand command);
}

public class UpdateMaterialClinicalDetailHistoryMapper : IUpdateMaterialClinicalDetailHistoryMapper
{
    public MaterialClinicalDetailHistory Map(MaterialClinicalDetailHistory materialclinicaldetailhistory, UpdateMaterialClinicalDetailHistoryCommand command)
    {
        // TODO: Implement mapper

        materialclinicaldetailhistory.Id = command.Id;
        materialclinicaldetailhistory.CreationDate = DateTime.UtcNow;

        // ...

        materialclinicaldetailhistory.DeletedOn = null;

        return materialclinicaldetailhistory;
    }
}