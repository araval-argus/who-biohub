using FluentValidation.Results;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.Users;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UseCases.Users.UpdateUser;

public interface IUpdateUserHandler
{
    Task<Either<UpdateUserCommandResponse, Errors>> Handle(UpdateUserCommand command, CancellationToken cancellationToken);
}

public class UpdateUserHandler : IUpdateUserHandler
{
    private readonly ILogger<UpdateUserHandler> _logger;
    private readonly UpdateUserCommandValidator _validator;
    private readonly IUpdateUserMapper _mapper;
    private readonly IUserWriteRepository _writeRepository;

    public UpdateUserHandler(
        ILogger<UpdateUserHandler> logger,
        UpdateUserCommandValidator validator,
        IUpdateUserMapper mapper,
        IUserWriteRepository writeRepository
    )
    {
        _logger = logger;
        _validator = validator;
        _mapper = mapper;
        _writeRepository = writeRepository;
    }

    public async Task<Either<UpdateUserCommandResponse, Errors>> Handle(
        UpdateUserCommand command,
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

            user = _mapper.Map(user, command);

            Errors? errors = await _writeRepository.Update(user, cancellationToken, transaction);
            if (errors.HasValue)
            {
                await Rollback(transaction);
                return new(errors.Value);
            }

            await transaction.CommitAsync();
            await transaction.DisposeAsync();

            return new(new UpdateUserCommandResponse(user.Id));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error creating a new User");
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