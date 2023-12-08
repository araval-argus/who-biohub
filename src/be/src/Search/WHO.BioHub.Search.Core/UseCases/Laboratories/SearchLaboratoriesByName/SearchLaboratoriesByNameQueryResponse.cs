using WHO.BioHub.Models.Models;

namespace WHO.BioHub.Search.Core.UseCases.Laboratories.SearchLaboratoriesByName;

public record struct SearchLaboratoriesByNameQueryResponse(IEnumerable<Laboratory> Laboratories) { }