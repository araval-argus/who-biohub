using FluentValidation.Results;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.MaterialTypes;
using WHO.BioHub.DataManagement.Core.UseCases.MaterialTypes.UpdateMaterialType;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UnitTests.UseCases.MaterialTypes.UpdateMaterialType;

public class UpdateMaterialTypeHandlerUnitTests
{
    [Fact]
    public async Task If_resource_is_updated_then_a_valid_response_is_returned()
    {
        // Arrange
        UpdateMaterialTypeCommandValidator validatorMock = Substitute.For<UpdateMaterialTypeCommandValidator>();
        ILogger<UpdateMaterialTypeHandler> loggerMock = Substitute.For<ILogger<UpdateMaterialTypeHandler>>();
        IMaterialTypeWriteRepository repositoryMock = Substitute.For<IMaterialTypeWriteRepository>();
        IUpdateMaterialTypeMapper mapperMock = Substitute.For<IUpdateMaterialTypeMapper>();
        CancellationToken cancellationToken = default;

        UpdateMaterialTypeHandler handler = new(loggerMock, validatorMock, mapperMock, repositoryMock);
        UpdateMaterialTypeCommand cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult());

        MaterialType materialtype = new() { Id = Guid.NewGuid() };
        MaterialType materialtypeMapped = new() { Id = materialtype.Id };

        mapperMock.Map(materialtype, cmd).ReturnsForAnyArgs(materialtypeMapped);

        repositoryMock
            .Update(materialtype, cancellationToken)
            .ReturnsForAnyArgs(Task.FromResult<Errors?>(null));

        // Act
        Either<UpdateMaterialTypeCommandResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

        // Assert
        response.Should()
            .NotBeNull(because: "A response is expected in this scenario");
        response.IsLeft.Should()
            .BeTrue(because: "Response is expected in this scenario");
        response.Left.Should()
            .NotBeNull(because: "Response should NOT be null");
        response.Left.MaterialType.Should()
            .NotBeNull(because: "MaterialType should NOT be null");
        response.Left.MaterialType.Should()
            .BeEquivalentTo(materialtypeMapped, because: "Returned materialtype must mach the one provided in request");

        Received.InOrder(async () =>
        {
            await validatorMock.ValidateAsync(Arg.Any<UpdateMaterialTypeCommand>(), cancellationToken).Received(1);
            mapperMock.Map(materialtype, Arg.Any<UpdateMaterialTypeCommand>()).Received(1);
            await repositoryMock.Update(Arg.Any<MaterialType>(), cancellationToken).Received(1);
        });
    }

    [Fact]
    public async Task If_validation_fails_then_errors_must_be_returned()
    {
        // Arrange
        UpdateMaterialTypeCommandValidator validatorMock = Substitute.For<UpdateMaterialTypeCommandValidator>();
        ILogger<UpdateMaterialTypeHandler> loggerMock = Substitute.For<ILogger<UpdateMaterialTypeHandler>>();
        IMaterialTypeWriteRepository repositoryMock = Substitute.For<IMaterialTypeWriteRepository>();
        IUpdateMaterialTypeMapper mapperMock = Substitute.For<IUpdateMaterialTypeMapper>();
        CancellationToken cancellationToken = default;

        UpdateMaterialTypeHandler handler = new(loggerMock, validatorMock, mapperMock, repositoryMock);
        UpdateMaterialTypeCommand cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult(new ValidationFailure[1] { new("test", "error message") }));

        // Act
        Either<UpdateMaterialTypeCommandResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

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
            await validatorMock.ValidateAsync(Arg.Any<UpdateMaterialTypeCommand>(), cancellationToken).Received(1);
            mapperMock.Map(Arg.Any<MaterialType>(), Arg.Any<UpdateMaterialTypeCommand>()).Received(0);
            await repositoryMock.Update(Arg.Any<MaterialType>(), cancellationToken).Received(0);
        });
    }

    [Fact]
    public async Task If_errors_are_returned_from_repository_then_they_must_be_forwarded_from_handler()
    {
        // Arrange
        UpdateMaterialTypeCommandValidator validatorMock = Substitute.For<UpdateMaterialTypeCommandValidator>();
        ILogger<UpdateMaterialTypeHandler> loggerMock = Substitute.For<ILogger<UpdateMaterialTypeHandler>>();
        IMaterialTypeWriteRepository repositoryMock = Substitute.For<IMaterialTypeWriteRepository>();
        IUpdateMaterialTypeMapper mapperMock = Substitute.For<IUpdateMaterialTypeMapper>();
        CancellationToken cancellationToken = default;

        UpdateMaterialTypeHandler handler = new(loggerMock, validatorMock, mapperMock, repositoryMock);
        UpdateMaterialTypeCommand cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult());

        MaterialType materialtype = new();
        MaterialType materialtypeMapped = new();
        mapperMock.Map(materialtype, cmd).ReturnsForAnyArgs(materialtypeMapped);

        // TODO: change error type
        repositoryMock
            .Update(materialtype, cancellationToken)
            .ReturnsForAnyArgs(Task.FromResult<Errors?>(
                new Errors(ErrorType.Validation, "I should be changed to a persistence error type")));

        // Act
        Either<UpdateMaterialTypeCommandResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

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
            await validatorMock.ValidateAsync(Arg.Any<UpdateMaterialTypeCommand>(), cancellationToken).Received(1);
            mapperMock.Map(Arg.Any<MaterialType>(), Arg.Any<UpdateMaterialTypeCommand>()).Received(1);
            await repositoryMock.Update(Arg.Any<MaterialType>(), cancellationToken).Received(1);
        });
    }
}