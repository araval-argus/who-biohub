using WHO.BioHub.Models.Models;

namespace WHO.BioHub.Search.Core.UseCases.Aggregates.FrontEndGlobalSearch;

public record struct FrontEndGlobalSearchQueryResponse(IEnumerable<Laboratory> Laboratories) { }