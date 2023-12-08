using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.MaterialClinicalDetailsHistory;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UseCases.MaterialClinicalDetailsHistory.ReadMaterialClinicalDetailHistory;

public interface IReadMaterialClinicalDetailHistoryHandler
{
    Task<Either<ReadMaterialClinicalDetailHistoryQueryResponse, Errors>> Handle(ReadMaterialClinicalDetailHistoryQuery query, CancellationToken cancellationToken);
}

public class ReadMaterialClinicalDetailHistoryHandler : IReadMaterialClinicalDetailHistoryHandler
{
    private readonly ILogger<ReadMaterialClinicalDetailHistoryHandler> _logger;
    private readonly ReadMaterialClinicalDetailHistoryQueryValidator _validator;
    private readonly IMaterialClinicalDetailHistoryReadRepository _readRepository;

    public ReadMaterialClinicalDetailHistoryHandler(
        ILogger<ReadMaterialClinicalDetailHistoryHandler> logger,
        ReadMaterialClinicalDetailHistoryQueryValidator validator,
        IMaterialClinicalDetailHistoryReadRepository readRepository)
    {
        _logger = logger;
        _validator = validator;
        _readRepository = readRepository;
    }

    public async Task<Either<ReadMaterialClinicalDetailHistoryQueryResponse, Errors>> Handle(
        ReadMaterialClinicalDetailHistoryQuery query,
        CancellationToken cancellationToken)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(query, cancellationToken);
        if (!validationResult.IsValid)
            return new(new Errors(validationResult));

        try
        {
            MaterialClinicalDetailHistory materialclinicaldetailhistory = await _readRepository.Read(query.Id, cancellationToken);
            if (materialclinicaldetailhistory == null)
                return new(new Errors(ErrorType.NotFound, $"MaterialClinicalDetailHistory with Id {query.Id} not found"));

            return new(new ReadMaterialClinicalDetailHistoryQueryResponse(materialclinicaldetailhistory));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error reading MaterialClinicalDetailHistory with Id {id}", query.Id);
            throw;
        }
    }
}