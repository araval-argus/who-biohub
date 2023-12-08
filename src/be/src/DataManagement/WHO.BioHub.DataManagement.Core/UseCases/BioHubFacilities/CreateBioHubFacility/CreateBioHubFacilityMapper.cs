using WHO.BioHub.Models.Models;

namespace WHO.BioHub.DataManagement.Core.UseCases.BioHubFacilities.CreateBioHubFacility;

public interface ICreateBioHubFacilityMapper
{
    BioHubFacility Map(CreateBioHubFacilityCommand command);
}

public class CreateBioHubFacilityMapper : ICreateBioHubFacilityMapper
{
    public BioHubFacility Map(CreateBioHubFacilityCommand command)
    {     

        BioHubFacility biohubfacility = new()
        {
            Id = Guid.NewGuid(),
            CreationDate = DateTime.UtcNow,
            Name = command.Name,
            Description = command.Description,
            Abbreviation = command.Abbreviation,
            Address = command.Address,
            Latitude = command.Latitude.GetValueOrDefault(),
            Longitude = command.Longitude.GetValueOrDefault(),
            IsActive = command.IsActive,
            BSLLevelId = command.BSLLevelId,
            CountryId = command.CountryId,
            IsPublicFacing = command.IsPublicFacing,
            DeletedOn = null,
            LastOperationByUserId = command.OperationById,
            LastOperationDate = DateTime.UtcNow
        };

        return biohubfacility;
    }
}