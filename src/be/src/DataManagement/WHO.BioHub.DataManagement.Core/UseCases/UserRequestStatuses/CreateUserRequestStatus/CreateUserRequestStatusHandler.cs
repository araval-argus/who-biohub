using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.UserRequestStatuses;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UseCases.UserRequestStatuses.CreateUserRequestStatus;

public interface ICreateUserRequestStatusHandler
{
    Task<Either<CreateUserRequestStatusCommandResponse, Errors>> Handle(CreateUserRequestStatusCommand command, CancellationToken cancellationToken);
}

public class CreateUserRequestStatusHandler : ICreateUserRequestStatusHandler
{
    private readonly ILogger<CreateUserRequestStatusHandler> _logger;
    private readonly CreateUserRequestStatusCommandValidator _validator;
    private readonly ICreateUserRequestStatusMapper _mapper;
    private readonly IUserRequestStatusWriteRepository _writeRepository;

    public CreateUserRequestStatusHandler(
        ILogger<CreateUserRequestStatusHandler> logger,
        CreateUserRequestStatusCommandValidator validator,
        ICreateUserRequestStatusMapper mapper,
        IUserRequestStatusWriteRepository writeRepository
    )
    {
        _logger = logger;
        _validator = validator;
        _mapper = mapper;
        _writeRepository = writeRepository;
    }

    public async Task<Either<CreateUserRequestStatusCommandResponse, Errors>> Handle(
        CreateUserRequestStatusCommand command,
        CancellationToken cancellationToken)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (!validationResult.IsValid)
            return new(new Errors(validationResult));

        UserRequestStatus userRequestStatus = _mapper.Map(command);

        try
        {
            Either<UserRequestStatus, Errors> response = await _writeRepository.Create(userRequestStatus, cancellationToken);
            if (response.IsRight)
                return new(response.Right);

            UserRequestStatus createdUserRequestStatus =
                response.Left ?? throw new Exception("This is a bug: UserRequestStatus value must be defined");
            return new(new CreateUserRequestStatusCommandResponse(response.Left));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error creating a new UserRequestStatus");
            throw;
        }
    }
}