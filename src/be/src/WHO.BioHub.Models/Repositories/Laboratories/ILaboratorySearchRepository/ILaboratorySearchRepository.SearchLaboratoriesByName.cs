using WHO.BioHub.Models.Models;

namespace WHO.BioHub.Models.Repositories.Laboratories;

public partial interface ILaboratorySearchRepository
{
    Task<IEnumerable<Laboratory>> SearchLaboratoriesByName(SearchLaboratoriesByNameDALQuery query, CancellationToken cancellationToken);
}

public record struct SearchLaboratoriesByNameDALQuery(string EntityName);
