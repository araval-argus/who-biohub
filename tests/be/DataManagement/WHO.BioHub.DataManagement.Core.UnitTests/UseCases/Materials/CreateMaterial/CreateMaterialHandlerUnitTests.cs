using FluentValidation.Results;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.Materials;
using WHO.BioHub.DataManagement.Core.UseCases.Materials.CreateMaterial;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UnitTests.UseCases.Materials.CreateMaterial;

public class CreateMaterialHandlerUnitTests
{
    [Fact]
    public async Task If_resource_is_created_then_a_valid_response_is_returned()
    {
        // Arrange
        IMaterialReadRepository readRepositoryMock = Substitute.For<IMaterialReadRepository>();
        CreateMaterialCommandValidator validatorMock = Substitute.For<CreateMaterialCommandValidator>(readRepositoryMock);
        ILogger<CreateMaterialHandler> loggerMock = Substitute.For<ILogger<CreateMaterialHandler>>();
        IMaterialWriteRepository repositoryMock = Substitute.For<IMaterialWriteRepository>();
        ICreateMaterialMapper mapperMock = Substitute.For<ICreateMaterialMapper>();
        CancellationToken cancellationToken = default;

        CreateMaterialHandler handler = new(loggerMock, validatorMock, mapperMock, repositoryMock);
        CreateMaterialCommand cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult());

        Material material = new();
        mapperMock.Map(cmd).ReturnsForAnyArgs(material);

        readRepositoryMock.ReferenceNumberAlreadyPresentForCreation(material.ReferenceNumber, cancellationToken).ReturnsForAnyArgs(Task.FromResult(false));

        Guid assignedId = Guid.NewGuid();
        repositoryMock
            .Create(material, cancellationToken)
            .ReturnsForAnyArgs(
                Task.Run<Either<Material, Errors>>(() =>
                {
                    material.Id = assignedId;
                    return new(material);
                }));

        // Act
        Either<CreateMaterialCommandResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

        // Assert
        response.Should()
            .NotBeNull(because: "A response is expected in this scenario");
        response.IsLeft.Should()
            .BeTrue(because: "Response is expected in this scenario");
        response.Left.Should()
            .NotBeNull(because: "Response should NOT be null");
        response.Left.Should()
            .NotBeNull(because: "Material should NOT be null");
        response.Left.Id.Should()
            .NotBeEmpty(because: "An Id should be assigned to the Material")
            .And.Be(assignedId, because: "Returned material Id must mach the one assigned by repository");

        Received.InOrder(async () =>
        {
            await validatorMock.ValidateAsync(Arg.Any<CreateMaterialCommand>(), cancellationToken).Received(1);
            mapperMock.Map(Arg.Any<CreateMaterialCommand>()).Received(1);
            await repositoryMock.Create(Arg.Any<Material>(), cancellationToken).Received(1);
        });
    }

    [Fact]
    public async Task If_validation_fails_then_errors_must_be_returned()
    {
        // Arrange
        IMaterialReadRepository readRepositoryMock = Substitute.For<IMaterialReadRepository>();
        CreateMaterialCommandValidator validatorMock = Substitute.For<CreateMaterialCommandValidator>(readRepositoryMock);
        ILogger<CreateMaterialHandler> loggerMock = Substitute.For<ILogger<CreateMaterialHandler>>();
        IMaterialWriteRepository repositoryMock = Substitute.For<IMaterialWriteRepository>();
        ICreateMaterialMapper mapperMock = Substitute.For<ICreateMaterialMapper>();
        CancellationToken cancellationToken = default;

        CreateMaterialHandler handler = new(loggerMock, validatorMock, mapperMock, repositoryMock);
        CreateMaterialCommand cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult(new ValidationFailure[1] { new("test", "error message") }));

        // Act
        Either<CreateMaterialCommandResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

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
            await validatorMock.ValidateAsync(Arg.Any<CreateMaterialCommand>(), cancellationToken).Received(1);
            mapperMock.Map(Arg.Any<CreateMaterialCommand>()).Received(0);
            await repositoryMock.Create(Arg.Any<Material>(), cancellationToken).Received(0);
        });
    }

    [Fact]
    public async Task If_errors_are_returned_from_repository_then_they_must_be_forwarded_from_handler()
    {
        // Arrange
        IMaterialReadRepository readRepositoryMock = Substitute.For<IMaterialReadRepository>();
        CreateMaterialCommandValidator validatorMock = Substitute.For<CreateMaterialCommandValidator>(readRepositoryMock);
        ILogger<CreateMaterialHandler> loggerMock = Substitute.For<ILogger<CreateMaterialHandler>>();
        IMaterialWriteRepository repositoryMock = Substitute.For<IMaterialWriteRepository>();
        ICreateMaterialMapper mapperMock = Substitute.For<ICreateMaterialMapper>();
        CancellationToken cancellationToken = default;

        CreateMaterialHandler handler = new(loggerMock, validatorMock, mapperMock, repositoryMock);
        CreateMaterialCommand cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult());

        Material material = new();
        mapperMock.Map(cmd).ReturnsForAnyArgs(material);
        readRepositoryMock.ReferenceNumberAlreadyPresentForCreation(material.ReferenceNumber, cancellationToken).ReturnsForAnyArgs(Task.FromResult(false));

        // TODO: change error type
        repositoryMock
            .Create(material, cancellationToken)
            .ReturnsForAnyArgs(Task.FromResult<Either<Material, Errors>>(
                new(new Errors(ErrorType.Validation, "I should be changed to a persistence error type"))));

        // Act
        Either<CreateMaterialCommandResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

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
            await validatorMock.ValidateAsync(Arg.Any<CreateMaterialCommand>(), cancellationToken).Received(1);
            mapperMock.Map(Arg.Any<CreateMaterialCommand>()).Received(1);
            await repositoryMock.Create(Arg.Any<Material>(), cancellationToken).Received(1);
        });
    }
}