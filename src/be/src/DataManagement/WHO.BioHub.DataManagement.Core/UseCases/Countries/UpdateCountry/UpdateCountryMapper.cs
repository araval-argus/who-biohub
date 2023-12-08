using WHO.BioHub.DataManagement.Core.UseCases.Countries.UpdateCountry;
using WHO.BioHub.Models.Models;

namespace WHO.BioHub.DataManagement.Core.UseCases.Countries.UpdateCountry;

public interface IUpdateCountryMapper
{
    Country Map(Country country, UpdateCountryCommand command);
}

public class UpdateCountryMapper : IUpdateCountryMapper
{
    public Country Map(Country country, UpdateCountryCommand command)
    {       

        country.Id = command.Id;
        country.CreationDate = DateTime.UtcNow;
        country.Uncode = command.Uncode;
        country.Name = command.Name;
        country.FullName = command.FullName;
        country.Iso2 = command.Iso2;
        country.Iso3 = command.Iso3;
        country.Latitude = command.Latitude;
        country.Longitude = command.Longitude;
        country.GmtHour = command.GmtHour;
        country.GmtMin = command.GmtMin;
        country.IsActive = command.IsActive;
        country.DeletedOn = null;

        return country;
    }
}