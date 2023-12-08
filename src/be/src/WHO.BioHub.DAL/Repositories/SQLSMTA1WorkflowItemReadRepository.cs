using Microsoft.EntityFrameworkCore;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.SMTA1WorkflowItems;
using WHO.BioHub.Shared.Dto;
using WHO.BioHub.Shared.Enums;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DAL.Repositories;

public class SQLSMTA1WorkflowItemReadRepository : ISMTA1WorkflowItemReadRepository
{
    private readonly BioHubDbContext _dbContext;

    public SQLSMTA1WorkflowItemReadRepository(BioHubDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<SMTA1WorkflowItem>> List(CancellationToken cancellationToken)
    {
        return await _dbContext.SMTA1WorkflowItems
            .Include(x => x.Laboratory)
            .ThenInclude(x => x.Country)
            .Include(x => x.LastOperationUser)
            .ThenInclude(x => x.Role)
            .Include(x => x.LastOperationUser)
            .ThenInclude(x => x.Laboratory)
            .Include(x => x.SMTA1WorkflowItemDocuments)
            .Where(l => l.DeletedOn == null)            
            .ToArrayAsync(cancellationToken);
    }

    public async Task<SMTA1WorkflowItem> Read(Guid id, CancellationToken cancellationToken)
    {
        return await _dbContext.SMTA1WorkflowItems
            .AsNoTracking()
            .FirstOrDefaultAsync(l => l.DeletedOn == null && l.Id == id, cancellationToken);
    }



    public async Task<SMTA1WorkflowItem> ReadByIdAndStatus(Guid id, SMTA1WorkflowStatus status, CancellationToken cancellationToken)
    {
        return await _dbContext.SMTA1WorkflowItems
            .AsNoTracking()
            .FirstOrDefaultAsync(l => l.DeletedOn == null && l.Id == id && l.Status == status, cancellationToken);
    }

    public async Task<SMTA1WorkflowItem> ReadByIdWithExtraInfo(Guid id, CancellationToken cancellationToken)
    {
        var status = (await _dbContext.SMTA1WorkflowItems.FirstOrDefaultAsync(l => l.DeletedOn == null && l.Id == id, cancellationToken))?.Status;

        if (status == null)
        {
            return null;
        }

        var query = _dbContext.SMTA1WorkflowItems.AsNoTracking();


        switch (status)
        {
            case SMTA1WorkflowStatus.SubmitSMTA1:
                query = query
                .Include(l => l.Laboratory)
                .Include(x => x.LastOperationUser)
                .ThenInclude(x => x.Role);                
                break;

            case SMTA1WorkflowStatus.WaitingForSMTA1SECsApproval:
            case SMTA1WorkflowStatus.SMTA1WorkflowComplete:
                query = query
                    .Include(l => l.Laboratory)
                    .Include(x => x.LastOperationUser)
                    .ThenInclude(x => x.Role)                    
                    .Include(x => x.SMTA1WorkflowItemDocuments)
                    .ThenInclude(x => x.Document)
                    .ThenInclude(x => x.UploadedBy);
                break;
        }

        query = query.AsSplitQuery();

        return await query.FirstOrDefaultAsync(l => l.DeletedOn == null && l.Id == id, cancellationToken);

    }


    public async Task<SMTA1WorkflowItem> ReadWithHistory(Guid id, CancellationToken cancellationToken)
    {
        return await _dbContext.SMTA1WorkflowItems
            .AsNoTracking()
            .AsSplitQuery()
            .Include(x => x.SMTA1WorkflowHistoryItems)
            .Where(l => l.DeletedOn == null)
            .Where(x => x.Id == id)
            .FirstOrDefaultAsync(cancellationToken);
    }



    public async Task<WorkflowEmailInfoDto> ReadInfoForEmail(Guid id, SMTA1WorkflowStatus toStatus, SMTA1WorkflowStatus fromStatus, CancellationToken cancellationToken)
    {
        SMTA1WorkflowItem? entity;
        SMTA1WorkflowHistoryItem? entityHistory;
        WorkflowEmailInfoDto emailInfo = new WorkflowEmailInfoDto();


        var query = _dbContext.SMTA1WorkflowItems.AsNoTracking();
        var queryHistory = _dbContext.SMTA1WorkflowHistoryItems.AsNoTracking();

        query = query
            .Include(x => x.Laboratory)
            .ThenInclude(x => x.Country);

        queryHistory = queryHistory
            .Include(x => x.Laboratory);

        switch (toStatus)
        {
            case SMTA1WorkflowStatus.SubmitSMTA1:

                if (fromStatus == SMTA1WorkflowStatus.RequestInitiation)
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

                    entityHistory = await (queryHistory.Where(l => l.DeletedOn == null && l.SMTA1WorkflowItemId == id && l.Status == SMTA1WorkflowStatus.WaitingForSMTA1SECsApproval && l.LastSubmissionApproved == true).OrderByDescending(x => x.CreationDate).FirstOrDefaultAsync(cancellationToken));
                    emailInfo.Id = id;
                    emailInfo.LaboratoryName = entityHistory.LastOperationUser.Laboratory.Name;
                    emailInfo.LaboratoryCountry = entityHistory.LastOperationUser.Laboratory.Country.Name;
                    emailInfo.LaboratoryUserFirstName = entityHistory.LastOperationUser.FirstName;
                    emailInfo.LaboratoryUserLastName = entityHistory.LastOperationUser.LastName;
                    emailInfo.LaboratoryUserEmail = entityHistory.LastOperationUser.Email;
                }
                break;


            case SMTA1WorkflowStatus.WaitingForSMTA1SECsApproval:
            case SMTA1WorkflowStatus.SMTA1WorkflowComplete:

                queryHistory = queryHistory
                    .Include(x => x.LastOperationUser)
                    .ThenInclude(x => x.Laboratory)
                    .ThenInclude(x => x.Country);

                queryHistory = queryHistory.AsSplitQuery();

                entityHistory = await (queryHistory.Where(l => l.DeletedOn == null && l.SMTA1WorkflowItemId == id && l.Status == SMTA1WorkflowStatus.SubmitSMTA1 && l.LastSubmissionApproved == true).OrderByDescending(x => x.CreationDate).FirstOrDefaultAsync(cancellationToken));
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