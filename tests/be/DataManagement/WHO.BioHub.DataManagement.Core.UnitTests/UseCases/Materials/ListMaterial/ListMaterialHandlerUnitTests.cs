using FluentValidation.Results;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.Materials;
using WHO.BioHub.DataManagement.Core.UseCases.Materials.ListMaterials;
using WHO.BioHub.Shared.Utils;
using WHO.BioHub.Shared.Enums;
using WHO.BioHub.Shared.Dto;

namespace WHO.BioHub.DataManagement.Core.UnitTests.UseCases.Materials.ListMaterials;

public class ListMaterialsHandlerUnitTests
{
    [Fact]
    public async Task If_no_materials_exists_then_empty_array_is_returned()
    {
        // Arrange
        ListMaterialsQueryValidator validatorMock = Substitute.For<ListMaterialsQueryValidator>();
        ILogger<ListMaterialsHandler> loggerMock = Substitute.For<ILogger<ListMaterialsHandler>>();
        IMaterialReadRepository repositoryMock = Substitute.For<IMaterialReadRepository>();
        CancellationToken cancellationToken = default;

        IListMaterialsMapper mapperMock = Substitute.For<IListMaterialsMapper>();

        ListMaterialsHandler handler = new(loggerMock, validatorMock, repositoryMock, mapperMock);
        ListMaterialsQuery cmd = new() { RoleType = RoleType.WHO};

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult());

        Guid assignedId = Guid.NewGuid();
        IEnumerable<Material> materials = Array.Empty<Material>();
        repositoryMock
            .List(cancellationToken)
            .ReturnsForAnyArgs(Task.FromResult(materials));

        // Act
        Either<ListMaterialsQueryResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

        // Assert
        response.Should()
            .NotBeNull(because: "A response is expected in this scenario");
        response.IsLeft.Should()
            .BeTrue(because: "Response is expected in this scenario");
        response.Left.Should()
            .NotBeNull(because: "Response should NOT be null");
        response.Left.Materials.Should()
            .BeEquivalentTo(materials, because: "Expected returned materials to be equivalent to ones returned from repository");

        Received.InOrder(async () =>
        {
            await validatorMock.ValidateAsync(Arg.Any<ListMaterialsQuery>(), cancellationToken).Received(1);
            await repositoryMock.List(cancellationToken).Received(1);
        });
    }

    [Fact]
    public async Task If_at_least_on_material_exists_then_it_is_returned()
    {
        // Arrange
        ListMaterialsQueryValidator validatorMock = Substitute.For<ListMaterialsQueryValidator>();
        ILogger<ListMaterialsHandler> loggerMock = Substitute.For<ILogger<ListMaterialsHandler>>();
        IMaterialReadRepository repositoryMock = Substitute.For<IMaterialReadRepository>();
        CancellationToken cancellationToken = default;

        IListMaterialsMapper mapperMock = Substitute.For<IListMaterialsMapper>();

        ListMaterialsHandler handler = new(loggerMock, validatorMock, repositoryMock, mapperMock);        
        ListMaterialsQuery cmd = new() { RoleType = RoleType.WHO };

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult());

        Guid assignedId = Guid.NewGuid();
        IEnumerable<Material> materials = new Material[1] { new() { Id = assignedId } };
        IEnumerable<MaterialViewModel> materialsViewModel = new MaterialViewModel[1] { new() { Id = assignedId } };

        repositoryMock
            .List(cancellationToken)
            .ReturnsForAnyArgs(Task.FromResult(materials));

        mapperMock
            .Map(materials)
            .ReturnsForAnyArgs(materialsViewModel);

        // Act
        Either<ListMaterialsQueryResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

        // Assert
        response.Should()
            .NotBeNull(because: "A response is expected in this scenario");
        response.IsLeft.Should()
            .BeTrue(because: "Response is expected in this scenario");
        response.Left.Should()
            .NotBeNull(because: "Response should NOT be null");
        response.Left.Materials.Should()
            .BeEquivalentTo(materialsViewModel, because: "Expected returned materials to be equivalent to ones returned from repository");

        Received.InOrder(async () =>
        {
            await validatorMock.ValidateAsync(Arg.Any<ListMaterialsQuery>(), cancellationToken).Received(1);
            await repositoryMock.List(cancellationToken).Received(1);
        });
    }


    [Fact]
    public async Task If_validation_fails_then_errors_must_be_returned()
    {
        // Arrange
        ListMaterialsQueryValidator validatorMock = Substitute.For<ListMaterialsQueryValidator>();
        ILogger<ListMaterialsHandler> loggerMock = Substitute.For<ILogger<ListMaterialsHandler>>();
        IMaterialReadRepository repositoryMock = Substitute.For<IMaterialReadRepository>();
        CancellationToken cancellationToken = default;

        IListMaterialsMapper mapperMock = Substitute.For<IListMaterialsMapper>();

        ListMaterialsHandler handler = new(loggerMock, validatorMock, repositoryMock, mapperMock);
        ListMaterialsQuery cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult(new ValidationFailure[1] { new("test", "error message") }));

        // Act
        Either<ListMaterialsQueryResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

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
            await validatorMock.ValidateAsync(Arg.Any<ListMaterialsQuery>(), cancellationToken).Received(1);
            await repositoryMock.List(cancellationToken).Received(0);
        });
    }
}