using FluentValidation.Results;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.Countries;
using WHO.BioHub.DataManagement.Core.UseCases.Countries.UpdateCountry;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UnitTests.UseCases.Countries.UpdateCountry;

public class UpdateCountryHandlerUnitTests
{
    [Fact]
    public async Task If_resource_is_updated_then_a_valid_response_is_returned()
    {
        // Arrange
        UpdateCountryCommandValidator validatorMock = Substitute.For<UpdateCountryCommandValidator>();
        ILogger<UpdateCountryHandler> loggerMock = Substitute.For<ILogger<UpdateCountryHandler>>();
        ICountryWriteRepository repositoryMock = Substitute.For<ICountryWriteRepository>();
        IUpdateCountryMapper mapperMock = Substitute.For<IUpdateCountryMapper>();
        CancellationToken cancellationToken = default;

        UpdateCountryHandler handler = new(loggerMock, validatorMock, mapperMock, repositoryMock);
        UpdateCountryCommand cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult());

        Country country = new() { Id = Guid.NewGuid() };
        Country countryMapped = new() { Id = country.Id };

        mapperMock.Map(country, cmd).ReturnsForAnyArgs(countryMapped);

        repositoryMock
            .Update(country, cancellationToken)
            .ReturnsForAnyArgs(Task.FromResult<Errors?>(null));

        // Act
        Either<UpdateCountryCommandResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

        // Assert
        response.Should()
            .NotBeNull(because: "A response is expected in this scenario");
        response.IsLeft.Should()
            .BeTrue(because: "Response is expected in this scenario");
        response.Left.Should()
            .NotBeNull(because: "Response should NOT be null");
        response.Left.Country.Should()
            .NotBeNull(because: "Country should NOT be null");
        response.Left.Country.Should()
            .BeEquivalentTo(countryMapped, because: "Returned country must mach the one provided in request");

        Received.InOrder(async () =>
        {
            await validatorMock.ValidateAsync(Arg.Any<UpdateCountryCommand>(), cancellationToken).Received(1);
            mapperMock.Map(country, Arg.Any<UpdateCountryCommand>()).Received(1);
            await repositoryMock.Update(Arg.Any<Country>(), cancellationToken).Received(1);
        });
    }

    [Fact]
    public async Task If_validation_fails_then_errors_must_be_returned()
    {
        // Arrange
        UpdateCountryCommandValidator validatorMock = Substitute.For<UpdateCountryCommandValidator>();
        ILogger<UpdateCountryHandler> loggerMock = Substitute.For<ILogger<UpdateCountryHandler>>();
        ICountryWriteRepository repositoryMock = Substitute.For<ICountryWriteRepository>();
        IUpdateCountryMapper mapperMock = Substitute.For<IUpdateCountryMapper>();
        CancellationToken cancellationToken = default;

        UpdateCountryHandler handler = new(loggerMock, validatorMock, mapperMock, repositoryMock);
        UpdateCountryCommand cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult(new ValidationFailure[1] { new("test", "error message") }));

        // Act
        Either<UpdateCountryCommandResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

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
            await validatorMock.ValidateAsync(Arg.Any<UpdateCountryCommand>(), cancellationToken).Received(1);
            mapperMock.Map(Arg.Any<Country>(), Arg.Any<UpdateCountryCommand>()).Received(0);
            await repositoryMock.Update(Arg.Any<Country>(), cancellationToken).Received(0);
        });
    }

    [Fact]
    public async Task If_errors_are_returned_from_repository_then_they_must_be_forwarded_from_handler()
    {
        // Arrange
        UpdateCountryCommandValidator validatorMock = Substitute.For<UpdateCountryCommandValidator>();
        ILogger<UpdateCountryHandler> loggerMock = Substitute.For<ILogger<UpdateCountryHandler>>();
        ICountryWriteRepository repositoryMock = Substitute.For<ICountryWriteRepository>();
        IUpdateCountryMapper mapperMock = Substitute.For<IUpdateCountryMapper>();
        CancellationToken cancellationToken = default;

        UpdateCountryHandler handler = new(loggerMock, validatorMock, mapperMock, repositoryMock);
        UpdateCountryCommand cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult());

        Country country = new();
        Country countryMapped = new();
        mapperMock.Map(country, cmd).ReturnsForAnyArgs(countryMapped);

        // TODO: change error type
        repositoryMock
            .Update(country, cancellationToken)
            .ReturnsForAnyArgs(Task.FromResult<Errors?>(
                new Errors(ErrorType.Validation, "I should be changed to a persistence error type")));

        // Act
        Either<UpdateCountryCommandResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

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
            await validatorMock.ValidateAsync(Arg.Any<UpdateCountryCommand>(), cancellationToken).Received(1);
            mapperMock.Map(Arg.Any<Country>(), Arg.Any<UpdateCountryCommand>()).Received(1);
            await repositoryMock.Update(Arg.Any<Country>(), cancellationToken).Received(1);
        });
    }
}