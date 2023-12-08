using FluentValidation.Results;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.Users;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UseCases.Users.DeleteUser;

public interface IDeleteUserHandler
{
    Task<Either<DeleteUserCommandResponse, Errors>> Handle(DeleteUserCommand command, CancellationToken cancellationToken);
}

public class DeleteUserHandler : IDeleteUserHandler
{
    private readonly ILogger<DeleteUserHandler> _logger;
    private readonly DeleteUserCommandValidator _validator;
    private readonly IUserWriteRepository _writeRepository;

    public DeleteUserHandler(
        ILogger<DeleteUserHandler> logger,
        DeleteUserCommandValidator validator,
        IUserWriteRepository writeRepository
    )
    {
        _logger = logger;
        _validator = validator;
        _writeRepository = writeRepository;
    }

    public async Task<Either<DeleteUserCommandResponse, Errors>> Handle(
        DeleteUserCommand command,
        CancellationToken cancellationToken)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (!validationResult.IsValid)
            return new(new Errors(validationResult));

        IDbContextTransaction transaction = null;

        try
        {
            User user = await _writeRepository.ReadForUpdate(command.Id, cancellationToken);

            if (user == null)
            {
                return new(new Errors(ErrorType.NotFound, $"User with Id {command.Id} not found"));
            }

            transaction = await _writeRepository.BeginTransactionAsync();

            await _writeRepository.CreateUserHistoryItem(user, cancellationToken, transaction);
            user.DeletedOn = DateTime.UtcNow;
            user.LastOperationDate = user.DeletedOn;
            user.LastOperationByUserId = command.OperationById;

            Errors? errors = await _writeRepository.Update(user, cancellationToken, transaction);
            if (errors.HasValue)
            {
                await Rollback(transaction);
                return new(errors.Value);
            }

            await transaction.CommitAsync();
            await transaction.DisposeAsync();

            return new(new DeleteUserCommandResponse());
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error deleting the User with {id}", command.Id);
            await Rollback(transaction);
            throw;
        }
    }

    private async Task Rollback(IDbContextTransaction transaction)
    {
        if (transaction != null)
        {
            await transaction.RollbackAsync();
            await transaction.DisposeAsync();
        }
    }
}