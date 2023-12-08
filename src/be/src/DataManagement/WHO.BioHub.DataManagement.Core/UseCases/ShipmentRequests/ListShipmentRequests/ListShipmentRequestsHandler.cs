using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.WorklistFromBioHubItems;
using WHO.BioHub.Models.Repositories.WorklistToBioHubItems;
using WHO.BioHub.Shared.Dto;
using WHO.BioHub.Shared.Enums;
using WHO.BioHub.Shared.Permissions;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UseCases.ShipmentRequests.ListShipmentRequests;

public interface IListShipmentRequestsHandler
{
    Task<Either<ListShipmentRequestsQueryResponse, Errors>> Handle(ListShipmentRequestsQuery query, CancellationToken cancellationToken);
}

public class ListShipmentRequestsHandler : IListShipmentRequestsHandler
{
    private readonly ILogger<ListShipmentRequestsHandler> _logger;
    private readonly ListShipmentRequestsQueryValidator _validator;
    private readonly IWorklistToBioHubItemReadRepository _worklistToBioHubItemReadRepository;
    private readonly IWorklistFromBioHubItemReadRepository _worklistFromBioHubItemReadRepository;
    private readonly IListShipmentRequestsMapper _mapper;

    public ListShipmentRequestsHandler(
        ILogger<ListShipmentRequestsHandler> logger,
        ListShipmentRequestsQueryValidator validator,
        IWorklistToBioHubItemReadRepository worklistToBioHubItemReadRepository,
        IWorklistFromBioHubItemReadRepository worklistFromBioHubItemReadRepository,
        IListShipmentRequestsMapper mapper)
    {
        _logger = logger;
        _validator = validator;
        _worklistToBioHubItemReadRepository = worklistToBioHubItemReadRepository;
        _worklistFromBioHubItemReadRepository = worklistFromBioHubItemReadRepository;
        _mapper = mapper;
    }

    public async Task<Either<ListShipmentRequestsQueryResponse, Errors>> Handle(
        ListShipmentRequestsQuery query,
        CancellationToken cancellationToken)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(query, cancellationToken);
        if (!validationResult.IsValid)
            return new(new Errors(validationResult));
        IEnumerable<WorklistToBioHubItem> worklistToBioHubItems;
        IEnumerable<WorklistFromBioHubItem> worklistFromBioHubItems;
        IEnumerable<ShipmentRequestViewModel> shipmentsViewModel;

        try
        {
            worklistToBioHubItems = await _worklistToBioHubItemReadRepository.List(cancellationToken);

            worklistToBioHubItems = FilterToBioHubItems(worklistToBioHubItems, query.RoleType, query.LaboratoryId, query.BioHubFacilityId, query.UserPermissions);

            worklistFromBioHubItems = await _worklistFromBioHubItemReadRepository.List(cancellationToken);

            worklistFromBioHubItems = FilterFromBioHubItems(worklistFromBioHubItems, query.RoleType, query.LaboratoryId, query.BioHubFacilityId, query.UserPermissions);

            shipmentsViewModel = _mapper.Map(worklistToBioHubItems, worklistFromBioHubItems);

            return new(new ListShipmentRequestsQueryResponse(shipmentsViewModel));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error retrieving all ShipmentRequests");
            throw;
        }
    }

    private IEnumerable<WorklistFromBioHubItem> FilterFromBioHubItems(IEnumerable<WorklistFromBioHubItem> worklistFromBioHubItems, RoleType? roleType, Guid? LaboratoryId, Guid? BioHubFacilityId, IEnumerable<string> userPermissions)
    {
        List<WorklistFromBioHubItem> filteredList = new List<WorklistFromBioHubItem>();

        switch (roleType)
        {
            case RoleType.BioHubFacility:
                worklistFromBioHubItems = worklistFromBioHubItems.Where(x => x.RequestInitiationFromBioHubFacilityId == BioHubFacilityId);
                break;

            case RoleType.Laboratory:
                worklistFromBioHubItems = worklistFromBioHubItems.Where(x => x.RequestInitiationToLaboratoryId == LaboratoryId);
                break;
        }
        foreach (WorklistFromBioHubItem worklistItem in worklistFromBioHubItems)
        {
            var requiredPermission = StatusPermissionMapper.GetWorklistFromBioHubStatusPermission(worklistItem.Status, PermissionType.Update, worklistItem.IsPast);
            if (userPermissions.Contains(requiredPermission))
            {

                filteredList.Add(worklistItem);
            }
        }
        return filteredList;
    }

    private IEnumerable<WorklistToBioHubItem> FilterToBioHubItems(IEnumerable<WorklistToBioHubItem> worklistToBioHubItems, RoleType? roleType, Guid? LaboratoryId, Guid? BioHubFacilityId, IEnumerable<string> userPermissions)
    {
        List<WorklistToBioHubItem> filteredList = new List<WorklistToBioHubItem>();

        switch (roleType)
        {
            case RoleType.BioHubFacility:
                worklistToBioHubItems = worklistToBioHubItems.Where(x => x.RequestInitiationToBioHubFacilityId == BioHubFacilityId);
                break;

            case RoleType.Laboratory:
                worklistToBioHubItems = worklistToBioHubItems.Where(x => x.RequestInitiationFromLaboratoryId == LaboratoryId);
                break;
        }
        foreach (WorklistToBioHubItem worklistItem in worklistToBioHubItems)
        {
            var requiredPermission = StatusPermissionMapper.GetWorklistToBioHubStatusPermission(worklistItem.Status, PermissionType.Update, worklistItem.IsPast);
            if (userPermissions.Contains(requiredPermission))
            {

                filteredList.Add(worklistItem);
            }
        }
        return filteredList;
    }
}