using FluentValidation.Results;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.TemperatureUnitOfMeasures;
using WHO.BioHub.DataManagement.Core.UseCases.TemperatureUnitOfMeasures.CreateTemperatureUnitOfMeasure;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UnitTests.UseCases.TemperatureUnitOfMeasures.CreateTemperatureUnitOfMeasure;

public class CreateTemperatureUnitOfMeasureHandlerUnitTests
{
    [Fact]
    public async Task If_resource_is_created_then_a_valid_response_is_returned()
    {
        // Arrange
        CreateTemperatureUnitOfMeasureCommandValidator validatorMock = Substitute.For<CreateTemperatureUnitOfMeasureCommandValidator>();
        ILogger<CreateTemperatureUnitOfMeasureHandler> loggerMock = Substitute.For<ILogger<CreateTemperatureUnitOfMeasureHandler>>();
        ITemperatureUnitOfMeasureWriteRepository repositoryMock = Substitute.For<ITemperatureUnitOfMeasureWriteRepository>();
        ICreateTemperatureUnitOfMeasureMapper mapperMock = Substitute.For<ICreateTemperatureUnitOfMeasureMapper>();
        CancellationToken cancellationToken = default;

        CreateTemperatureUnitOfMeasureHandler handler = new(loggerMock, validatorMock, mapperMock, repositoryMock);
        CreateTemperatureUnitOfMeasureCommand cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult());

        TemperatureUnitOfMeasure temperatureunitofmeasure = new();
        mapperMock.Map(cmd).ReturnsForAnyArgs(temperatureunitofmeasure);

        Guid assignedId = Guid.NewGuid();
        repositoryMock
            .Create(temperatureunitofmeasure, cancellationToken)
            .ReturnsForAnyArgs(
                Task.Run<Either<TemperatureUnitOfMeasure, Errors>>(() =>
                {
                    temperatureunitofmeasure.Id = assignedId;
                    return new(temperatureunitofmeasure);
                }));

        // Act
        Either<CreateTemperatureUnitOfMeasureCommandResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

        // Assert
        response.Should()
            .NotBeNull(because: "A response is expected in this scenario");
        response.IsLeft.Should()
            .BeTrue(because: "Response is expected in this scenario");
        response.Left.Should()
            .NotBeNull(because: "Response should NOT be null");
        response.Left.TemperatureUnitOfMeasure.Should()
            .NotBeNull(because: "TemperatureUnitOfMeasure should NOT be null");
        response.Left.TemperatureUnitOfMeasure.Id.Should()
            .NotBeEmpty(because: "An Id should be assigned to the TemperatureUnitOfMeasure")
            .And.Be(assignedId, because: "Returned temperatureunitofmeasure Id must mach the one assigned by repository");

        Received.InOrder(async () =>
        {
            await validatorMock.ValidateAsync(Arg.Any<CreateTemperatureUnitOfMeasureCommand>(), cancellationToken).Received(1);
            mapperMock.Map(Arg.Any<CreateTemperatureUnitOfMeasureCommand>()).Received(1);
            await repositoryMock.Create(Arg.Any<TemperatureUnitOfMeasure>(), cancellationToken).Received(1);
        });
    }

    [Fact]
    public async Task If_validation_fails_then_errors_must_be_returned()
    {
        // Arrange
        CreateTemperatureUnitOfMeasureCommandValidator validatorMock = Substitute.For<CreateTemperatureUnitOfMeasureCommandValidator>();
        ILogger<CreateTemperatureUnitOfMeasureHandler> loggerMock = Substitute.For<ILogger<CreateTemperatureUnitOfMeasureHandler>>();
        ITemperatureUnitOfMeasureWriteRepository repositoryMock = Substitute.For<ITemperatureUnitOfMeasureWriteRepository>();
        ICreateTemperatureUnitOfMeasureMapper mapperMock = Substitute.For<ICreateTemperatureUnitOfMeasureMapper>();
        CancellationToken cancellationToken = default;

        CreateTemperatureUnitOfMeasureHandler handler = new(loggerMock, validatorMock, mapperMock, repositoryMock);
        CreateTemperatureUnitOfMeasureCommand cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult(new ValidationFailure[1] { new("test", "error message") }));

        // Act
        Either<CreateTemperatureUnitOfMeasureCommandResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

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
            await validatorMock.ValidateAsync(Arg.Any<CreateTemperatureUnitOfMeasureCommand>(), cancellationToken).Received(1);
            mapperMock.Map(Arg.Any<CreateTemperatureUnitOfMeasureCommand>()).Received(0);
            await repositoryMock.Create(Arg.Any<TemperatureUnitOfMeasure>(), cancellationToken).Received(0);
        });
    }

    [Fact]
    public async Task If_errors_are_returned_from_repository_then_they_must_be_forwarded_from_handler()
    {
        // Arrange
        CreateTemperatureUnitOfMeasureCommandValidator validatorMock = Substitute.For<CreateTemperatureUnitOfMeasureCommandValidator>();
        ILogger<CreateTemperatureUnitOfMeasureHandler> loggerMock = Substitute.For<ILogger<CreateTemperatureUnitOfMeasureHandler>>();
        ITemperatureUnitOfMeasureWriteRepository repositoryMock = Substitute.For<ITemperatureUnitOfMeasureWriteRepository>();
        ICreateTemperatureUnitOfMeasureMapper mapperMock = Substitute.For<ICreateTemperatureUnitOfMeasureMapper>();
        CancellationToken cancellationToken = default;

        CreateTemperatureUnitOfMeasureHandler handler = new(loggerMock, validatorMock, mapperMock, repositoryMock);
        CreateTemperatureUnitOfMeasureCommand cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult());

        TemperatureUnitOfMeasure temperatureunitofmeasure = new();
        mapperMock.Map(cmd).ReturnsForAnyArgs(temperatureunitofmeasure);

        // TODO: change error type
        repositoryMock
            .Create(temperatureunitofmeasure, cancellationToken)
            .ReturnsForAnyArgs(Task.FromResult<Either<TemperatureUnitOfMeasure, Errors>>(
                new(new Errors(ErrorType.Validation, "I should be changed to a persistence error type"))));

        // Act
        Either<CreateTemperatureUnitOfMeasureCommandResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

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
            await validatorMock.ValidateAsync(Arg.Any<CreateTemperatureUnitOfMeasureCommand>(), cancellationToken).Received(1);
            mapperMock.Map(Arg.Any<CreateTemperatureUnitOfMeasureCommand>()).Received(1);
            await repositoryMock.Create(Arg.Any<TemperatureUnitOfMeasure>(), cancellationToken).Received(1);
        });
    }
}