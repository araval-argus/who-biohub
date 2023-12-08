using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.TransportModes;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UseCases.TransportModes.CreateTransportMode;

public interface ICreateTransportModeHandler
{
    Task<Either<CreateTransportModeCommandResponse, Errors>> Handle(CreateTransportModeCommand command, CancellationToken cancellationToken);
}

public class CreateTransportModeHandler : ICreateTransportModeHandler
{
    private readonly ILogger<CreateTransportModeHandler> _logger;
    private readonly CreateTransportModeCommandValidator _validator;
    private readonly ICreateTransportModeMapper _mapper;
    private readonly ITransportModeWriteRepository _writeRepository;

    public CreateTransportModeHandler(
        ILogger<CreateTransportModeHandler> logger,
        CreateTransportModeCommandValidator validator,
        ICreateTransportModeMapper mapper,
        ITransportModeWriteRepository writeRepository
    )
    {
        _logger = logger;
        _validator = validator;
        _mapper = mapper;
        _writeRepository = writeRepository;
    }

    public async Task<Either<CreateTransportModeCommandResponse, Errors>> Handle(
        CreateTransportModeCommand command,
        CancellationToken cancellationToken)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (!validationResult.IsValid)
            return new(new Errors(validationResult));

        TransportMode transportmode = _mapper.Map(command);

        try
        {
            Either<TransportMode, Errors> response = await _writeRepository.Create(transportmode, cancellationToken);
            if (response.IsRight)
                return new(response.Right);

            TransportMode createdTransportMode =
                response.Left ?? throw new Exception("This is a bug: transportmode value must be defined");
            return new(new CreateTransportModeCommandResponse(response.Left));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error creating a new TransportMode");
            throw;
        }
    }
}