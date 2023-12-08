using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.TransportModes;
using WHO.BioHub.Shared.Utils;
using WHO.BioHub.Public.SQL.Abstractions;

namespace WHO.BioHub.PublicData.Core.UseCases.TransportModes.ListTransportModes;

public interface IListTransportModesHandler
{
    Task<Either<ListTransportModesQueryResponse, Errors>> Handle(ListTransportModesQuery query, CancellationToken cancellationToken);
}

public class ListTransportModesHandler : IListTransportModesHandler
{
    private readonly ILogger<ListTransportModesHandler> _logger;
    private readonly ListTransportModesQueryValidator _validator;
    private readonly ITransportModePublicReadRepository _readRepository;

    public ListTransportModesHandler(
        ILogger<ListTransportModesHandler> logger,
        ListTransportModesQueryValidator validator,
        ITransportModePublicReadRepository readRepository)
    {
        _logger = logger;
        _validator = validator;
        _readRepository = readRepository;
    }

    public async Task<Either<ListTransportModesQueryResponse, Errors>> Handle(
        ListTransportModesQuery query,
        CancellationToken cancellationToken)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(query, cancellationToken);
        if (!validationResult.IsValid)
            return new(new Errors(validationResult));

        try
        {
            IEnumerable<TransportMode> transportmodes = await _readRepository.List(cancellationToken);
            return new(new ListTransportModesQueryResponse(transportmodes));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error retrieving all TransportModes");
            throw;
        }
    }
}