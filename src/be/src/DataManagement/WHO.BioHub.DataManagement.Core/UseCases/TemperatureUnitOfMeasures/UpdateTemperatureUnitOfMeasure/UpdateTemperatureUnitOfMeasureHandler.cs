using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.TemperatureUnitOfMeasures;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UseCases.TemperatureUnitOfMeasures.UpdateTemperatureUnitOfMeasure;

public interface IUpdateTemperatureUnitOfMeasureHandler
{
    Task<Either<UpdateTemperatureUnitOfMeasureCommandResponse, Errors>> Handle(UpdateTemperatureUnitOfMeasureCommand command, CancellationToken cancellationToken);
}

public class UpdateTemperatureUnitOfMeasureHandler : IUpdateTemperatureUnitOfMeasureHandler
{
    private readonly ILogger<UpdateTemperatureUnitOfMeasureHandler> _logger;
    private readonly UpdateTemperatureUnitOfMeasureCommandValidator _validator;
    private readonly IUpdateTemperatureUnitOfMeasureMapper _mapper;
    private readonly ITemperatureUnitOfMeasureWriteRepository _writeRepository;

    public UpdateTemperatureUnitOfMeasureHandler(
        ILogger<UpdateTemperatureUnitOfMeasureHandler> logger,
        UpdateTemperatureUnitOfMeasureCommandValidator validator,
        IUpdateTemperatureUnitOfMeasureMapper mapper,
        ITemperatureUnitOfMeasureWriteRepository writeRepository
    )
    {
        _logger = logger;
        _validator = validator;
        _mapper = mapper;
        _writeRepository = writeRepository;
    }

    public async Task<Either<UpdateTemperatureUnitOfMeasureCommandResponse, Errors>> Handle(
        UpdateTemperatureUnitOfMeasureCommand command,
        CancellationToken cancellationToken)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (!validationResult.IsValid)
            return new(new Errors(validationResult));

        try
        {
            TemperatureUnitOfMeasure temperatureunitofmeasure = await _writeRepository.ReadForUpdate(command.Id, cancellationToken);
            temperatureunitofmeasure = _mapper.Map(temperatureunitofmeasure, command);

            Errors? errors = await _writeRepository.Update(temperatureunitofmeasure, cancellationToken);
            if (errors.HasValue)
                return new(errors.Value);

            return new(new UpdateTemperatureUnitOfMeasureCommandResponse(temperatureunitofmeasure));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error creating a new TemperatureUnitOfMeasure");
            throw;
        }
    }
}