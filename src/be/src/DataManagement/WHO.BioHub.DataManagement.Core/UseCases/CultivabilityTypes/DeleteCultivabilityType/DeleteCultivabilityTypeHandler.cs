using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Models.Repositories.CultivabilityTypes;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UseCases.CultivabilityTypes.DeleteCultivabilityType;

public interface IDeleteCultivabilityTypeHandler
{
    Task<Either<DeleteCultivabilityTypeCommandResponse, Errors>> Handle(DeleteCultivabilityTypeCommand command, CancellationToken cancellationToken);
}

public class DeleteCultivabilityTypeHandler : IDeleteCultivabilityTypeHandler
{
    private readonly ILogger<DeleteCultivabilityTypeHandler> _logger;
    private readonly DeleteCultivabilityTypeCommandValidator _validator;
    private readonly ICultivabilityTypeWriteRepository _writeRepository;

    public DeleteCultivabilityTypeHandler(
        ILogger<DeleteCultivabilityTypeHandler> logger,
        DeleteCultivabilityTypeCommandValidator validator,
        ICultivabilityTypeWriteRepository writeRepository
    )
    {
        _logger = logger;
        _validator = validator;
        _writeRepository = writeRepository;
    }

    public async Task<Either<DeleteCultivabilityTypeCommandResponse, Errors>> Handle(
        DeleteCultivabilityTypeCommand command,
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

            return new(new DeleteCultivabilityTypeCommandResponse());
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error deleting the CultivabilityType with {id}", command.Id);
            throw;
        }
    }
}