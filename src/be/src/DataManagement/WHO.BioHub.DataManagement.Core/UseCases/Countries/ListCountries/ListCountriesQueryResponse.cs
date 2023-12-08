using WHO.BioHub.Models.Models;
using WHO.BioHub.Shared.Dto;

namespace WHO.BioHub.DataManagement.Core.UseCases.Countries.ListCountries;

public record struct ListCountriesQueryResponse(IEnumerable<CountryDto> Countries) { }