using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.DataManagement.Core.UseCases.WorklistToBioHubItems.ListWorklistToBioHubItem;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.Roles;
using WHO.BioHub.Models.Repositories.WorklistToBioHubItems;
using WHO.BioHub.Shared.Dto;
using WHO.BioHub.Shared.Enums;
using WHO.BioHub.Shared.Permissions;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UseCases.WorklistToBioHubItems.ListWorklistToBioHubItems;

public interface IListWorklistToBioHubItemsHandler
{
    Task<Either<ListWorklistToBioHubItemsQueryResponse, Errors>> Handle(ListWorklistToBioHubItemsQuery query, CancellationToken cancellationToken);
}

public class ListWorklistToBioHubItemsHandler : IListWorklistToBioHubItemsHandler
{
    private readonly ILogger<ListWorklistToBioHubItemsHandler> _logger;
    private readonly ListWorklistToBioHubItemsQueryValidator _validator;
    private readonly IWorklistToBioHubItemReadRepository _readRepository;
    private readonly IRoleReadRepository _roleReadRepository;
    private readonly IListWorklistToBioHubItemMapper _mapper;

    public ListWorklistToBioHubItemsHandler(
        ILogger<ListWorklistToBioHubItemsHandler> logger,
        ListWorklistToBioHubItemsQueryValidator validator,
        IWorklistToBioHubItemReadRepository readRepository,
        IRoleReadRepository roleReadRepository,
        IListWorklistToBioHubItemMapper mapper)
    {
        _logger = logger;
        _validator = validator;
        _readRepository = readRepository;
        _mapper = mapper;
        _roleReadRepository = roleReadRepository;
    }

    public async Task<Either<ListWorklistToBioHubItemsQueryResponse, Errors>> Handle(
        ListWorklistToBioHubItemsQuery query,
        CancellationToken cancellationToken)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(query, cancellationToken);
        if (!validationResult.IsValid)
            return new(new Errors(validationResult));

        try
        {
            IEnumerable<WorklistToBioHubItem> worklisttobiohubitems = await _readRepository.List(cancellationToken);

            worklisttobiohubitems = FilterItems(worklisttobiohubitems, query.RoleType, query.UserLaboratoryId, query.UserBioHubFacilityId, query.UserPermissions);

            var worklisttobiohubitemsDto = _mapper.Map(worklisttobiohubitems.ToList());

            await UpdateNextOperationBy(worklisttobiohubitemsDto, cancellationToken);

            worklisttobiohubitemsDto = worklisttobiohubitemsDto.OrderByDescending(x => x.OperationDate).ToList();

            return new(new ListWorklistToBioHubItemsQueryResponse(worklisttobiohubitemsDto));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error retrieving all WorklistToBioHubItems");
            throw;
        }
    }

    private IEnumerable<WorklistToBioHubItem> FilterItems(IEnumerable<WorklistToBioHubItem> worklisttobiohubitems, RoleType? roleType, Guid? LaboratoryId, Guid? BioHubFacilityId, IEnumerable<string> userPermissions)
    {
        List<WorklistToBioHubItem> filteredList = new List<WorklistToBioHubItem>();

        switch (roleType)
        {
            case RoleType.BioHubFacility:
                worklisttobiohubitems = worklisttobiohubitems.Where(x => x.RequestInitiationToBioHubFacilityId == BioHubFacilityId);
                break;

            case RoleType.Laboratory:
                worklisttobiohubitems = worklisttobiohubitems.Where(x => x.RequestInitiationFromLaboratoryId == LaboratoryId);
                break;
        }
        foreach (WorklistToBioHubItem worklistItem in worklisttobiohubitems)
        {
            var requiredPermission = StatusPermissionMapper.GetWorklistToBioHubStatusPermission(worklistItem.Status, PermissionType.Read, worklistItem.IsPast);
            if (userPermissions.Contains(requiredPermission))
            {

                filteredList.Add(worklistItem);
            }
        }
        return filteredList;
    }

    private async Task UpdateNextOperationBy(IEnumerable<WorklistToBioHubItemDto> worklistToBioHubItemDtos, CancellationToken cancellationToken)
    {
        foreach (WorklistToBioHubItemDto worklistItemDto in worklistToBioHubItemDtos)
        {
            var requiredPermission = StatusPermissionMapper.GetWorklistToBioHubStatusPermission(worklistItemDto.CurrentStatus, PermissionType.Update, worklistItemDto.IsPast);

            if (!string.IsNullOrEmpty(requiredPermission))
            {
                var roles = await _roleReadRepository.GetRolesByPermissionName(requiredPermission, cancellationToken);
                var names = new List<string>();
                foreach (var role in roles)
                {
                    switch (role.RoleType)
                    {
                        case RoleType.WHO:
                            names.Add(role.Name);
                            break;

                        case RoleType.BioHubFacility:
                            names.Add(worklistItemDto.BioHubFacilityName);
                            break;

                        case RoleType.Laboratory:
                            names.Add(worklistItemDto.LaboratoryName);
                            break;

                        default:
                            break;

                    }                    
                }
                names = names.Distinct().ToList();
                worklistItemDto.NextActionBy = string.Join(',', names);
            }
        }
    }    
}