using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.UserRequests;
using WHO.BioHub.Models.Repositories.UserRequestStatuses;
using WHO.BioHub.Notifications;
using WHO.BioHub.Shared.Enums;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UseCases.UserRequests.ApproveOrRejectUserRequest;

public interface IApproveOrRejectUserRequestHandler
{
    Task<Either<ApproveOrRejectUserRequestCommandResponse, Errors>> Handle(ApproveOrRejectUserRequestCommand command, CancellationToken cancellationToken);
}

public class ApproveOrRejectUserRequestHandler : IApproveOrRejectUserRequestHandler
{
    private readonly ILogger<ApproveOrRejectUserRequestHandler> _logger;
    private readonly ApproveOrRejectUserRequestCommandValidator _validator;
    private readonly IApproveOrRejectUserRequestMapper _mapper;
    private readonly IUserRequestWriteRepository _writeRepository;
    private readonly IUserRequestStatusReadRepository _readUserRequestStatusRepository;
    private readonly ISendNotification _sendNotification;

    public ApproveOrRejectUserRequestHandler(
        ILogger<ApproveOrRejectUserRequestHandler> logger,
        ApproveOrRejectUserRequestCommandValidator validator,
        IApproveOrRejectUserRequestMapper mapper,
        IUserRequestWriteRepository writeRepository,
        IUserRequestStatusReadRepository readUserRequestStatusRepository,
        ISendNotification sendNotification
    )
    {
        _logger = logger;
        _validator = validator;
        _mapper = mapper;
        _writeRepository = writeRepository;
        _readUserRequestStatusRepository = readUserRequestStatusRepository;
        _sendNotification = sendNotification;
    }

    public async Task<Either<ApproveOrRejectUserRequestCommandResponse, Errors>> Handle(
        ApproveOrRejectUserRequestCommand command,
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

            try
            {

                var userRequestStatus = await _readUserRequestStatusRepository.ReadByStatus(command.Status, cancellationToken);

                if (userRequestStatus != null)
                {
                    var body = userRequest.Message;

                    var subject = userRequestStatus.Subject;

                    var toEmails = new List<string>() { userRequest.Email };

                    await _sendNotification.SendEmail(toEmails, Enumerable.Empty<string>(), Enumerable.Empty<string>(), body, "", subject);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error sending Email");
            }

            return new(new ApproveOrRejectUserRequestCommandResponse(userRequest));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error creating a new UserRequest");
            throw;
        }
    }
}