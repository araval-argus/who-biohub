using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.UserRequests;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UseCases.UserRequests.ListUserRequests;

public interface IListUserRequestsHandler
{
    Task<Either<ListUserRequestsQueryResponse, Errors>> Handle(ListUserRequestsQuery query, CancellationToken cancellationToken);
}

public class ListUserRequestsHandler : IListUserRequestsHandler
{
    private readonly ILogger<ListUserRequestsHandler> _logger;
    private readonly ListUserRequestsQueryValidator _validator;
    private readonly IUserRequestReadRepository _readRepository;
    private readonly IListUserRequestsMapper _mapper;

    public ListUserRequestsHandler(
        ILogger<ListUserRequestsHandler> logger,
        ListUserRequestsQueryValidator validator,
        IUserRequestReadRepository readRepository,
        IListUserRequestsMapper mapper)
    {
        _logger = logger;
        _validator = validator;
        _readRepository = readRepository;
        _mapper = mapper;
    }

    public async Task<Either<ListUserRequestsQueryResponse, Errors>> Handle(
        ListUserRequestsQuery query,
        CancellationToken cancellationToken)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(query, cancellationToken);
        if (!validationResult.IsValid)
            return new(new Errors(validationResult));

        try
        {
            IEnumerable<UserRequest> userrequests = await _readRepository.List(cancellationToken);
            var userRequestsViewModels = _mapper.Map(userrequests);
            return new(new ListUserRequestsQueryResponse(userRequestsViewModels));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error retrieving all UserRequests");
            throw;
        }
    }
}