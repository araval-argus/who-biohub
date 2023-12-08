using FluentValidation.Results;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.Countries;
using WHO.BioHub.DataManagement.Core.UseCases.Countries.CreateCountry;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UnitTests.UseCases.Countries.CreateCountry;

public class CreateCountryHandlerUnitTests
{
    [Fact]
    public async Task If_resource_is_created_then_a_valid_response_is_returned()
    {
        // Arrange
        CreateCountryCommandValidator validatorMock = Substitute.For<CreateCountryCommandValidator>();
        ILogger<CreateCountryHandler> loggerMock = Substitute.For<ILogger<CreateCountryHandler>>();
        ICountryWriteRepository repositoryMock = Substitute.For<ICountryWriteRepository>();
        ICreateCountryMapper mapperMock = Substitute.For<ICreateCountryMapper>();
        CancellationToken cancellationToken = default;

        CreateCountryHandler handler = new(loggerMock, validatorMock, mapperMock, repositoryMock);
        CreateCountryCommand cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult());

        Country country = new();
        mapperMock.Map(cmd).ReturnsForAnyArgs(country);

        Guid assignedId = Guid.NewGuid();
        repositoryMock
            .Create(country, cancellationToken)
            .ReturnsForAnyArgs(
                Task.Run<Either<Country, Errors>>(() =>
                {
                    country.Id = assignedId;
                    return new(country);
                }));

        // Act
        Either<CreateCountryCommandResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

        // Assert
        response.Should()
            .NotBeNull(because: "A response is expected in this scenario");
        response.IsLeft.Should()
            .BeTrue(because: "Response is expected in this scenario");
        response.Left.Should()
            .NotBeNull(because: "Response should NOT be null");
        response.Left.Country.Should()
            .NotBeNull(because: "Country should NOT be null");
        response.Left.Country.Id.Should()
            .NotBeEmpty(because: "An Id should be assigned to the Country")
            .And.Be(assignedId, because: "Returned country Id must mach the one assigned by repository");

        Received.InOrder(async () =>
        {
            await validatorMock.ValidateAsync(Arg.Any<CreateCountryCommand>(), cancellationToken).Received(1);
            mapperMock.Map(Arg.Any<CreateCountryCommand>()).Received(1);
            await repositoryMock.Create(Arg.Any<Country>(), cancellationToken).Received(1);
        });
    }

    [Fact]
    public async Task If_validation_fails_then_errors_must_be_returned()
    {
        // Arrange
        CreateCountryCommandValidator validatorMock = Substitute.For<CreateCountryCommandValidator>();
        ILogger<CreateCountryHandler> loggerMock = Substitute.For<ILogger<CreateCountryHandler>>();
        ICountryWriteRepository repositoryMock = Substitute.For<ICountryWriteRepository>();
        ICreateCountryMapper mapperMock = Substitute.For<ICreateCountryMapper>();
        CancellationToken cancellationToken = default;

        CreateCountryHandler handler = new(loggerMock, validatorMock, mapperMock, repositoryMock);
        CreateCountryCommand cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult(new ValidationFailure[1] { new("test", "error message") }));

        // Act
        Either<CreateCountryCommandResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

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
            await validatorMock.ValidateAsync(Arg.Any<CreateCountryCommand>(), cancellationToken).Received(1);
            mapperMock.Map(Arg.Any<CreateCountryCommand>()).Received(0);
            await repositoryMock.Create(Arg.Any<Country>(), cancellationToken).Received(0);
        });
    }

    [Fact]
    public async Task If_errors_are_returned_from_repository_then_they_must_be_forwarded_from_handler()
    {
        // Arrange
        CreateCountryCommandValidator validatorMock = Substitute.For<CreateCountryCommandValidator>();
        ILogger<CreateCountryHandler> loggerMock = Substitute.For<ILogger<CreateCountryHandler>>();
        ICountryWriteRepository repositoryMock = Substitute.For<ICountryWriteRepository>();
        ICreateCountryMapper mapperMock = Substitute.For<ICreateCountryMapper>();
        CancellationToken cancellationToken = default;

        CreateCountryHandler handler = new(loggerMock, validatorMock, mapperMock, repositoryMock);
        CreateCountryCommand cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult());

        Country country = new();
        mapperMock.Map(cmd).ReturnsForAnyArgs(country);

        // TODO: change error type
        repositoryMock
            .Create(country, cancellationToken)
            .ReturnsForAnyArgs(Task.FromResult<Either<Country, Errors>>(
                new(new Errors(ErrorType.Validation, "I should be changed to a persistence error type"))));

        // Act
        Either<CreateCountryCommandResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

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
            await validatorMock.ValidateAsync(Arg.Any<CreateCountryCommand>(), cancellationToken).Received(1);
            mapperMock.Map(Arg.Any<CreateCountryCommand>()).Received(1);
            await repositoryMock.Create(Arg.Any<Country>(), cancellationToken).Received(1);
        });
    }
}