using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.Roles;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UseCases.Roles.CreateRole;

public interface ICreateRoleHandler
{
    Task<Either<CreateRoleCommandResponse, Errors>> Handle(CreateRoleCommand command, CancellationToken cancellationToken);
}

public class CreateRoleHandler : ICreateRoleHandler
{
    private readonly ILogger<CreateRoleHandler> _logger;
    private readonly CreateRoleCommandValidator _validator;
    private readonly ICreateRoleMapper _mapper;
    private readonly IRoleWriteRepository _writeRepository;

    public CreateRoleHandler(
        ILogger<CreateRoleHandler> logger,
        CreateRoleCommandValidator validator,
        ICreateRoleMapper mapper,
        IRoleWriteRepository writeRepository
    )
    {
        _logger = logger;
        _validator = validator;
        _mapper = mapper;
        _writeRepository = writeRepository;
    }

    public async Task<Either<CreateRoleCommandResponse, Errors>> Handle(
        CreateRoleCommand command,
        CancellationToken cancellationToken)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (!validationResult.IsValid)
            return new(new Errors(validationResult));

        Role role = _mapper.Map(command);

        try
        {
            Either<Role, Errors> response = await _writeRepository.Create(role, cancellationToken);
            if (response.IsRight)
                return new(response.Right);

            Role createdRole =
                response.Left ?? throw new Exception("This is a bug: role value must be defined");
            return new(new CreateRoleCommandResponse(response.Left));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error creating a new Role");
            throw;
        }
    }
}