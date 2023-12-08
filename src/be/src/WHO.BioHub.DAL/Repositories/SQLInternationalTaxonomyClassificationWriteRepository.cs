using Microsoft.EntityFrameworkCore;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.InternationalTaxonomyClassifications;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DAL.Repositories;

public class SQLInternationalTaxonomyClassificationWriteRepository : IInternationalTaxonomyClassificationWriteRepository
{
    private readonly BioHubDbContext _dbContext;
    private DbSet<InternationalTaxonomyClassification> InternationalTaxonomyClassifications => _dbContext.InternationalTaxonomyClassifications;

    public SQLInternationalTaxonomyClassificationWriteRepository(BioHubDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Either<InternationalTaxonomyClassification, Errors>> Create(InternationalTaxonomyClassification internationaltaxonomyclassification, CancellationToken cancellationToken)
    {
        await _dbContext.AddAsync(internationaltaxonomyclassification, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return new(internationaltaxonomyclassification);
    }

    public async Task<Errors?> Delete(Guid id, CancellationToken cancellationToken)
    {
        InternationalTaxonomyClassification lab = await InternationalTaxonomyClassifications.FirstOrDefaultAsync(l => l.DeletedOn == null && l.Id == id, cancellationToken);
        lab.DeletedOn = DateTime.Now;
        InternationalTaxonomyClassifications.Update(lab);

        await _dbContext.SaveChangesAsync(cancellationToken);
        return null;
    }

    public async Task<InternationalTaxonomyClassification> ReadForUpdate(Guid id, CancellationToken cancellationToken)
    {
        return await _dbContext.InternationalTaxonomyClassifications
            .FirstOrDefaultAsync(l => l.DeletedOn == null && l.Id == id, cancellationToken);
    }

    public async Task<Errors?> Update(InternationalTaxonomyClassification internationaltaxonomyclassification, CancellationToken cancellationToken)
    {
        InternationalTaxonomyClassifications.Update(internationaltaxonomyclassification);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return null;
    }
}