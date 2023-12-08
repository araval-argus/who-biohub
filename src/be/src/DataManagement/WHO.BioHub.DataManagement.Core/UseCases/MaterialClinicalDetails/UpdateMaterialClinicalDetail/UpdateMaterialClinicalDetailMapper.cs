using WHO.BioHub.Models.Models;

namespace WHO.BioHub.DataManagement.Core.UseCases.MaterialClinicalDetails.UpdateMaterialClinicalDetail;

public interface IUpdateMaterialClinicalDetailMapper
{
    MaterialClinicalDetail Map(MaterialClinicalDetail materialclinicaldetail, UpdateMaterialClinicalDetailCommand command);
}

public class UpdateMaterialClinicalDetailMapper : IUpdateMaterialClinicalDetailMapper
{
    public MaterialClinicalDetail Map(MaterialClinicalDetail materialclinicaldetail, UpdateMaterialClinicalDetailCommand command)
    {
        // TODO: Implement mapper

        materialclinicaldetail.Id = command.Id;
        materialclinicaldetail.CreationDate = DateTime.UtcNow;

        // ...

        materialclinicaldetail.DeletedOn = null;

        return materialclinicaldetail;
    }
}