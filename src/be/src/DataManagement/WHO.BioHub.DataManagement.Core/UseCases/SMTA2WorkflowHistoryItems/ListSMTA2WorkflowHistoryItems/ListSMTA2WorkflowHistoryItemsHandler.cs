using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.DataManagement.Core.UseCases.SMTA2WorkflowHistoryItems.ReadSMTA2WorkflowHistoryItem;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.SMTA2WorkflowHistoryItems;
using WHO.BioHub.Shared.Enums;
using WHO.BioHub.Shared.Permissions;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UseCases.SMTA2WorkflowHistoryItems.ListSMTA2WorkflowHistoryItems;

public interface IListSMTA2WorkflowHistoryItemsHandler
{
    Task<Either<ListSMTA2WorkflowHistoryItemsQueryResponse, Errors>> Handle(ListSMTA2WorkflowHistoryItemsQuery query, CancellationToken cancellationToken);
}

public class ListSMTA2WorkflowHistoryItemsHandler : IListSMTA2WorkflowHistoryItemsHandler
{
    private readonly ILogger<ListSMTA2WorkflowHistoryItemsHandler> _logger;
    private readonly ListSMTA2WorkflowHistoryItemsQueryValidator _validator;
    private readonly ISMTA2WorkflowHistoryItemReadRepository _readRepository;
    private readonly IListSMTA2WorkflowHistoryItemMapper _mapper;

    public ListSMTA2WorkflowHistoryItemsHandler(
        ILogger<ListSMTA2WorkflowHistoryItemsHandler> logger,
        ListSMTA2WorkflowHistoryItemsQueryValidator validator,
        ISMTA2WorkflowHistoryItemReadRepository readRepository,
        IListSMTA2WorkflowHistoryItemMapper mapper)
    {
        _logger = logger;
        _validator = validator;
        _readRepository = readRepository;
        _mapper = mapper;
    }

    public async Task<Either<ListSMTA2WorkflowHistoryItemsQueryResponse, Errors>> Handle(
        ListSMTA2WorkflowHistoryItemsQuery query,
        CancellationToken cancellationToken)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(query, cancellationToken);
        if (!validationResult.IsValid)
            return new(new Errors(validationResult));

        try
        {
            IEnumerable<SMTA2WorkflowHistoryItem> SMTA2WorkflowHistoryItems = await _readRepository.ListByWorklistItemIdWithExtraInfo(query.WorlistToBioHubItemId, cancellationToken);

            SMTA2WorkflowHistoryItems = FilterItems(SMTA2WorkflowHistoryItems, query.RoleType, query.UserLaboratoryId, query.UserPermissions);

            var SMTA2WorkflowHistoryItemsDto = _mapper.Map(SMTA2WorkflowHistoryItems.ToList(), query.UserPermissions);

            return new(new ListSMTA2WorkflowHistoryItemsQueryResponse(SMTA2WorkflowHistoryItemsDto));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error retrieving all SMTA2WorkflowHistoryItems");
            throw;
        }
    }

    private IEnumerable<SMTA2WorkflowHistoryItem> FilterItems(IEnumerable<SMTA2WorkflowHistoryItem> SMTA2WorkflowItems, RoleType? roleType, Guid? LaboratoryId, IEnumerable<string> userPermissions)
    {
        List<SMTA2WorkflowHistoryItem> filteredList = new List<SMTA2WorkflowHistoryItem>();

        switch (roleType)
        {

            case RoleType.Laboratory:
                SMTA2WorkflowItems = SMTA2WorkflowItems.Where(x => x.LaboratoryId == LaboratoryId);
                break;
        }
        foreach (SMTA2WorkflowHistoryItem worklistItem in SMTA2WorkflowItems)
        {
            var requiredPermission = StatusPermissionMapper.GetSMTA2WorkflowStatusPermission(worklistItem.Status, PermissionType.Read, worklistItem.IsPast);
            if (userPermissions.Contains(requiredPermission))
            {

                filteredList.Add(worklistItem);
            }
        }
        return filteredList;
    }
}