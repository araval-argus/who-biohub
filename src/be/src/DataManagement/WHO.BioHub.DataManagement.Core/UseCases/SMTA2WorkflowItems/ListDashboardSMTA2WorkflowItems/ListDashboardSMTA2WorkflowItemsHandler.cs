using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.DataManagement.Core.UseCases.SMTA2WorkflowItems.ListDashboardSMTA2WorkflowItem;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.SMTA2WorkflowItems;
using WHO.BioHub.Shared.Enums;
using WHO.BioHub.Shared.Permissions;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UseCases.SMTA2WorkflowItems.ListDashboardSMTA2WorkflowItems;

public interface IListDashboardSMTA2WorkflowItemsHandler
{
    Task<Either<ListDashboardSMTA2WorkflowItemsQueryResponse, Errors>> Handle(ListDashboardSMTA2WorkflowItemsQuery query, CancellationToken cancellationToken);
}

public class ListDashboardSMTA2WorkflowItemsHandler : IListDashboardSMTA2WorkflowItemsHandler
{
    private readonly ILogger<ListDashboardSMTA2WorkflowItemsHandler> _logger;
    private readonly ListDashboardSMTA2WorkflowItemsQueryValidator _validator;
    private readonly ISMTA2WorkflowItemReadRepository _readRepository;
    private readonly IListDashboardSMTA2WorkflowItemMapper _mapper;

    public ListDashboardSMTA2WorkflowItemsHandler(
        ILogger<ListDashboardSMTA2WorkflowItemsHandler> logger,
        ListDashboardSMTA2WorkflowItemsQueryValidator validator,
        ISMTA2WorkflowItemReadRepository readRepository,
        IListDashboardSMTA2WorkflowItemMapper mapper)
    {
        _logger = logger;
        _validator = validator;
        _readRepository = readRepository;
        _mapper = mapper;
    }

    public async Task<Either<ListDashboardSMTA2WorkflowItemsQueryResponse, Errors>> Handle(
        ListDashboardSMTA2WorkflowItemsQuery query,
        CancellationToken cancellationToken)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(query, cancellationToken);
        if (!validationResult.IsValid)
            return new(new Errors(validationResult));

        try
        {
            IEnumerable<SMTA2WorkflowItem> SMTA2WorkflowItems = await _readRepository.List(cancellationToken);

            SMTA2WorkflowItems = FilterItems(SMTA2WorkflowItems, query.RoleType, query.UserLaboratoryId, query.UserPermissions);

            var SMTA2WorkflowItemsDto = _mapper.Map(SMTA2WorkflowItems.ToList());

            return new(new ListDashboardSMTA2WorkflowItemsQueryResponse(SMTA2WorkflowItemsDto));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error retrieving all SMTA2WorkflowItems");
            throw;
        }
    }

    private IEnumerable<SMTA2WorkflowItem> FilterItems(IEnumerable<SMTA2WorkflowItem> SMTA2WorkflowItems, RoleType? roleType, Guid? LaboratoryId, IEnumerable<string> userPermissions)
    {
        List<SMTA2WorkflowItem> filteredList = new List<SMTA2WorkflowItem>();

        switch (roleType)
        {
            case RoleType.Laboratory:
                SMTA2WorkflowItems = SMTA2WorkflowItems.Where(x => x.LaboratoryId == LaboratoryId);
                break;
        }
        foreach (SMTA2WorkflowItem worklistItem in SMTA2WorkflowItems)
        {
            var requiredPermission = StatusPermissionMapper.GetSMTA2WorkflowStatusPermission(worklistItem.Status, PermissionType.Update, worklistItem.IsPast);
            if (userPermissions.Contains(requiredPermission))
            {

                filteredList.Add(worklistItem);
            }
        }
        return filteredList;
    }
}