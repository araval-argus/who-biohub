using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.MaterialShippingInformations;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UseCases.MaterialShippingInformations.ListMaterialShippingInformations;

public interface IListMaterialShippingInformationsHandler
{
    Task<Either<ListMaterialShippingInformationsQueryResponse, Errors>> Handle(ListMaterialShippingInformationsQuery query, CancellationToken cancellationToken);
}

public class ListMaterialShippingInformationsHandler : IListMaterialShippingInformationsHandler
{
    private readonly ILogger<ListMaterialShippingInformationsHandler> _logger;
    private readonly ListMaterialShippingInformationsQueryValidator _validator;
    private readonly IMaterialShippingInformationReadRepository _readRepository;

    public ListMaterialShippingInformationsHandler(
        ILogger<ListMaterialShippingInformationsHandler> logger,
        ListMaterialShippingInformationsQueryValidator validator,
        IMaterialShippingInformationReadRepository readRepository)
    {
        _logger = logger;
        _validator = validator;
        _readRepository = readRepository;
    }

    public async Task<Either<ListMaterialShippingInformationsQueryResponse, Errors>> Handle(
        ListMaterialShippingInformationsQuery query,
        CancellationToken cancellationToken)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(query, cancellationToken);
        if (!validationResult.IsValid)
            return new(new Errors(validationResult));

        try
        {
            IEnumerable<MaterialShippingInformation> materialshippinginformations = await _readRepository.List(cancellationToken);
            return new(new ListMaterialShippingInformationsQueryResponse(materialshippinginformations));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error retrieving all MaterialShippingInformations");
            throw;
        }
    }
}