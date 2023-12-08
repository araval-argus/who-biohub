using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Models.Repositories.MaterialShippingInformationsHistory;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UseCases.MaterialShippingInformationsHistory.DeleteMaterialShippingInformationHistory;

public interface IDeleteMaterialShippingInformationHistoryHandler
{
    Task<Either<DeleteMaterialShippingInformationHistoryCommandResponse, Errors>> Handle(DeleteMaterialShippingInformationHistoryCommand command, CancellationToken cancellationToken);
}

public class DeleteMaterialShippingInformationHistoryHandler : IDeleteMaterialShippingInformationHistoryHandler
{
    private readonly ILogger<DeleteMaterialShippingInformationHistoryHandler> _logger;
    private readonly DeleteMaterialShippingInformationHistoryCommandValidator _validator;
    private readonly IMaterialShippingInformationHistoryWriteRepository _writeRepository;

    public DeleteMaterialShippingInformationHistoryHandler(
        ILogger<DeleteMaterialShippingInformationHistoryHandler> logger,
        DeleteMaterialShippingInformationHistoryCommandValidator validator,
        IMaterialShippingInformationHistoryWriteRepository writeRepository
    )
    {
        _logger = logger;
        _validator = validator;
        _writeRepository = writeRepository;
    }

    public async Task<Either<DeleteMaterialShippingInformationHistoryCommandResponse, Errors>> Handle(
        DeleteMaterialShippingInformationHistoryCommand command,
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

            return new(new DeleteMaterialShippingInformationHistoryCommandResponse());
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error deleting the MaterialShippingInformationHistory with {id}", command.Id);
            throw;
        }
    }
}