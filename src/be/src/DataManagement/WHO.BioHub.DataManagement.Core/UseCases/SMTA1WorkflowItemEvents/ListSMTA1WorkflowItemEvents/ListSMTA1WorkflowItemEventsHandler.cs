using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.DataManagement.Core.UseCases.SMTA1WorkflowItems.ListSMTA1WorkflowEventItem;
using WHO.BioHub.DataManagement.Core.UseCases.WorklistToBioHubItems.ListWorklistToBioHubEventItem;
using WHO.BioHub.Models.Repositories.SMTA1WorkflowItems;
using WHO.BioHub.Models.Repositories.WorklistToBioHubItems;
using WHO.BioHub.Shared.Dto;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UseCases.SMTA1WorkflowItemEvents.ListSMTA1WorkflowItemEvents;

public interface IListSMTA1WorkflowItemEventsHandler
{
    Task<Either<ListSMTA1WorkflowItemEventsQueryResponse, Errors>> Handle(ListSMTA1WorkflowItemEventsQuery query, CancellationToken cancellationToken);
}

public class ListSMTA1WorkflowItemEventsHandler : IListSMTA1WorkflowItemEventsHandler
{
    private readonly ILogger<ListSMTA1WorkflowItemEventsHandler> _logger;
    private readonly ListSMTA1WorkflowItemEventsQueryValidator _validator;
    private readonly ISMTA1WorkflowItemReadRepository _readRepository;
    private readonly IListSMTA1WorkflowItemEventMapper _mapper;

    public ListSMTA1WorkflowItemEventsHandler(
        ILogger<ListSMTA1WorkflowItemEventsHandler> logger,
        ListSMTA1WorkflowItemEventsQueryValidator validator,
        ISMTA1WorkflowItemReadRepository readRepository,
        IListSMTA1WorkflowItemEventMapper mapper)
    {
        _logger = logger;
        _validator = validator;
        _readRepository = readRepository;
        _mapper = mapper;
    }

    public async Task<Either<ListSMTA1WorkflowItemEventsQueryResponse, Errors>> Handle(
        ListSMTA1WorkflowItemEventsQuery query,
        CancellationToken cancellationToken)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(query, cancellationToken);
        if (!validationResult.IsValid)
            return new(new Errors(validationResult));

        try
        {
            var SMTA1WorkflowItem = await _readRepository.ReadWithHistory(query.SMTA1WorkflowItemId, cancellationToken);

            if (SMTA1WorkflowItem == null)
            {
                return new(new ListSMTA1WorkflowItemEventsQueryResponse(Array.Empty<WorklistTimeline>()));
            }


            var listSMTA1WorkflowItemEvents = _mapper.Map(SMTA1WorkflowItem);

            return new(new ListSMTA1WorkflowItemEventsQueryResponse(listSMTA1WorkflowItemEvents));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error retrieving all SMTA1WorkflowItemEvents");
            throw;
        }
    }


}