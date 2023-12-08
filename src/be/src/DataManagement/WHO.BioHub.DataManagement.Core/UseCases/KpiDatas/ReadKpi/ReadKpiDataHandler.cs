using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.Shipments;
using WHO.BioHub.Shared.Dto;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.Data.Core.UseCases.KpiDatas.ReadKpiData;

public interface IReadKpiDataHandler
{
    Task<Either<ReadKpiDataQueryResponse, Errors>> Handle(ReadKpiDataQuery query, CancellationToken cancellationToken);
}

public class ReadKpiDataHandler : IReadKpiDataHandler
{
    private readonly ILogger<ReadKpiDataHandler> _logger;
    private readonly ReadKpiDataQueryValidator _validator;
    private readonly IShipmentReadRepository _readRepository;

    public ReadKpiDataHandler(
        ILogger<ReadKpiDataHandler> logger,
        ReadKpiDataQueryValidator validator,
        IShipmentReadRepository readRepository)
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
            KpiViewModel kpiViewModel = new KpiViewModel();

            kpiViewModel.IncomingShipmentInformation = await _readRepository.IncomingShipmentsKPIData(cancellationToken);
            kpiViewModel.OutgoingShipmentInformation = await _readRepository.OutgoingShipmentsKPIData(cancellationToken);

            return new(new ReadKpiDataQueryResponse(kpiViewModel));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error reading Kpi");
            throw;
        }
    }
}