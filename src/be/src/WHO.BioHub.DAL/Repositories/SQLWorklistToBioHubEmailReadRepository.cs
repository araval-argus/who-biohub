using Microsoft.EntityFrameworkCore;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.WorklistToBioHubEmails;
using WHO.BioHub.Shared.Enums;

namespace WHO.BioHub.DAL.Repositories;

public class SQLWorklistToBioHubEmailReadRepository : IWorklistToBioHubEmailReadRepository
{
    private readonly BioHubDbContext _dbContext;

    public SQLWorklistToBioHubEmailReadRepository(BioHubDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<WorklistToBioHubEmail>> List(CancellationToken cancellationToken)
    {
        return await _dbContext.WorklistToBioHubEmails
            .Where(l => l.DeletedOn == null)
            .ToArrayAsync(cancellationToken);
    }

    public async Task<WorklistToBioHubEmail> Read(Guid id, CancellationToken cancellationToken)
    {
        return await _dbContext.WorklistToBioHubEmails
            .AsNoTracking()
            .FirstOrDefaultAsync(l => l.DeletedOn == null && l.Id == id, cancellationToken);
    }

    public async Task<WorklistToBioHubEmail> ReadByStatusRoleApproved(
        WorklistToBioHubStatus fromStatus,
        WorklistToBioHubStatus toStatus,
        bool approvedSubmission,
        Guid roleId,
        CancellationToken cancellationToken,
        bool isCourier = false)
    {
        return await _dbContext.WorklistToBioHubEmails
            .Include(x => x.Role)
            .AsNoTracking()
            .FirstOrDefaultAsync(l => l.DeletedOn == null && l.FromStatus == fromStatus && l.ToStatus == toStatus && l.ApprovedSubmission == approvedSubmission && l.RoleId == roleId && l.IsCourier == isCourier, cancellationToken);
    }
}