using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.TransportModes;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UseCases.TransportModes.UpdateTransportMode;

public interface IUpdateTransportModeHandler
{
    Task<Either<UpdateTransportModeCommandResponse, Errors>> Handle(UpdateTransportModeCommand command, CancellationToken cancellationToken);
}

public class UpdateTransportModeHandler : IUpdateTransportModeHandler
{
    private readonly ILogger<UpdateTransportModeHandler> _logger;
    private readonly UpdateTransportModeCommandValidator _validator;
    private readonly IUpdateTransportModeMapper _mapper;
    private readonly ITransportModeWriteRepository _writeRepository;

    public UpdateTransportModeHandler(
        ILogger<UpdateTransportModeHandler> logger,
        UpdateTransportModeCommandValidator validator,
        IUpdateTransportModeMapper mapper,
        ITransportModeWriteRepository writeRepository
    )
    {
        _logger = logger;
        _validator = validator;
        _mapper = mapper;
        _writeRepository = writeRepository;
    }

    public async Task<Either<UpdateTransportModeCommandResponse, Errors>> Handle(
        UpdateTransportModeCommand command,
        CancellationToken cancellationToken)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (!validationResult.IsValid)
            return new(new Errors(validationResult));

        try
        {
            TransportMode transportmode = await _writeRepository.ReadForUpdate(command.Id, cancellationToken);
            transportmode = _mapper.Map(transportmode, command);

            Errors? errors = await _writeRepository.Update(transportmode, cancellationToken);
            if (errors.HasValue)
                return new(errors.Value);

            return new(new UpdateTransportModeCommandResponse(transportmode));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error creating a new TransportMode");
            throw;
        }
    }
}