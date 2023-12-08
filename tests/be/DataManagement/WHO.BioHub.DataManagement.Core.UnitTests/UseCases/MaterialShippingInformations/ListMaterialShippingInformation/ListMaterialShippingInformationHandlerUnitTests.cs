using FluentValidation.Results;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.MaterialShippingInformations;
using WHO.BioHub.DataManagement.Core.UseCases.MaterialShippingInformations.ListMaterialShippingInformations;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UnitTests.UseCases.MaterialShippingInformations.ListMaterialShippingInformations;

public class ListMaterialShippingInformationsHandlerUnitTests
{
    [Fact]
    public async Task If_no_materialshippinginformations_exists_then_empty_array_is_returned()
    {
        // Arrange
        ListMaterialShippingInformationsQueryValidator validatorMock = Substitute.For<ListMaterialShippingInformationsQueryValidator>();
        ILogger<ListMaterialShippingInformationsHandler> loggerMock = Substitute.For<ILogger<ListMaterialShippingInformationsHandler>>();
        IMaterialShippingInformationReadRepository repositoryMock = Substitute.For<IMaterialShippingInformationReadRepository>();
        CancellationToken cancellationToken = default;

        ListMaterialShippingInformationsHandler handler = new(loggerMock, validatorMock, repositoryMock);
        ListMaterialShippingInformationsQuery cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult());

        Guid assignedId = Guid.NewGuid();
        IEnumerable<MaterialShippingInformation> materialshippinginformations = Array.Empty<MaterialShippingInformation>();
        repositoryMock
            .List(cancellationToken)
            .ReturnsForAnyArgs(Task.FromResult(materialshippinginformations));

        // Act
        Either<ListMaterialShippingInformationsQueryResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

        // Assert
        response.Should()
            .NotBeNull(because: "A response is expected in this scenario");
        response.IsLeft.Should()
            .BeTrue(because: "Response is expected in this scenario");
        response.Left.Should()
            .NotBeNull(because: "Response should NOT be null");
        response.Left.MaterialShippingInformations.Should()
            .BeEquivalentTo(materialshippinginformations, because: "Expected returned materialshippinginformations to be equivalent to ones returned from repository");

        Received.InOrder(async () =>
        {
            await validatorMock.ValidateAsync(Arg.Any<ListMaterialShippingInformationsQuery>(), cancellationToken).Received(1);
            await repositoryMock.List(cancellationToken).Received(1);
        });
    }

    [Fact]
    public async Task If_at_least_on_materialshippinginformation_exists_then_it_is_returned()
    {
        // Arrange
        ListMaterialShippingInformationsQueryValidator validatorMock = Substitute.For<ListMaterialShippingInformationsQueryValidator>();
        ILogger<ListMaterialShippingInformationsHandler> loggerMock = Substitute.For<ILogger<ListMaterialShippingInformationsHandler>>();
        IMaterialShippingInformationReadRepository repositoryMock = Substitute.For<IMaterialShippingInformationReadRepository>();
        CancellationToken cancellationToken = default;

        ListMaterialShippingInformationsHandler handler = new(loggerMock, validatorMock, repositoryMock);
        ListMaterialShippingInformationsQuery cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult());

        Guid assignedId = Guid.NewGuid();
        IEnumerable<MaterialShippingInformation> materialshippinginformations = new MaterialShippingInformation[1] { new() };
        repositoryMock
            .List(cancellationToken)
            .ReturnsForAnyArgs(Task.FromResult(materialshippinginformations));

        // Act
        Either<ListMaterialShippingInformationsQueryResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

        // Assert
        response.Should()
            .NotBeNull(because: "A response is expected in this scenario");
        response.IsLeft.Should()
            .BeTrue(because: "Response is expected in this scenario");
        response.Left.Should()
            .NotBeNull(because: "Response should NOT be null");
        response.Left.MaterialShippingInformations.Should()
            .BeEquivalentTo(materialshippinginformations, because: "Expected returned materialshippinginformations to be equivalent to ones returned from repository");

        Received.InOrder(async () =>
        {
            await validatorMock.ValidateAsync(Arg.Any<ListMaterialShippingInformationsQuery>(), cancellationToken).Received(1);
            await repositoryMock.List(cancellationToken).Received(1);
        });
    }


    [Fact]
    public async Task If_validation_fails_then_errors_must_be_returned()
    {
        // Arrange
        ListMaterialShippingInformationsQueryValidator validatorMock = Substitute.For<ListMaterialShippingInformationsQueryValidator>();
        ILogger<ListMaterialShippingInformationsHandler> loggerMock = Substitute.For<ILogger<ListMaterialShippingInformationsHandler>>();
        IMaterialShippingInformationReadRepository repositoryMock = Substitute.For<IMaterialShippingInformationReadRepository>();
        CancellationToken cancellationToken = default;

        ListMaterialShippingInformationsHandler handler = new(loggerMock, validatorMock, repositoryMock);
        ListMaterialShippingInformationsQuery cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult(new ValidationFailure[1] { new("test", "error message") }));

        // Act
        Either<ListMaterialShippingInformationsQueryResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

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
            await validatorMock.ValidateAsync(Arg.Any<ListMaterialShippingInformationsQuery>(), cancellationToken).Received(1);
            await repositoryMock.List(cancellationToken).Received(0);
        });
    }
}