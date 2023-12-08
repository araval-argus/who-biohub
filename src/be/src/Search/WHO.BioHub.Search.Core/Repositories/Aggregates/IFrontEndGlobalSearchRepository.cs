using WHO.BioHub.Models.Models;

namespace WHO.BioHub.Search.Core.Repositories.Aggregates;

public interface IFrontEndGlobalSearchRepository
{
    Task<FrontEndGlobalSearchDALResponse> FrontEndGlobalSearch(FrontEndGlobalSearchDALQuery query, CancellationToken cancellationToken);
}

public record struct FrontEndGlobalSearchDALQuery(string LaboratoryName);

public record struct FrontEndGlobalSearchDALResponse(IEnumerable<Laboratory> Laboratories);
