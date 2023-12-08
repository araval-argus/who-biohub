using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.UserRequestStatuses;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UseCases.UserRequestStatuses.UpdateUserRequestStatus;

public interface IUpdateUserRequestStatusHandler
{
    Task<Either<UpdateUserRequestStatusCommandResponse, Errors>> Handle(UpdateUserRequestStatusCommand command, CancellationToken cancellationToken);
}

public class UpdateUserRequestStatusHandler : IUpdateUserRequestStatusHandler
{
    private readonly ILogger<UpdateUserRequestStatusHandler> _logger;
    private readonly UpdateUserRequestStatusCommandValidator _validator;
    private readonly IUpdateUserRequestStatusMapper _mapper;
    private readonly IUserRequestStatusWriteRepository _writeRepository;

    public UpdateUserRequestStatusHandler(
        ILogger<UpdateUserRequestStatusHandler> logger,
        UpdateUserRequestStatusCommandValidator validator,
        IUpdateUserRequestStatusMapper mapper,
        IUserRequestStatusWriteRepository writeRepository
    )
    {
        _logger = logger;
        _validator = validator;
        _mapper = mapper;
        _writeRepository = writeRepository;
    }

    public async Task<Either<UpdateUserRequestStatusCommandResponse, Errors>> Handle(
        UpdateUserRequestStatusCommand command,
        CancellationToken cancellationToken)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (!validationResult.IsValid)
            return new(new Errors(validationResult));

        try
        {
            UserRequestStatus userRequestStatus = await _writeRepository.ReadForUpdate(command.Id, cancellationToken);
            userRequestStatus = _mapper.Map(userRequestStatus, command);

            Errors? errors = await _writeRepository.Update(userRequestStatus, cancellationToken);
            if (errors.HasValue)
                return new(errors.Value);

            return new(new UpdateUserRequestStatusCommandResponse(userRequestStatus));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error creating a new UserRequestStatus");
            throw;
        }
    }
}