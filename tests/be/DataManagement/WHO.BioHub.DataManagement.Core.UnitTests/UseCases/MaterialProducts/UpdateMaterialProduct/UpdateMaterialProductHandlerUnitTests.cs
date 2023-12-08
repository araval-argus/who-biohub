using FluentValidation.Results;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.MaterialProducts;
using WHO.BioHub.DataManagement.Core.UseCases.MaterialProducts.UpdateMaterialProduct;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UnitTests.UseCases.MaterialProducts.UpdateMaterialProduct;

public class UpdateMaterialProductHandlerUnitTests
{
    [Fact]
    public async Task If_resource_is_updated_then_a_valid_response_is_returned()
    {
        // Arrange
        UpdateMaterialProductCommandValidator validatorMock = Substitute.For<UpdateMaterialProductCommandValidator>();
        ILogger<UpdateMaterialProductHandler> loggerMock = Substitute.For<ILogger<UpdateMaterialProductHandler>>();
        IMaterialProductWriteRepository repositoryMock = Substitute.For<IMaterialProductWriteRepository>();
        IUpdateMaterialProductMapper mapperMock = Substitute.For<IUpdateMaterialProductMapper>();
        CancellationToken cancellationToken = default;

        UpdateMaterialProductHandler handler = new(loggerMock, validatorMock, mapperMock, repositoryMock);
        UpdateMaterialProductCommand cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult());

        MaterialProduct materialproduct = new() { Id = Guid.NewGuid() };
        MaterialProduct materialproductMapped = new() { Id = materialproduct.Id };

        mapperMock.Map(materialproduct, cmd).ReturnsForAnyArgs(materialproductMapped);

        repositoryMock
            .Update(materialproduct, cancellationToken)
            .ReturnsForAnyArgs(Task.FromResult<Errors?>(null));

        // Act
        Either<UpdateMaterialProductCommandResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

        // Assert
        response.Should()
            .NotBeNull(because: "A response is expected in this scenario");
        response.IsLeft.Should()
            .BeTrue(because: "Response is expected in this scenario");
        response.Left.Should()
            .NotBeNull(because: "Response should NOT be null");
        response.Left.MaterialProduct.Should()
            .NotBeNull(because: "MaterialProduct should NOT be null");
        response.Left.MaterialProduct.Should()
            .BeEquivalentTo(materialproductMapped, because: "Returned materialproduct must mach the one provided in request");

        Received.InOrder(async () =>
        {
            await validatorMock.ValidateAsync(Arg.Any<UpdateMaterialProductCommand>(), cancellationToken).Received(1);
            mapperMock.Map(materialproduct, Arg.Any<UpdateMaterialProductCommand>()).Received(1);
            await repositoryMock.Update(Arg.Any<MaterialProduct>(), cancellationToken).Received(1);
        });
    }

    [Fact]
    public async Task If_validation_fails_then_errors_must_be_returned()
    {
        // Arrange
        UpdateMaterialProductCommandValidator validatorMock = Substitute.For<UpdateMaterialProductCommandValidator>();
        ILogger<UpdateMaterialProductHandler> loggerMock = Substitute.For<ILogger<UpdateMaterialProductHandler>>();
        IMaterialProductWriteRepository repositoryMock = Substitute.For<IMaterialProductWriteRepository>();
        IUpdateMaterialProductMapper mapperMock = Substitute.For<IUpdateMaterialProductMapper>();
        CancellationToken cancellationToken = default;

        UpdateMaterialProductHandler handler = new(loggerMock, validatorMock, mapperMock, repositoryMock);
        UpdateMaterialProductCommand cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult(new ValidationFailure[1] { new("test", "error message") }));

        // Act
        Either<UpdateMaterialProductCommandResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

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
            await validatorMock.ValidateAsync(Arg.Any<UpdateMaterialProductCommand>(), cancellationToken).Received(1);
            mapperMock.Map(Arg.Any<MaterialProduct>(), Arg.Any<UpdateMaterialProductCommand>()).Received(0);
            await repositoryMock.Update(Arg.Any<MaterialProduct>(), cancellationToken).Received(0);
        });
    }

    [Fact]
    public async Task If_errors_are_returned_from_repository_then_they_must_be_forwarded_from_handler()
    {
        // Arrange
        UpdateMaterialProductCommandValidator validatorMock = Substitute.For<UpdateMaterialProductCommandValidator>();
        ILogger<UpdateMaterialProductHandler> loggerMock = Substitute.For<ILogger<UpdateMaterialProductHandler>>();
        IMaterialProductWriteRepository repositoryMock = Substitute.For<IMaterialProductWriteRepository>();
        IUpdateMaterialProductMapper mapperMock = Substitute.For<IUpdateMaterialProductMapper>();
        CancellationToken cancellationToken = default;

        UpdateMaterialProductHandler handler = new(loggerMock, validatorMock, mapperMock, repositoryMock);
        UpdateMaterialProductCommand cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult());

        MaterialProduct materialproduct = new();
        MaterialProduct materialproductMapped = new();
        mapperMock.Map(materialproduct, cmd).ReturnsForAnyArgs(materialproductMapped);

        // TODO: change error type
        repositoryMock
            .Update(materialproduct, cancellationToken)
            .ReturnsForAnyArgs(Task.FromResult<Errors?>(
                new Errors(ErrorType.Validation, "I should be changed to a persistence error type")));

        // Act
        Either<UpdateMaterialProductCommandResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

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
            await validatorMock.ValidateAsync(Arg.Any<UpdateMaterialProductCommand>(), cancellationToken).Received(1);
            mapperMock.Map(Arg.Any<MaterialProduct>(), Arg.Any<UpdateMaterialProductCommand>()).Received(1);
            await repositoryMock.Update(Arg.Any<MaterialProduct>(), cancellationToken).Received(1);
        });
    }
}