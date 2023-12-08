using FluentValidation.Results;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.MaterialUsagePermissions;
using WHO.BioHub.DataManagement.Core.UseCases.MaterialUsagePermissions.UpdateMaterialUsagePermission;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UnitTests.UseCases.MaterialUsagePermissions.UpdateMaterialUsagePermission;

public class UpdateMaterialUsagePermissionHandlerUnitTests
{
    [Fact]
    public async Task If_resource_is_updated_then_a_valid_response_is_returned()
    {
        // Arrange
        UpdateMaterialUsagePermissionCommandValidator validatorMock = Substitute.For<UpdateMaterialUsagePermissionCommandValidator>();
        ILogger<UpdateMaterialUsagePermissionHandler> loggerMock = Substitute.For<ILogger<UpdateMaterialUsagePermissionHandler>>();
        IMaterialUsagePermissionWriteRepository repositoryMock = Substitute.For<IMaterialUsagePermissionWriteRepository>();
        IUpdateMaterialUsagePermissionMapper mapperMock = Substitute.For<IUpdateMaterialUsagePermissionMapper>();
        CancellationToken cancellationToken = default;

        UpdateMaterialUsagePermissionHandler handler = new(loggerMock, validatorMock, mapperMock, repositoryMock);
        UpdateMaterialUsagePermissionCommand cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult());

        MaterialUsagePermission materialusagepermission = new() { Id = Guid.NewGuid() };
        MaterialUsagePermission materialusagepermissionMapped = new() { Id = materialusagepermission.Id };

        mapperMock.Map(materialusagepermission, cmd).ReturnsForAnyArgs(materialusagepermissionMapped);

        repositoryMock
            .Update(materialusagepermission, cancellationToken)
            .ReturnsForAnyArgs(Task.FromResult<Errors?>(null));

        // Act
        Either<UpdateMaterialUsagePermissionCommandResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

        // Assert
        response.Should()
            .NotBeNull(because: "A response is expected in this scenario");
        response.IsLeft.Should()
            .BeTrue(because: "Response is expected in this scenario");
        response.Left.Should()
            .NotBeNull(because: "Response should NOT be null");
        response.Left.MaterialUsagePermission.Should()
            .NotBeNull(because: "MaterialUsagePermission should NOT be null");
        response.Left.MaterialUsagePermission.Should()
            .BeEquivalentTo(materialusagepermissionMapped, because: "Returned materialusagepermission must mach the one provided in request");

        Received.InOrder(async () =>
        {
            await validatorMock.ValidateAsync(Arg.Any<UpdateMaterialUsagePermissionCommand>(), cancellationToken).Received(1);
            mapperMock.Map(materialusagepermission, Arg.Any<UpdateMaterialUsagePermissionCommand>()).Received(1);
            await repositoryMock.Update(Arg.Any<MaterialUsagePermission>(), cancellationToken).Received(1);
        });
    }

    [Fact]
    public async Task If_validation_fails_then_errors_must_be_returned()
    {
        // Arrange
        UpdateMaterialUsagePermissionCommandValidator validatorMock = Substitute.For<UpdateMaterialUsagePermissionCommandValidator>();
        ILogger<UpdateMaterialUsagePermissionHandler> loggerMock = Substitute.For<ILogger<UpdateMaterialUsagePermissionHandler>>();
        IMaterialUsagePermissionWriteRepository repositoryMock = Substitute.For<IMaterialUsagePermissionWriteRepository>();
        IUpdateMaterialUsagePermissionMapper mapperMock = Substitute.For<IUpdateMaterialUsagePermissionMapper>();
        CancellationToken cancellationToken = default;

        UpdateMaterialUsagePermissionHandler handler = new(loggerMock, validatorMock, mapperMock, repositoryMock);
        UpdateMaterialUsagePermissionCommand cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult(new ValidationFailure[1] { new("test", "error message") }));

        // Act
        Either<UpdateMaterialUsagePermissionCommandResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

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
            await validatorMock.ValidateAsync(Arg.Any<UpdateMaterialUsagePermissionCommand>(), cancellationToken).Received(1);
            mapperMock.Map(Arg.Any<MaterialUsagePermission>(), Arg.Any<UpdateMaterialUsagePermissionCommand>()).Received(0);
            await repositoryMock.Update(Arg.Any<MaterialUsagePermission>(), cancellationToken).Received(0);
        });
    }

    [Fact]
    public async Task If_errors_are_returned_from_repository_then_they_must_be_forwarded_from_handler()
    {
        // Arrange
        UpdateMaterialUsagePermissionCommandValidator validatorMock = Substitute.For<UpdateMaterialUsagePermissionCommandValidator>();
        ILogger<UpdateMaterialUsagePermissionHandler> loggerMock = Substitute.For<ILogger<UpdateMaterialUsagePermissionHandler>>();
        IMaterialUsagePermissionWriteRepository repositoryMock = Substitute.For<IMaterialUsagePermissionWriteRepository>();
        IUpdateMaterialUsagePermissionMapper mapperMock = Substitute.For<IUpdateMaterialUsagePermissionMapper>();
        CancellationToken cancellationToken = default;

        UpdateMaterialUsagePermissionHandler handler = new(loggerMock, validatorMock, mapperMock, repositoryMock);
        UpdateMaterialUsagePermissionCommand cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult());

        MaterialUsagePermission materialusagepermission = new();
        MaterialUsagePermission materialusagepermissionMapped = new();
        mapperMock.Map(materialusagepermission, cmd).ReturnsForAnyArgs(materialusagepermissionMapped);

        // TODO: change error type
        repositoryMock
            .Update(materialusagepermission, cancellationToken)
            .ReturnsForAnyArgs(Task.FromResult<Errors?>(
                new Errors(ErrorType.Validation, "I should be changed to a persistence error type")));

        // Act
        Either<UpdateMaterialUsagePermissionCommandResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

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
            await validatorMock.ValidateAsync(Arg.Any<UpdateMaterialUsagePermissionCommand>(), cancellationToken).Received(1);
            mapperMock.Map(Arg.Any<MaterialUsagePermission>(), Arg.Any<UpdateMaterialUsagePermissionCommand>()).Received(1);
            await repositoryMock.Update(Arg.Any<MaterialUsagePermission>(), cancellationToken).Received(1);
        });
    }
}