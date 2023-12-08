using Microsoft.EntityFrameworkCore;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.InternationalTaxonomyClassifications;
using WHO.BioHub.DAL;
using WHO.BioHub.Public.SQL.Abstractions;

namespace WHO.BioHub.Public.SQL.Repositories;

public class SQLInternationalTaxonomyClassificationPublicReadRepository : IInternationalTaxonomyClassificationPublicReadRepository
{
    private readonly BioHubDbContext _dbContext;

    public SQLInternationalTaxonomyClassificationPublicReadRepository(BioHubDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<InternationalTaxonomyClassification>> List(CancellationToken cancellationToken)
    {
        return await _dbContext.InternationalTaxonomyClassifications
            .Where(l => l.DeletedOn == null)
            .ToArrayAsync(cancellationToken);
    }

    public async Task<InternationalTaxonomyClassification> Read(Guid id, CancellationToken cancellationToken)
    {
        return await _dbContext.InternationalTaxonomyClassifications
            .AsNoTracking()
            .FirstOrDefaultAsync(l => l.DeletedOn == null && l.Id == id, cancellationToken);
    }
}