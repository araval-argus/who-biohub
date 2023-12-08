using FluentValidation.Results;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.TransportCategories;
using WHO.BioHub.DataManagement.Core.UseCases.TransportCategories.ListTransportCategories;
using WHO.BioHub.Shared.Utils;
using WHO.BioHub.Shared.Dto;

namespace WHO.BioHub.DataManagement.Core.UnitTests.UseCases.TransportCategories.ListTransportCategories;

public class ListTransportCategoriesHandlerUnitTests
{
    [Fact]
    public async Task If_no_transportcategories_exists_then_empty_array_is_returned()
    {
        // Arrange
        ListTransportCategoriesQueryValidator validatorMock = Substitute.For<ListTransportCategoriesQueryValidator>();
        ILogger<ListTransportCategoriesHandler> loggerMock = Substitute.For<ILogger<ListTransportCategoriesHandler>>();
        ITransportCategoryReadRepository repositoryMock = Substitute.For<ITransportCategoryReadRepository>();
        CancellationToken cancellationToken = default;

        ListTransportCategoriesHandler handler = new(loggerMock, validatorMock, repositoryMock);
        ListTransportCategoriesQuery cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult());

        Guid assignedId = Guid.NewGuid();
        IEnumerable<TransportCategory> transportcategories = Array.Empty<TransportCategory>();
        repositoryMock
            .List(cancellationToken)
            .ReturnsForAnyArgs(Task.FromResult(transportcategories));

        // Act
        Either<ListTransportCategoriesQueryResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

        // Assert
        response.Should()
            .NotBeNull(because: "A response is expected in this scenario");
        response.IsLeft.Should()
            .BeTrue(because: "Response is expected in this scenario");
        response.Left.Should()
            .NotBeNull(because: "Response should NOT be null");
        response.Left.TransportCategories.Should()
            .BeEquivalentTo(transportcategories, because: "Expected returned transportcategories to be equivalent to ones returned from repository");

        Received.InOrder(async () =>
        {
            await validatorMock.ValidateAsync(Arg.Any<ListTransportCategoriesQuery>(), cancellationToken).Received(1);
            await repositoryMock.List(cancellationToken).Received(1);
        });
    }

    [Fact]
    public async Task If_at_least_on_transportcategory_exists_then_it_is_returned()
    {
        // Arrange
        ListTransportCategoriesQueryValidator validatorMock = Substitute.For<ListTransportCategoriesQueryValidator>();
        ILogger<ListTransportCategoriesHandler> loggerMock = Substitute.For<ILogger<ListTransportCategoriesHandler>>();
        ITransportCategoryReadRepository repositoryMock = Substitute.For<ITransportCategoryReadRepository>();
        CancellationToken cancellationToken = default;

        ListTransportCategoriesHandler handler = new(loggerMock, validatorMock, repositoryMock);
        ListTransportCategoriesQuery cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult());

        Guid assignedId = Guid.NewGuid();
        IEnumerable<TransportCategory> transportcategories = new TransportCategory[1] { new() { Id = assignedId } };


        IEnumerable<TransportCategoryDto> transportcategorieDtos = new TransportCategoryDto[1] { new() { Id = assignedId } };

        repositoryMock
            .List(cancellationToken)
            .ReturnsForAnyArgs(Task.FromResult(transportcategories));

        // Act
        Either<ListTransportCategoriesQueryResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

        // Assert
        response.Should()
            .NotBeNull(because: "A response is expected in this scenario");
        response.IsLeft.Should()
            .BeTrue(because: "Response is expected in this scenario");
        response.Left.Should()
            .NotBeNull(because: "Response should NOT be null");
        response.Left.TransportCategories.Should()
            .BeEquivalentTo(transportcategorieDtos, because: "Expected returned transportcategories to be equivalent to ones returned from repository");

        Received.InOrder(async () =>
        {
            await validatorMock.ValidateAsync(Arg.Any<ListTransportCategoriesQuery>(), cancellationToken).Received(1);
            await repositoryMock.List(cancellationToken).Received(1);
        });
    }


    [Fact]
    public async Task If_validation_fails_then_errors_must_be_returned()
    {
        // Arrange
        ListTransportCategoriesQueryValidator validatorMock = Substitute.For<ListTransportCategoriesQueryValidator>();
        ILogger<ListTransportCategoriesHandler> loggerMock = Substitute.For<ILogger<ListTransportCategoriesHandler>>();
        ITransportCategoryReadRepository repositoryMock = Substitute.For<ITransportCategoryReadRepository>();
        CancellationToken cancellationToken = default;

        ListTransportCategoriesHandler handler = new(loggerMock, validatorMock, repositoryMock);
        ListTransportCategoriesQuery cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult(new ValidationFailure[1] { new("test", "error message") }));

        // Act
        Either<ListTransportCategoriesQueryResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

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
            await validatorMock.ValidateAsync(Arg.Any<ListTransportCategoriesQuery>(), cancellationToken).Received(1);
            await repositoryMock.List(cancellationToken).Received(0);
        });
    }
}