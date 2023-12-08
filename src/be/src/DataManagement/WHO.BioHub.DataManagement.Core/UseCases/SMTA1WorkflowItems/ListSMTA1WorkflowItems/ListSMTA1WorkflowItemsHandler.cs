using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.DataManagement.Core.UseCases.SMTA1WorkflowItems.ListSMTA1WorkflowItem;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.Roles;
using WHO.BioHub.Models.Repositories.SMTA1WorkflowItems;
using WHO.BioHub.Shared.Dto;
using WHO.BioHub.Shared.Enums;
using WHO.BioHub.Shared.Permissions;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UseCases.SMTA1WorkflowItems.ListSMTA1WorkflowItems;

public interface IListSMTA1WorkflowItemsHandler
{
    Task<Either<ListSMTA1WorkflowItemsQueryResponse, Errors>> Handle(ListSMTA1WorkflowItemsQuery query, CancellationToken cancellationToken);
}

public class ListSMTA1WorkflowItemsHandler : IListSMTA1WorkflowItemsHandler
{
    private readonly ILogger<ListSMTA1WorkflowItemsHandler> _logger;
    private readonly ListSMTA1WorkflowItemsQueryValidator _validator;
    private readonly ISMTA1WorkflowItemReadRepository _readRepository;
    private readonly IRoleReadRepository _roleReadRepository;
    private readonly IListSMTA1WorkflowItemMapper _mapper;

    public ListSMTA1WorkflowItemsHandler(
        ILogger<ListSMTA1WorkflowItemsHandler> logger,
        ListSMTA1WorkflowItemsQueryValidator validator,
        ISMTA1WorkflowItemReadRepository readRepository,
        IRoleReadRepository roleReadRepository,
        IListSMTA1WorkflowItemMapper mapper)
    {
        _logger = logger;
        _validator = validator;
        _readRepository = readRepository;
        _mapper = mapper;
        _roleReadRepository = roleReadRepository;
    }

    public async Task<Either<ListSMTA1WorkflowItemsQueryResponse, Errors>> Handle(
        ListSMTA1WorkflowItemsQuery query,
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

            await UpdateNextOperationBy(SMTA1WorkflowItemsDto, cancellationToken);

            SMTA1WorkflowItemsDto = SMTA1WorkflowItemsDto.OrderByDescending(x => x.OperationDate).ToList();


            return new(new ListSMTA1WorkflowItemsQueryResponse(SMTA1WorkflowItemsDto));
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
            var requiredPermission = StatusPermissionMapper.GetSMTA1WorkflowStatusPermission(worklistItem.Status, PermissionType.Read, worklistItem.IsPast);

            if (userPermissions.Contains(requiredPermission))
            {

                filteredList.Add(worklistItem);
            }
        }
        return filteredList;
    }


    private async Task UpdateNextOperationBy(IEnumerable<SMTA1WorkflowItemDto> SMTA1WorkflowItemDtos, CancellationToken cancellationToken)
    {
        foreach (SMTA1WorkflowItemDto worklistItemDto in SMTA1WorkflowItemDtos)
        {
            var requiredPermission = StatusPermissionMapper.GetSMTA1WorkflowStatusPermission(worklistItemDto.CurrentStatus, PermissionType.Update, worklistItemDto.IsPast);

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