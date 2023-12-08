using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Models.Repositories.MaterialShippingInformations;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UseCases.MaterialShippingInformations.DeleteMaterialShippingInformation;

public interface IDeleteMaterialShippingInformationHandler
{
    Task<Either<DeleteMaterialShippingInformationCommandResponse, Errors>> Handle(DeleteMaterialShippingInformationCommand command, CancellationToken cancellationToken);
}

public class DeleteMaterialShippingInformationHandler : IDeleteMaterialShippingInformationHandler
{
    private readonly ILogger<DeleteMaterialShippingInformationHandler> _logger;
    private readonly DeleteMaterialShippingInformationCommandValidator _validator;
    private readonly IMaterialShippingInformationWriteRepository _writeRepository;

    public DeleteMaterialShippingInformationHandler(
        ILogger<DeleteMaterialShippingInformationHandler> logger,
        DeleteMaterialShippingInformationCommandValidator validator,
        IMaterialShippingInformationWriteRepository writeRepository
    )
    {
        _logger = logger;
        _validator = validator;
        _writeRepository = writeRepository;
    }

    public async Task<Either<DeleteMaterialShippingInformationCommandResponse, Errors>> Handle(
        DeleteMaterialShippingInformationCommand command,
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

            return new(new DeleteMaterialShippingInformationCommandResponse());
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error deleting the MaterialShippingInformation with {id}", command.Id);
            throw;
        }
    }
}