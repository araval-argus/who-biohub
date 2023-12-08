using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Models.Repositories.UserRequests;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UseCases.UserRequests.DeleteUserRequest;

public interface IDeleteUserRequestHandler
{
    Task<Either<DeleteUserRequestCommandResponse, Errors>> Handle(DeleteUserRequestCommand command, CancellationToken cancellationToken);
}

public class DeleteUserRequestHandler : IDeleteUserRequestHandler
{
    private readonly ILogger<DeleteUserRequestHandler> _logger;
    private readonly DeleteUserRequestCommandValidator _validator;
    private readonly IUserRequestWriteRepository _writeRepository;

    public DeleteUserRequestHandler(
        ILogger<DeleteUserRequestHandler> logger,
        DeleteUserRequestCommandValidator validator,
        IUserRequestWriteRepository writeRepository
    )
    {
        _logger = logger;
        _validator = validator;
        _writeRepository = writeRepository;
    }

    public async Task<Either<DeleteUserRequestCommandResponse, Errors>> Handle(
        DeleteUserRequestCommand command,
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

            return new(new DeleteUserRequestCommandResponse());
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error deleting the UserRequest with {id}", command.Id);
            throw;
        }
    }
}