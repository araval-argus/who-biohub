using FluentValidation.Results;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.UserRequestStatuses;
using WHO.BioHub.DataManagement.Core.UseCases.UserRequestStatuses.ListUserRequestStatuses;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UnitTests.UseCases.UserRequestStatuses.ListUserRequestStatuses;

public class ListUserRequestStatusesHandlerUnitTests
{
    [Fact]
    public async Task If_no_userrequeststatuses_exists_then_empty_array_is_returned()
    {
        // Arrange
        ListUserRequestStatusesQueryValidator validatorMock = Substitute.For<ListUserRequestStatusesQueryValidator>();
        ILogger<ListUserRequestStatusesHandler> loggerMock = Substitute.For<ILogger<ListUserRequestStatusesHandler>>();
        IUserRequestStatusReadRepository repositoryMock = Substitute.For<IUserRequestStatusReadRepository>();
        CancellationToken cancellationToken = default;

        ListUserRequestStatusesHandler handler = new(loggerMock, validatorMock, repositoryMock);
        ListUserRequestStatusesQuery cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult());

        Guid assignedId = Guid.NewGuid();
        IEnumerable<UserRequestStatus> userrequeststatuses = Array.Empty<UserRequestStatus>();
        repositoryMock
            .List(cancellationToken)
            .ReturnsForAnyArgs(Task.FromResult(userrequeststatuses));

        // Act
        Either<ListUserRequestStatusesQueryResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

        // Assert
        response.Should()
            .NotBeNull(because: "A response is expected in this scenario");
        response.IsLeft.Should()
            .BeTrue(because: "Response is expected in this scenario");
        response.Left.Should()
            .NotBeNull(because: "Response should NOT be null");
        response.Left.UserRequestStatuses.Should()
            .BeEquivalentTo(userrequeststatuses, because: "Expected returned userrequeststatuses to be equivalent to ones returned from repository");

        Received.InOrder(async () =>
        {
            await validatorMock.ValidateAsync(Arg.Any<ListUserRequestStatusesQuery>(), cancellationToken).Received(1);
            await repositoryMock.List(cancellationToken).Received(1);
        });
    }

    [Fact]
    public async Task If_at_least_on_userrequeststatus_exists_then_it_is_returned()
    {
        // Arrange
        ListUserRequestStatusesQueryValidator validatorMock = Substitute.For<ListUserRequestStatusesQueryValidator>();
        ILogger<ListUserRequestStatusesHandler> loggerMock = Substitute.For<ILogger<ListUserRequestStatusesHandler>>();
        IUserRequestStatusReadRepository repositoryMock = Substitute.For<IUserRequestStatusReadRepository>();
        CancellationToken cancellationToken = default;

        ListUserRequestStatusesHandler handler = new(loggerMock, validatorMock, repositoryMock);
        ListUserRequestStatusesQuery cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult());

        Guid assignedId = Guid.NewGuid();
        IEnumerable<UserRequestStatus> userrequeststatuses = new UserRequestStatus[1] { new() };
        repositoryMock
            .List(cancellationToken)
            .ReturnsForAnyArgs(Task.FromResult(userrequeststatuses));

        // Act
        Either<ListUserRequestStatusesQueryResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

        // Assert
        response.Should()
            .NotBeNull(because: "A response is expected in this scenario");
        response.IsLeft.Should()
            .BeTrue(because: "Response is expected in this scenario");
        response.Left.Should()
            .NotBeNull(because: "Response should NOT be null");
        response.Left.UserRequestStatuses.Should()
            .BeEquivalentTo(userrequeststatuses, because: "Expected returned userrequeststatuses to be equivalent to ones returned from repository");

        Received.InOrder(async () =>
        {
            await validatorMock.ValidateAsync(Arg.Any<ListUserRequestStatusesQuery>(), cancellationToken).Received(1);
            await repositoryMock.List(cancellationToken).Received(1);
        });
    }


    [Fact]
    public async Task If_validation_fails_then_errors_must_be_returned()
    {
        // Arrange
        ListUserRequestStatusesQueryValidator validatorMock = Substitute.For<ListUserRequestStatusesQueryValidator>();
        ILogger<ListUserRequestStatusesHandler> loggerMock = Substitute.For<ILogger<ListUserRequestStatusesHandler>>();
        IUserRequestStatusReadRepository repositoryMock = Substitute.For<IUserRequestStatusReadRepository>();
        CancellationToken cancellationToken = default;

        ListUserRequestStatusesHandler handler = new(loggerMock, validatorMock, repositoryMock);
        ListUserRequestStatusesQuery cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult(new ValidationFailure[1] { new("test", "error message") }));

        // Act
        Either<ListUserRequestStatusesQueryResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

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
            await validatorMock.ValidateAsync(Arg.Any<ListUserRequestStatusesQuery>(), cancellationToken).Received(1);
            await repositoryMock.List(cancellationToken).Received(0);
        });
    }
}