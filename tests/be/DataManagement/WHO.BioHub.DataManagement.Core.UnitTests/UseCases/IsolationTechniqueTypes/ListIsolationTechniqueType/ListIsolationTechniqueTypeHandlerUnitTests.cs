using FluentValidation.Results;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.IsolationTechniqueTypes;
using WHO.BioHub.DataManagement.Core.UseCases.IsolationTechniqueTypes.ListIsolationTechniqueTypes;
using WHO.BioHub.Shared.Utils;
using WHO.BioHub.Shared.Dto;

namespace WHO.BioHub.DataManagement.Core.UnitTests.UseCases.IsolationTechniqueTypes.ListIsolationTechniqueTypes;

public class ListIsolationTechniqueTypesHandlerUnitTests
{
    [Fact]
    public async Task If_no_isolationtechniquetypes_exists_then_empty_array_is_returned()
    {
        // Arrange
        ListIsolationTechniqueTypesQueryValidator validatorMock = Substitute.For<ListIsolationTechniqueTypesQueryValidator>();
        ILogger<ListIsolationTechniqueTypesHandler> loggerMock = Substitute.For<ILogger<ListIsolationTechniqueTypesHandler>>();
        IIsolationTechniqueTypeReadRepository repositoryMock = Substitute.For<IIsolationTechniqueTypeReadRepository>();
        CancellationToken cancellationToken = default;

        ListIsolationTechniqueTypesHandler handler = new(loggerMock, validatorMock, repositoryMock);
        ListIsolationTechniqueTypesQuery cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult());

        Guid assignedId = Guid.NewGuid();
        IEnumerable<IsolationTechniqueType> isolationtechniquetypes = Array.Empty<IsolationTechniqueType>();
        repositoryMock
            .List(cancellationToken)
            .ReturnsForAnyArgs(Task.FromResult(isolationtechniquetypes));

        // Act
        Either<ListIsolationTechniqueTypesQueryResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

        // Assert
        response.Should()
            .NotBeNull(because: "A response is expected in this scenario");
        response.IsLeft.Should()
            .BeTrue(because: "Response is expected in this scenario");
        response.Left.Should()
            .NotBeNull(because: "Response should NOT be null");
        response.Left.IsolationTechniqueTypes.Should()
            .BeEquivalentTo(isolationtechniquetypes, because: "Expected returned isolationtechniquetypes to be equivalent to ones returned from repository");

        Received.InOrder(async () =>
        {
            await validatorMock.ValidateAsync(Arg.Any<ListIsolationTechniqueTypesQuery>(), cancellationToken).Received(1);
            await repositoryMock.List(cancellationToken).Received(1);
        });
    }

    [Fact]
    public async Task If_at_least_on_isolationtechniquetype_exists_then_it_is_returned()
    {
        // Arrange
        ListIsolationTechniqueTypesQueryValidator validatorMock = Substitute.For<ListIsolationTechniqueTypesQueryValidator>();
        ILogger<ListIsolationTechniqueTypesHandler> loggerMock = Substitute.For<ILogger<ListIsolationTechniqueTypesHandler>>();
        IIsolationTechniqueTypeReadRepository repositoryMock = Substitute.For<IIsolationTechniqueTypeReadRepository>();
        CancellationToken cancellationToken = default;

        ListIsolationTechniqueTypesHandler handler = new(loggerMock, validatorMock, repositoryMock);
        ListIsolationTechniqueTypesQuery cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult());

        Guid assignedId = Guid.NewGuid();
        IEnumerable<IsolationTechniqueType> isolationtechniquetypes = new IsolationTechniqueType[1] { new() { Id = assignedId } };

        IEnumerable<IsolationTechniqueTypeDto> isolationtechniquetypeDtos = new IsolationTechniqueTypeDto[1] { new() { Id = assignedId } };

        repositoryMock
            .List(cancellationToken)
            .ReturnsForAnyArgs(Task.FromResult(isolationtechniquetypes));

        // Act
        Either<ListIsolationTechniqueTypesQueryResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

        // Assert
        response.Should()
            .NotBeNull(because: "A response is expected in this scenario");
        response.IsLeft.Should()
            .BeTrue(because: "Response is expected in this scenario");
        response.Left.Should()
            .NotBeNull(because: "Response should NOT be null");
        response.Left.IsolationTechniqueTypes.Should()
            .BeEquivalentTo(isolationtechniquetypeDtos, because: "Expected returned isolationtechniquetypes to be equivalent to ones returned from repository");

        Received.InOrder(async () =>
        {
            await validatorMock.ValidateAsync(Arg.Any<ListIsolationTechniqueTypesQuery>(), cancellationToken).Received(1);
            await repositoryMock.List(cancellationToken).Received(1);
        });
    }


    [Fact]
    public async Task If_validation_fails_then_errors_must_be_returned()
    {
        // Arrange
        ListIsolationTechniqueTypesQueryValidator validatorMock = Substitute.For<ListIsolationTechniqueTypesQueryValidator>();
        ILogger<ListIsolationTechniqueTypesHandler> loggerMock = Substitute.For<ILogger<ListIsolationTechniqueTypesHandler>>();
        IIsolationTechniqueTypeReadRepository repositoryMock = Substitute.For<IIsolationTechniqueTypeReadRepository>();
        CancellationToken cancellationToken = default;

        ListIsolationTechniqueTypesHandler handler = new(loggerMock, validatorMock, repositoryMock);
        ListIsolationTechniqueTypesQuery cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult(new ValidationFailure[1] { new("test", "error message") }));

        // Act
        Either<ListIsolationTechniqueTypesQueryResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

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
            await validatorMock.ValidateAsync(Arg.Any<ListIsolationTechniqueTypesQuery>(), cancellationToken).Received(1);
            await repositoryMock.List(cancellationToken).Received(0);
        });
    }
}