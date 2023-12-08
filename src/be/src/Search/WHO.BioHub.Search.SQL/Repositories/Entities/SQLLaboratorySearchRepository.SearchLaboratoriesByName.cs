using Microsoft.EntityFrameworkCore;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.Laboratories;

namespace WHO.BioHub.Search.SQL.Repositories.Entities;

partial class SQLLaboratorySearchRepository : ILaboratorySearchRepository
{
    public async Task<IEnumerable<Laboratory>> SearchLaboratoriesByName(SearchLaboratoriesByNameDALQuery query, CancellationToken cancellationToken)
    {
        return await _dbContext.Laboratories
            .AsNoTracking()
            .Where(l => l.DeletedOn == null)
            .SearchLaboratoriesByName(query)
            .ToListAsync(cancellationToken);
    }
}

internal static class SearchLaboratoriesByNameExtensions
{
    internal static IQueryable<Laboratory> SearchLaboratoriesByName(this IQueryable<Laboratory> laboratories, SearchLaboratoriesByNameDALQuery query)
    {
        // search provided name
        // TODO: enhance this filter
        return laboratories.Where(entity => entity.Name.Contains(query.EntityName));
    }
}