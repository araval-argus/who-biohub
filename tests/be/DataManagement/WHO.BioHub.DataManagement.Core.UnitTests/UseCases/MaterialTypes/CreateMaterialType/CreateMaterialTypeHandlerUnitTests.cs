using FluentValidation.Results;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.MaterialTypes;
using WHO.BioHub.DataManagement.Core.UseCases.MaterialTypes.CreateMaterialType;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UnitTests.UseCases.MaterialTypes.CreateMaterialType;

public class CreateMaterialTypeHandlerUnitTests
{
    [Fact]
    public async Task If_resource_is_created_then_a_valid_response_is_returned()
    {
        // Arrange
        CreateMaterialTypeCommandValidator validatorMock = Substitute.For<CreateMaterialTypeCommandValidator>();
        ILogger<CreateMaterialTypeHandler> loggerMock = Substitute.For<ILogger<CreateMaterialTypeHandler>>();
        IMaterialTypeWriteRepository repositoryMock = Substitute.For<IMaterialTypeWriteRepository>();
        ICreateMaterialTypeMapper mapperMock = Substitute.For<ICreateMaterialTypeMapper>();
        CancellationToken cancellationToken = default;

        CreateMaterialTypeHandler handler = new(loggerMock, validatorMock, mapperMock, repositoryMock);
        CreateMaterialTypeCommand cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult());

        MaterialType materialtype = new();
        mapperMock.Map(cmd).ReturnsForAnyArgs(materialtype);

        Guid assignedId = Guid.NewGuid();
        repositoryMock
            .Create(materialtype, cancellationToken)
            .ReturnsForAnyArgs(
                Task.Run<Either<MaterialType, Errors>>(() =>
                {
                    materialtype.Id = assignedId;
                    return new(materialtype);
                }));

        // Act
        Either<CreateMaterialTypeCommandResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

        // Assert
        response.Should()
            .NotBeNull(because: "A response is expected in this scenario");
        response.IsLeft.Should()
            .BeTrue(because: "Response is expected in this scenario");
        response.Left.Should()
            .NotBeNull(because: "Response should NOT be null");
        response.Left.MaterialType.Should()
            .NotBeNull(because: "MaterialType should NOT be null");
        response.Left.MaterialType.Id.Should()
            .NotBeEmpty(because: "An Id should be assigned to the MaterialType")
            .And.Be(assignedId, because: "Returned materialtype Id must mach the one assigned by repository");

        Received.InOrder(async () =>
        {
            await validatorMock.ValidateAsync(Arg.Any<CreateMaterialTypeCommand>(), cancellationToken).Received(1);
            mapperMock.Map(Arg.Any<CreateMaterialTypeCommand>()).Received(1);
            await repositoryMock.Create(Arg.Any<MaterialType>(), cancellationToken).Received(1);
        });
    }

    [Fact]
    public async Task If_validation_fails_then_errors_must_be_returned()
    {
        // Arrange
        CreateMaterialTypeCommandValidator validatorMock = Substitute.For<CreateMaterialTypeCommandValidator>();
        ILogger<CreateMaterialTypeHandler> loggerMock = Substitute.For<ILogger<CreateMaterialTypeHandler>>();
        IMaterialTypeWriteRepository repositoryMock = Substitute.For<IMaterialTypeWriteRepository>();
        ICreateMaterialTypeMapper mapperMock = Substitute.For<ICreateMaterialTypeMapper>();
        CancellationToken cancellationToken = default;

        CreateMaterialTypeHandler handler = new(loggerMock, validatorMock, mapperMock, repositoryMock);
        CreateMaterialTypeCommand cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult(new ValidationFailure[1] { new("test", "error message") }));

        // Act
        Either<CreateMaterialTypeCommandResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

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
            await validatorMock.ValidateAsync(Arg.Any<CreateMaterialTypeCommand>(), cancellationToken).Received(1);
            mapperMock.Map(Arg.Any<CreateMaterialTypeCommand>()).Received(0);
            await repositoryMock.Create(Arg.Any<MaterialType>(), cancellationToken).Received(0);
        });
    }

    [Fact]
    public async Task If_errors_are_returned_from_repository_then_they_must_be_forwarded_from_handler()
    {
        // Arrange
        CreateMaterialTypeCommandValidator validatorMock = Substitute.For<CreateMaterialTypeCommandValidator>();
        ILogger<CreateMaterialTypeHandler> loggerMock = Substitute.For<ILogger<CreateMaterialTypeHandler>>();
        IMaterialTypeWriteRepository repositoryMock = Substitute.For<IMaterialTypeWriteRepository>();
        ICreateMaterialTypeMapper mapperMock = Substitute.For<ICreateMaterialTypeMapper>();
        CancellationToken cancellationToken = default;

        CreateMaterialTypeHandler handler = new(loggerMock, validatorMock, mapperMock, repositoryMock);
        CreateMaterialTypeCommand cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult());

        MaterialType materialtype = new();
        mapperMock.Map(cmd).ReturnsForAnyArgs(materialtype);

        // TODO: change error type
        repositoryMock
            .Create(materialtype, cancellationToken)
            .ReturnsForAnyArgs(Task.FromResult<Either<MaterialType, Errors>>(
                new(new Errors(ErrorType.Validation, "I should be changed to a persistence error type"))));

        // Act
        Either<CreateMaterialTypeCommandResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

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
            await validatorMock.ValidateAsync(Arg.Any<CreateMaterialTypeCommand>(), cancellationToken).Received(1);
            mapperMock.Map(Arg.Any<CreateMaterialTypeCommand>()).Received(1);
            await repositoryMock.Create(Arg.Any<MaterialType>(), cancellationToken).Received(1);
        });
    }
}