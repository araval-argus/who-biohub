using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.TemperatureUnitOfMeasures;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UseCases.TemperatureUnitOfMeasures.CreateTemperatureUnitOfMeasure;

public interface ICreateTemperatureUnitOfMeasureHandler
{
    Task<Either<CreateTemperatureUnitOfMeasureCommandResponse, Errors>> Handle(CreateTemperatureUnitOfMeasureCommand command, CancellationToken cancellationToken);
}

public class CreateTemperatureUnitOfMeasureHandler : ICreateTemperatureUnitOfMeasureHandler
{
    private readonly ILogger<CreateTemperatureUnitOfMeasureHandler> _logger;
    private readonly CreateTemperatureUnitOfMeasureCommandValidator _validator;
    private readonly ICreateTemperatureUnitOfMeasureMapper _mapper;
    private readonly ITemperatureUnitOfMeasureWriteRepository _writeRepository;

    public CreateTemperatureUnitOfMeasureHandler(
        ILogger<CreateTemperatureUnitOfMeasureHandler> logger,
        CreateTemperatureUnitOfMeasureCommandValidator validator,
        ICreateTemperatureUnitOfMeasureMapper mapper,
        ITemperatureUnitOfMeasureWriteRepository writeRepository
    )
    {
        _logger = logger;
        _validator = validator;
        _mapper = mapper;
        _writeRepository = writeRepository;
    }

    public async Task<Either<CreateTemperatureUnitOfMeasureCommandResponse, Errors>> Handle(
        CreateTemperatureUnitOfMeasureCommand command,
        CancellationToken cancellationToken)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (!validationResult.IsValid)
            return new(new Errors(validationResult));

        TemperatureUnitOfMeasure temperatureunitofmeasure = _mapper.Map(command);

        try
        {
            Either<TemperatureUnitOfMeasure, Errors> response = await _writeRepository.Create(temperatureunitofmeasure, cancellationToken);
            if (response.IsRight)
                return new(response.Right);

            TemperatureUnitOfMeasure createdTemperatureUnitOfMeasure =
                response.Left ?? throw new Exception("This is a bug: temperatureunitofmeasure value must be defined");
            return new(new CreateTemperatureUnitOfMeasureCommandResponse(response.Left));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error creating a new TemperatureUnitOfMeasure");
            throw;
        }
    }
}