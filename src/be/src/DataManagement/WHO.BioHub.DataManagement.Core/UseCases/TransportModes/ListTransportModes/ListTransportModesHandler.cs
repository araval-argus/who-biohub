using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.TransportModes;
using WHO.BioHub.Shared.Dto;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UseCases.TransportModes.ListTransportModes;

public interface IListTransportModesHandler
{
    Task<Either<ListTransportModesQueryResponse, Errors>> Handle(ListTransportModesQuery query, CancellationToken cancellationToken);
}

public class ListTransportModesHandler : IListTransportModesHandler
{
    private readonly ILogger<ListTransportModesHandler> _logger;
    private readonly ListTransportModesQueryValidator _validator;
    private readonly ITransportModeReadRepository _readRepository;

    public ListTransportModesHandler(
        ILogger<ListTransportModesHandler> logger,
        ListTransportModesQueryValidator validator,
        ITransportModeReadRepository readRepository)
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
            var transportModeDtos = new List<TransportModeDto>();
            foreach (var transportMode in transportmodes)
            {
                TransportModeDto transportModeDto = new()
                {
                    Id = transportMode.Id,
                    Name = transportMode.Name,
                    Description = transportMode.Description,
                    HexColor = transportMode.HexColor,
                    IsActive = transportMode.IsActive
                };

                transportModeDtos.Add(transportModeDto);
            }
            return new(new ListTransportModesQueryResponse(transportModeDtos));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error retrieving all TransportModes");
            throw;
        }
    }
}