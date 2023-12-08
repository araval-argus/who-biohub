using WHO.BioHub.Models.Models;

namespace WHO.BioHub.Models.Repositories.Laboratories;

public partial interface ILaboratorySearchRepository
{
    Task<IEnumerable<Laboratory>> SearchLaboratoriesByCountry(SearchLaboratoriesByCountryDALQuery query, CancellationToken cancellationToken);
}

public record struct SearchLaboratoriesByCountryDALQuery(string Country);
