using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.WorklistFromBioHubItems;
using WHO.BioHub.Models.Repositories.WorklistItemUsedReferenceNumber;
using WHO.BioHub.Shared.Enums;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DAL.Repositories;

public class SQLWorklistItemUsedReferenceNumberWriteRepository : IWorklistItemUsedReferenceNumberWriteRepository
{
    private readonly BioHubDbContext _dbContext;

    public SQLWorklistItemUsedReferenceNumberWriteRepository(BioHubDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Either<WorklistItemUsedReferenceNumber, Errors>> Create(WorklistItemUsedReferenceNumber worklistItemUsedReferenceNumber, CancellationToken cancellationToken, IDbContextTransaction? transaction = null)
    {
        if (transaction != null)
        {
            _dbContext.Database.UseTransaction(transaction.GetDbTransaction());
        }




        await _dbContext.AddAsync(worklistItemUsedReferenceNumber, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return new(worklistItemUsedReferenceNumber);
    }

    public async Task<Either<WorklistItemUsedReferenceNumber, Errors>> Create(bool? isPast, CancellationToken cancellationToken, IDbContextTransaction? transaction = null)
    {
        if (transaction != null)
        {
            _dbContext.Database.UseTransaction(transaction.GetDbTransaction());
        }


        DateTime firstDateOfTheYear = new DateTime(DateTime.UtcNow.Year, 1, 1);

        List<WorklistItemUsedReferenceNumber> total;

        if (isPast == true)
        {
            total = await _dbContext.WorklistItemUsedReferenceNumbers
            .Where(l => l.CreationDate >= firstDateOfTheYear && l.IsPast == true).ToListAsync(cancellationToken);
        }
        else
        {
            total = await _dbContext.WorklistItemUsedReferenceNumbers
            .Where(l => l.CreationDate >= firstDateOfTheYear && l.IsPast != true).ToListAsync(cancellationToken);
        }

        int numberOfRequestOfTheYear = 0;

        if (total != null)
        {
            numberOfRequestOfTheYear = total.Count();
        }
        

        int nextReferenceNumber = numberOfRequestOfTheYear + 1;

        var referenceNumber = DateTime.UtcNow.Year.ToString() + "-WHO-APPSR-" + nextReferenceNumber.ToString("D3");

        if (isPast == true)
        {
            referenceNumber += "-PAST";
        }

        while(total.Select(x => x.ReferenceNumber).Contains(referenceNumber))
        {
            nextReferenceNumber = nextReferenceNumber + 1;

            referenceNumber = DateTime.UtcNow.Year.ToString() + "-WHO-APPSR-" + nextReferenceNumber.ToString("D3");

            if (isPast == true)
            {
                referenceNumber += "-PAST";
            }
        }

        var worklistItemUsedReferenceNumber = new WorklistItemUsedReferenceNumber();
        worklistItemUsedReferenceNumber.Id = Guid.NewGuid();
        worklistItemUsedReferenceNumber.CreationDate = DateTime.UtcNow;
        worklistItemUsedReferenceNumber.ReferenceNumber = referenceNumber;
        worklistItemUsedReferenceNumber.IsPast = isPast;


        await _dbContext.AddAsync(worklistItemUsedReferenceNumber, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return new(worklistItemUsedReferenceNumber);
    }
}