using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.SMTA2WorkflowItems;
using WHO.BioHub.Models.Repositories.SMTA1WorkflowItems;
using WHO.BioHub.Shared.Dto;
using WHO.BioHub.Shared.Enums;
using WHO.BioHub.Shared.Permissions;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UseCases.SMTARequests.ListSMTARequests;

public interface IListSMTARequestsHandler
{
    Task<Either<ListSMTARequestsQueryResponse, Errors>> Handle(ListSMTARequestsQuery query, CancellationToken cancellationToken);
}

public class ListSMTARequestsHandler : IListSMTARequestsHandler
{
    private readonly ILogger<ListSMTARequestsHandler> _logger;
    private readonly ListSMTARequestsQueryValidator _validator;
    private readonly ISMTA1WorkflowItemReadRepository _SMTA1WorkflowItemReadRepository;
    private readonly ISMTA2WorkflowItemReadRepository _SMTA2WorkflowItemReadRepository;
    private readonly IListSMTARequestsMapper _mapper;

    public ListSMTARequestsHandler(
        ILogger<ListSMTARequestsHandler> logger,
        ListSMTARequestsQueryValidator validator,
        ISMTA1WorkflowItemReadRepository SMTA1WorkflowItemReadRepository,
        ISMTA2WorkflowItemReadRepository SMTA2WorkflowItemReadRepository,
        IListSMTARequestsMapper mapper)
    {
        _logger = logger;
        _validator = validator;
        _SMTA1WorkflowItemReadRepository = SMTA1WorkflowItemReadRepository;
        _SMTA2WorkflowItemReadRepository = SMTA2WorkflowItemReadRepository;
        _mapper = mapper;
    }

    public async Task<Either<ListSMTARequestsQueryResponse, Errors>> Handle(
        ListSMTARequestsQuery query,
        CancellationToken cancellationToken)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(query, cancellationToken);
        if (!validationResult.IsValid)
            return new(new Errors(validationResult));
        IEnumerable<SMTA1WorkflowItem> SMTA1WorkflowItems;
        IEnumerable<SMTA2WorkflowItem> SMTA2WorkflowItems;
        IEnumerable<SMTARequestViewModel> smtasViewModel;

        try
        {
            SMTA1WorkflowItems = await _SMTA1WorkflowItemReadRepository.List(cancellationToken);

            SMTA1WorkflowItems = FilterSMTA1Items(SMTA1WorkflowItems, query.RoleType, query.LaboratoryId, query.BioHubFacilityId, query.UserPermissions);

            SMTA2WorkflowItems = await _SMTA2WorkflowItemReadRepository.List(cancellationToken);

            SMTA2WorkflowItems = FilterSMTA2Items(SMTA2WorkflowItems, query.RoleType, query.LaboratoryId, query.BioHubFacilityId, query.UserPermissions);

            smtasViewModel = _mapper.Map(SMTA1WorkflowItems, SMTA2WorkflowItems);

            return new(new ListSMTARequestsQueryResponse(smtasViewModel));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error retrieving all SMTARequests");
            throw;
        }
    }



    private IEnumerable<SMTA1WorkflowItem> FilterSMTA1Items(IEnumerable<SMTA1WorkflowItem> SMTA1WorkflowItems, RoleType? roleType, Guid? LaboratoryId, Guid? BioHubFacilityId, IEnumerable<string> userPermissions)
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

    private IEnumerable<SMTA2WorkflowItem> FilterSMTA2Items(IEnumerable<SMTA2WorkflowItem> SMTA2WorkflowItems, RoleType? roleType, Guid? LaboratoryId, Guid? BioHubFacilityId, IEnumerable<string> userPermissions)
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