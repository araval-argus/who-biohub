using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.Shipments;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DAL.Repositories;

public class SQLShipmentWriteRepository : IShipmentWriteRepository
{
    private readonly BioHubDbContext _dbContext;
    private DbSet<Shipment> Shipments => _dbContext.Shipments;

    public SQLShipmentWriteRepository(BioHubDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Either<Shipment, Errors>> Create(Shipment shipment, CancellationToken cancellationToken, IDbContextTransaction? transaction = null)
    {
        if (transaction != null)
        {
            _dbContext.Database.UseTransaction(transaction.GetDbTransaction());
        }

        await _dbContext.AddAsync(shipment, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return new(shipment);
    }

    public async Task<Errors?> Delete(Guid id, CancellationToken cancellationToken)
    {
        Shipment lab = await Shipments.FirstOrDefaultAsync(l => l.DeletedOn == null && l.Id == id, cancellationToken);
        lab.DeletedOn = DateTime.Now;
        Shipments.Update(lab);

        await _dbContext.SaveChangesAsync(cancellationToken);
        return null;
    }

    public async Task<Shipment> ReadForUpdate(Guid id, CancellationToken cancellationToken)
    {
        return await _dbContext.Shipments
            .FirstOrDefaultAsync(l => l.DeletedOn == null && l.Id == id, cancellationToken);
    }

    public async Task<Errors?> Update(Shipment shipment, CancellationToken cancellationToken)
    {
        Shipments.Update(shipment);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return null;
    }
}