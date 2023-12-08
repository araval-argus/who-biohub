using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.MaterialClinicalDetailsHistory;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UseCases.MaterialClinicalDetailsHistory.ListMaterialClinicalDetailsHistory;

public interface IListMaterialClinicalDetailsHistoryHandler
{
    Task<Either<ListMaterialClinicalDetailsHistoryQueryResponse, Errors>> Handle(ListMaterialClinicalDetailsHistoryQuery query, CancellationToken cancellationToken);
}

public class ListMaterialClinicalDetailsHistoryHandler : IListMaterialClinicalDetailsHistoryHandler
{
    private readonly ILogger<ListMaterialClinicalDetailsHistoryHandler> _logger;
    private readonly ListMaterialClinicalDetailsHistoryQueryValidator _validator;
    private readonly IMaterialClinicalDetailHistoryReadRepository _readRepository;

    public ListMaterialClinicalDetailsHistoryHandler(
        ILogger<ListMaterialClinicalDetailsHistoryHandler> logger,
        ListMaterialClinicalDetailsHistoryQueryValidator validator,
        IMaterialClinicalDetailHistoryReadRepository readRepository)
    {
        _logger = logger;
        _validator = validator;
        _readRepository = readRepository;
    }

    public async Task<Either<ListMaterialClinicalDetailsHistoryQueryResponse, Errors>> Handle(
        ListMaterialClinicalDetailsHistoryQuery query,
        CancellationToken cancellationToken)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(query, cancellationToken);
        if (!validationResult.IsValid)
            return new(new Errors(validationResult));

        try
        {
            IEnumerable<MaterialClinicalDetailHistory> materialclinicaldetailshistory = await _readRepository.List(cancellationToken);
            return new(new ListMaterialClinicalDetailsHistoryQueryResponse(materialclinicaldetailshistory));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error retrieving all MaterialClinicalDetailsHistory");
            throw;
        }
    }
}