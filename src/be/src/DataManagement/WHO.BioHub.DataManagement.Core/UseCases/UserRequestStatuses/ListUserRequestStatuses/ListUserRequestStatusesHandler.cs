using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.UserRequestStatuses;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UseCases.UserRequestStatuses.ListUserRequestStatuses;

public interface IListUserRequestStatusesHandler
{
    Task<Either<ListUserRequestStatusesQueryResponse, Errors>> Handle(ListUserRequestStatusesQuery query, CancellationToken cancellationToken);
}

public class ListUserRequestStatusesHandler : IListUserRequestStatusesHandler
{
    private readonly ILogger<ListUserRequestStatusesHandler> _logger;
    private readonly ListUserRequestStatusesQueryValidator _validator;
    private readonly IUserRequestStatusReadRepository _readRepository;

    public ListUserRequestStatusesHandler(
        ILogger<ListUserRequestStatusesHandler> logger,
        ListUserRequestStatusesQueryValidator validator,
        IUserRequestStatusReadRepository readRepository)
    {
        _logger = logger;
        _validator = validator;
        _readRepository = readRepository;
    }

    public async Task<Either<ListUserRequestStatusesQueryResponse, Errors>> Handle(
        ListUserRequestStatusesQuery query,
        CancellationToken cancellationToken)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(query, cancellationToken);
        if (!validationResult.IsValid)
            return new(new Errors(validationResult));

        try
        {
            IEnumerable<UserRequestStatus> userRequestStatus = await _readRepository.List(cancellationToken);
            return new(new ListUserRequestStatusesQueryResponse(userRequestStatus));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error retrieving all UserRequestStatuses");
            throw;
        }
    }
}