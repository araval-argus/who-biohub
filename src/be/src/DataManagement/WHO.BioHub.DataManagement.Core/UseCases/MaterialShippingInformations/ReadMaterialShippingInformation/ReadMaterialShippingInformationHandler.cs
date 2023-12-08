using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.MaterialShippingInformations;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UseCases.MaterialShippingInformations.ReadMaterialShippingInformation;

public interface IReadMaterialShippingInformationHandler
{
    Task<Either<ReadMaterialShippingInformationQueryResponse, Errors>> Handle(ReadMaterialShippingInformationQuery query, CancellationToken cancellationToken);
}

public class ReadMaterialShippingInformationHandler : IReadMaterialShippingInformationHandler
{
    private readonly ILogger<ReadMaterialShippingInformationHandler> _logger;
    private readonly ReadMaterialShippingInformationQueryValidator _validator;
    private readonly IMaterialShippingInformationReadRepository _readRepository;

    public ReadMaterialShippingInformationHandler(
        ILogger<ReadMaterialShippingInformationHandler> logger,
        ReadMaterialShippingInformationQueryValidator validator,
        IMaterialShippingInformationReadRepository readRepository)
    {
        _logger = logger;
        _validator = validator;
        _readRepository = readRepository;
    }

    public async Task<Either<ReadMaterialShippingInformationQueryResponse, Errors>> Handle(
        ReadMaterialShippingInformationQuery query,
        CancellationToken cancellationToken)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(query, cancellationToken);
        if (!validationResult.IsValid)
            return new(new Errors(validationResult));

        try
        {
            MaterialShippingInformation materialshippinginformation = await _readRepository.Read(query.Id, cancellationToken);
            if (materialshippinginformation == null)
                return new(new Errors(ErrorType.NotFound, $"MaterialShippingInformation with Id {query.Id} not found"));

            return new(new ReadMaterialShippingInformationQueryResponse(materialshippinginformation));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error reading MaterialShippingInformation with Id {id}", query.Id);
            throw;
        }
    }
}