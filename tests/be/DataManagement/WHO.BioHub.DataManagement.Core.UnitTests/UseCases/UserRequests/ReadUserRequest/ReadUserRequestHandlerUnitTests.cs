using FluentValidation.Results;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.UserRequests;
using WHO.BioHub.DataManagement.Core.UseCases.UserRequests.ReadUserRequest;
using WHO.BioHub.Shared.Utils;
using WHO.BioHub.Shared.Dto;

namespace WHO.BioHub.DataManagement.Core.UnitTests.UseCases.UserRequests.ReadUserRequest;

public class ReadUserRequestHandlerUnitTests
{
    [Fact]
    public async Task If_resource_is_read_then_a_valid_response_is_returned()
    {
        // Arrange
        ReadUserRequestQueryValidator validatorMock = Substitute.For<ReadUserRequestQueryValidator>();
        ILogger<ReadUserRequestHandler> loggerMock = Substitute.For<ILogger<ReadUserRequestHandler>>();
        IUserRequestReadRepository repositoryMock = Substitute.For<IUserRequestReadRepository>();
        CancellationToken cancellationToken = default;

        IReadUserRequestMapper mapperMock = Substitute.For<IReadUserRequestMapper>();       
        ReadUserRequestHandler handler = new(loggerMock, validatorMock, repositoryMock, mapperMock);
        ReadUserRequestQuery cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult());

        Guid id = Guid.NewGuid();
        UserRequest userRequest = new() { Id = id };

        Guid assignedId = Guid.NewGuid();
        repositoryMock
            .Read(id, cancellationToken)
            .ReturnsForAnyArgs(Task.FromResult(userRequest));

        UserRequestViewModel userrequestViewModel = new() { Id = id };

        mapperMock.Map(userRequest).ReturnsForAnyArgs(userrequestViewModel);

        // Act
        Either<ReadUserRequestQueryResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

        // Assert
        response.Should()
            .NotBeNull(because: "A response is expected in this scenario");
        response.IsLeft.Should()
            .BeTrue(because: "Response is expected in this scenario");
        response.Left.Should()
            .NotBeNull(because: "Response should NOT be null");
        response.Left.UserRequest.Id.Should()
            .Be(id, because: "Expected id to be the requested one");

        Received.InOrder(async () =>
        {
            await validatorMock.ValidateAsync(Arg.Any<ReadUserRequestQuery>(), cancellationToken).Received(1);
            await repositoryMock.Read(id, cancellationToken).Received(1);
        });
    }

    [Fact]
    public async Task If_validation_fails_then_errors_must_be_returned()
    {
        // Arrange
        ReadUserRequestQueryValidator validatorMock = Substitute.For<ReadUserRequestQueryValidator>();
        ILogger<ReadUserRequestHandler> loggerMock = Substitute.For<ILogger<ReadUserRequestHandler>>();
        IUserRequestReadRepository repositoryMock = Substitute.For<IUserRequestReadRepository>();
        CancellationToken cancellationToken = default;

        IReadUserRequestMapper mapperMock = Substitute.For<IReadUserRequestMapper>();
        ReadUserRequestHandler handler = new(loggerMock, validatorMock, repositoryMock, mapperMock);
        ReadUserRequestQuery cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult(new ValidationFailure[1] { new("test", "error message") }));

        // Act
        Either<ReadUserRequestQueryResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

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
            await validatorMock.ValidateAsync(Arg.Any<ReadUserRequestQuery>(), cancellationToken).Received(1);
            await repositoryMock.Read(Arg.Any<Guid>(), cancellationToken).Received(0);
        });
    }
}