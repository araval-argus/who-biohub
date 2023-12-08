using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.Users;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UseCases.Users.CreateUserFromUserRequest;

public interface ICreateUserFromUserRequestHandler
{
    Task<Either<CreateUserFromUserRequestCommandResponse, Errors>> Handle(CreateUserFromUserRequestCommand command, CancellationToken cancellationToken);
}

public class CreateUserFromUserRequestHandler : ICreateUserFromUserRequestHandler
{
    private readonly ILogger<CreateUserFromUserRequestHandler> _logger;
    private readonly CreateUserFromUserRequestCommandValidator _validator;
    private readonly ICreateUserFromUserRequestMapper _mapper;
    private readonly IUserWriteRepository _writeRepository;

    public CreateUserFromUserRequestHandler(
        ILogger<CreateUserFromUserRequestHandler> logger,
        CreateUserFromUserRequestCommandValidator validator,
        ICreateUserFromUserRequestMapper mapper,
        IUserWriteRepository writeRepository
    )
    {
        _logger = logger;
        _validator = validator;
        _mapper = mapper;
        _writeRepository = writeRepository;
    }

    public async Task<Either<CreateUserFromUserRequestCommandResponse, Errors>> Handle(
        CreateUserFromUserRequestCommand command,
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
            return new(new CreateUserFromUserRequestCommandResponse(response.Left.Id));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error creating a new User");
            throw;
        }
    }
}