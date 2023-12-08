using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.MaterialClinicalDetails;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UseCases.MaterialClinicalDetails.ReadMaterialClinicalDetail;

public interface IReadMaterialClinicalDetailHandler
{
    Task<Either<ReadMaterialClinicalDetailQueryResponse, Errors>> Handle(ReadMaterialClinicalDetailQuery query, CancellationToken cancellationToken);
}

public class ReadMaterialClinicalDetailHandler : IReadMaterialClinicalDetailHandler
{
    private readonly ILogger<ReadMaterialClinicalDetailHandler> _logger;
    private readonly ReadMaterialClinicalDetailQueryValidator _validator;
    private readonly IMaterialClinicalDetailReadRepository _readRepository;

    public ReadMaterialClinicalDetailHandler(
        ILogger<ReadMaterialClinicalDetailHandler> logger,
        ReadMaterialClinicalDetailQueryValidator validator,
        IMaterialClinicalDetailReadRepository readRepository)
    {
        _logger = logger;
        _validator = validator;
        _readRepository = readRepository;
    }

    public async Task<Either<ReadMaterialClinicalDetailQueryResponse, Errors>> Handle(
        ReadMaterialClinicalDetailQuery query,
        CancellationToken cancellationToken)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(query, cancellationToken);
        if (!validationResult.IsValid)
            return new(new Errors(validationResult));

        try
        {
            MaterialClinicalDetail materialclinicaldetail = await _readRepository.Read(query.Id, cancellationToken);
            if (materialclinicaldetail == null)
                return new(new Errors(ErrorType.NotFound, $"MaterialClinicalDetail with Id {query.Id} not found"));

            return new(new ReadMaterialClinicalDetailQueryResponse(materialclinicaldetail));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error reading MaterialClinicalDetail with Id {id}", query.Id);
            throw;
        }
    }
}