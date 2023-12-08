using FluentValidation.Results;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.TemperatureUnitOfMeasures;
using WHO.BioHub.DataManagement.Core.UseCases.TemperatureUnitOfMeasures.UpdateTemperatureUnitOfMeasure;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UnitTests.UseCases.TemperatureUnitOfMeasures.UpdateTemperatureUnitOfMeasure;

public class UpdateTemperatureUnitOfMeasureHandlerUnitTests
{
    [Fact]
    public async Task If_resource_is_updated_then_a_valid_response_is_returned()
    {
        // Arrange
        UpdateTemperatureUnitOfMeasureCommandValidator validatorMock = Substitute.For<UpdateTemperatureUnitOfMeasureCommandValidator>();
        ILogger<UpdateTemperatureUnitOfMeasureHandler> loggerMock = Substitute.For<ILogger<UpdateTemperatureUnitOfMeasureHandler>>();
        ITemperatureUnitOfMeasureWriteRepository repositoryMock = Substitute.For<ITemperatureUnitOfMeasureWriteRepository>();
        IUpdateTemperatureUnitOfMeasureMapper mapperMock = Substitute.For<IUpdateTemperatureUnitOfMeasureMapper>();
        CancellationToken cancellationToken = default;

        UpdateTemperatureUnitOfMeasureHandler handler = new(loggerMock, validatorMock, mapperMock, repositoryMock);
        UpdateTemperatureUnitOfMeasureCommand cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult());

        TemperatureUnitOfMeasure temperatureunitofmeasure = new() { Id = Guid.NewGuid() };
        TemperatureUnitOfMeasure temperatureunitofmeasureMapped = new() { Id = temperatureunitofmeasure.Id };

        mapperMock.Map(temperatureunitofmeasure, cmd).ReturnsForAnyArgs(temperatureunitofmeasureMapped);

        repositoryMock
            .Update(temperatureunitofmeasure, cancellationToken)
            .ReturnsForAnyArgs(Task.FromResult<Errors?>(null));

        // Act
        Either<UpdateTemperatureUnitOfMeasureCommandResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

        // Assert
        response.Should()
            .NotBeNull(because: "A response is expected in this scenario");
        response.IsLeft.Should()
            .BeTrue(because: "Response is expected in this scenario");
        response.Left.Should()
            .NotBeNull(because: "Response should NOT be null");
        response.Left.TemperatureUnitOfMeasure.Should()
            .NotBeNull(because: "TemperatureUnitOfMeasure should NOT be null");
        response.Left.TemperatureUnitOfMeasure.Should()
            .BeEquivalentTo(temperatureunitofmeasureMapped, because: "Returned temperatureunitofmeasure must mach the one provided in request");

        Received.InOrder(async () =>
        {
            await validatorMock.ValidateAsync(Arg.Any<UpdateTemperatureUnitOfMeasureCommand>(), cancellationToken).Received(1);
            mapperMock.Map(temperatureunitofmeasure, Arg.Any<UpdateTemperatureUnitOfMeasureCommand>()).Received(1);
            await repositoryMock.Update(Arg.Any<TemperatureUnitOfMeasure>(), cancellationToken).Received(1);
        });
    }

    [Fact]
    public async Task If_validation_fails_then_errors_must_be_returned()
    {
        // Arrange
        UpdateTemperatureUnitOfMeasureCommandValidator validatorMock = Substitute.For<UpdateTemperatureUnitOfMeasureCommandValidator>();
        ILogger<UpdateTemperatureUnitOfMeasureHandler> loggerMock = Substitute.For<ILogger<UpdateTemperatureUnitOfMeasureHandler>>();
        ITemperatureUnitOfMeasureWriteRepository repositoryMock = Substitute.For<ITemperatureUnitOfMeasureWriteRepository>();
        IUpdateTemperatureUnitOfMeasureMapper mapperMock = Substitute.For<IUpdateTemperatureUnitOfMeasureMapper>();
        CancellationToken cancellationToken = default;

        UpdateTemperatureUnitOfMeasureHandler handler = new(loggerMock, validatorMock, mapperMock, repositoryMock);
        UpdateTemperatureUnitOfMeasureCommand cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult(new ValidationFailure[1] { new("test", "error message") }));

        // Act
        Either<UpdateTemperatureUnitOfMeasureCommandResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

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
            await validatorMock.ValidateAsync(Arg.Any<UpdateTemperatureUnitOfMeasureCommand>(), cancellationToken).Received(1);
            mapperMock.Map(Arg.Any<TemperatureUnitOfMeasure>(), Arg.Any<UpdateTemperatureUnitOfMeasureCommand>()).Received(0);
            await repositoryMock.Update(Arg.Any<TemperatureUnitOfMeasure>(), cancellationToken).Received(0);
        });
    }

    [Fact]
    public async Task If_errors_are_returned_from_repository_then_they_must_be_forwarded_from_handler()
    {
        // Arrange
        UpdateTemperatureUnitOfMeasureCommandValidator validatorMock = Substitute.For<UpdateTemperatureUnitOfMeasureCommandValidator>();
        ILogger<UpdateTemperatureUnitOfMeasureHandler> loggerMock = Substitute.For<ILogger<UpdateTemperatureUnitOfMeasureHandler>>();
        ITemperatureUnitOfMeasureWriteRepository repositoryMock = Substitute.For<ITemperatureUnitOfMeasureWriteRepository>();
        IUpdateTemperatureUnitOfMeasureMapper mapperMock = Substitute.For<IUpdateTemperatureUnitOfMeasureMapper>();
        CancellationToken cancellationToken = default;

        UpdateTemperatureUnitOfMeasureHandler handler = new(loggerMock, validatorMock, mapperMock, repositoryMock);
        UpdateTemperatureUnitOfMeasureCommand cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult());

        TemperatureUnitOfMeasure temperatureunitofmeasure = new();
        TemperatureUnitOfMeasure temperatureunitofmeasureMapped = new();
        mapperMock.Map(temperatureunitofmeasure, cmd).ReturnsForAnyArgs(temperatureunitofmeasureMapped);

        // TODO: change error type
        repositoryMock
            .Update(temperatureunitofmeasure, cancellationToken)
            .ReturnsForAnyArgs(Task.FromResult<Errors?>(
                new Errors(ErrorType.Validation, "I should be changed to a persistence error type")));

        // Act
        Either<UpdateTemperatureUnitOfMeasureCommandResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

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
            await validatorMock.ValidateAsync(Arg.Any<UpdateTemperatureUnitOfMeasureCommand>(), cancellationToken).Received(1);
            mapperMock.Map(Arg.Any<TemperatureUnitOfMeasure>(), Arg.Any<UpdateTemperatureUnitOfMeasureCommand>()).Received(1);
            await repositoryMock.Update(Arg.Any<TemperatureUnitOfMeasure>(), cancellationToken).Received(1);
        });
    }
}