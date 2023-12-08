using Microsoft.EntityFrameworkCore;
using WHO.BioHub.DAL;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Search.Core.Repositories.Aggregates;

namespace WHO.BioHub.Search.SQL.Repositories.Aggregates;

public class SQLFrontEndGlobalSearchRepository : IFrontEndGlobalSearchRepository
{
    private readonly BioHubDbContext _dbContext;

    public SQLFrontEndGlobalSearchRepository(BioHubDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<FrontEndGlobalSearchDALResponse> FrontEndGlobalSearch(FrontEndGlobalSearchDALQuery query, CancellationToken cancellationToken)
    {
        IEnumerable<Laboratory> laboratories = await _dbContext.Laboratories
            .AsNoTracking()
            .Where(l => l.DeletedOn == null)
            .Where(l => l.Name.Contains(query.LaboratoryName))
            .ToListAsync(cancellationToken);

        return new(laboratories);
    }
}
