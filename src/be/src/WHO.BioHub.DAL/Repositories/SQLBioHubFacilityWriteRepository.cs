using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.BioHubFacilities;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DAL.Repositories;

public class SQLBioHubFacilityWriteRepository : IBioHubFacilityWriteRepository
{
    private readonly BioHubDbContext _dbContext;
    private DbSet<BioHubFacility> BioHubFacilities => _dbContext.BioHubFacilities;
    private DbSet<BioHubFacilityHistory> BioHubFacilitiesHistory => _dbContext.BioHubFacilitiesHistory;

    public SQLBioHubFacilityWriteRepository(BioHubDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Either<BioHubFacility, Errors>> Create(BioHubFacility biohubfacility, CancellationToken cancellationToken)
    {
        await _dbContext.AddAsync(biohubfacility, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return new(biohubfacility);
    }

    public async Task<Errors?> Delete(Guid id, CancellationToken cancellationToken)
    {
        BioHubFacility lab = await BioHubFacilities.FirstOrDefaultAsync(l => l.DeletedOn == null && l.Id == id, cancellationToken);
        lab.DeletedOn = DateTime.Now;
        BioHubFacilities.Update(lab);

        await _dbContext.SaveChangesAsync(cancellationToken);
        return null;
    }

    public async Task<BioHubFacility> ReadForUpdate(Guid id, CancellationToken cancellationToken)
    {
        return await _dbContext.BioHubFacilities
            .FirstOrDefaultAsync(l => l.DeletedOn == null && l.Id == id, cancellationToken);
    }

    public async Task<Errors?> Update(BioHubFacility biohubfacility, CancellationToken cancellationToken, IDbContextTransaction? transaction = null)
    {
        if (transaction != null)
        {
            _dbContext.Database.UseTransaction(transaction.GetDbTransaction());
        }

        BioHubFacilities.Update(biohubfacility);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return null;
    }

    public async Task<Errors?> CreateBioHubFacilityHistoryItem(BioHubFacility bioHubFacility, CancellationToken cancellationToken, IDbContextTransaction? transaction = null)
    {
        if (transaction != null)
        {
            _dbContext.Database.UseTransaction(transaction.GetDbTransaction());
        }

        BioHubFacilityHistory bioHubFacilityHistory = new BioHubFacilityHistory();

        bioHubFacilityHistory.Id = Guid.NewGuid();
        bioHubFacilityHistory.BioHubFacilityId = bioHubFacility.Id;
        bioHubFacilityHistory.Address = bioHubFacility.Address;
        bioHubFacilityHistory.Abbreviation = bioHubFacility.Abbreviation;
        bioHubFacilityHistory.BSLLevelId = bioHubFacility.BSLLevelId;
        bioHubFacilityHistory.CountryId = bioHubFacility.CountryId;
        bioHubFacilityHistory.CreationDate = DateTime.UtcNow;
        bioHubFacilityHistory.DeletedOn = bioHubFacility.DeletedOn;
        bioHubFacilityHistory.LastOperationDate = bioHubFacility.LastOperationDate;
        bioHubFacilityHistory.IsActive = bioHubFacility.IsActive;
        bioHubFacilityHistory.IsPublicFacing = bioHubFacility.IsPublicFacing;
        bioHubFacilityHistory.LastOperationByUserId = bioHubFacility.LastOperationByUserId;

        bioHubFacilityHistory.Latitude = bioHubFacility.Latitude;
        bioHubFacilityHistory.Longitude = bioHubFacility.Longitude;
        bioHubFacilityHistory.Name = bioHubFacility.Name;
        bioHubFacilityHistory.Description = bioHubFacility.Description;

        await BioHubFacilitiesHistory.AddAsync(bioHubFacilityHistory);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return null;
    }

    public async Task<IDbContextTransaction> BeginTransactionAsync()
    {
        return await _dbContext.Database.BeginTransactionAsync();
    }
}