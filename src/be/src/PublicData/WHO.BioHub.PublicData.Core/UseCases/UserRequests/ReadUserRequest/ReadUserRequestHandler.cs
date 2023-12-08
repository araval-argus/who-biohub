using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Public.SQL.Abstractions;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.PublicData.Core.UseCases.UserRequests.ReadUserRequest;

public interface IReadUserRequestHandler
{
    Task<Either<ReadUserRequestQueryResponse, Errors>> Handle(ReadUserRequestQuery query, CancellationToken cancellationToken);
}

public class ReadUserRequestHandler : IReadUserRequestHandler
{
    private readonly ILogger<ReadUserRequestHandler> _logger;
    private readonly ReadUserRequestQueryValidator _validator;
    private readonly IUserRequestPublicReadRepository _readRepository;

    public ReadUserRequestHandler(
        ILogger<ReadUserRequestHandler> logger,
        ReadUserRequestQueryValidator validator,
        IUserRequestPublicReadRepository readRepository)
    {
        _logger = logger;
        _validator = validator;
        _readRepository = readRepository;
    }

    public async Task<Either<ReadUserRequestQueryResponse, Errors>> Handle(
        ReadUserRequestQuery query,
        CancellationToken cancellationToken)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(query, cancellationToken);
        if (!validationResult.IsValid)
            return new(new Errors(validationResult));

        try
        {
            UserRequest userRequest = await _readRepository.Read(query.Id, cancellationToken);
            if (userRequest == null)
                return new(new Errors(ErrorType.NotFound, $"UserRequest with Id {query.Id} not found"));

            return new(new ReadUserRequestQueryResponse(userRequest));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error reading UserRequest with Id {id}", query.Id);
            throw;
        }
    }
}