using WHO.BioHub.Models.Models;
using WHO.BioHub.PublicData.Core.Dto;

namespace WHO.BioHub.PublicData.Core.UseCases.Countries.ListCountries;

public record struct ListCountriesQueryResponse(IEnumerable<CountryPublicDto> Countries) { }