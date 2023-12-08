using WHO.BioHub.Models.Models;

namespace WHO.BioHub.DataManagement.Core.UseCases.MaterialClinicalDetails.CreateMaterialClinicalDetail;

public interface ICreateMaterialClinicalDetailMapper
{
    MaterialClinicalDetail Map(CreateMaterialClinicalDetailCommand command);
}

public class CreateMaterialClinicalDetailMapper : ICreateMaterialClinicalDetailMapper
{
    public MaterialClinicalDetail Map(CreateMaterialClinicalDetailCommand command)
    {
        // TODO: Implement mapper

        MaterialClinicalDetail materialclinicaldetail = new()
        {
            Id = Guid.NewGuid(),
            CreationDate = DateTime.UtcNow,

            // ...

            DeletedOn = null,
        };

        return materialclinicaldetail;
    }
}