using Microsoft.EntityFrameworkCore;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.Laboratories;

namespace WHO.BioHub.Search.SQL.Repositories.Entities;

partial class SQLLaboratorySearchRepository : ILaboratorySearchRepository
{
    public async Task<IEnumerable<Laboratory>> SearchLaboratoriesByCountry(SearchLaboratoriesByCountryDALQuery query, CancellationToken cancellationToken)
    {
        return await _dbContext.Laboratories
            .AsNoTracking()
            .Where(l => l.DeletedOn == null)
            .SearchLaboratoriesByCountry(query)
            .ToListAsync(cancellationToken);
    }
}

internal static class SearchLaboratoriesByCountryExtensions
{
    internal static IQueryable<Laboratory> SearchLaboratoriesByCountry(this IQueryable<Laboratory> entities, SearchLaboratoriesByCountryDALQuery query)
    {
        return entities.Where(l => l.Country.FullName.Contains(query.Country));
    }
}