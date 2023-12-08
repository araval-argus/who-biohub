using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.Couriers;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DAL.Repositories;

public class SQLCourierWriteRepository : ICourierWriteRepository
{
    private readonly BioHubDbContext _dbContext;
    private DbSet<Courier> Couriers => _dbContext.Couriers;
    private DbSet<CourierHistory> CouriersHistory => _dbContext.CouriersHistory;

    public SQLCourierWriteRepository(BioHubDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Either<Courier, Errors>> Create(Courier courier, CancellationToken cancellationToken)
    {
        await _dbContext.AddAsync(courier, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return new(courier);
    }

    public async Task<Errors?> Delete(Guid id, CancellationToken cancellationToken)
    {
        Courier lab = await Couriers.FirstOrDefaultAsync(l => l.DeletedOn == null && l.Id == id, cancellationToken);
        lab.DeletedOn = DateTime.Now;
        Couriers.Update(lab);

        await _dbContext.SaveChangesAsync(cancellationToken);
        return null;
    }

    public async Task<Courier> ReadForUpdate(Guid id, CancellationToken cancellationToken)
    {
        return await _dbContext.Couriers
            .FirstOrDefaultAsync(l => l.DeletedOn == null && l.Id == id, cancellationToken);
    }

    public async Task<Errors?> Update(Courier courier, CancellationToken cancellationToken, IDbContextTransaction? transaction = null)
    {
        if (transaction != null)
        {
            _dbContext.Database.UseTransaction(transaction.GetDbTransaction());
        }
        Couriers.Update(courier);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return null;
    }

    public async Task<Errors?> CreateCourierHistoryItem(Courier courier, CancellationToken cancellationToken, IDbContextTransaction? transaction = null)
    {
        if (transaction != null)
        {
            _dbContext.Database.UseTransaction(transaction.GetDbTransaction());
        }

        CourierHistory courierHistory = new CourierHistory();

        courierHistory.Id = Guid.NewGuid();
        courierHistory.CourierId = courier.Id;
        courierHistory.Address = courier.Address;       
        courierHistory.CountryId = courier.CountryId;
        courierHistory.CreationDate = DateTime.UtcNow;
        courierHistory.DeletedOn = courier.DeletedOn;
        courierHistory.LastOperationDate = courier.LastOperationDate;
        courierHistory.IsActive = courier.IsActive;
        courierHistory.Email = courier.Email;
        courierHistory.BusinessPhone = courier.BusinessPhone;
        courierHistory.Address = courier.Address;
        courierHistory.WHOAccountNumber = courier.WHOAccountNumber;


        courierHistory.LastOperationByUserId = courier.LastOperationByUserId;
        courierHistory.LastOperationDate = courier.LastOperationDate;

        courierHistory.Latitude = courier.Latitude;
        courierHistory.Longitude = courier.Longitude;
        courierHistory.Name = courier.Name;
        courierHistory.Description = courier.Description;

        await CouriersHistory.AddAsync(courierHistory);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return null;
    }

    public async Task<IDbContextTransaction> BeginTransactionAsync()
    {
        return await _dbContext.Database.BeginTransactionAsync();
    }
}