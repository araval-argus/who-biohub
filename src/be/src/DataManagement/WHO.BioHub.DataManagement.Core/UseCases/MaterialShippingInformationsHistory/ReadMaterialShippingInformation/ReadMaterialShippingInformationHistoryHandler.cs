using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.DataManagement.Core.UseCases.MaterialShippingInformationsHistory.ReadMaterialShippingInformationHistory;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.MaterialShippingInformations;
using WHO.BioHub.Models.Repositories.MaterialShippingInformationsHistory;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UseCases.MaterialShippingInformationsHistory.ReadMaterialShippingInformation;

public interface IReadMaterialShippingInformationHistoryHandler
{
    Task<Either<ReadMaterialShippingInformationHistoryQueryResponse, Errors>> Handle(ReadMaterialShippingInformationHistoryQuery query, CancellationToken cancellationToken);
}

public class ReadMaterialShippingInformationHistoryHandler : IReadMaterialShippingInformationHistoryHandler
{
    private readonly ILogger<ReadMaterialShippingInformationHistoryHandler> _logger;
    private readonly ReadMaterialShippingInformationHistoryQueryValidator _validator;
    private readonly IMaterialShippingInformationHistoryReadRepository _readRepository;

    public ReadMaterialShippingInformationHistoryHandler(
        ILogger<ReadMaterialShippingInformationHistoryHandler> logger,
        ReadMaterialShippingInformationHistoryQueryValidator validator,
        IMaterialShippingInformationHistoryReadRepository readRepository)
    {
        _logger = logger;
        _validator = validator;
        _readRepository = readRepository;
    }

    public async Task<Either<ReadMaterialShippingInformationHistoryQueryResponse, Errors>> Handle(
        ReadMaterialShippingInformationHistoryQuery query,
        CancellationToken cancellationToken)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(query, cancellationToken);
        if (!validationResult.IsValid)
            return new(new Errors(validationResult));

        try
        {
            MaterialShippingInformationHistory materialshippinginformationHistory = await _readRepository.Read(query.Id, cancellationToken);
            if (materialshippinginformationHistory == null)
                return new(new Errors(ErrorType.NotFound, $"MaterialShippingInformationHistory with Id {query.Id} not found"));

            return new(new ReadMaterialShippingInformationHistoryQueryResponse(materialshippinginformationHistory));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error reading MaterialShippingInformationHistory with Id {id}", query.Id);
            throw;
        }
    }
}