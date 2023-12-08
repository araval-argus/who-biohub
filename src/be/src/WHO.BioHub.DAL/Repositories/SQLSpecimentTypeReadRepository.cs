using Microsoft.EntityFrameworkCore;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.SpecimenTypes;

namespace WHO.BioHub.DAL.Repositories;

public class SQLSpecimenTypeReadRepository : ISpecimenTypeReadRepository
{
    private readonly BioHubDbContext _dbContext;

    public SQLSpecimenTypeReadRepository(BioHubDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<SpecimenType>> List(CancellationToken cancellationToken)
    {
        return await _dbContext.SpecimenTypes
            .Where(l => l.DeletedOn == null)
            .ToArrayAsync(cancellationToken);
    }

    public async Task<SpecimenType> Read(Guid id, CancellationToken cancellationToken)
    {
        return await _dbContext.SpecimenTypes
            .AsNoTracking()
            .FirstOrDefaultAsync(l => l.DeletedOn == null && l.Id == id, cancellationToken);
    }
}