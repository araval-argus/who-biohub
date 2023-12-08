using Microsoft.EntityFrameworkCore;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.SMTA2WorkflowEmails;
using WHO.BioHub.Shared.Enums;

namespace WHO.BioHub.DAL.Repositories;

public class SQLSMTA2WorkflowEmailReadRepository : ISMTA2WorkflowEmailReadRepository
{
    private readonly BioHubDbContext _dbContext;

    public SQLSMTA2WorkflowEmailReadRepository(BioHubDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<SMTA2WorkflowEmail>> List(CancellationToken cancellationToken)
    {
        return await _dbContext.SMTA2WorkflowEmails
            .Where(l => l.DeletedOn == null)
            .ToArrayAsync(cancellationToken);
    }

    public async Task<SMTA2WorkflowEmail> Read(Guid id, CancellationToken cancellationToken)
    {
        return await _dbContext.SMTA2WorkflowEmails
            .AsNoTracking()
            .FirstOrDefaultAsync(l => l.DeletedOn == null && l.Id == id, cancellationToken);
    }

    public async Task<SMTA2WorkflowEmail> ReadByStatusRoleApproved(
        SMTA2WorkflowStatus fromStatus,
        SMTA2WorkflowStatus toStatus,
        bool approvedSubmission,
        Guid roleId,
        CancellationToken cancellationToken,
        bool isCourier = false)
    {
        return await _dbContext.SMTA2WorkflowEmails
            .Include(x => x.Role)
            .AsNoTracking()
            .FirstOrDefaultAsync(l => l.DeletedOn == null && l.FromStatus == fromStatus && l.ToStatus == toStatus && l.ApprovedSubmission == approvedSubmission && l.RoleId == roleId && l.IsCourier == isCourier, cancellationToken);
    }
}