using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.Laboratories;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DAL.Repositories;

public class SQLLaboratoryWriteRepository : ILaboratoryWriteRepository
{
    private readonly BioHubDbContext _dbContext;
    private DbSet<Laboratory> Laboratories => _dbContext.Laboratories;
    private DbSet<LaboratoryHistory> LaboratoriesHistory => _dbContext.LaboratoriesHistory;
    public SQLLaboratoryWriteRepository(BioHubDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Either<Laboratory, Errors>> Create(Laboratory laboratory, CancellationToken cancellationToken)
    {
        await _dbContext.AddAsync(laboratory, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return new(laboratory);
    }

    public async Task<Errors?> Delete(Guid id, CancellationToken cancellationToken)
    {
        Laboratory lab = await Laboratories.FirstOrDefaultAsync(l => l.DeletedOn == null && l.Id == id, cancellationToken);
        lab.DeletedOn = DateTime.Now;
        Laboratories.Update(lab);

        await _dbContext.SaveChangesAsync(cancellationToken);
        return null;
    }

    public async Task<Laboratory> ReadForUpdate(Guid id, CancellationToken cancellationToken)
    {
        return await _dbContext.Laboratories
            .FirstOrDefaultAsync(l => l.DeletedOn == null && l.Id == id, cancellationToken);
    }

    public async Task<Errors?> Update(Laboratory laboratory, CancellationToken cancellationToken, IDbContextTransaction? transaction = null)
    {
        if (transaction != null)
        {
            _dbContext.Database.UseTransaction(transaction.GetDbTransaction());
        }

        Laboratories.Update(laboratory);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return null;
    }

    public async Task<Errors?> CreateLaboratoryHistoryItem(Laboratory laboratory, CancellationToken cancellationToken, IDbContextTransaction? transaction = null)
    {
        if (transaction != null)
        {
            _dbContext.Database.UseTransaction(transaction.GetDbTransaction());
        }

        LaboratoryHistory laboratoryHistory = new LaboratoryHistory();

        laboratoryHistory.Id = Guid.NewGuid();
        laboratoryHistory.LaboratoryId = laboratory.Id;
        laboratoryHistory.Address = laboratory.Address;
        laboratoryHistory.Abbreviation = laboratory.Abbreviation;
        laboratoryHistory.BSLLevelId = laboratory.BSLLevelId;
        laboratoryHistory.CountryId = laboratory.CountryId;
        laboratoryHistory.CreationDate = DateTime.UtcNow;
        laboratoryHistory.DeletedOn = laboratory.DeletedOn;
        laboratoryHistory.LastOperationDate = laboratory.LastOperationDate;
        laboratoryHistory.IsActive = laboratory.IsActive;
        laboratoryHistory.IsPublicFacing = laboratory.IsPublicFacing;        
        laboratoryHistory.LastOperationByUserId = laboratory.LastOperationByUserId;

        laboratoryHistory.Latitude = laboratory.Latitude;
        laboratoryHistory.Longitude = laboratory.Longitude;
        laboratoryHistory.Name = laboratory.Name;
        laboratoryHistory.Description = laboratory.Description;

        await LaboratoriesHistory.AddAsync(laboratoryHistory);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return null;
    }

    public async Task<IDbContextTransaction> BeginTransactionAsync()
    {
        return await _dbContext.Database.BeginTransactionAsync();
    }
}