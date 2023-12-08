using Microsoft.EntityFrameworkCore;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.SMTA1WorkflowEmails;
using WHO.BioHub.Shared.Enums;

namespace WHO.BioHub.DAL.Repositories;

public class SQLSMTA1WorkflowEmailReadRepository : ISMTA1WorkflowEmailReadRepository
{
    private readonly BioHubDbContext _dbContext;

    public SQLSMTA1WorkflowEmailReadRepository(BioHubDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<SMTA1WorkflowEmail>> List(CancellationToken cancellationToken)
    {
        return await _dbContext.SMTA1WorkflowEmails
            .Where(l => l.DeletedOn == null)
            .ToArrayAsync(cancellationToken);
    }

    public async Task<SMTA1WorkflowEmail> Read(Guid id, CancellationToken cancellationToken)
    {
        return await _dbContext.SMTA1WorkflowEmails
            .AsNoTracking()
            .FirstOrDefaultAsync(l => l.DeletedOn == null && l.Id == id, cancellationToken);
    }

    public async Task<SMTA1WorkflowEmail> ReadByStatusRoleApproved(
        SMTA1WorkflowStatus fromStatus,
        SMTA1WorkflowStatus toStatus,
        bool approvedSubmission,
        Guid roleId,
        CancellationToken cancellationToken,
        bool isCourier = false)
    {
        return await _dbContext.SMTA1WorkflowEmails
            .Include(x => x.Role)
            .AsNoTracking()
            .FirstOrDefaultAsync(l => l.DeletedOn == null && l.FromStatus == fromStatus && l.ToStatus == toStatus && l.ApprovedSubmission == approvedSubmission && l.RoleId == roleId && l.IsCourier == isCourier, cancellationToken);
    }
}