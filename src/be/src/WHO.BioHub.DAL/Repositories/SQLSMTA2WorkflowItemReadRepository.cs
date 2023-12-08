using Microsoft.EntityFrameworkCore;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.SMTA2WorkflowItems;
using WHO.BioHub.Shared.Dto;
using WHO.BioHub.Shared.Enums;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DAL.Repositories;

public class SQLSMTA2WorkflowItemReadRepository : ISMTA2WorkflowItemReadRepository
{
    private readonly BioHubDbContext _dbContext;

    public SQLSMTA2WorkflowItemReadRepository(BioHubDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<SMTA2WorkflowItem>> List(CancellationToken cancellationToken)
    {
        return await _dbContext.SMTA2WorkflowItems
            .Include(x => x.Laboratory)
            .ThenInclude(x => x.Country)
            .Include(x => x.LastOperationUser)
            .ThenInclude(x => x.Role)
            .Include(x => x.LastOperationUser)
            .ThenInclude(x => x.Laboratory)
            .Include(x => x.SMTA2WorkflowItemDocuments)
            .Where(l => l.DeletedOn == null)
            .ToArrayAsync(cancellationToken);
    }

    public async Task<SMTA2WorkflowItem> Read(Guid id, CancellationToken cancellationToken)
    {
        return await _dbContext.SMTA2WorkflowItems
            .AsNoTracking()
            .FirstOrDefaultAsync(l => l.DeletedOn == null && l.Id == id, cancellationToken);
    }



    public async Task<SMTA2WorkflowItem> ReadByIdAndStatus(Guid id, SMTA2WorkflowStatus status, CancellationToken cancellationToken)
    {
        return await _dbContext.SMTA2WorkflowItems
            .AsNoTracking()
            .FirstOrDefaultAsync(l => l.DeletedOn == null && l.Id == id && l.Status == status, cancellationToken);
    }

    public async Task<SMTA2WorkflowItem> ReadByIdWithExtraInfo(Guid id, CancellationToken cancellationToken)
    {
        var status = (await _dbContext.SMTA2WorkflowItems.FirstOrDefaultAsync(l => l.DeletedOn == null && l.Id == id, cancellationToken))?.Status;

        if (status == null)
        {
            return null;
        }

        var query = _dbContext.SMTA2WorkflowItems.AsNoTracking();


        switch (status)
        {
            case SMTA2WorkflowStatus.SubmitSMTA2:
                query = query
                .Include(l => l.Laboratory)
                .Include(x => x.LastOperationUser)
                .ThenInclude(x => x.Role);
                break;


            case SMTA2WorkflowStatus.WaitingForSMTA2SECsApproval:
            case SMTA2WorkflowStatus.SMTA2WorkflowComplete:
                query = query
                    .Include(l => l.Laboratory)
                    .Include(x => x.LastOperationUser)
                    .ThenInclude(x => x.Role)
                    .Include(x => x.SMTA2WorkflowItemDocuments)
                    .ThenInclude(x => x.Document)
                    .ThenInclude(x => x.UploadedBy);
                break;
        }

        query = query.AsSplitQuery();

        return await query.FirstOrDefaultAsync(l => l.DeletedOn == null && l.Id == id, cancellationToken);

    }

    public async Task<SMTA2WorkflowItem> ReadWithHistory(Guid id, CancellationToken cancellationToken)
    {
        return await _dbContext.SMTA2WorkflowItems
            .AsNoTracking()
            .AsSplitQuery()
            .Include(x => x.SMTA2WorkflowHistoryItems)
            .Where(l => l.DeletedOn == null)
            .Where(x => x.Id == id)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<WorkflowEmailInfoDto> ReadInfoForEmail(Guid id, SMTA2WorkflowStatus toStatus, SMTA2WorkflowStatus fromStatus, CancellationToken cancellationToken)
    {
        SMTA2WorkflowItem? entity;
        SMTA2WorkflowHistoryItem? entityHistory;
        WorkflowEmailInfoDto emailInfo = new WorkflowEmailInfoDto();


        var query = _dbContext.SMTA2WorkflowItems.AsNoTracking();
        var queryHistory = _dbContext.SMTA2WorkflowHistoryItems.AsNoTracking();

        query = query
            .Include(x => x.Laboratory)
            .ThenInclude(x => x.Country);

        queryHistory = queryHistory
            .Include(x => x.Laboratory);


        switch (toStatus)
        {
            case SMTA2WorkflowStatus.SubmitSMTA2:

                if (fromStatus == SMTA2WorkflowStatus.RequestInitiation)
                {
                    query = query
                    .Include(x => x.LastOperationUser)
                    .ThenInclude(x => x.Laboratory)
                    .ThenInclude(x => x.Country)
                    .AsSplitQuery();

                    entity = await (query.Where(l => l.DeletedOn == null && l.Id == id).FirstOrDefaultAsync(cancellationToken));
                    emailInfo.Id = id;
                    emailInfo.LaboratoryName = entity.LastOperationUser.Laboratory.Name;
                    emailInfo.LaboratoryCountry = entity.LastOperationUser.Laboratory.Country.Name;
                    emailInfo.LaboratoryUserFirstName = entity.LastOperationUser.FirstName;
                    emailInfo.LaboratoryUserLastName = entity.LastOperationUser.LastName;
                    emailInfo.LaboratoryUserEmail = entity.LastOperationUser.Email;
                }
                else
                {
                    queryHistory = queryHistory
                    .Include(x => x.LastOperationUser)
                    .ThenInclude(x => x.Laboratory)
                    .ThenInclude(x => x.Country)
                    .AsSplitQuery();

                    entityHistory = await (queryHistory.Where(l => l.DeletedOn == null && l.SMTA2WorkflowItemId == id && l.LastSubmissionApproved == true).OrderByDescending(x => x.CreationDate).FirstOrDefaultAsync(cancellationToken));
                    emailInfo.Id = id;
                    emailInfo.LaboratoryName = entityHistory.LastOperationUser.Laboratory.Name;
                    emailInfo.LaboratoryCountry = entityHistory.LastOperationUser.Laboratory.Country.Name;
                    emailInfo.LaboratoryUserFirstName = entityHistory.LastOperationUser.FirstName;
                    emailInfo.LaboratoryUserLastName = entityHistory.LastOperationUser.LastName;
                    emailInfo.LaboratoryUserEmail = entityHistory.LastOperationUser.Email;
                }
                break;


            case SMTA2WorkflowStatus.WaitingForSMTA2SECsApproval:
            case SMTA2WorkflowStatus.SMTA2WorkflowComplete:

                queryHistory = queryHistory
                    .Include(x => x.LastOperationUser)
                    .ThenInclude(x => x.Laboratory)
                    .ThenInclude(x => x.Country);

                queryHistory = queryHistory.AsSplitQuery();

                entityHistory = await (queryHistory.Where(l => l.DeletedOn == null && l.SMTA2WorkflowItemId == id && l.Status == SMTA2WorkflowStatus.SubmitSMTA2 && l.LastSubmissionApproved == true).OrderByDescending(x => x.CreationDate).FirstOrDefaultAsync(cancellationToken));
                emailInfo.Id = id;
                emailInfo.LaboratoryName = entityHistory.LastOperationUser.Laboratory.Name;
                emailInfo.LaboratoryCountry = entityHistory.LastOperationUser.Laboratory.Country.Name;
                emailInfo.LaboratoryUserFirstName = entityHistory.LastOperationUser.FirstName;
                emailInfo.LaboratoryUserLastName = entityHistory.LastOperationUser.LastName;
                emailInfo.LaboratoryUserEmail = entityHistory.LastOperationUser.Email;

                break;
        }

        return emailInfo;
    }
}