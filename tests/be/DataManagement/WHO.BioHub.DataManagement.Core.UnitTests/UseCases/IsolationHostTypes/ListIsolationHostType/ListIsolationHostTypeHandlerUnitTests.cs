using FluentValidation.Results;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.IsolationHostTypes;
using WHO.BioHub.DataManagement.Core.UseCases.IsolationHostTypes.ListIsolationHostTypes;
using WHO.BioHub.Shared.Utils;
using WHO.BioHub.Shared.Dto;

namespace WHO.BioHub.DataManagement.Core.UnitTests.UseCases.IsolationHostTypes.ListIsolationHostTypes;

public class ListIsolationHostTypesHandlerUnitTests
{
    [Fact]
    public async Task If_no_isolationhosttypes_exists_then_empty_array_is_returned()
    {
        // Arrange
        ListIsolationHostTypesQueryValidator validatorMock = Substitute.For<ListIsolationHostTypesQueryValidator>();
        ILogger<ListIsolationHostTypesHandler> loggerMock = Substitute.For<ILogger<ListIsolationHostTypesHandler>>();
        IIsolationHostTypeReadRepository repositoryMock = Substitute.For<IIsolationHostTypeReadRepository>();
        CancellationToken cancellationToken = default;

        ListIsolationHostTypesHandler handler = new(loggerMock, validatorMock, repositoryMock);
        ListIsolationHostTypesQuery cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult());

        Guid assignedId = Guid.NewGuid();
        IEnumerable<IsolationHostType> isolationhosttypes = Array.Empty<IsolationHostType>();
        repositoryMock
            .List(cancellationToken)
            .ReturnsForAnyArgs(Task.FromResult(isolationhosttypes));

        // Act
        Either<ListIsolationHostTypesQueryResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

        // Assert
        response.Should()
            .NotBeNull(because: "A response is expected in this scenario");
        response.IsLeft.Should()
            .BeTrue(because: "Response is expected in this scenario");
        response.Left.Should()
            .NotBeNull(because: "Response should NOT be null");
        response.Left.IsolationHostTypes.Should()
            .BeEquivalentTo(isolationhosttypes, because: "Expected returned isolationhosttypes to be equivalent to ones returned from repository");

        Received.InOrder(async () =>
        {
            await validatorMock.ValidateAsync(Arg.Any<ListIsolationHostTypesQuery>(), cancellationToken).Received(1);
            await repositoryMock.List(cancellationToken).Received(1);
        });
    }

    [Fact]
    public async Task If_at_least_on_isolationhosttype_exists_then_it_is_returned()
    {
        // Arrange
        ListIsolationHostTypesQueryValidator validatorMock = Substitute.For<ListIsolationHostTypesQueryValidator>();
        ILogger<ListIsolationHostTypesHandler> loggerMock = Substitute.For<ILogger<ListIsolationHostTypesHandler>>();
        IIsolationHostTypeReadRepository repositoryMock = Substitute.For<IIsolationHostTypeReadRepository>();
        CancellationToken cancellationToken = default;

        ListIsolationHostTypesHandler handler = new(loggerMock, validatorMock, repositoryMock);
        ListIsolationHostTypesQuery cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult());

        Guid assignedId = Guid.NewGuid();
        IEnumerable<IsolationHostType> isolationhosttypes = new IsolationHostType[1] { new() { Id = assignedId } };

        IEnumerable<IsolationHostTypeDto> isolationhosttypeDtos = new IsolationHostTypeDto[1] { new() { Id = assignedId } };

        repositoryMock
            .List(cancellationToken)
            .ReturnsForAnyArgs(Task.FromResult(isolationhosttypes));

        // Act
        Either<ListIsolationHostTypesQueryResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

        // Assert
        response.Should()
            .NotBeNull(because: "A response is expected in this scenario");
        response.IsLeft.Should()
            .BeTrue(because: "Response is expected in this scenario");
        response.Left.Should()
            .NotBeNull(because: "Response should NOT be null");
        response.Left.IsolationHostTypes.Should()
            .BeEquivalentTo(isolationhosttypeDtos, because: "Expected returned isolationhosttypes to be equivalent to ones returned from repository");

        Received.InOrder(async () =>
        {
            await validatorMock.ValidateAsync(Arg.Any<ListIsolationHostTypesQuery>(), cancellationToken).Received(1);
            await repositoryMock.List(cancellationToken).Received(1);
        });
    }


    [Fact]
    public async Task If_validation_fails_then_errors_must_be_returned()
    {
        // Arrange
        ListIsolationHostTypesQueryValidator validatorMock = Substitute.For<ListIsolationHostTypesQueryValidator>();
        ILogger<ListIsolationHostTypesHandler> loggerMock = Substitute.For<ILogger<ListIsolationHostTypesHandler>>();
        IIsolationHostTypeReadRepository repositoryMock = Substitute.For<IIsolationHostTypeReadRepository>();
        CancellationToken cancellationToken = default;

        ListIsolationHostTypesHandler handler = new(loggerMock, validatorMock, repositoryMock);
        ListIsolationHostTypesQuery cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult(new ValidationFailure[1] { new("test", "error message") }));

        // Act
        Either<ListIsolationHostTypesQueryResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

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
            await validatorMock.ValidateAsync(Arg.Any<ListIsolationHostTypesQuery>(), cancellationToken).Received(1);
            await repositoryMock.List(cancellationToken).Received(0);
        });
    }
}