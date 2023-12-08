using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Models.Repositories.UserRequestStatuses;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UseCases.UserRequestStatuses.DeleteUserRequestStatus;

public interface IDeleteUserRequestStatusHandler
{
    Task<Either<DeleteUserRequestStatusCommandResponse, Errors>> Handle(DeleteUserRequestStatusCommand command, CancellationToken cancellationToken);
}

public class DeleteUserRequestStatusHandler : IDeleteUserRequestStatusHandler
{
    private readonly ILogger<DeleteUserRequestStatusHandler> _logger;
    private readonly DeleteUserRequestStatusCommandValidator _validator;
    private readonly IUserRequestStatusWriteRepository _writeRepository;

    public DeleteUserRequestStatusHandler(
        ILogger<DeleteUserRequestStatusHandler> logger,
        DeleteUserRequestStatusCommandValidator validator,
        IUserRequestStatusWriteRepository writeRepository
    )
    {
        _logger = logger;
        _validator = validator;
        _writeRepository = writeRepository;
    }

    public async Task<Either<DeleteUserRequestStatusCommandResponse, Errors>> Handle(
        DeleteUserRequestStatusCommand command,
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

            return new(new DeleteUserRequestStatusCommandResponse());
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error deleting the UserRequestStatus with {id}", command.Id);
            throw;
        }
    }
}