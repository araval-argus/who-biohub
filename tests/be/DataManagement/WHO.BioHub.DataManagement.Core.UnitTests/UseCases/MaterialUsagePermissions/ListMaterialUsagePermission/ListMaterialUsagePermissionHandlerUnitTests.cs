using FluentValidation.Results;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.MaterialUsagePermissions;
using WHO.BioHub.DataManagement.Core.UseCases.MaterialUsagePermissions.ListMaterialUsagePermissions;
using WHO.BioHub.Shared.Utils;
using WHO.BioHub.Shared.Dto;

namespace WHO.BioHub.DataManagement.Core.UnitTests.UseCases.MaterialUsagePermissions.ListMaterialUsagePermissions;

public class ListMaterialUsagePermissionsHandlerUnitTests
{
    [Fact]
    public async Task If_no_materialusagepermissions_exists_then_empty_array_is_returned()
    {
        // Arrange
        ListMaterialUsagePermissionsQueryValidator validatorMock = Substitute.For<ListMaterialUsagePermissionsQueryValidator>();
        ILogger<ListMaterialUsagePermissionsHandler> loggerMock = Substitute.For<ILogger<ListMaterialUsagePermissionsHandler>>();
        IMaterialUsagePermissionReadRepository repositoryMock = Substitute.For<IMaterialUsagePermissionReadRepository>();
        CancellationToken cancellationToken = default;

        ListMaterialUsagePermissionsHandler handler = new(loggerMock, validatorMock, repositoryMock);
        ListMaterialUsagePermissionsQuery cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult());

        Guid assignedId = Guid.NewGuid();
        IEnumerable<MaterialUsagePermission> materialusagepermissions = Array.Empty<MaterialUsagePermission>();
        repositoryMock
            .List(cancellationToken)
            .ReturnsForAnyArgs(Task.FromResult(materialusagepermissions));

        // Act
        Either<ListMaterialUsagePermissionsQueryResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

        // Assert
        response.Should()
            .NotBeNull(because: "A response is expected in this scenario");
        response.IsLeft.Should()
            .BeTrue(because: "Response is expected in this scenario");
        response.Left.Should()
            .NotBeNull(because: "Response should NOT be null");
        response.Left.MaterialUsagePermissions.Should()
            .BeEquivalentTo(materialusagepermissions, because: "Expected returned materialusagepermissions to be equivalent to ones returned from repository");

        Received.InOrder(async () =>
        {
            await validatorMock.ValidateAsync(Arg.Any<ListMaterialUsagePermissionsQuery>(), cancellationToken).Received(1);
            await repositoryMock.List(cancellationToken).Received(1);
        });
    }

    [Fact]
    public async Task If_at_least_on_materialusagepermission_exists_then_it_is_returned()
    {
        // Arrange
        ListMaterialUsagePermissionsQueryValidator validatorMock = Substitute.For<ListMaterialUsagePermissionsQueryValidator>();
        ILogger<ListMaterialUsagePermissionsHandler> loggerMock = Substitute.For<ILogger<ListMaterialUsagePermissionsHandler>>();
        IMaterialUsagePermissionReadRepository repositoryMock = Substitute.For<IMaterialUsagePermissionReadRepository>();
        CancellationToken cancellationToken = default;

        ListMaterialUsagePermissionsHandler handler = new(loggerMock, validatorMock, repositoryMock);
        ListMaterialUsagePermissionsQuery cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult());

        Guid assignedId = Guid.NewGuid();
        IEnumerable<MaterialUsagePermission> materialusagepermissions = new MaterialUsagePermission[1] { new() { Id = assignedId } };

        IEnumerable<MaterialUsagePermissionDto> materialusagepermissionDtos = new MaterialUsagePermissionDto[1] { new() { Id = assignedId } };


        repositoryMock
            .List(cancellationToken)
            .ReturnsForAnyArgs(Task.FromResult(materialusagepermissions));

        // Act
        Either<ListMaterialUsagePermissionsQueryResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

        // Assert
        response.Should()
            .NotBeNull(because: "A response is expected in this scenario");
        response.IsLeft.Should()
            .BeTrue(because: "Response is expected in this scenario");
        response.Left.Should()
            .NotBeNull(because: "Response should NOT be null");
        response.Left.MaterialUsagePermissions.Should()
            .BeEquivalentTo(materialusagepermissionDtos, because: "Expected returned materialusagepermissions to be equivalent to ones returned from repository");

        Received.InOrder(async () =>
        {
            await validatorMock.ValidateAsync(Arg.Any<ListMaterialUsagePermissionsQuery>(), cancellationToken).Received(1);
            await repositoryMock.List(cancellationToken).Received(1);
        });
    }


    [Fact]
    public async Task If_validation_fails_then_errors_must_be_returned()
    {
        // Arrange
        ListMaterialUsagePermissionsQueryValidator validatorMock = Substitute.For<ListMaterialUsagePermissionsQueryValidator>();
        ILogger<ListMaterialUsagePermissionsHandler> loggerMock = Substitute.For<ILogger<ListMaterialUsagePermissionsHandler>>();
        IMaterialUsagePermissionReadRepository repositoryMock = Substitute.For<IMaterialUsagePermissionReadRepository>();
        CancellationToken cancellationToken = default;

        ListMaterialUsagePermissionsHandler handler = new(loggerMock, validatorMock, repositoryMock);
        ListMaterialUsagePermissionsQuery cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult(new ValidationFailure[1] { new("test", "error message") }));

        // Act
        Either<ListMaterialUsagePermissionsQueryResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

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
            await validatorMock.ValidateAsync(Arg.Any<ListMaterialUsagePermissionsQuery>(), cancellationToken).Received(1);
            await repositoryMock.List(cancellationToken).Received(0);
        });
    }
}