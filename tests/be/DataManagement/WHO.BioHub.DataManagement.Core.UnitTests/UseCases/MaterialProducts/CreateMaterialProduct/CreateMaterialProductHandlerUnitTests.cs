using FluentValidation.Results;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.MaterialProducts;
using WHO.BioHub.DataManagement.Core.UseCases.MaterialProducts.CreateMaterialProduct;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UnitTests.UseCases.MaterialProducts.CreateMaterialProduct;

public class CreateMaterialProductHandlerUnitTests
{
    [Fact]
    public async Task If_resource_is_created_then_a_valid_response_is_returned()
    {
        // Arrange
        CreateMaterialProductCommandValidator validatorMock = Substitute.For<CreateMaterialProductCommandValidator>();
        ILogger<CreateMaterialProductHandler> loggerMock = Substitute.For<ILogger<CreateMaterialProductHandler>>();
        IMaterialProductWriteRepository repositoryMock = Substitute.For<IMaterialProductWriteRepository>();
        ICreateMaterialProductMapper mapperMock = Substitute.For<ICreateMaterialProductMapper>();
        CancellationToken cancellationToken = default;

        CreateMaterialProductHandler handler = new(loggerMock, validatorMock, mapperMock, repositoryMock);
        CreateMaterialProductCommand cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult());

        MaterialProduct materialproduct = new();
        mapperMock.Map(cmd).ReturnsForAnyArgs(materialproduct);

        Guid assignedId = Guid.NewGuid();
        repositoryMock
            .Create(materialproduct, cancellationToken)
            .ReturnsForAnyArgs(
                Task.Run<Either<MaterialProduct, Errors>>(() =>
                {
                    materialproduct.Id = assignedId;
                    return new(materialproduct);
                }));

        // Act
        Either<CreateMaterialProductCommandResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

        // Assert
        response.Should()
            .NotBeNull(because: "A response is expected in this scenario");
        response.IsLeft.Should()
            .BeTrue(because: "Response is expected in this scenario");
        response.Left.Should()
            .NotBeNull(because: "Response should NOT be null");
        response.Left.MaterialProduct.Should()
            .NotBeNull(because: "MaterialProduct should NOT be null");
        response.Left.MaterialProduct.Id.Should()
            .NotBeEmpty(because: "An Id should be assigned to the MaterialProduct")
            .And.Be(assignedId, because: "Returned materialproduct Id must mach the one assigned by repository");

        Received.InOrder(async () =>
        {
            await validatorMock.ValidateAsync(Arg.Any<CreateMaterialProductCommand>(), cancellationToken).Received(1);
            mapperMock.Map(Arg.Any<CreateMaterialProductCommand>()).Received(1);
            await repositoryMock.Create(Arg.Any<MaterialProduct>(), cancellationToken).Received(1);
        });
    }

    [Fact]
    public async Task If_validation_fails_then_errors_must_be_returned()
    {
        // Arrange
        CreateMaterialProductCommandValidator validatorMock = Substitute.For<CreateMaterialProductCommandValidator>();
        ILogger<CreateMaterialProductHandler> loggerMock = Substitute.For<ILogger<CreateMaterialProductHandler>>();
        IMaterialProductWriteRepository repositoryMock = Substitute.For<IMaterialProductWriteRepository>();
        ICreateMaterialProductMapper mapperMock = Substitute.For<ICreateMaterialProductMapper>();
        CancellationToken cancellationToken = default;

        CreateMaterialProductHandler handler = new(loggerMock, validatorMock, mapperMock, repositoryMock);
        CreateMaterialProductCommand cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult(new ValidationFailure[1] { new("test", "error message") }));

        // Act
        Either<CreateMaterialProductCommandResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

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
            await validatorMock.ValidateAsync(Arg.Any<CreateMaterialProductCommand>(), cancellationToken).Received(1);
            mapperMock.Map(Arg.Any<CreateMaterialProductCommand>()).Received(0);
            await repositoryMock.Create(Arg.Any<MaterialProduct>(), cancellationToken).Received(0);
        });
    }

    [Fact]
    public async Task If_errors_are_returned_from_repository_then_they_must_be_forwarded_from_handler()
    {
        // Arrange
        CreateMaterialProductCommandValidator validatorMock = Substitute.For<CreateMaterialProductCommandValidator>();
        ILogger<CreateMaterialProductHandler> loggerMock = Substitute.For<ILogger<CreateMaterialProductHandler>>();
        IMaterialProductWriteRepository repositoryMock = Substitute.For<IMaterialProductWriteRepository>();
        ICreateMaterialProductMapper mapperMock = Substitute.For<ICreateMaterialProductMapper>();
        CancellationToken cancellationToken = default;

        CreateMaterialProductHandler handler = new(loggerMock, validatorMock, mapperMock, repositoryMock);
        CreateMaterialProductCommand cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult());

        MaterialProduct materialproduct = new();
        mapperMock.Map(cmd).ReturnsForAnyArgs(materialproduct);

        // TODO: change error type
        repositoryMock
            .Create(materialproduct, cancellationToken)
            .ReturnsForAnyArgs(Task.FromResult<Either<MaterialProduct, Errors>>(
                new(new Errors(ErrorType.Validation, "I should be changed to a persistence error type"))));

        // Act
        Either<CreateMaterialProductCommandResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

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
            await validatorMock.ValidateAsync(Arg.Any<CreateMaterialProductCommand>(), cancellationToken).Received(1);
            mapperMock.Map(Arg.Any<CreateMaterialProductCommand>()).Received(1);
            await repositoryMock.Create(Arg.Any<MaterialProduct>(), cancellationToken).Received(1);
        });
    }
}