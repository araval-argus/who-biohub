using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.Users;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DAL.Repositories;

public class SQLUserWriteRepository : IUserWriteRepository
{
    private readonly BioHubDbContext _dbContext;
    private DbSet<User> Users => _dbContext.Users;
    private DbSet<UserHistory> UsersHistory => _dbContext.UsersHistory;

    public SQLUserWriteRepository(BioHubDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Either<User, Errors>> Create(User user, CancellationToken cancellationToken)
    {
        await _dbContext.AddAsync(user, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return new(user);
    }

    public async Task<Errors?> Delete(Guid id, CancellationToken cancellationToken)
    {
        User lab = await Users.FirstOrDefaultAsync(l => l.DeletedOn == null && l.Id == id, cancellationToken);
        lab.DeletedOn = DateTime.Now;
        Users.Update(lab);

        await _dbContext.SaveChangesAsync(cancellationToken);
        return null;
    }

    public async Task<User> ReadForUpdate(Guid id, CancellationToken cancellationToken)
    {
        return await _dbContext.Users
            .FirstOrDefaultAsync(l => l.DeletedOn == null && l.Id == id, cancellationToken);
    }

    public async Task<Errors?> DeleteUsersByLaboratory(Guid laboratoryId, Guid? operatorId, DateTime deletedOn, CancellationToken cancellationToken, IDbContextTransaction? transaction = null)
    {
        if (transaction != null)
        {
            _dbContext.Database.UseTransaction(transaction.GetDbTransaction());
        }

        var users = Users.Where(x => x.DeletedOn == null && x.LaboratoryId == laboratoryId);

        foreach (var user in users)
        {
            user.DeletedOn = deletedOn;
            user.LastOperationDate = deletedOn;
            user.LastOperationByUserId = operatorId;
        }

        Users.UpdateRange(users);

        await _dbContext.SaveChangesAsync(cancellationToken);
        return null;
    }


    public async Task<Errors?> DeleteUsersByBioHubFacility(Guid bioHubFacilityId, Guid? operatorId, DateTime deletedOn, CancellationToken cancellationToken, IDbContextTransaction? transaction = null)
    {
        if (transaction != null)
        {
            _dbContext.Database.UseTransaction(transaction.GetDbTransaction());
        }

        var users = Users.Where(x => x.DeletedOn == null && x.BioHubFacilityId == bioHubFacilityId);

        foreach (var user in users)
        {
            user.DeletedOn = deletedOn;
            user.LastOperationDate = deletedOn;
            user.LastOperationByUserId = operatorId;
        }

        Users.UpdateRange(users);

        await _dbContext.SaveChangesAsync(cancellationToken);
        return null;
    }

    public async Task<Errors?> Update(User user, CancellationToken cancellationToken, IDbContextTransaction? transaction = null)
    {
        if (transaction != null)
        {
            _dbContext.Database.UseTransaction(transaction.GetDbTransaction());
        }

        Users.Update(user);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return null;
    }

    public async Task<Errors?> CreateUserHistoryItem(User user, CancellationToken cancellationToken, IDbContextTransaction? transaction = null)
    {
        if (transaction != null)
        {
            _dbContext.Database.UseTransaction(transaction.GetDbTransaction());
        }

        UserHistory userHistory = new UserHistory();

        userHistory.Id = Guid.NewGuid();
        userHistory.UserId = user.Id;
        userHistory.FirstName = user.FirstName;
        userHistory.LastName = user.LastName;
        userHistory.Email = user.Email;
        userHistory.JobTitle = user.JobTitle;
        userHistory.MobilePhone = user.MobilePhone;
        userHistory.BusinessPhone = user.BusinessPhone;
        userHistory.OperationalFocalPoint = user.OperationalFocalPoint;
        userHistory.RoleId = user.RoleId;
        userHistory.LaboratoryId = user.LaboratoryId;
        userHistory.BioHubFacilityId = user.BioHubFacilityId;
        userHistory.CourierId = user.CourierId;
        userHistory.Notes = user.Notes;
        userHistory.LastOperationByUserId = user.LastOperationByUserId;
        userHistory.LastOperationDate = user.LastOperationDate;
        userHistory.CreationDate = DateTime.UtcNow;

        await UsersHistory.AddAsync(userHistory);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return null;
    }

    public async Task<IDbContextTransaction> BeginTransactionAsync()
    {
        return await _dbContext.Database.BeginTransactionAsync();
    }
}