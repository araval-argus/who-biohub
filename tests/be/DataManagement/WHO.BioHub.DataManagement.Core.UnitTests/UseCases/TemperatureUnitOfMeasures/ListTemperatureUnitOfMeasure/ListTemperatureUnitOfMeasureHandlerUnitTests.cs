using FluentValidation.Results;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.TemperatureUnitOfMeasures;
using WHO.BioHub.DataManagement.Core.UseCases.TemperatureUnitOfMeasures.ListTemperatureUnitOfMeasures;
using WHO.BioHub.Shared.Utils;
using WHO.BioHub.Shared.Dto;

namespace WHO.BioHub.DataManagement.Core.UnitTests.UseCases.TemperatureUnitOfMeasures.ListTemperatureUnitOfMeasures;

public class ListTemperatureUnitOfMeasuresHandlerUnitTests
{
    [Fact]
    public async Task If_no_temperatureunitofmeasures_exists_then_empty_array_is_returned()
    {
        // Arrange
        ListTemperatureUnitOfMeasuresQueryValidator validatorMock = Substitute.For<ListTemperatureUnitOfMeasuresQueryValidator>();
        ILogger<ListTemperatureUnitOfMeasuresHandler> loggerMock = Substitute.For<ILogger<ListTemperatureUnitOfMeasuresHandler>>();
        ITemperatureUnitOfMeasureReadRepository repositoryMock = Substitute.For<ITemperatureUnitOfMeasureReadRepository>();
        CancellationToken cancellationToken = default;

        ListTemperatureUnitOfMeasuresHandler handler = new(loggerMock, validatorMock, repositoryMock);
        ListTemperatureUnitOfMeasuresQuery cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult());

        Guid assignedId = Guid.NewGuid();
        IEnumerable<TemperatureUnitOfMeasure> temperatureunitofmeasures = Array.Empty<TemperatureUnitOfMeasure>();
        repositoryMock
            .List(cancellationToken)
            .ReturnsForAnyArgs(Task.FromResult(temperatureunitofmeasures));

        // Act
        Either<ListTemperatureUnitOfMeasuresQueryResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

        // Assert
        response.Should()
            .NotBeNull(because: "A response is expected in this scenario");
        response.IsLeft.Should()
            .BeTrue(because: "Response is expected in this scenario");
        response.Left.Should()
            .NotBeNull(because: "Response should NOT be null");
        response.Left.TemperatureUnitOfMeasures.Should()
            .BeEquivalentTo(temperatureunitofmeasures, because: "Expected returned temperatureunitofmeasures to be equivalent to ones returned from repository");

        Received.InOrder(async () =>
        {
            await validatorMock.ValidateAsync(Arg.Any<ListTemperatureUnitOfMeasuresQuery>(), cancellationToken).Received(1);
            await repositoryMock.List(cancellationToken).Received(1);
        });
    }

    [Fact]
    public async Task If_at_least_on_temperatureunitofmeasure_exists_then_it_is_returned()
    {
        // Arrange
        ListTemperatureUnitOfMeasuresQueryValidator validatorMock = Substitute.For<ListTemperatureUnitOfMeasuresQueryValidator>();
        ILogger<ListTemperatureUnitOfMeasuresHandler> loggerMock = Substitute.For<ILogger<ListTemperatureUnitOfMeasuresHandler>>();
        ITemperatureUnitOfMeasureReadRepository repositoryMock = Substitute.For<ITemperatureUnitOfMeasureReadRepository>();
        CancellationToken cancellationToken = default;

        ListTemperatureUnitOfMeasuresHandler handler = new(loggerMock, validatorMock, repositoryMock);
        ListTemperatureUnitOfMeasuresQuery cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult());

        Guid assignedId = Guid.NewGuid();
        IEnumerable<TemperatureUnitOfMeasure> temperatureunitofmeasures = new TemperatureUnitOfMeasure[1] { new() { Id = assignedId } };

        IEnumerable<TemperatureUnitOfMeasureDto> temperatureunitofmeasureDtos = new TemperatureUnitOfMeasureDto[1] { new() { Id = assignedId } };


        repositoryMock
            .List(cancellationToken)
            .ReturnsForAnyArgs(Task.FromResult(temperatureunitofmeasures));

        // Act
        Either<ListTemperatureUnitOfMeasuresQueryResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

        // Assert
        response.Should()
            .NotBeNull(because: "A response is expected in this scenario");
        response.IsLeft.Should()
            .BeTrue(because: "Response is expected in this scenario");
        response.Left.Should()
            .NotBeNull(because: "Response should NOT be null");
        response.Left.TemperatureUnitOfMeasures.Should()
            .BeEquivalentTo(temperatureunitofmeasureDtos, because: "Expected returned temperatureunitofmeasures to be equivalent to ones returned from repository");

        Received.InOrder(async () =>
        {
            await validatorMock.ValidateAsync(Arg.Any<ListTemperatureUnitOfMeasuresQuery>(), cancellationToken).Received(1);
            await repositoryMock.List(cancellationToken).Received(1);
        });
    }


    [Fact]
    public async Task If_validation_fails_then_errors_must_be_returned()
    {
        // Arrange
        ListTemperatureUnitOfMeasuresQueryValidator validatorMock = Substitute.For<ListTemperatureUnitOfMeasuresQueryValidator>();
        ILogger<ListTemperatureUnitOfMeasuresHandler> loggerMock = Substitute.For<ILogger<ListTemperatureUnitOfMeasuresHandler>>();
        ITemperatureUnitOfMeasureReadRepository repositoryMock = Substitute.For<ITemperatureUnitOfMeasureReadRepository>();
        CancellationToken cancellationToken = default;

        ListTemperatureUnitOfMeasuresHandler handler = new(loggerMock, validatorMock, repositoryMock);
        ListTemperatureUnitOfMeasuresQuery cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult(new ValidationFailure[1] { new("test", "error message") }));

        // Act
        Either<ListTemperatureUnitOfMeasuresQueryResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

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
            await validatorMock.ValidateAsync(Arg.Any<ListTemperatureUnitOfMeasuresQuery>(), cancellationToken).Received(1);
            await repositoryMock.List(cancellationToken).Received(0);
        });
    }
}