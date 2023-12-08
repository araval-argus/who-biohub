using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Models.Repositories.MaterialClinicalDetailsHistory;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UseCases.MaterialClinicalDetailsHistory.DeleteMaterialClinicalDetailHistory;

public interface IDeleteMaterialClinicalDetailHistoryHandler
{
    Task<Either<DeleteMaterialClinicalDetailHistoryCommandResponse, Errors>> Handle(DeleteMaterialClinicalDetailHistoryCommand command, CancellationToken cancellationToken);
}

public class DeleteMaterialClinicalDetailHistoryHandler : IDeleteMaterialClinicalDetailHistoryHandler
{
    private readonly ILogger<DeleteMaterialClinicalDetailHistoryHandler> _logger;
    private readonly DeleteMaterialClinicalDetailHistoryCommandValidator _validator;
    private readonly IMaterialClinicalDetailHistoryWriteRepository _writeRepository;

    public DeleteMaterialClinicalDetailHistoryHandler(
        ILogger<DeleteMaterialClinicalDetailHistoryHandler> logger,
        DeleteMaterialClinicalDetailHistoryCommandValidator validator,
        IMaterialClinicalDetailHistoryWriteRepository writeRepository
    )
    {
        _logger = logger;
        _validator = validator;
        _writeRepository = writeRepository;
    }

    public async Task<Either<DeleteMaterialClinicalDetailHistoryCommandResponse, Errors>> Handle(
        DeleteMaterialClinicalDetailHistoryCommand command,
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

            return new(new DeleteMaterialClinicalDetailHistoryCommandResponse());
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error deleting the MaterialClinicalDetailHistory with {id}", command.Id);
            throw;
        }
    }
}