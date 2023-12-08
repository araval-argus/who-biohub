using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.MaterialShippingInformationsHistory;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UseCases.MaterialShippingInformationsHistory.ListMaterialShippingInformationsHistory;

public interface IListMaterialShippingInformationsHistoryHandler
{
    Task<Either<ListMaterialShippingInformationsHistoryQueryResponse, Errors>> Handle(ListMaterialShippingInformationsHistoryQuery query, CancellationToken cancellationToken);
}

public class ListMaterialShippingInformationsHistoryHandler : IListMaterialShippingInformationsHistoryHandler
{
    private readonly ILogger<ListMaterialShippingInformationsHistoryHandler> _logger;
    private readonly ListMaterialShippingInformationsHistoryQueryValidator _validator;
    private readonly IMaterialShippingInformationHistoryReadRepository _readRepository;

    public ListMaterialShippingInformationsHistoryHandler(
        ILogger<ListMaterialShippingInformationsHistoryHandler> logger,
        ListMaterialShippingInformationsHistoryQueryValidator validator,
        IMaterialShippingInformationHistoryReadRepository readRepository)
    {
        _logger = logger;
        _validator = validator;
        _readRepository = readRepository;
    }

    public async Task<Either<ListMaterialShippingInformationsHistoryQueryResponse, Errors>> Handle(
        ListMaterialShippingInformationsHistoryQuery query,
        CancellationToken cancellationToken)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(query, cancellationToken);
        if (!validationResult.IsValid)
            return new(new Errors(validationResult));

        try
        {
            IEnumerable<MaterialShippingInformationHistory> materialshippinginformationsHistory = await _readRepository.List(cancellationToken);
            return new(new ListMaterialShippingInformationsHistoryQueryResponse(materialshippinginformationsHistory));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error retrieving all MaterialShippingInformationsHistory");
            throw;
        }
    }
}