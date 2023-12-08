using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.DataManagement.Core.UseCases.WorklistToBioHubItems.ListWorklistToBioHubEventItem;
using WHO.BioHub.Models.Repositories.WorklistToBioHubItems;
using WHO.BioHub.Shared.Dto;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UseCases.WorklistToBioHubItemEvents.ListWorklistToBioHubItemEvents;

public interface IListWorklistToBioHubItemEventsHandler
{
    Task<Either<ListWorklistToBioHubItemEventsQueryResponse, Errors>> Handle(ListWorklistToBioHubItemEventsQuery query, CancellationToken cancellationToken);
}

public class ListWorklistToBioHubItemEventsHandler : IListWorklistToBioHubItemEventsHandler
{
    private readonly ILogger<ListWorklistToBioHubItemEventsHandler> _logger;
    private readonly ListWorklistToBioHubItemEventsQueryValidator _validator;
    private readonly IWorklistToBioHubItemReadRepository _readRepository;
    private readonly IListWorklistToBioHubItemEventMapper _mapper;

    public ListWorklistToBioHubItemEventsHandler(
        ILogger<ListWorklistToBioHubItemEventsHandler> logger,
        ListWorklistToBioHubItemEventsQueryValidator validator,
        IWorklistToBioHubItemReadRepository readRepository,
        IListWorklistToBioHubItemEventMapper mapper)
    {
        _logger = logger;
        _validator = validator;
        _readRepository = readRepository;
        _mapper = mapper;
    }

    public async Task<Either<ListWorklistToBioHubItemEventsQueryResponse, Errors>> Handle(
        ListWorklistToBioHubItemEventsQuery query,
        CancellationToken cancellationToken)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(query, cancellationToken);
        if (!validationResult.IsValid)
            return new(new Errors(validationResult));

        try
        {
            var worklistToBioHubItem = await _readRepository.ReadWithHistory(query.WorklistToBioHubItemId, cancellationToken);

            if (worklistToBioHubItem == null)
            {
                return new(new ListWorklistToBioHubItemEventsQueryResponse(Array.Empty<WorklistTimeline>()));
            }


            var listWorklistToBioHubItemEvents = _mapper.Map(worklistToBioHubItem);

            return new(new ListWorklistToBioHubItemEventsQueryResponse(listWorklistToBioHubItemEvents));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error retrieving all WorklistToBioHubItemEvents");
            throw;
        }
    }


}