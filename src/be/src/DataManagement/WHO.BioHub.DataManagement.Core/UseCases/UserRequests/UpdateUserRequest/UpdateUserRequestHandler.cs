using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.UserRequests;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UseCases.UserRequests.UpdateUserRequest;

public interface IUpdateUserRequestHandler
{
    Task<Either<UpdateUserRequestCommandResponse, Errors>> Handle(UpdateUserRequestCommand command, CancellationToken cancellationToken);
}

public class UpdateUserRequestHandler : IUpdateUserRequestHandler
{
    private readonly ILogger<UpdateUserRequestHandler> _logger;
    private readonly UpdateUserRequestCommandValidator _validator;
    private readonly IUpdateUserRequestMapper _mapper;
    private readonly IUserRequestWriteRepository _writeRepository;

    public UpdateUserRequestHandler(
        ILogger<UpdateUserRequestHandler> logger,
        UpdateUserRequestCommandValidator validator,
        IUpdateUserRequestMapper mapper,
        IUserRequestWriteRepository writeRepository
    )
    {
        _logger = logger;
        _validator = validator;
        _mapper = mapper;
        _writeRepository = writeRepository;
    }

    public async Task<Either<UpdateUserRequestCommandResponse, Errors>> Handle(
        UpdateUserRequestCommand command,
        CancellationToken cancellationToken)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (!validationResult.IsValid)
            return new(new Errors(validationResult));

        try
        {
            UserRequest userRequest = await _writeRepository.ReadForUpdate(command.Id, cancellationToken);
            userRequest = _mapper.Map(userRequest, command);

            Errors? errors = await _writeRepository.Update(userRequest, cancellationToken);
            if (errors.HasValue)
                return new(errors.Value);

            return new(new UpdateUserRequestCommandResponse(userRequest));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error creating a new UserRequest");
            throw;
        }
    }
}