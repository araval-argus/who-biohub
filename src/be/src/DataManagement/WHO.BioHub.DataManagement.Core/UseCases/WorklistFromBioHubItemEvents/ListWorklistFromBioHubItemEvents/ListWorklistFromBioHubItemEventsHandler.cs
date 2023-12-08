using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.DataManagement.Core.UseCases.WorklistFromBioHubItems.ListWorklistFromBioHubEventItem;
using WHO.BioHub.Models.Repositories.WorklistFromBioHubItems;
using WHO.BioHub.Shared.Dto;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UseCases.WorklistFromBioHubItemEvents.ListWorklistFromBioHubItemEvents;

public interface IListWorklistFromBioHubItemEventsHandler
{
    Task<Either<ListWorklistFromBioHubItemEventsQueryResponse, Errors>> Handle(ListWorklistFromBioHubItemEventsQuery query, CancellationToken cancellationToken);
}

public class ListWorklistFromBioHubItemEventsHandler : IListWorklistFromBioHubItemEventsHandler
{
    private readonly ILogger<ListWorklistFromBioHubItemEventsHandler> _logger;
    private readonly ListWorklistFromBioHubItemEventsQueryValidator _validator;
    private readonly IWorklistFromBioHubItemReadRepository _readRepository;
    private readonly IListWorklistFromBioHubItemEventMapper _mapper;

    public ListWorklistFromBioHubItemEventsHandler(
        ILogger<ListWorklistFromBioHubItemEventsHandler> logger,
        ListWorklistFromBioHubItemEventsQueryValidator validator,
        IWorklistFromBioHubItemReadRepository readRepository,
        IListWorklistFromBioHubItemEventMapper mapper)
    {
        _logger = logger;
        _validator = validator;
        _readRepository = readRepository;
        _mapper = mapper;
    }

    public async Task<Either<ListWorklistFromBioHubItemEventsQueryResponse, Errors>> Handle(
        ListWorklistFromBioHubItemEventsQuery query,
        CancellationToken cancellationToken)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(query, cancellationToken);
        if (!validationResult.IsValid)
            return new(new Errors(validationResult));

        try
        {
            var worklistFromBioHubItem = await _readRepository.ReadWithHistory(query.WorklistFromBioHubItemId, cancellationToken);

            if (worklistFromBioHubItem == null)
            {
                return new(new ListWorklistFromBioHubItemEventsQueryResponse(Array.Empty<WorklistTimeline>()));
            }


            var listWorklistFromBioHubItemEvents = _mapper.Map(worklistFromBioHubItem);

            return new(new ListWorklistFromBioHubItemEventsQueryResponse(listWorklistFromBioHubItemEvents));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error retrieving all WorklistFromBioHubItemEvents");
            throw;
        }
    }


}