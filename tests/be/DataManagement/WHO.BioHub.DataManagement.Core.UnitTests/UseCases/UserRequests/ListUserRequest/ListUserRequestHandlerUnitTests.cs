using FluentValidation.Results;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.UserRequests;
using WHO.BioHub.DataManagement.Core.UseCases.UserRequests.ListUserRequests;
using WHO.BioHub.Shared.Utils;
using WHO.BioHub.DataManagement.Core.UseCases.UserRequests.ReadUserRequest;
using WHO.BioHub.Shared.Dto;

namespace WHO.BioHub.DataManagement.Core.UnitTests.UseCases.UserRequests.ListUserRequests;

public class ListUserRequestsHandlerUnitTests
{
    [Fact]
    public async Task If_no_userrequests_exists_then_empty_array_is_returned()
    {
        // Arrange
        ListUserRequestsQueryValidator validatorMock = Substitute.For<ListUserRequestsQueryValidator>();
        ILogger<ListUserRequestsHandler> loggerMock = Substitute.For<ILogger<ListUserRequestsHandler>>();
        IUserRequestReadRepository repositoryMock = Substitute.For<IUserRequestReadRepository>();
        CancellationToken cancellationToken = default;

        IListUserRequestsMapper mapperMock = Substitute.For<IListUserRequestsMapper>();
        ListUserRequestsHandler handler = new(loggerMock, validatorMock, repositoryMock, mapperMock);
        ListUserRequestsQuery cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult());

        Guid assignedId = Guid.NewGuid();
        IEnumerable<UserRequest> userrequests = Array.Empty<UserRequest>();
        repositoryMock
            .List(cancellationToken)
            .ReturnsForAnyArgs(Task.FromResult(userrequests));

        // Act
        Either<ListUserRequestsQueryResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

        // Assert
        response.Should()
            .NotBeNull(because: "A response is expected in this scenario");
        response.IsLeft.Should()
            .BeTrue(because: "Response is expected in this scenario");
        response.Left.Should()
            .NotBeNull(because: "Response should NOT be null");
        response.Left.UserRequests.Should()
            .BeEquivalentTo(userrequests, because: "Expected returned userrequests to be equivalent to ones returned from repository");

        Received.InOrder(async () =>
        {
            await validatorMock.ValidateAsync(Arg.Any<ListUserRequestsQuery>(), cancellationToken).Received(1);
            await repositoryMock.List(cancellationToken).Received(1);
        });
    }

    [Fact]
    public async Task If_at_least_on_userrequest_exists_then_it_is_returned()
    {
        // Arrange
        ListUserRequestsQueryValidator validatorMock = Substitute.For<ListUserRequestsQueryValidator>();
        ILogger<ListUserRequestsHandler> loggerMock = Substitute.For<ILogger<ListUserRequestsHandler>>();
        IUserRequestReadRepository repositoryMock = Substitute.For<IUserRequestReadRepository>();
        CancellationToken cancellationToken = default;

        IListUserRequestsMapper mapperMock = Substitute.For<IListUserRequestsMapper>();
        ListUserRequestsHandler handler = new(loggerMock, validatorMock, repositoryMock, mapperMock);
        ListUserRequestsQuery cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult());

        Guid assignedId = Guid.NewGuid();
        IEnumerable<UserRequest> userrequests = new UserRequest[1] { new() { Id = assignedId } };
        IEnumerable<UserRequestViewModel> userrequestViewModels = new UserRequestViewModel[1] { new() { Id = assignedId } };
        repositoryMock
            .List(cancellationToken)
            .ReturnsForAnyArgs(Task.FromResult(userrequests));

        mapperMock.Map(userrequests).ReturnsForAnyArgs(userrequestViewModels);

        // Act
        Either<ListUserRequestsQueryResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

        // Assert
        response.Should()
            .NotBeNull(because: "A response is expected in this scenario");
        response.IsLeft.Should()
            .BeTrue(because: "Response is expected in this scenario");
        response.Left.Should()
            .NotBeNull(because: "Response should NOT be null");
        response.Left.UserRequests.Should()
            .BeEquivalentTo(userrequestViewModels, because: "Expected returned userrequests to be equivalent to ones returned from repository");

        Received.InOrder(async () =>
        {
            await validatorMock.ValidateAsync(Arg.Any<ListUserRequestsQuery>(), cancellationToken).Received(1);
            await repositoryMock.List(cancellationToken).Received(1);
        });
    }


    [Fact]
    public async Task If_validation_fails_then_errors_must_be_returned()
    {
        // Arrange
        ListUserRequestsQueryValidator validatorMock = Substitute.For<ListUserRequestsQueryValidator>();
        ILogger<ListUserRequestsHandler> loggerMock = Substitute.For<ILogger<ListUserRequestsHandler>>();
        IUserRequestReadRepository repositoryMock = Substitute.For<IUserRequestReadRepository>();
        CancellationToken cancellationToken = default;

        IListUserRequestsMapper mapperMock = Substitute.For<IListUserRequestsMapper>();
        ListUserRequestsHandler handler = new(loggerMock, validatorMock, repositoryMock, mapperMock);
        ListUserRequestsQuery cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult(new ValidationFailure[1] { new("test", "error message") }));

        // Act
        Either<ListUserRequestsQueryResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

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
            await validatorMock.ValidateAsync(Arg.Any<ListUserRequestsQuery>(), cancellationToken).Received(1);
            await repositoryMock.List(cancellationToken).Received(0);
        });
    }
}