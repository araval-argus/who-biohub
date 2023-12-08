using Microsoft.EntityFrameworkCore;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.WorklistFromBioHubEmails;
using WHO.BioHub.Shared.Enums;

namespace WHO.BioHub.DAL.Repositories;

public class SQLWorklistFromBioHubEmailReadRepository : IWorklistFromBioHubEmailReadRepository
{
    private readonly BioHubDbContext _dbContext;

    public SQLWorklistFromBioHubEmailReadRepository(BioHubDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<WorklistFromBioHubEmail>> List(CancellationToken cancellationToken)
    {
        return await _dbContext.WorklistFromBioHubEmails
            .Where(l => l.DeletedOn == null)
            .ToArrayAsync(cancellationToken);
    }

    public async Task<WorklistFromBioHubEmail> Read(Guid id, CancellationToken cancellationToken)
    {
        return await _dbContext.WorklistFromBioHubEmails
            .AsNoTracking()
            .FirstOrDefaultAsync(l => l.DeletedOn == null && l.Id == id, cancellationToken);
    }


    public async Task<WorklistFromBioHubEmail> ReadByStatusRoleApproved(
        WorklistFromBioHubStatus fromStatus,
        WorklistFromBioHubStatus toStatus,
        bool approvedSubmission,
        Guid roleId,
        CancellationToken cancellationToken,
        bool isCourier = false,
        bool IsNumberOfVialsWarning = false)
    {
        return await _dbContext.WorklistFromBioHubEmails
            .Include(x => x.Role)
            .AsNoTracking()
            .FirstOrDefaultAsync(l => l.DeletedOn == null && l.FromStatus == fromStatus && l.ToStatus == toStatus && l.ApprovedSubmission == approvedSubmission && l.RoleId == roleId && l.IsCourier == isCourier && l.IsNumberOfVialsWarning == IsNumberOfVialsWarning, cancellationToken);
    }
}