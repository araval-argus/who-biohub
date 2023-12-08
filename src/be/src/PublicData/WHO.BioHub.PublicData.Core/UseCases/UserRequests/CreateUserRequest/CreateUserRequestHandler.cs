using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Shared.Utils;
using WHO.BioHub.Public.SQL.Abstractions;
using WHO.BioHub.PublicData.SQL.Abstractions;
using WHO.BioHub.Shared.Enums;
using WHO.BioHub.Notifications;

namespace WHO.BioHub.PublicData.Core.UseCases.UserRequests.CreateUserRequest;

public interface ICreateUserRequestHandler
{
    Task<Either<CreateUserRequestCommandResponse, Errors>> Handle(CreateUserRequestCommand command, CancellationToken cancellationToken);
}

public class CreateUserRequestHandler : ICreateUserRequestHandler
{
    private readonly ILogger<CreateUserRequestHandler> _logger;
    private readonly CreateUserRequestCommandValidator _validator;
    private readonly ICreateUserRequestMapper _mapper;
    private readonly IUserRequestPublicWriteRepository _writeRepository;
    private readonly IUserPublicReadRepository _readUserRepository;
    private readonly IUserRequestStatusPublicReadRepository _readUserRequestStatusRepository;

    private readonly ISendNotification _sendNotification;

    public CreateUserRequestHandler(
        ILogger<CreateUserRequestHandler> logger,
        CreateUserRequestCommandValidator validator,
        ICreateUserRequestMapper mapper,
        IUserRequestPublicWriteRepository writeRepository,
        IUserPublicReadRepository readUserRepository,
        IUserRequestStatusPublicReadRepository readUserRequestStatusRepository,
        ISendNotification sendNotification
    )
    {
        _logger = logger;
        _validator = validator;
        _mapper = mapper;
        _writeRepository = writeRepository;
        _readUserRepository = readUserRepository;
        _readUserRequestStatusRepository = readUserRequestStatusRepository;
        _sendNotification = sendNotification;
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

            try
            {
                var userRequestStatus = await _readUserRequestStatusRepository.ReadByStatus(UserRegistrationStatus.Pending, cancellationToken);

                if (userRequestStatus != null)
                {
                    var usersToBeNotified = await _readUserRepository.ListUsersForRequestAccessEmail(cancellationToken);

                    if (usersToBeNotified != null && usersToBeNotified.Any())
                    {

                        var body = userRequestStatus.Message;
                        body = body.Replace("{firstname}", userRequest.FirstName);
                        body = body.Replace("{lastname}", userRequest.LastName);
                        body = body.Replace("{email}", userRequest.Email);
                        body = body.Replace("{instituteName}", userRequest.InstituteName);
                        var subject = userRequestStatus.Subject;


                        var toEmails = usersToBeNotified.Select(x => x.Email).ToList();


                        await _sendNotification.SendEmail(toEmails, Enumerable.Empty<string>(), Enumerable.Empty<string>(), body, "", subject);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error sending Email");
            }



            return new(new CreateUserRequestCommandResponse(response.Left.Id));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error creating a new UserRequest");
            throw;
        }
    }
}