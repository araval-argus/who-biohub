using FluentValidation.Results;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.MaterialClinicalDetails;
using WHO.BioHub.DataManagement.Core.UseCases.MaterialClinicalDetails.ListMaterialClinicalDetails;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UnitTests.UseCases.MaterialClinicalDetails.ListMaterialClinicalDetails;

public class ListMaterialClinicalDetailsHandlerUnitTests
{
    [Fact]
    public async Task If_no_materialclinicaldetails_exists_then_empty_array_is_returned()
    {
        // Arrange
        ListMaterialClinicalDetailsQueryValidator validatorMock = Substitute.For<ListMaterialClinicalDetailsQueryValidator>();
        ILogger<ListMaterialClinicalDetailsHandler> loggerMock = Substitute.For<ILogger<ListMaterialClinicalDetailsHandler>>();
        IMaterialClinicalDetailReadRepository repositoryMock = Substitute.For<IMaterialClinicalDetailReadRepository>();
        CancellationToken cancellationToken = default;

        ListMaterialClinicalDetailsHandler handler = new(loggerMock, validatorMock, repositoryMock);
        ListMaterialClinicalDetailsQuery cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult());

        Guid assignedId = Guid.NewGuid();
        IEnumerable<MaterialClinicalDetail> materialclinicaldetails = Array.Empty<MaterialClinicalDetail>();
        repositoryMock
            .List(cancellationToken)
            .ReturnsForAnyArgs(Task.FromResult(materialclinicaldetails));

        // Act
        Either<ListMaterialClinicalDetailsQueryResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

        // Assert
        response.Should()
            .NotBeNull(because: "A response is expected in this scenario");
        response.IsLeft.Should()
            .BeTrue(because: "Response is expected in this scenario");
        response.Left.Should()
            .NotBeNull(because: "Response should NOT be null");
        response.Left.MaterialClinicalDetails.Should()
            .BeEquivalentTo(materialclinicaldetails, because: "Expected returned materialclinicaldetails to be equivalent to ones returned from repository");

        Received.InOrder(async () =>
        {
            await validatorMock.ValidateAsync(Arg.Any<ListMaterialClinicalDetailsQuery>(), cancellationToken).Received(1);
            await repositoryMock.List(cancellationToken).Received(1);
        });
    }

    [Fact]
    public async Task If_at_least_on_materialclinicaldetail_exists_then_it_is_returned()
    {
        // Arrange
        ListMaterialClinicalDetailsQueryValidator validatorMock = Substitute.For<ListMaterialClinicalDetailsQueryValidator>();
        ILogger<ListMaterialClinicalDetailsHandler> loggerMock = Substitute.For<ILogger<ListMaterialClinicalDetailsHandler>>();
        IMaterialClinicalDetailReadRepository repositoryMock = Substitute.For<IMaterialClinicalDetailReadRepository>();
        CancellationToken cancellationToken = default;

        ListMaterialClinicalDetailsHandler handler = new(loggerMock, validatorMock, repositoryMock);
        ListMaterialClinicalDetailsQuery cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult());

        Guid assignedId = Guid.NewGuid();
        IEnumerable<MaterialClinicalDetail> materialclinicaldetails = new MaterialClinicalDetail[1] { new() };
        repositoryMock
            .List(cancellationToken)
            .ReturnsForAnyArgs(Task.FromResult(materialclinicaldetails));

        // Act
        Either<ListMaterialClinicalDetailsQueryResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

        // Assert
        response.Should()
            .NotBeNull(because: "A response is expected in this scenario");
        response.IsLeft.Should()
            .BeTrue(because: "Response is expected in this scenario");
        response.Left.Should()
            .NotBeNull(because: "Response should NOT be null");
        response.Left.MaterialClinicalDetails.Should()
            .BeEquivalentTo(materialclinicaldetails, because: "Expected returned materialclinicaldetails to be equivalent to ones returned from repository");

        Received.InOrder(async () =>
        {
            await validatorMock.ValidateAsync(Arg.Any<ListMaterialClinicalDetailsQuery>(), cancellationToken).Received(1);
            await repositoryMock.List(cancellationToken).Received(1);
        });
    }


    [Fact]
    public async Task If_validation_fails_then_errors_must_be_returned()
    {
        // Arrange
        ListMaterialClinicalDetailsQueryValidator validatorMock = Substitute.For<ListMaterialClinicalDetailsQueryValidator>();
        ILogger<ListMaterialClinicalDetailsHandler> loggerMock = Substitute.For<ILogger<ListMaterialClinicalDetailsHandler>>();
        IMaterialClinicalDetailReadRepository repositoryMock = Substitute.For<IMaterialClinicalDetailReadRepository>();
        CancellationToken cancellationToken = default;

        ListMaterialClinicalDetailsHandler handler = new(loggerMock, validatorMock, repositoryMock);
        ListMaterialClinicalDetailsQuery cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult(new ValidationFailure[1] { new("test", "error message") }));

        // Act
        Either<ListMaterialClinicalDetailsQueryResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

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
            await validatorMock.ValidateAsync(Arg.Any<ListMaterialClinicalDetailsQuery>(), cancellationToken).Received(1);
            await repositoryMock.List(cancellationToken).Received(0);
        });
    }
}