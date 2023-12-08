using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.DataManagement.Core.UseCases.SMTA1WorkflowItems.ListDashboardSMTA1WorkflowItem;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.SMTA1WorkflowItems;
using WHO.BioHub.Shared.Enums;
using WHO.BioHub.Shared.Permissions;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UseCases.SMTA1WorkflowItems.ListDashboardSMTA1WorkflowItems;

public interface IListDashboardSMTA1WorkflowItemsHandler
{
    Task<Either<ListDashboardSMTA1WorkflowItemsQueryResponse, Errors>> Handle(ListDashboardSMTA1WorkflowItemsQuery query, CancellationToken cancellationToken);
}

public class ListDashboardSMTA1WorkflowItemsHandler : IListDashboardSMTA1WorkflowItemsHandler
{
    private readonly ILogger<ListDashboardSMTA1WorkflowItemsHandler> _logger;
    private readonly ListDashboardSMTA1WorkflowItemsQueryValidator _validator;
    private readonly ISMTA1WorkflowItemReadRepository _readRepository;
    private readonly IListDashboardSMTA1WorkflowItemMapper _mapper;

    public ListDashboardSMTA1WorkflowItemsHandler(
        ILogger<ListDashboardSMTA1WorkflowItemsHandler> logger,
        ListDashboardSMTA1WorkflowItemsQueryValidator validator,
        ISMTA1WorkflowItemReadRepository readRepository,
        IListDashboardSMTA1WorkflowItemMapper mapper)
    {
        _logger = logger;
        _validator = validator;
        _readRepository = readRepository;
        _mapper = mapper;
    }

    public async Task<Either<ListDashboardSMTA1WorkflowItemsQueryResponse, Errors>> Handle(
        ListDashboardSMTA1WorkflowItemsQuery query,
        CancellationToken cancellationToken)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(query, cancellationToken);
        if (!validationResult.IsValid)
            return new(new Errors(validationResult));

        try
        {
            IEnumerable<SMTA1WorkflowItem> SMTA1WorkflowItems = await _readRepository.List(cancellationToken);

            SMTA1WorkflowItems = FilterItems(SMTA1WorkflowItems, query.RoleType, query.UserLaboratoryId, query.UserBioHubFacilityId, query.UserPermissions);

            var SMTA1WorkflowItemsDto = _mapper.Map(SMTA1WorkflowItems.ToList());

            return new(new ListDashboardSMTA1WorkflowItemsQueryResponse(SMTA1WorkflowItemsDto));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error retrieving all SMTA1WorkflowItems");
            throw;
        }
    }

    private IEnumerable<SMTA1WorkflowItem> FilterItems(IEnumerable<SMTA1WorkflowItem> SMTA1WorkflowItems, RoleType? roleType, Guid? LaboratoryId, Guid? BioHubFacilityId, IEnumerable<string> userPermissions)
    {
        List<SMTA1WorkflowItem> filteredList = new List<SMTA1WorkflowItem>();

        switch (roleType)
        {

            case RoleType.Laboratory:
                SMTA1WorkflowItems = SMTA1WorkflowItems.Where(x => x.LaboratoryId == LaboratoryId);
                break;
        }
        foreach (SMTA1WorkflowItem worklistItem in SMTA1WorkflowItems)
        {
            var requiredPermission = StatusPermissionMapper.GetSMTA1WorkflowStatusPermission(worklistItem.Status, PermissionType.Update, worklistItem.IsPast);
            if (userPermissions.Contains(requiredPermission))
            {

                filteredList.Add(worklistItem);
            }
        }
        return filteredList;
    }
}