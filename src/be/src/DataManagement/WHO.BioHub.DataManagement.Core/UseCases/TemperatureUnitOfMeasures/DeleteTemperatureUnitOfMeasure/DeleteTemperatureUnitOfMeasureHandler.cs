using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Models.Repositories.TemperatureUnitOfMeasures;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UseCases.TemperatureUnitOfMeasures.DeleteTemperatureUnitOfMeasure;

public interface IDeleteTemperatureUnitOfMeasureHandler
{
    Task<Either<DeleteTemperatureUnitOfMeasureCommandResponse, Errors>> Handle(DeleteTemperatureUnitOfMeasureCommand command, CancellationToken cancellationToken);
}

public class DeleteTemperatureUnitOfMeasureHandler : IDeleteTemperatureUnitOfMeasureHandler
{
    private readonly ILogger<DeleteTemperatureUnitOfMeasureHandler> _logger;
    private readonly DeleteTemperatureUnitOfMeasureCommandValidator _validator;
    private readonly ITemperatureUnitOfMeasureWriteRepository _writeRepository;

    public DeleteTemperatureUnitOfMeasureHandler(
        ILogger<DeleteTemperatureUnitOfMeasureHandler> logger,
        DeleteTemperatureUnitOfMeasureCommandValidator validator,
        ITemperatureUnitOfMeasureWriteRepository writeRepository
    )
    {
        _logger = logger;
        _validator = validator;
        _writeRepository = writeRepository;
    }

    public async Task<Either<DeleteTemperatureUnitOfMeasureCommandResponse, Errors>> Handle(
        DeleteTemperatureUnitOfMeasureCommand command,
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

            return new(new DeleteTemperatureUnitOfMeasureCommandResponse());
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error deleting the TemperatureUnitOfMeasure with {id}", command.Id);
            throw;
        }
    }
}