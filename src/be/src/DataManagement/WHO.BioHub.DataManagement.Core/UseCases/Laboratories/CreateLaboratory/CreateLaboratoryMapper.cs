using WHO.BioHub.DataManagement.Core.UseCases.Laboratories.CreateLaboratory;
using WHO.BioHub.Models.Models;

namespace WHO.BioHub.DataManagement.Core.UseCases.Laboratories.CreateLaboratory;

public interface ICreateLaboratoryMapper
{
    Laboratory Map(CreateLaboratoryCommand command);
}

public class CreateLaboratoryMapper : ICreateLaboratoryMapper
{
    public Laboratory Map(CreateLaboratoryCommand command)
    {
        Laboratory laboratory = new()
        {
            CreationDate = DateTime.UtcNow,
            Name = command.Name,
            Description = command.Description,
            Abbreviation = command.Abbreviation != null ? command.Abbreviation : string.Empty,
            Address = command.Address,
            Latitude = command.Latitude.GetValueOrDefault(),
            Longitude = command.Longitude.GetValueOrDefault(),
            IsActive = command.IsActive,
            BSLLevelId = command.BSLLevelId,
            CountryId = command.CountryId,
            DeletedOn = null,
            IsPublicFacing = command.IsPublicFacing,
            LastOperationByUserId = command.OperationById,
            LastOperationDate = DateTime.UtcNow
        };

        return laboratory;
    }
}