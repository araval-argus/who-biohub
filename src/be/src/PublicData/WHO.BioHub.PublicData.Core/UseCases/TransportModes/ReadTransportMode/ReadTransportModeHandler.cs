using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.TransportModes;
using WHO.BioHub.Shared.Utils;
using WHO.BioHub.Public.SQL.Abstractions;

namespace WHO.BioHub.PublicData.Core.UseCases.TransportModes.ReadTransportMode;

public interface IReadTransportModeHandler
{
    Task<Either<ReadTransportModeQueryResponse, Errors>> Handle(ReadTransportModeQuery query, CancellationToken cancellationToken);
}

public class ReadTransportModeHandler : IReadTransportModeHandler
{
    private readonly ILogger<ReadTransportModeHandler> _logger;
    private readonly ReadTransportModeQueryValidator _validator;
    private readonly ITransportModePublicReadRepository _readRepository;

    public ReadTransportModeHandler(
        ILogger<ReadTransportModeHandler> logger,
        ReadTransportModeQueryValidator validator,
        ITransportModePublicReadRepository readRepository)
    {
        _logger = logger;
        _validator = validator;
        _readRepository = readRepository;
    }

    public async Task<Either<ReadTransportModeQueryResponse, Errors>> Handle(
        ReadTransportModeQuery query,
        CancellationToken cancellationToken)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(query, cancellationToken);
        if (!validationResult.IsValid)
            return new(new Errors(validationResult));

        try
        {
            TransportMode transportmode = await _readRepository.Read(query.Id, cancellationToken);
            if (transportmode == null)
                return new(new Errors(ErrorType.NotFound, $"TransportMode with Id {query.Id} not found"));

            return new(new ReadTransportModeQueryResponse(transportmode));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error reading TransportMode with Id {id}", query.Id);
            throw;
        }
    }
}