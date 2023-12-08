using FluentValidation.Results;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.MaterialUsagePermissions;
using WHO.BioHub.DataManagement.Core.UseCases.MaterialUsagePermissions.CreateMaterialUsagePermission;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UnitTests.UseCases.MaterialUsagePermissions.CreateMaterialUsagePermission;

public class CreateMaterialUsagePermissionHandlerUnitTests
{
    [Fact]
    public async Task If_resource_is_created_then_a_valid_response_is_returned()
    {
        // Arrange
        CreateMaterialUsagePermissionCommandValidator validatorMock = Substitute.For<CreateMaterialUsagePermissionCommandValidator>();
        ILogger<CreateMaterialUsagePermissionHandler> loggerMock = Substitute.For<ILogger<CreateMaterialUsagePermissionHandler>>();
        IMaterialUsagePermissionWriteRepository repositoryMock = Substitute.For<IMaterialUsagePermissionWriteRepository>();
        ICreateMaterialUsagePermissionMapper mapperMock = Substitute.For<ICreateMaterialUsagePermissionMapper>();
        CancellationToken cancellationToken = default;

        CreateMaterialUsagePermissionHandler handler = new(loggerMock, validatorMock, mapperMock, repositoryMock);
        CreateMaterialUsagePermissionCommand cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult());

        MaterialUsagePermission materialusagepermission = new();
        mapperMock.Map(cmd).ReturnsForAnyArgs(materialusagepermission);

        Guid assignedId = Guid.NewGuid();
        repositoryMock
            .Create(materialusagepermission, cancellationToken)
            .ReturnsForAnyArgs(
                Task.Run<Either<MaterialUsagePermission, Errors>>(() =>
                {
                    materialusagepermission.Id = assignedId;
                    return new(materialusagepermission);
                }));

        // Act
        Either<CreateMaterialUsagePermissionCommandResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

        // Assert
        response.Should()
            .NotBeNull(because: "A response is expected in this scenario");
        response.IsLeft.Should()
            .BeTrue(because: "Response is expected in this scenario");
        response.Left.Should()
            .NotBeNull(because: "Response should NOT be null");
        response.Left.MaterialUsagePermission.Should()
            .NotBeNull(because: "MaterialUsagePermission should NOT be null");
        response.Left.MaterialUsagePermission.Id.Should()
            .NotBeEmpty(because: "An Id should be assigned to the MaterialUsagePermission")
            .And.Be(assignedId, because: "Returned materialusagepermission Id must mach the one assigned by repository");

        Received.InOrder(async () =>
        {
            await validatorMock.ValidateAsync(Arg.Any<CreateMaterialUsagePermissionCommand>(), cancellationToken).Received(1);
            mapperMock.Map(Arg.Any<CreateMaterialUsagePermissionCommand>()).Received(1);
            await repositoryMock.Create(Arg.Any<MaterialUsagePermission>(), cancellationToken).Received(1);
        });
    }

    [Fact]
    public async Task If_validation_fails_then_errors_must_be_returned()
    {
        // Arrange
        CreateMaterialUsagePermissionCommandValidator validatorMock = Substitute.For<CreateMaterialUsagePermissionCommandValidator>();
        ILogger<CreateMaterialUsagePermissionHandler> loggerMock = Substitute.For<ILogger<CreateMaterialUsagePermissionHandler>>();
        IMaterialUsagePermissionWriteRepository repositoryMock = Substitute.For<IMaterialUsagePermissionWriteRepository>();
        ICreateMaterialUsagePermissionMapper mapperMock = Substitute.For<ICreateMaterialUsagePermissionMapper>();
        CancellationToken cancellationToken = default;

        CreateMaterialUsagePermissionHandler handler = new(loggerMock, validatorMock, mapperMock, repositoryMock);
        CreateMaterialUsagePermissionCommand cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult(new ValidationFailure[1] { new("test", "error message") }));

        // Act
        Either<CreateMaterialUsagePermissionCommandResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

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
            await validatorMock.ValidateAsync(Arg.Any<CreateMaterialUsagePermissionCommand>(), cancellationToken).Received(1);
            mapperMock.Map(Arg.Any<CreateMaterialUsagePermissionCommand>()).Received(0);
            await repositoryMock.Create(Arg.Any<MaterialUsagePermission>(), cancellationToken).Received(0);
        });
    }

    [Fact]
    public async Task If_errors_are_returned_from_repository_then_they_must_be_forwarded_from_handler()
    {
        // Arrange
        CreateMaterialUsagePermissionCommandValidator validatorMock = Substitute.For<CreateMaterialUsagePermissionCommandValidator>();
        ILogger<CreateMaterialUsagePermissionHandler> loggerMock = Substitute.For<ILogger<CreateMaterialUsagePermissionHandler>>();
        IMaterialUsagePermissionWriteRepository repositoryMock = Substitute.For<IMaterialUsagePermissionWriteRepository>();
        ICreateMaterialUsagePermissionMapper mapperMock = Substitute.For<ICreateMaterialUsagePermissionMapper>();
        CancellationToken cancellationToken = default;

        CreateMaterialUsagePermissionHandler handler = new(loggerMock, validatorMock, mapperMock, repositoryMock);
        CreateMaterialUsagePermissionCommand cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult());

        MaterialUsagePermission materialusagepermission = new();
        mapperMock.Map(cmd).ReturnsForAnyArgs(materialusagepermission);

        // TODO: change error type
        repositoryMock
            .Create(materialusagepermission, cancellationToken)
            .ReturnsForAnyArgs(Task.FromResult<Either<MaterialUsagePermission, Errors>>(
                new(new Errors(ErrorType.Validation, "I should be changed to a persistence error type"))));

        // Act
        Either<CreateMaterialUsagePermissionCommandResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

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
            await validatorMock.ValidateAsync(Arg.Any<CreateMaterialUsagePermissionCommand>(), cancellationToken).Received(1);
            mapperMock.Map(Arg.Any<CreateMaterialUsagePermissionCommand>()).Received(1);
            await repositoryMock.Create(Arg.Any<MaterialUsagePermission>(), cancellationToken).Received(1);
        });
    }
}