using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.Users;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UseCases.Users.ReadUserByExternalId;

public interface IReadUserByExternalIdHandler
{
    Task<Either<ReadUserByExternalIdQueryResponse, Errors>> Handle(ReadUserByExternalIdQuery query, CancellationToken cancellationToken);
}

public class ReadUserByExternalIdHandler : IReadUserByExternalIdHandler
{
    private readonly ILogger<ReadUserByExternalIdHandler> _logger;
    private readonly ReadUserByExternalIdQueryValidator _validator;
    private readonly IUserReadRepository _readRepository;

    public ReadUserByExternalIdHandler(
        ILogger<ReadUserByExternalIdHandler> logger,
        ReadUserByExternalIdQueryValidator validator,
        IUserReadRepository readRepository)
    {
        _logger = logger;
        _validator = validator;
        _readRepository = readRepository;
    }

    public async Task<Either<ReadUserByExternalIdQueryResponse, Errors>> Handle(
        ReadUserByExternalIdQuery query,
        CancellationToken cancellationToken)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(query, cancellationToken);
        if (!validationResult.IsValid)
            return new(new Errors(validationResult));

        try
        {
            User user = await _readRepository.ReadByExternalId(query.ExternalId, cancellationToken);
            if (user == null)
                return new(new Errors(ErrorType.NotFound, $"UserByExternalId with Id {query.ExternalId} not found"));

            return new(new ReadUserByExternalIdQueryResponse(user));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error reading UserByExternalId with ExternalId {id}", query.ExternalId);
            throw;
        }
    }
}