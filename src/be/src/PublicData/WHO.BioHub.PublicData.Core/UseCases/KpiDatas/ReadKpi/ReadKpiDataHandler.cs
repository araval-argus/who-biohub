using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Public.SQL.Abstractions;
using WHO.BioHub.PublicData.Core.Dto;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.PublicData.Core.UseCases.KpiDatas.ReadKpiData;

public interface IReadKpiDataHandler
{
    Task<Either<ReadKpiDataQueryResponse, Errors>> Handle(ReadKpiDataQuery query, CancellationToken cancellationToken);
}

public class ReadKpiDataHandler : IReadKpiDataHandler
{
    private readonly ILogger<ReadKpiDataHandler> _logger;
    private readonly ReadKpiDataQueryValidator _validator;
    private readonly IShipmentPublicReadRepository _readRepository;

    public ReadKpiDataHandler(
        ILogger<ReadKpiDataHandler> logger,
        ReadKpiDataQueryValidator validator,
        IShipmentPublicReadRepository readRepository)
    {
        _logger = logger;
        _validator = validator;
        _readRepository = readRepository;
    }

    public async Task<Either<ReadKpiDataQueryResponse, Errors>> Handle(
        ReadKpiDataQuery query,
        CancellationToken cancellationToken)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(query, cancellationToken);
        if (!validationResult.IsValid)
            return new(new Errors(validationResult));

        try
        {
            PublicKpiViewModel publicKpiViewModel = new PublicKpiViewModel();

            publicKpiViewModel.OutgoingShipments = await _readRepository.NumberOfOutgoingShipments(cancellationToken);
            publicKpiViewModel.IncomingShipments = await _readRepository.NumberOfIncomingShipments(cancellationToken);
            publicKpiViewModel.CountryNumber = await _readRepository.CountryNumber(cancellationToken);
            publicKpiViewModel.MaterialNumber = await _readRepository.MaterialNumber(cancellationToken);

            return new(new ReadKpiDataQueryResponse(publicKpiViewModel));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error reading Kpi");
            throw;
        }
    }
}