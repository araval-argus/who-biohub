using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.MaterialClinicalDetails;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UseCases.MaterialClinicalDetails.ListMaterialClinicalDetails;

public interface IListMaterialClinicalDetailsHandler
{
    Task<Either<ListMaterialClinicalDetailsQueryResponse, Errors>> Handle(ListMaterialClinicalDetailsQuery query, CancellationToken cancellationToken);
}

public class ListMaterialClinicalDetailsHandler : IListMaterialClinicalDetailsHandler
{
    private readonly ILogger<ListMaterialClinicalDetailsHandler> _logger;
    private readonly ListMaterialClinicalDetailsQueryValidator _validator;
    private readonly IMaterialClinicalDetailReadRepository _readRepository;

    public ListMaterialClinicalDetailsHandler(
        ILogger<ListMaterialClinicalDetailsHandler> logger,
        ListMaterialClinicalDetailsQueryValidator validator,
        IMaterialClinicalDetailReadRepository readRepository)
    {
        _logger = logger;
        _validator = validator;
        _readRepository = readRepository;
    }

    public async Task<Either<ListMaterialClinicalDetailsQueryResponse, Errors>> Handle(
        ListMaterialClinicalDetailsQuery query,
        CancellationToken cancellationToken)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(query, cancellationToken);
        if (!validationResult.IsValid)
            return new(new Errors(validationResult));

        try
        {
            IEnumerable<MaterialClinicalDetail> materialclinicaldetails = await _readRepository.List(cancellationToken);
            return new(new ListMaterialClinicalDetailsQueryResponse(materialclinicaldetails));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error retrieving all MaterialClinicalDetails");
            throw;
        }
    }
}