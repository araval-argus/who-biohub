using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.DAL.Repositories.SMTA1WorkflowHistoryItems;
using WHO.BioHub.DataManagement.Core.UseCases.SMTA1WorkflowHistoryItems.ReadSMTA1WorkflowHistoryItem;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Shared.Enums;
using WHO.BioHub.Shared.Permissions;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UseCases.SMTA1WorkflowHistoryItems.ListSMTA1WorkflowHistoryItems;

public interface IListSMTA1WorkflowHistoryItemsHandler
{
    Task<Either<ListSMTA1WorkflowHistoryItemsQueryResponse, Errors>> Handle(ListSMTA1WorkflowHistoryItemsQuery query, CancellationToken cancellationToken);
}

public class ListSMTA1WorkflowHistoryItemsHandler : IListSMTA1WorkflowHistoryItemsHandler
{
    private readonly ILogger<ListSMTA1WorkflowHistoryItemsHandler> _logger;
    private readonly ListSMTA1WorkflowHistoryItemsQueryValidator _validator;
    private readonly ISMTA1WorkflowHistoryItemReadRepository _readRepository;
    private readonly IListSMTA1WorkflowHistoryItemMapper _mapper;

    public ListSMTA1WorkflowHistoryItemsHandler(
        ILogger<ListSMTA1WorkflowHistoryItemsHandler> logger,
        ListSMTA1WorkflowHistoryItemsQueryValidator validator,
        ISMTA1WorkflowHistoryItemReadRepository readRepository,
        IListSMTA1WorkflowHistoryItemMapper mapper)
    {
        _logger = logger;
        _validator = validator;
        _readRepository = readRepository;
        _mapper = mapper;
    }

    public async Task<Either<ListSMTA1WorkflowHistoryItemsQueryResponse, Errors>> Handle(
        ListSMTA1WorkflowHistoryItemsQuery query,
        CancellationToken cancellationToken)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(query, cancellationToken);
        if (!validationResult.IsValid)
            return new(new Errors(validationResult));

        try
        {
            IEnumerable<SMTA1WorkflowHistoryItem> SMTA1WorkflowHistoryItems = await _readRepository.ListByWorklistItemIdWithExtraInfo(query.WorlistToBioHubItemId, cancellationToken);

            SMTA1WorkflowHistoryItems = FilterItems(SMTA1WorkflowHistoryItems, query.RoleType, query.UserLaboratoryId, query.UserBioHubFacilityId, query.UserPermissions);

            var SMTA1WorkflowHistoryItemsDto = _mapper.Map(SMTA1WorkflowHistoryItems.ToList(), query.UserPermissions);

            return new(new ListSMTA1WorkflowHistoryItemsQueryResponse(SMTA1WorkflowHistoryItemsDto));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error retrieving all SMTA1WorkflowHistoryItems");
            throw;
        }
    }

    private IEnumerable<SMTA1WorkflowHistoryItem> FilterItems(IEnumerable<SMTA1WorkflowHistoryItem> SMTA1WorkflowItems, RoleType? roleType, Guid? LaboratoryId, Guid? BioHubFacilityId, IEnumerable<string> userPermissions)
    {
        List<SMTA1WorkflowHistoryItem> filteredList = new List<SMTA1WorkflowHistoryItem>();

        switch (roleType)
        {

            case RoleType.Laboratory:
                SMTA1WorkflowItems = SMTA1WorkflowItems.Where(x => x.LaboratoryId == LaboratoryId);
                break;
        }
        foreach (SMTA1WorkflowHistoryItem worklistItem in SMTA1WorkflowItems)
        {
            var requiredPermission = StatusPermissionMapper.SMTA1WorkflowStatusReadPermissionMapper[worklistItem.Status];
            if (userPermissions.Contains(requiredPermission))
            {

                filteredList.Add(worklistItem);
            }
        }
        return filteredList;
    }
}