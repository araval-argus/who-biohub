using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Public.SQL.Abstractions;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.PublicData.Core.UseCases.UserRequestStatuses.ReadUserRequestStatus;

public interface IReadUserRequestStatusHandler
{
    Task<Either<ReadUserRequestStatusQueryResponse, Errors>> Handle(ReadUserRequestStatusQuery query, CancellationToken cancellationToken);
}

public class ReadUserRequestStatusHandler : IReadUserRequestStatusHandler
{
    private readonly ILogger<ReadUserRequestStatusHandler> _logger;
    private readonly ReadUserRequestStatusQueryValidator _validator;
    private readonly IUserRequestStatusPublicReadRepository _readRepository;

    public ReadUserRequestStatusHandler(
        ILogger<ReadUserRequestStatusHandler> logger,
        ReadUserRequestStatusQueryValidator validator,
        IUserRequestStatusPublicReadRepository readRepository)
    {
        _logger = logger;
        _validator = validator;
        _readRepository = readRepository;
    }

    public async Task<Either<ReadUserRequestStatusQueryResponse, Errors>> Handle(
        ReadUserRequestStatusQuery query,
        CancellationToken cancellationToken)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(query, cancellationToken);
        if (!validationResult.IsValid)
            return new(new Errors(validationResult));

        try
        {
            UserRequestStatus userRequest = await _readRepository.Read(query.Id, cancellationToken);
            if (userRequest == null)
                return new(new Errors(ErrorType.NotFound, $"UserRequestStatus with Id {query.Id} not found"));

            return new(new ReadUserRequestStatusQueryResponse(userRequest));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error reading UserRequestStatus with Id {id}", query.Id);
            throw;
        }
    }
}