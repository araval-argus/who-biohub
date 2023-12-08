using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Models.Repositories.MaterialClinicalDetails;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UseCases.MaterialClinicalDetails.DeleteMaterialClinicalDetail;

public interface IDeleteMaterialClinicalDetailHandler
{
    Task<Either<DeleteMaterialClinicalDetailCommandResponse, Errors>> Handle(DeleteMaterialClinicalDetailCommand command, CancellationToken cancellationToken);
}

public class DeleteMaterialClinicalDetailHandler : IDeleteMaterialClinicalDetailHandler
{
    private readonly ILogger<DeleteMaterialClinicalDetailHandler> _logger;
    private readonly DeleteMaterialClinicalDetailCommandValidator _validator;
    private readonly IMaterialClinicalDetailWriteRepository _writeRepository;

    public DeleteMaterialClinicalDetailHandler(
        ILogger<DeleteMaterialClinicalDetailHandler> logger,
        DeleteMaterialClinicalDetailCommandValidator validator,
        IMaterialClinicalDetailWriteRepository writeRepository
    )
    {
        _logger = logger;
        _validator = validator;
        _writeRepository = writeRepository;
    }

    public async Task<Either<DeleteMaterialClinicalDetailCommandResponse, Errors>> Handle(
        DeleteMaterialClinicalDetailCommand command,
        CancellationToken cancellationToken)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (!validationResult.IsValid)
            return new(new Errors(validationResult));

        try
        {
            Errors? errors = await _writeRepository.Delete(command.Id, cancellationToken);
            if (errors.HasValue)
                return new(errors.Value);

            return new(new DeleteMaterialClinicalDetailCommandResponse());
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error deleting the MaterialClinicalDetail with {id}", command.Id);
            throw;
        }
    }
}