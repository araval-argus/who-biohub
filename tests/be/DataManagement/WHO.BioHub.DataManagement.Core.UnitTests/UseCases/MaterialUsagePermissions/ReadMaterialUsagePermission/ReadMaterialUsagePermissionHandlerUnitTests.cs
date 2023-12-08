using FluentValidation.Results;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.MaterialUsagePermissions;
using WHO.BioHub.DataManagement.Core.UseCases.MaterialUsagePermissions.ReadMaterialUsagePermission;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UnitTests.UseCases.MaterialUsagePermissions.ReadMaterialUsagePermission;

public class ReadMaterialUsagePermissionHandlerUnitTests
{
    [Fact]
    public async Task If_resource_is_read_then_a_valid_response_is_returned()
    {
        // Arrange
        ReadMaterialUsagePermissionQueryValidator validatorMock = Substitute.For<ReadMaterialUsagePermissionQueryValidator>();
        ILogger<ReadMaterialUsagePermissionHandler> loggerMock = Substitute.For<ILogger<ReadMaterialUsagePermissionHandler>>();
        IMaterialUsagePermissionReadRepository repositoryMock = Substitute.For<IMaterialUsagePermissionReadRepository>();
        CancellationToken cancellationToken = default;

        ReadMaterialUsagePermissionHandler handler = new(loggerMock, validatorMock, repositoryMock);
        ReadMaterialUsagePermissionQuery cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult());

        Guid id = Guid.NewGuid();
        MaterialUsagePermission materialusagepermission = new() { Id = id };

        Guid assignedId = Guid.NewGuid();
        repositoryMock
            .Read(id, cancellationToken)
            .ReturnsForAnyArgs(Task.FromResult(materialusagepermission));

        // Act
        Either<ReadMaterialUsagePermissionQueryResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

        // Assert
        response.Should()
            .NotBeNull(because: "A response is expected in this scenario");
        response.IsLeft.Should()
            .BeTrue(because: "Response is expected in this scenario");
        response.Left.Should()
            .NotBeNull(because: "Response should NOT be null");
        response.Left.MaterialUsagePermission.Id.Should()
            .Be(id, because: "Expected id to be the requested one");

        Received.InOrder(async () =>
        {
            await validatorMock.ValidateAsync(Arg.Any<ReadMaterialUsagePermissionQuery>(), cancellationToken).Received(1);
            await repositoryMock.Read(id, cancellationToken).Received(1);
        });
    }

    [Fact]
    public async Task If_validation_fails_then_errors_must_be_returned()
    {
        // Arrange
        ReadMaterialUsagePermissionQueryValidator validatorMock = Substitute.For<ReadMaterialUsagePermissionQueryValidator>();
        ILogger<ReadMaterialUsagePermissionHandler> loggerMock = Substitute.For<ILogger<ReadMaterialUsagePermissionHandler>>();
        IMaterialUsagePermissionReadRepository repositoryMock = Substitute.For<IMaterialUsagePermissionReadRepository>();
        CancellationToken cancellationToken = default;

        ReadMaterialUsagePermissionHandler handler = new(loggerMock, validatorMock, repositoryMock);
        ReadMaterialUsagePermissionQuery cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult(new ValidationFailure[1] { new("test", "error message") }));

        // Act
        Either<ReadMaterialUsagePermissionQueryResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

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
            await validatorMock.ValidateAsync(Arg.Any<ReadMaterialUsagePermissionQuery>(), cancellationToken).Received(1);
            await repositoryMock.Read(Arg.Any<Guid>(), cancellationToken).Received(0);
        });
    }
}