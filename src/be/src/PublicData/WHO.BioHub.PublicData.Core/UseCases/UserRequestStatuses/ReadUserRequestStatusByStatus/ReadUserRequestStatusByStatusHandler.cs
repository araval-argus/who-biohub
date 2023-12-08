using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Public.SQL.Abstractions;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.PublicData.Core.UseCases.UserRequestStatuses.ReadUserRequestStatus;

public interface IReadUserRequestStatusByStatusHandler
{
    Task<Either<ReadUserRequestStatusByStatusQueryResponse, Errors>> Handle(ReadUserRequestStatusByStatusQuery query, CancellationToken cancellationToken);
}

public class ReadUserRequestStatusByStatusHandler : IReadUserRequestStatusByStatusHandler
{
    private readonly ILogger<ReadUserRequestStatusByStatusHandler> _logger;
    private readonly ReadUserRequestStatusByStatusQueryValidator _validator;
    private readonly IUserRequestStatusPublicReadRepository _readRepository;

    public ReadUserRequestStatusByStatusHandler(
        ILogger<ReadUserRequestStatusByStatusHandler> logger,
        ReadUserRequestStatusByStatusQueryValidator validator,
        IUserRequestStatusPublicReadRepository readRepository)
    {
        _logger = logger;
        _validator = validator;
        _readRepository = readRepository;
    }

    public async Task<Either<ReadUserRequestStatusByStatusQueryResponse, Errors>> Handle(
        ReadUserRequestStatusByStatusQuery query,
        CancellationToken cancellationToken)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(query, cancellationToken);
        if (!validationResult.IsValid)
            return new(new Errors(validationResult));

        try
        {
            UserRequestStatus userRequest = await _readRepository.ReadByStatus(query.Status, cancellationToken);
            if (userRequest == null)
                return new(new Errors(ErrorType.NotFound, $"UserRequestStatus with Id {query.Status} not found"));

            return new(new ReadUserRequestStatusByStatusQueryResponse(userRequest));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error reading UserRequestStatus with status {status}", query.Status);
            throw;
        }
    }
}