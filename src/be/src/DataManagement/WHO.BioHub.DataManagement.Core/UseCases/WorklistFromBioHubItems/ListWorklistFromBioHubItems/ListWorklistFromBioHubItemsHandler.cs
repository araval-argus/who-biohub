using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.DataManagement.Core.UseCases.WorklistFromBioHubItems.ListWorklistFromBioHubItem;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.Roles;
using WHO.BioHub.Models.Repositories.WorklistFromBioHubItems;
using WHO.BioHub.Shared.Dto;
using WHO.BioHub.Shared.Enums;
using WHO.BioHub.Shared.Permissions;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UseCases.WorklistFromBioHubItems.ListWorklistFromBioHubItems;

public interface IListWorklistFromBioHubItemsHandler
{
    Task<Either<ListWorklistFromBioHubItemsQueryResponse, Errors>> Handle(ListWorklistFromBioHubItemsQuery query, CancellationToken cancellationToken);
}

public class ListWorklistFromBioHubItemsHandler : IListWorklistFromBioHubItemsHandler
{
    private readonly ILogger<ListWorklistFromBioHubItemsHandler> _logger;
    private readonly ListWorklistFromBioHubItemsQueryValidator _validator;
    private readonly IWorklistFromBioHubItemReadRepository _readRepository;
    private readonly IRoleReadRepository _roleReadRepository;
    private readonly IListWorklistFromBioHubItemMapper _mapper;

    public ListWorklistFromBioHubItemsHandler(
        ILogger<ListWorklistFromBioHubItemsHandler> logger,
        ListWorklistFromBioHubItemsQueryValidator validator,
        IWorklistFromBioHubItemReadRepository readRepository,
        IRoleReadRepository roleReadRepository,
        IListWorklistFromBioHubItemMapper mapper)
    {
        _logger = logger;
        _validator = validator;
        _readRepository = readRepository;
        _mapper = mapper;
        _roleReadRepository = roleReadRepository;
    }

    public async Task<Either<ListWorklistFromBioHubItemsQueryResponse, Errors>> Handle(
        ListWorklistFromBioHubItemsQuery query,
        CancellationToken cancellationToken)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(query, cancellationToken);
        if (!validationResult.IsValid)
            return new(new Errors(validationResult));

        try
        {
            IEnumerable<WorklistFromBioHubItem> worklistfrombiohubitems = await _readRepository.List(cancellationToken);

            worklistfrombiohubitems = FilterItems(worklistfrombiohubitems, query.RoleType, query.UserLaboratoryId, query.UserBioHubFacilityId, query.UserPermissions);

            var worklistfrombiohubitemsDto = _mapper.Map(worklistfrombiohubitems.ToList());

            await UpdateNextOperationBy(worklistfrombiohubitemsDto, cancellationToken);

            worklistfrombiohubitemsDto = worklistfrombiohubitemsDto.OrderByDescending(x => x.OperationDate).ToList();

            return new(new ListWorklistFromBioHubItemsQueryResponse(worklistfrombiohubitemsDto));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error retrieving all WorklistFromBioHubItems");
            throw;
        }
    }

    private IEnumerable<WorklistFromBioHubItem> FilterItems(IEnumerable<WorklistFromBioHubItem> worklistfrombiohubitems, RoleType? roleType, Guid? LaboratoryId, Guid? BioHubFacilityId, IEnumerable<string> userPermissions)
    {
        List<WorklistFromBioHubItem> filteredList = new List<WorklistFromBioHubItem>();

        switch (roleType)
        {
            case RoleType.BioHubFacility:
                worklistfrombiohubitems = worklistfrombiohubitems.Where(x => x.RequestInitiationFromBioHubFacilityId == BioHubFacilityId);
                break;

            case RoleType.Laboratory:
                worklistfrombiohubitems = worklistfrombiohubitems.Where(x => x.RequestInitiationToLaboratoryId == LaboratoryId);
                break;
        }
        foreach (WorklistFromBioHubItem worklistItem in worklistfrombiohubitems)
        {
            var requiredPermission = StatusPermissionMapper.GetWorklistFromBioHubStatusPermission(worklistItem.Status, PermissionType.Read, worklistItem.IsPast);
            if (userPermissions.Contains(requiredPermission))
            {

                filteredList.Add(worklistItem);
            }
        }
        return filteredList;
    }

    private async Task UpdateNextOperationBy(IEnumerable<WorklistFromBioHubItemDto> SMTA1WorkflowItemDtos, CancellationToken cancellationToken)
    {
        foreach (WorklistFromBioHubItemDto worklistItemDto in SMTA1WorkflowItemDtos)
        {
            var requiredPermission = StatusPermissionMapper.GetWorklistFromBioHubStatusPermission(worklistItemDto.CurrentStatus, PermissionType.Update, worklistItemDto.IsPast);

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