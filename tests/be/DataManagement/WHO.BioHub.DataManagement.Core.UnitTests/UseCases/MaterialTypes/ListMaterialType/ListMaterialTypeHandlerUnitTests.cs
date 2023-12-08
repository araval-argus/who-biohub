using FluentValidation.Results;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.MaterialTypes;
using WHO.BioHub.DataManagement.Core.UseCases.MaterialTypes.ListMaterialTypes;
using WHO.BioHub.Shared.Utils;
using WHO.BioHub.Shared.Dto;

namespace WHO.BioHub.DataManagement.Core.UnitTests.UseCases.MaterialTypes.ListMaterialTypes;

public class ListMaterialTypesHandlerUnitTests
{
    [Fact]
    public async Task If_no_materialtypes_exists_then_empty_array_is_returned()
    {
        // Arrange
        ListMaterialTypesQueryValidator validatorMock = Substitute.For<ListMaterialTypesQueryValidator>();
        ILogger<ListMaterialTypesHandler> loggerMock = Substitute.For<ILogger<ListMaterialTypesHandler>>();
        IMaterialTypeReadRepository repositoryMock = Substitute.For<IMaterialTypeReadRepository>();
        CancellationToken cancellationToken = default;

        ListMaterialTypesHandler handler = new(loggerMock, validatorMock, repositoryMock);
        ListMaterialTypesQuery cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult());

        Guid assignedId = Guid.NewGuid();
        IEnumerable<MaterialType> materialtypes = Array.Empty<MaterialType>();
        repositoryMock
            .List(cancellationToken)
            .ReturnsForAnyArgs(Task.FromResult(materialtypes));

        // Act
        Either<ListMaterialTypesQueryResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

        // Assert
        response.Should()
            .NotBeNull(because: "A response is expected in this scenario");
        response.IsLeft.Should()
            .BeTrue(because: "Response is expected in this scenario");
        response.Left.Should()
            .NotBeNull(because: "Response should NOT be null");
        response.Left.MaterialTypes.Should()
            .BeEquivalentTo(materialtypes, because: "Expected returned materialtypes to be equivalent to ones returned from repository");

        Received.InOrder(async () =>
        {
            await validatorMock.ValidateAsync(Arg.Any<ListMaterialTypesQuery>(), cancellationToken).Received(1);
            await repositoryMock.List(cancellationToken).Received(1);
        });
    }

    [Fact]
    public async Task If_at_least_on_materialtype_exists_then_it_is_returned()
    {
        // Arrange
        ListMaterialTypesQueryValidator validatorMock = Substitute.For<ListMaterialTypesQueryValidator>();
        ILogger<ListMaterialTypesHandler> loggerMock = Substitute.For<ILogger<ListMaterialTypesHandler>>();
        IMaterialTypeReadRepository repositoryMock = Substitute.For<IMaterialTypeReadRepository>();
        CancellationToken cancellationToken = default;

        ListMaterialTypesHandler handler = new(loggerMock, validatorMock, repositoryMock);
        ListMaterialTypesQuery cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult());

        Guid assignedId = Guid.NewGuid();
        IEnumerable<MaterialType> materialtypes = new MaterialType[1] { new() { Id = assignedId } };

        IEnumerable<MaterialTypeDto> materialtypeDtos = new MaterialTypeDto[1] { new() { Id = assignedId } };


        repositoryMock
            .List(cancellationToken)
            .ReturnsForAnyArgs(Task.FromResult(materialtypes));

        // Act
        Either<ListMaterialTypesQueryResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

        // Assert
        response.Should()
            .NotBeNull(because: "A response is expected in this scenario");
        response.IsLeft.Should()
            .BeTrue(because: "Response is expected in this scenario");
        response.Left.Should()
            .NotBeNull(because: "Response should NOT be null");
        response.Left.MaterialTypes.Should()
            .BeEquivalentTo(materialtypeDtos, because: "Expected returned materialtypes to be equivalent to ones returned from repository");

        Received.InOrder(async () =>
        {
            await validatorMock.ValidateAsync(Arg.Any<ListMaterialTypesQuery>(), cancellationToken).Received(1);
            await repositoryMock.List(cancellationToken).Received(1);
        });
    }


    [Fact]
    public async Task If_validation_fails_then_errors_must_be_returned()
    {
        // Arrange
        ListMaterialTypesQueryValidator validatorMock = Substitute.For<ListMaterialTypesQueryValidator>();
        ILogger<ListMaterialTypesHandler> loggerMock = Substitute.For<ILogger<ListMaterialTypesHandler>>();
        IMaterialTypeReadRepository repositoryMock = Substitute.For<IMaterialTypeReadRepository>();
        CancellationToken cancellationToken = default;

        ListMaterialTypesHandler handler = new(loggerMock, validatorMock, repositoryMock);
        ListMaterialTypesQuery cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult(new ValidationFailure[1] { new("test", "error message") }));

        // Act
        Either<ListMaterialTypesQueryResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

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
            await validatorMock.ValidateAsync(Arg.Any<ListMaterialTypesQuery>(), cancellationToken).Received(1);
            await repositoryMock.List(cancellationToken).Received(0);
        });
    }
}