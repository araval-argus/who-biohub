using FluentValidation.Results;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.UserRequests;
using WHO.BioHub.DataManagement.Core.UseCases.UserRequests.UpdateUserRequest;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UnitTests.UseCases.UserRequests.UpdateUserRequest;

public class UpdateUserRequestHandlerUnitTests
{
    [Fact]
    public async Task If_resource_is_updated_then_a_valid_response_is_returned()
    {
        // Arrange
        UpdateUserRequestCommandValidator validatorMock = Substitute.For<UpdateUserRequestCommandValidator>();
        ILogger<UpdateUserRequestHandler> loggerMock = Substitute.For<ILogger<UpdateUserRequestHandler>>();
        IUserRequestWriteRepository repositoryMock = Substitute.For<IUserRequestWriteRepository>();
        IUpdateUserRequestMapper mapperMock = Substitute.For<IUpdateUserRequestMapper>();
        CancellationToken cancellationToken = default;

        UpdateUserRequestHandler handler = new(loggerMock, validatorMock, mapperMock, repositoryMock);
        UpdateUserRequestCommand cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult());

        UserRequest userRequest = new() { Id = Guid.NewGuid() };
        UserRequest userrequestMapped = new() { Id = userRequest.Id };

        mapperMock.Map(userRequest, cmd).ReturnsForAnyArgs(userrequestMapped);

        repositoryMock
            .Update(userRequest, cancellationToken)
            .ReturnsForAnyArgs(Task.FromResult<Errors?>(null));

        // Act
        Either<UpdateUserRequestCommandResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

        // Assert
        response.Should()
            .NotBeNull(because: "A response is expected in this scenario");
        response.IsLeft.Should()
            .BeTrue(because: "Response is expected in this scenario");
        response.Left.Should()
            .NotBeNull(because: "Response should NOT be null");
        response.Left.UserRequest.Should()
            .NotBeNull(because: "UserRequest should NOT be null");
        response.Left.UserRequest.Should()
            .BeEquivalentTo(userrequestMapped, because: "Returned userRequest must mach the one provided in request");

        Received.InOrder(async () =>
        {
            await validatorMock.ValidateAsync(Arg.Any<UpdateUserRequestCommand>(), cancellationToken).Received(1);
            mapperMock.Map(userRequest, Arg.Any<UpdateUserRequestCommand>()).Received(1);
            await repositoryMock.Update(Arg.Any<UserRequest>(), cancellationToken).Received(1);
        });
    }

    [Fact]
    public async Task If_validation_fails_then_errors_must_be_returned()
    {
        // Arrange
        UpdateUserRequestCommandValidator validatorMock = Substitute.For<UpdateUserRequestCommandValidator>();
        ILogger<UpdateUserRequestHandler> loggerMock = Substitute.For<ILogger<UpdateUserRequestHandler>>();
        IUserRequestWriteRepository repositoryMock = Substitute.For<IUserRequestWriteRepository>();
        IUpdateUserRequestMapper mapperMock = Substitute.For<IUpdateUserRequestMapper>();
        CancellationToken cancellationToken = default;

        UpdateUserRequestHandler handler = new(loggerMock, validatorMock, mapperMock, repositoryMock);
        UpdateUserRequestCommand cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult(new ValidationFailure[1] { new("test", "error message") }));

        // Act
        Either<UpdateUserRequestCommandResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

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
            await validatorMock.ValidateAsync(Arg.Any<UpdateUserRequestCommand>(), cancellationToken).Received(1);
            mapperMock.Map(Arg.Any<UserRequest>(), Arg.Any<UpdateUserRequestCommand>()).Received(0);
            await repositoryMock.Update(Arg.Any<UserRequest>(), cancellationToken).Received(0);
        });
    }

    [Fact]
    public async Task If_errors_are_returned_from_repository_then_they_must_be_forwarded_from_handler()
    {
        // Arrange
        UpdateUserRequestCommandValidator validatorMock = Substitute.For<UpdateUserRequestCommandValidator>();
        ILogger<UpdateUserRequestHandler> loggerMock = Substitute.For<ILogger<UpdateUserRequestHandler>>();
        IUserRequestWriteRepository repositoryMock = Substitute.For<IUserRequestWriteRepository>();
        IUpdateUserRequestMapper mapperMock = Substitute.For<IUpdateUserRequestMapper>();
        CancellationToken cancellationToken = default;

        UpdateUserRequestHandler handler = new(loggerMock, validatorMock, mapperMock, repositoryMock);
        UpdateUserRequestCommand cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult());

        UserRequest userRequest = new();
        UserRequest userrequestMapped = new();
        mapperMock.Map(userRequest, cmd).ReturnsForAnyArgs(userrequestMapped);

        // TODO: change error type
        repositoryMock
            .Update(userRequest, cancellationToken)
            .ReturnsForAnyArgs(Task.FromResult<Errors?>(
                new Errors(ErrorType.Validation, "I should be changed to a persistence error type")));

        // Act
        Either<UpdateUserRequestCommandResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

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
            await validatorMock.ValidateAsync(Arg.Any<UpdateUserRequestCommand>(), cancellationToken).Received(1);
            mapperMock.Map(Arg.Any<UserRequest>(), Arg.Any<UpdateUserRequestCommand>()).Received(1);
            await repositoryMock.Update(Arg.Any<UserRequest>(), cancellationToken).Received(1);
        });
    }
}