using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.Users;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UseCases.Users.CreateUser;

public interface ICreateUserHandler
{
    Task<Either<CreateUserCommandResponse, Errors>> Handle(CreateUserCommand command, CancellationToken cancellationToken);
}

public class CreateUserHandler : ICreateUserHandler
{
    private readonly ILogger<CreateUserHandler> _logger;
    private readonly CreateUserCommandValidator _validator;
    private readonly ICreateUserMapper _mapper;
    private readonly IUserWriteRepository _writeRepository;

    public CreateUserHandler(
        ILogger<CreateUserHandler> logger,
        CreateUserCommandValidator validator,
        ICreateUserMapper mapper,
        IUserWriteRepository writeRepository
    )
    {
        _logger = logger;
        _validator = validator;
        _mapper = mapper;
        _writeRepository = writeRepository;
    }

    public async Task<Either<CreateUserCommandResponse, Errors>> Handle(
        CreateUserCommand command,
        CancellationToken cancellationToken)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (!validationResult.IsValid)
            return new(new Errors(validationResult));

        User user = _mapper.Map(command);

        try
        {
            Either<User, Errors> response = await _writeRepository.Create(user, cancellationToken);
            if (response.IsRight)
                return new(response.Right);

            User createdUser =
                response.Left ?? throw new Exception("This is a bug: user value must be defined");
            return new(new CreateUserCommandResponse(response.Left.Id));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error creating a new User");
            throw;
        }
    }
}