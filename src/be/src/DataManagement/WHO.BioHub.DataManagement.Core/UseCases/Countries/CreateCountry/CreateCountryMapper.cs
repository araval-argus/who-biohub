using WHO.BioHub.DataManagement.Core.UseCases.Countries.CreateCountry;
using WHO.BioHub.Models.Models;

namespace WHO.BioHub.DataManagement.Core.UseCases.Countries.CreateCountry;
public interface ICreateCountryMapper
{
    Country Map(CreateCountryCommand command);
}

public class CreateCountryMapper : ICreateCountryMapper
{
    public Country Map(CreateCountryCommand command)
    {     

        Country country = new()
        {
            Id = Guid.NewGuid(),
            CreationDate = DateTime.UtcNow,
            Uncode = command.Uncode,
            Name = command.Name,
            FullName = command.FullName,
            Iso2 = command.Iso2,
            Iso3 = command.Iso3,
            Latitude = command.Latitude,
            Longitude = command.Longitude,
            GmtHour = command.GmtHour,
            GmtMin = command.GmtMin,
            IsActive = command.IsActive,
            DeletedOn = null,
        };

        return country;
    }
}