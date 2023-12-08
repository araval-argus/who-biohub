using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.Roles;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UseCases.Roles.UpdateRole;

public interface IUpdateRoleHandler
{
    Task<Either<UpdateRoleCommandResponse, Errors>> Handle(UpdateRoleCommand command, CancellationToken cancellationToken);
}

public class UpdateRoleHandler : IUpdateRoleHandler
{
    private readonly ILogger<UpdateRoleHandler> _logger;
    private readonly UpdateRoleCommandValidator _validator;
    private readonly IUpdateRoleMapper _mapper;
    private readonly IRoleWriteRepository _writeRepository;

    public UpdateRoleHandler(
        ILogger<UpdateRoleHandler> logger,
        UpdateRoleCommandValidator validator,
        IUpdateRoleMapper mapper,
        IRoleWriteRepository writeRepository
    )
    {
        _logger = logger;
        _validator = validator;
        _mapper = mapper;
        _writeRepository = writeRepository;
    }

    public async Task<Either<UpdateRoleCommandResponse, Errors>> Handle(
        UpdateRoleCommand command,
        CancellationToken cancellationToken)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (!validationResult.IsValid)
            return new(new Errors(validationResult));

        try
        {
            Role role = await _writeRepository.ReadForUpdate(command.Id, cancellationToken);
            role = _mapper.Map(role, command);

            Errors? errors = await _writeRepository.Update(role, cancellationToken);
            if (errors.HasValue)
                return new(errors.Value);

            return new(new UpdateRoleCommandResponse(role));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error creating a new Role");
            throw;
        }
    }
}