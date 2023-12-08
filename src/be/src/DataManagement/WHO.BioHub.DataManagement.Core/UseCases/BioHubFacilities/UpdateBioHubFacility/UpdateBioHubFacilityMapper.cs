using WHO.BioHub.DataManagement.Core.UseCases.BioHubFacilities.UpdateBioHubFacility;
using WHO.BioHub.Models.Models;

namespace WHO.BioHub.DataManagement.Core.UseCases.BioHubFacilities.UpdateBioHubFacility;
public interface IUpdateBioHubFacilityMapper
{
    BioHubFacility Map(BioHubFacility biohubfacility, UpdateBioHubFacilityCommand command);
}

public class UpdateBioHubFacilityMapper : IUpdateBioHubFacilityMapper
{
    public BioHubFacility Map(BioHubFacility biohubfacility, UpdateBioHubFacilityCommand command)
    {       

        biohubfacility.Id = command.Id;
        biohubfacility.Name = command.Name;
        biohubfacility.Description = command.Description;
        biohubfacility.Abbreviation = command.Abbreviation;
        biohubfacility.Address = command.Address;
        biohubfacility.Latitude = command.Latitude.GetValueOrDefault();
        biohubfacility.Longitude = command.Longitude.GetValueOrDefault();
        biohubfacility.IsActive = command.IsActive;
        biohubfacility.BSLLevelId = command.BSLLevelId;
        biohubfacility.CountryId = command.CountryId;
        biohubfacility.IsPublicFacing = command.IsPublicFacing;
        biohubfacility.LastOperationByUserId = command.OperationById;
        biohubfacility.LastOperationDate = DateTime.UtcNow;

        biohubfacility.DeletedOn = null;

        return biohubfacility;
    }
}