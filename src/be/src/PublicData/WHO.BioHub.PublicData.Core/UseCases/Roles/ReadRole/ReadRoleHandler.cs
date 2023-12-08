using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.Roles;
using WHO.BioHub.Shared.Utils;
using WHO.BioHub.Public.SQL.Abstractions;

namespace WHO.BioHub.PublicData.Core.UseCases.Roles.ReadRole;

public interface IReadRoleHandler
{
    Task<Either<ReadRoleQueryResponse, Errors>> Handle(ReadRoleQuery query, CancellationToken cancellationToken);
}

public class ReadRoleHandler : IReadRoleHandler
{
    private readonly ILogger<ReadRoleHandler> _logger;
    private readonly ReadRoleQueryValidator _validator;
    private readonly IRolePublicReadRepository _readRepository;

    public ReadRoleHandler(
        ILogger<ReadRoleHandler> logger,
        ReadRoleQueryValidator validator,
        IRolePublicReadRepository readRepository)
    {
        _logger = logger;
        _validator = validator;
        _readRepository = readRepository;
    }

    public async Task<Either<ReadRoleQueryResponse, Errors>> Handle(
        ReadRoleQuery query,
        CancellationToken cancellationToken)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(query, cancellationToken);
        if (!validationResult.IsValid)
            return new(new Errors(validationResult));

        try
        {
            Role temperatureunitofmeasure = await _readRepository.Read(query.Id, cancellationToken);
            if (temperatureunitofmeasure == null)
                return new(new Errors(ErrorType.NotFound, $"Role with Id {query.Id} not found"));

            return new(new ReadRoleQueryResponse(temperatureunitofmeasure));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error reading Role with Id {id}", query.Id);
            throw;
        }
    }
}