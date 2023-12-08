using WHO.BioHub.Models.Models;

namespace WHO.BioHub.DataManagement.Core.UseCases.Countries.CreateCountry;

public record struct CreateCountryCommandResponse(Country Country) { }