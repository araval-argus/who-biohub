using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.DataManagement.Core.UseCases.SMTA2WorkflowItems.ListSMTA2WorkflowItem;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.Roles;
using WHO.BioHub.Models.Repositories.SMTA2WorkflowItems;
using WHO.BioHub.Shared.Dto;
using WHO.BioHub.Shared.Enums;
using WHO.BioHub.Shared.Permissions;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UseCases.SMTA2WorkflowItems.ListSMTA2WorkflowItems;

public interface IListSMTA2WorkflowItemsHandler
{
    Task<Either<ListSMTA2WorkflowItemsQueryResponse, Errors>> Handle(ListSMTA2WorkflowItemsQuery query, CancellationToken cancellationToken);
}

public class ListSMTA2WorkflowItemsHandler : IListSMTA2WorkflowItemsHandler
{
    private readonly ILogger<ListSMTA2WorkflowItemsHandler> _logger;
    private readonly ListSMTA2WorkflowItemsQueryValidator _validator;
    private readonly ISMTA2WorkflowItemReadRepository _readRepository;
    private readonly IRoleReadRepository _roleReadRepository;
    private readonly IListSMTA2WorkflowItemMapper _mapper;

    public ListSMTA2WorkflowItemsHandler(
        ILogger<ListSMTA2WorkflowItemsHandler> logger,
        ListSMTA2WorkflowItemsQueryValidator validator,
        ISMTA2WorkflowItemReadRepository readRepository,
        IRoleReadRepository roleReadRepository,
        IListSMTA2WorkflowItemMapper mapper)
    {
        _logger = logger;
        _validator = validator;
        _readRepository = readRepository;
        _mapper = mapper;
        _roleReadRepository = roleReadRepository;
    }

    public async Task<Either<ListSMTA2WorkflowItemsQueryResponse, Errors>> Handle(
        ListSMTA2WorkflowItemsQuery query,
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

            await UpdateNextOperationBy(SMTA2WorkflowItemsDto, cancellationToken);

            SMTA2WorkflowItemsDto = SMTA2WorkflowItemsDto.OrderByDescending(x => x.OperationDate).ToList();


            return new(new ListSMTA2WorkflowItemsQueryResponse(SMTA2WorkflowItemsDto));
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
            var requiredPermission = StatusPermissionMapper.GetSMTA2WorkflowStatusPermission(worklistItem.Status, PermissionType.Read, worklistItem.IsPast);
            if (userPermissions.Contains(requiredPermission))
            {

                filteredList.Add(worklistItem);
            }
        }
        return filteredList;
    }

    private async Task UpdateNextOperationBy(IEnumerable<SMTA2WorkflowItemDto> SMTA2WorkflowItemDtos, CancellationToken cancellationToken)
    {
        foreach (SMTA2WorkflowItemDto worklistItemDto in SMTA2WorkflowItemDtos)
        {
            var requiredPermission = StatusPermissionMapper.GetSMTA2WorkflowStatusPermission(worklistItemDto.CurrentStatus, PermissionType.Update, worklistItemDto.IsPast);

            if (!string.IsNullOrEmpty(requiredPermission))
            {
                var roles = await _roleReadRepository.GetRolesByPermissionName(requiredPermission, cancellationToken);
                var names = new List<string>();

                foreach (var role in roles)
                {
                    if (role.RoleType == RoleType.WHO)
                    {
                        names.Add(role.Name);
                    }
                    else
                    {
                        names.Add(worklistItemDto.LaboratoryName);
                    }
                }
                names = names.Distinct().ToList();
                worklistItemDto.NextActionBy = string.Join(',', names);
            }
        }
    }   
}