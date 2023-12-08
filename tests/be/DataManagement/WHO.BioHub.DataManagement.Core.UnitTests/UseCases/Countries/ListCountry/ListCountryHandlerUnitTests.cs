using FluentValidation.Results;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.Countries;
using WHO.BioHub.DataManagement.Core.UseCases.Countries.ListCountries;
using WHO.BioHub.Shared.Utils;
using WHO.BioHub.Shared.Dto;

namespace WHO.BioHub.DataManagement.Core.UnitTests.UseCases.Countries.ListCountries;

public class ListCountriesHandlerUnitTests
{
    [Fact]
    public async Task If_no_countries_exists_then_empty_array_is_returned()
    {
        // Arrange
        ListCountriesQueryValidator validatorMock = Substitute.For<ListCountriesQueryValidator>();
        ILogger<ListCountriesHandler> loggerMock = Substitute.For<ILogger<ListCountriesHandler>>();
        ICountryReadRepository repositoryMock = Substitute.For<ICountryReadRepository>();
        CancellationToken cancellationToken = default;

        ListCountriesHandler handler = new(loggerMock, validatorMock, repositoryMock);
        ListCountriesQuery cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult());

        Guid assignedId = Guid.NewGuid();
        IEnumerable<Country> countries = Array.Empty<Country>();
        repositoryMock
            .List(cancellationToken)
            .ReturnsForAnyArgs(Task.FromResult(countries));

        // Act
        Either<ListCountriesQueryResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

        // Assert
        response.Should()
            .NotBeNull(because: "A response is expected in this scenario");
        response.IsLeft.Should()
            .BeTrue(because: "Response is expected in this scenario");
        response.Left.Should()
            .NotBeNull(because: "Response should NOT be null");
        response.Left.Countries.Should()
            .BeEquivalentTo(countries, because: "Expected returned countries to be equivalent to ones returned from repository");

        Received.InOrder(async () =>
        {
            await validatorMock.ValidateAsync(Arg.Any<ListCountriesQuery>(), cancellationToken).Received(1);
            await repositoryMock.List(cancellationToken).Received(1);
        });
    }

    [Fact]
    public async Task If_at_least_on_country_exists_then_it_is_returned()
    {
        // Arrange
        ListCountriesQueryValidator validatorMock = Substitute.For<ListCountriesQueryValidator>();
        ILogger<ListCountriesHandler> loggerMock = Substitute.For<ILogger<ListCountriesHandler>>();
        ICountryReadRepository repositoryMock = Substitute.For<ICountryReadRepository>();
        CancellationToken cancellationToken = default;

        ListCountriesHandler handler = new(loggerMock, validatorMock, repositoryMock);
        ListCountriesQuery cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult());

        Guid assignedId = Guid.NewGuid();
        IEnumerable<Country> countries = new Country[1] { new() { Id = assignedId } };

        IEnumerable<CountryDto> countryDtos = new CountryDto[1] { new() { Id = assignedId } };
        repositoryMock
            .List(cancellationToken)
            .ReturnsForAnyArgs(Task.FromResult(countries));

        // Act
        Either<ListCountriesQueryResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

        // Assert
        response.Should()
            .NotBeNull(because: "A response is expected in this scenario");
        response.IsLeft.Should()
            .BeTrue(because: "Response is expected in this scenario");
        response.Left.Should()
            .NotBeNull(because: "Response should NOT be null");
        response.Left.Countries.Should()
            .BeEquivalentTo(countryDtos, because: "Expected returned countries to be equivalent to ones returned from repository");

        Received.InOrder(async () =>
        {
            await validatorMock.ValidateAsync(Arg.Any<ListCountriesQuery>(), cancellationToken).Received(1);
            await repositoryMock.List(cancellationToken).Received(1);
        });
    }


    [Fact]
    public async Task If_validation_fails_then_errors_must_be_returned()
    {
        // Arrange
        ListCountriesQueryValidator validatorMock = Substitute.For<ListCountriesQueryValidator>();
        ILogger<ListCountriesHandler> loggerMock = Substitute.For<ILogger<ListCountriesHandler>>();
        ICountryReadRepository repositoryMock = Substitute.For<ICountryReadRepository>();
        CancellationToken cancellationToken = default;

        ListCountriesHandler handler = new(loggerMock, validatorMock, repositoryMock);
        ListCountriesQuery cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult(new ValidationFailure[1] { new("test", "error message") }));

        // Act
        Either<ListCountriesQueryResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

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
            await validatorMock.ValidateAsync(Arg.Any<ListCountriesQuery>(), cancellationToken).Received(1);
            await repositoryMock.List(cancellationToken).Received(0);
        });
    }
}