using WHO.BioHub.Models.Models;

namespace WHO.BioHub.Search.Core.UseCases.Laboratories.SearchLaboratoriesByCountry;

public record struct SearchLaboratoriesByCountryQueryResponse(IEnumerable<Laboratory> Laboratories) { }