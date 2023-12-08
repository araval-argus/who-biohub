using FluentValidation.Results;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.UserRequestStatuses;
using WHO.BioHub.DataManagement.Core.UseCases.UserRequestStatuses.UpdateUserRequestStatus;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UnitTests.UseCases.UserRequestStatuses.UpdateUserRequestStatus;

public class UpdateUserRequestStatusHandlerUnitTests
{
    [Fact]
    public async Task If_resource_is_updated_then_a_valid_response_is_returned()
    {
        // Arrange
        UpdateUserRequestStatusCommandValidator validatorMock = Substitute.For<UpdateUserRequestStatusCommandValidator>();
        ILogger<UpdateUserRequestStatusHandler> loggerMock = Substitute.For<ILogger<UpdateUserRequestStatusHandler>>();
        IUserRequestStatusWriteRepository repositoryMock = Substitute.For<IUserRequestStatusWriteRepository>();
        IUpdateUserRequestStatusMapper mapperMock = Substitute.For<IUpdateUserRequestStatusMapper>();
        CancellationToken cancellationToken = default;

        UpdateUserRequestStatusHandler handler = new(loggerMock, validatorMock, mapperMock, repositoryMock);
        UpdateUserRequestStatusCommand cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult());

        UserRequestStatus userRequestStatus = new() { Id = Guid.NewGuid() };
        UserRequestStatus userrequeststatusMapped = new() { Id = userRequestStatus.Id };

        mapperMock.Map(userRequestStatus, cmd).ReturnsForAnyArgs(userrequeststatusMapped);

        repositoryMock
            .Update(userRequestStatus, cancellationToken)
            .ReturnsForAnyArgs(Task.FromResult<Errors?>(null));

        // Act
        Either<UpdateUserRequestStatusCommandResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

        // Assert
        response.Should()
            .NotBeNull(because: "A response is expected in this scenario");
        response.IsLeft.Should()
            .BeTrue(because: "Response is expected in this scenario");
        response.Left.Should()
            .NotBeNull(because: "Response should NOT be null");
        response.Left.UserRequestStatus.Should()
            .NotBeNull(because: "UserRequestStatus should NOT be null");
        response.Left.UserRequestStatus.Should()
            .BeEquivalentTo(userrequeststatusMapped, because: "Returned userRequestStatus must mach the one provided in request");

        Received.InOrder(async () =>
        {
            await validatorMock.ValidateAsync(Arg.Any<UpdateUserRequestStatusCommand>(), cancellationToken).Received(1);
            mapperMock.Map(userRequestStatus, Arg.Any<UpdateUserRequestStatusCommand>()).Received(1);
            await repositoryMock.Update(Arg.Any<UserRequestStatus>(), cancellationToken).Received(1);
        });
    }

    [Fact]
    public async Task If_validation_fails_then_errors_must_be_returned()
    {
        // Arrange
        UpdateUserRequestStatusCommandValidator validatorMock = Substitute.For<UpdateUserRequestStatusCommandValidator>();
        ILogger<UpdateUserRequestStatusHandler> loggerMock = Substitute.For<ILogger<UpdateUserRequestStatusHandler>>();
        IUserRequestStatusWriteRepository repositoryMock = Substitute.For<IUserRequestStatusWriteRepository>();
        IUpdateUserRequestStatusMapper mapperMock = Substitute.For<IUpdateUserRequestStatusMapper>();
        CancellationToken cancellationToken = default;

        UpdateUserRequestStatusHandler handler = new(loggerMock, validatorMock, mapperMock, repositoryMock);
        UpdateUserRequestStatusCommand cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult(new ValidationFailure[1] { new("test", "error message") }));

        // Act
        Either<UpdateUserRequestStatusCommandResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

        // Assert
        response.Should()
            .NotBeNull(because: "A response is expected in this scenario");
        response.IsRight.Should()
            .BeTrue(because: "Errors are expected in this scenario");
        response.Right.Should()
            .NotBeNull(because: "If errors are returned, errors should NOT be null");
        response.Right.Messages.Should()
            .NotBeNullOrEmpty(because: "If errors are returned, at least one message must be defined");
        response.Right.ErrorType.Should()
            .Be(ErrorType.Validation, because: "Validation Errors are expected in this scenario");

        Received.InOrder(async () =>
        {
            await validatorMock.ValidateAsync(Arg.Any<UpdateUserRequestStatusCommand>(), cancellationToken).Received(1);
            mapperMock.Map(Arg.Any<UserRequestStatus>(), Arg.Any<UpdateUserRequestStatusCommand>()).Received(0);
            await repositoryMock.Update(Arg.Any<UserRequestStatus>(), cancellationToken).Received(0);
        });
    }

    [Fact]
    public async Task If_errors_are_returned_from_repository_then_they_must_be_forwarded_from_handler()
    {
        // Arrange
        UpdateUserRequestStatusCommandValidator validatorMock = Substitute.For<UpdateUserRequestStatusCommandValidator>();
        ILogger<UpdateUserRequestStatusHandler> loggerMock = Substitute.For<ILogger<UpdateUserRequestStatusHandler>>();
        IUserRequestStatusWriteRepository repositoryMock = Substitute.For<IUserRequestStatusWriteRepository>();
        IUpdateUserRequestStatusMapper mapperMock = Substitute.For<IUpdateUserRequestStatusMapper>();
        CancellationToken cancellationToken = default;

        UpdateUserRequestStatusHandler handler = new(loggerMock, validatorMock, mapperMock, repositoryMock);
        UpdateUserRequestStatusCommand cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult());

        UserRequestStatus userRequestStatus = new();
        UserRequestStatus userrequeststatusMapped = new();
        mapperMock.Map(userRequestStatus, cmd).ReturnsForAnyArgs(userrequeststatusMapped);

        // TODO: change error type
        repositoryMock
            .Update(userRequestStatus, cancellationToken)
            .ReturnsForAnyArgs(Task.FromResult<Errors?>(
                new Errors(ErrorType.Validation, "I should be changed to a persistence error type")));

        // Act
        Either<UpdateUserRequestStatusCommandResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

        // Assert
        response.Should()
            .NotBeNull(because: "A response is expected in this scenario");
        response.IsRight.Should()
            .BeTrue(because: "Errors are expected in this scenario");
        response.Right.Should()
            .NotBeNull(because: "If errors are returned, errors should NOT be null");
        response.Right.Messages.Should()
            .NotBeNullOrEmpty(because: "If errors are returned, at least one message must be defined");
        response.Right.ErrorType.Should()
            .Be(ErrorType.Validation, because: "Validation Errors are expected in this scenario");

        Received.InOrder(async () =>
        {
            await validatorMock.ValidateAsync(Arg.Any<UpdateUserRequestStatusCommand>(), cancellationToken).Received(1);
            mapperMock.Map(Arg.Any<UserRequestStatus>(), Arg.Any<UpdateUserRequestStatusCommand>()).Received(1);
            await repositoryMock.Update(Arg.Any<UserRequestStatus>(), cancellationToken).Received(1);
        });
    }
}