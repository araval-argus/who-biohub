using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.DataManagement.Core.UseCases.SMTA2WorkflowItems.ListSMTA2WorkflowEventItem;
using WHO.BioHub.Models.Repositories.SMTA2WorkflowItems;
using WHO.BioHub.Models.Repositories.WorklistToBioHubItems;
using WHO.BioHub.Shared.Dto;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UseCases.SMTA2WorkflowItemEvents.ListSMTA2WorkflowItemEvents;

public interface IListSMTA2WorkflowItemEventsHandler
{
    Task<Either<ListSMTA2WorkflowItemEventsQueryResponse, Errors>> Handle(ListSMTA2WorkflowItemEventsQuery query, CancellationToken cancellationToken);
}

public class ListSMTA2WorkflowItemEventsHandler : IListSMTA2WorkflowItemEventsHandler
{
    private readonly ILogger<ListSMTA2WorkflowItemEventsHandler> _logger;
    private readonly ListSMTA2WorkflowItemEventsQueryValidator _validator;
    private readonly ISMTA2WorkflowItemReadRepository _readRepository;
    private readonly IListSMTA2WorkflowItemEventMapper _mapper;

    public ListSMTA2WorkflowItemEventsHandler(
        ILogger<ListSMTA2WorkflowItemEventsHandler> logger,
        ListSMTA2WorkflowItemEventsQueryValidator validator,
        ISMTA2WorkflowItemReadRepository readRepository,
        IListSMTA2WorkflowItemEventMapper mapper)
    {
        _logger = logger;
        _validator = validator;
        _readRepository = readRepository;
        _mapper = mapper;
    }

    public async Task<Either<ListSMTA2WorkflowItemEventsQueryResponse, Errors>> Handle(
        ListSMTA2WorkflowItemEventsQuery query,
        CancellationToken cancellationToken)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(query, cancellationToken);
        if (!validationResult.IsValid)
            return new(new Errors(validationResult));

        try
        {
            var SMTA2WorkflowItem = await _readRepository.ReadWithHistory(query.SMTA2WorkflowItemId, cancellationToken);

            if (SMTA2WorkflowItem == null)
            {
                return new(new ListSMTA2WorkflowItemEventsQueryResponse(Array.Empty<WorklistTimeline>()));
            }


            var listSMTA2WorkflowItemEvents = _mapper.Map(SMTA2WorkflowItem);

            return new(new ListSMTA2WorkflowItemEventsQueryResponse(listSMTA2WorkflowItemEvents));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error retrieving all SMTA2WorkflowItemEvents");
            throw;
        }
    }


}