using FluentValidation.Results;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.MaterialProducts;
using WHO.BioHub.DataManagement.Core.UseCases.MaterialProducts.ListMaterialProducts;
using WHO.BioHub.Shared.Utils;
using WHO.BioHub.Shared.Dto;

namespace WHO.BioHub.DataManagement.Core.UnitTests.UseCases.MaterialProducts.ListMaterialProducts;

public class ListMaterialProductsHandlerUnitTests
{
    [Fact]
    public async Task If_no_materialproducts_exists_then_empty_array_is_returned()
    {
        // Arrange
        ListMaterialProductsQueryValidator validatorMock = Substitute.For<ListMaterialProductsQueryValidator>();
        ILogger<ListMaterialProductsHandler> loggerMock = Substitute.For<ILogger<ListMaterialProductsHandler>>();
        IMaterialProductReadRepository repositoryMock = Substitute.For<IMaterialProductReadRepository>();
        CancellationToken cancellationToken = default;

        ListMaterialProductsHandler handler = new(loggerMock, validatorMock, repositoryMock);
        ListMaterialProductsQuery cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult());

        Guid assignedId = Guid.NewGuid();
        IEnumerable<MaterialProduct> materialproducts = Array.Empty<MaterialProduct>();
        repositoryMock
            .List(cancellationToken)
            .ReturnsForAnyArgs(Task.FromResult(materialproducts));

        // Act
        Either<ListMaterialProductsQueryResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

        // Assert
        response.Should()
            .NotBeNull(because: "A response is expected in this scenario");
        response.IsLeft.Should()
            .BeTrue(because: "Response is expected in this scenario");
        response.Left.Should()
            .NotBeNull(because: "Response should NOT be null");
        response.Left.MaterialProducts.Should()
            .BeEquivalentTo(materialproducts, because: "Expected returned materialproducts to be equivalent to ones returned from repository");

        Received.InOrder(async () =>
        {
            await validatorMock.ValidateAsync(Arg.Any<ListMaterialProductsQuery>(), cancellationToken).Received(1);
            await repositoryMock.List(cancellationToken).Received(1);
        });
    }

    [Fact]
    public async Task If_at_least_on_materialproduct_exists_then_it_is_returned()
    {
        // Arrange
        ListMaterialProductsQueryValidator validatorMock = Substitute.For<ListMaterialProductsQueryValidator>();
        ILogger<ListMaterialProductsHandler> loggerMock = Substitute.For<ILogger<ListMaterialProductsHandler>>();
        IMaterialProductReadRepository repositoryMock = Substitute.For<IMaterialProductReadRepository>();
        CancellationToken cancellationToken = default;

        ListMaterialProductsHandler handler = new(loggerMock, validatorMock, repositoryMock);
        ListMaterialProductsQuery cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult());

        Guid assignedId = Guid.NewGuid();
        IEnumerable<MaterialProduct> materialproducts = new MaterialProduct[1] { new() { Id = assignedId } };

        IEnumerable<MaterialProductDto> materialproductDtos = new MaterialProductDto[1] { new() { Id = assignedId } };


        repositoryMock
            .List(cancellationToken)
            .ReturnsForAnyArgs(Task.FromResult(materialproducts));

        // Act
        Either<ListMaterialProductsQueryResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

        // Assert
        response.Should()
            .NotBeNull(because: "A response is expected in this scenario");
        response.IsLeft.Should()
            .BeTrue(because: "Response is expected in this scenario");
        response.Left.Should()
            .NotBeNull(because: "Response should NOT be null");
        response.Left.MaterialProducts.Should()
            .BeEquivalentTo(materialproductDtos, because: "Expected returned materialproducts to be equivalent to ones returned from repository");

        Received.InOrder(async () =>
        {
            await validatorMock.ValidateAsync(Arg.Any<ListMaterialProductsQuery>(), cancellationToken).Received(1);
            await repositoryMock.List(cancellationToken).Received(1);
        });
    }


    [Fact]
    public async Task If_validation_fails_then_errors_must_be_returned()
    {
        // Arrange
        ListMaterialProductsQueryValidator validatorMock = Substitute.For<ListMaterialProductsQueryValidator>();
        ILogger<ListMaterialProductsHandler> loggerMock = Substitute.For<ILogger<ListMaterialProductsHandler>>();
        IMaterialProductReadRepository repositoryMock = Substitute.For<IMaterialProductReadRepository>();
        CancellationToken cancellationToken = default;

        ListMaterialProductsHandler handler = new(loggerMock, validatorMock, repositoryMock);
        ListMaterialProductsQuery cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult(new ValidationFailure[1] { new("test", "error message") }));

        // Act
        Either<ListMaterialProductsQueryResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

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
            await validatorMock.ValidateAsync(Arg.Any<ListMaterialProductsQuery>(), cancellationToken).Received(1);
            await repositoryMock.List(cancellationToken).Received(0);
        });
    }
}