using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.UserRequests;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UseCases.UserRequests.CreateUserRequest;

public interface ICreateUserRequestHandler
{
    Task<Either<CreateUserRequestCommandResponse, Errors>> Handle(CreateUserRequestCommand command, CancellationToken cancellationToken);
}

public class CreateUserRequestHandler : ICreateUserRequestHandler
{
    private readonly ILogger<CreateUserRequestHandler> _logger;
    private readonly CreateUserRequestCommandValidator _validator;
    private readonly ICreateUserRequestMapper _mapper;
    private readonly IUserRequestWriteRepository _writeRepository;

    public CreateUserRequestHandler(
        ILogger<CreateUserRequestHandler> logger,
        CreateUserRequestCommandValidator validator,
        ICreateUserRequestMapper mapper,
        IUserRequestWriteRepository writeRepository
    )
    {
        _logger = logger;
        _validator = validator;
        _mapper = mapper;
        _writeRepository = writeRepository;
    }

    public async Task<Either<CreateUserRequestCommandResponse, Errors>> Handle(
        CreateUserRequestCommand command,
        CancellationToken cancellationToken)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (!validationResult.IsValid)
            return new(new Errors(validationResult));

        UserRequest userRequest = _mapper.Map(command);

        try
        {
            Either<UserRequest, Errors> response = await _writeRepository.Create(userRequest, cancellationToken);
            if (response.IsRight)
                return new(response.Right);

            UserRequest createdUserRequest =
                response.Left ?? throw new Exception("This is a bug: userRequest value must be defined");
            return new(new CreateUserRequestCommandResponse(response.Left));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error creating a new UserRequest");
            throw;
        }
    }
}