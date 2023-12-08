using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Models.Repositories.Roles;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UseCases.Roles.DeleteRole;

public interface IDeleteRoleHandler
{
    Task<Either<DeleteRoleCommandResponse, Errors>> Handle(DeleteRoleCommand command, CancellationToken cancellationToken);
}

public class DeleteRoleHandler : IDeleteRoleHandler
{
    private readonly ILogger<DeleteRoleHandler> _logger;
    private readonly DeleteRoleCommandValidator _validator;
    private readonly IRoleWriteRepository _writeRepository;

    public DeleteRoleHandler(
        ILogger<DeleteRoleHandler> logger,
        DeleteRoleCommandValidator validator,
        IRoleWriteRepository writeRepository
    )
    {
        _logger = logger;
        _validator = validator;
        _writeRepository = writeRepository;
    }

    public async Task<Either<DeleteRoleCommandResponse, Errors>> Handle(
        DeleteRoleCommand command,
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

            return new(new DeleteRoleCommandResponse());
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error deleting the Role with {id}", command.Id);
            throw;
        }
    }
}