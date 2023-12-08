using WHO.BioHub.DataManagement.Core.UseCases.Laboratories.UpdateLaboratory;
using WHO.BioHub.Models.Models;

namespace WHO.BioHub.DataManagement.Core.UseCases.Laboratories.UpdateLaboratory;

public interface IUpdateLaboratoryMapper
{
    Laboratory Map(Laboratory laboratory, UpdateLaboratoryCommand command);
}

public class UpdateLaboratoryMapper : IUpdateLaboratoryMapper
{
    public Laboratory Map(Laboratory laboratory, UpdateLaboratoryCommand command)
    {
        laboratory.Id = command.Id;
        laboratory.Name = command.Name;
        laboratory.Description = command.Description;
        laboratory.Abbreviation = command.Abbreviation != null ? command.Abbreviation : string.Empty;
        laboratory.Address = command.Address;
        laboratory.Latitude = command.Latitude.GetValueOrDefault();
        laboratory.Longitude = command.Longitude.GetValueOrDefault();
        laboratory.IsActive = command.IsActive;
        laboratory.BSLLevelId = command.BSLLevelId;
        laboratory.CountryId = command.CountryId;
        laboratory.IsPublicFacing = command.IsPublicFacing;
        laboratory.DeletedOn = null;
        laboratory.LastOperationByUserId = command.OperationById;
        laboratory.LastOperationDate = DateTime.UtcNow;

        return laboratory;
    }
}