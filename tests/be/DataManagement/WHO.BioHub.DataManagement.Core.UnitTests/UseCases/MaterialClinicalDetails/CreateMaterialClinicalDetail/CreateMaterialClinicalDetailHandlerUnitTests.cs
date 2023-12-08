using FluentValidation.Results;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.MaterialClinicalDetails;
using WHO.BioHub.DataManagement.Core.UseCases.MaterialClinicalDetails.CreateMaterialClinicalDetail;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UnitTests.UseCases.MaterialClinicalDetails.CreateMaterialClinicalDetail;

public class CreateMaterialClinicalDetailHandlerUnitTests
{
    [Fact]
    public async Task If_resource_is_created_then_a_valid_response_is_returned()
    {
        // Arrange
        CreateMaterialClinicalDetailCommandValidator validatorMock = Substitute.For<CreateMaterialClinicalDetailCommandValidator>();
        ILogger<CreateMaterialClinicalDetailHandler> loggerMock = Substitute.For<ILogger<CreateMaterialClinicalDetailHandler>>();
        IMaterialClinicalDetailWriteRepository repositoryMock = Substitute.For<IMaterialClinicalDetailWriteRepository>();
        ICreateMaterialClinicalDetailMapper mapperMock = Substitute.For<ICreateMaterialClinicalDetailMapper>();
        CancellationToken cancellationToken = default;

        CreateMaterialClinicalDetailHandler handler = new(loggerMock, validatorMock, mapperMock, repositoryMock);
        CreateMaterialClinicalDetailCommand cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult());

        MaterialClinicalDetail materialclinicaldetail = new();
        mapperMock.Map(cmd).ReturnsForAnyArgs(materialclinicaldetail);

        Guid assignedId = Guid.NewGuid();
        repositoryMock
            .Create(materialclinicaldetail, cancellationToken)
            .ReturnsForAnyArgs(
                Task.Run<Either<MaterialClinicalDetail, Errors>>(() =>
                {
                    materialclinicaldetail.Id = assignedId;
                    return new(materialclinicaldetail);
                }));

        // Act
        Either<CreateMaterialClinicalDetailCommandResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

        // Assert
        response.Should()
            .NotBeNull(because: "A response is expected in this scenario");
        response.IsLeft.Should()
            .BeTrue(because: "Response is expected in this scenario");
        response.Left.Should()
            .NotBeNull(because: "Response should NOT be null");
        response.Left.MaterialClinicalDetail.Should()
            .NotBeNull(because: "MaterialClinicalDetail should NOT be null");
        response.Left.MaterialClinicalDetail.Id.Should()
            .NotBeEmpty(because: "An Id should be assigned to the MaterialClinicalDetail")
            .And.Be(assignedId, because: "Returned materialclinicaldetail Id must mach the one assigned by repository");

        Received.InOrder(async () =>
        {
            await validatorMock.ValidateAsync(Arg.Any<CreateMaterialClinicalDetailCommand>(), cancellationToken).Received(1);
            mapperMock.Map(Arg.Any<CreateMaterialClinicalDetailCommand>()).Received(1);
            await repositoryMock.Create(Arg.Any<MaterialClinicalDetail>(), cancellationToken).Received(1);
        });
    }

    [Fact]
    public async Task If_validation_fails_then_errors_must_be_returned()
    {
        // Arrange
        CreateMaterialClinicalDetailCommandValidator validatorMock = Substitute.For<CreateMaterialClinicalDetailCommandValidator>();
        ILogger<CreateMaterialClinicalDetailHandler> loggerMock = Substitute.For<ILogger<CreateMaterialClinicalDetailHandler>>();
        IMaterialClinicalDetailWriteRepository repositoryMock = Substitute.For<IMaterialClinicalDetailWriteRepository>();
        ICreateMaterialClinicalDetailMapper mapperMock = Substitute.For<ICreateMaterialClinicalDetailMapper>();
        CancellationToken cancellationToken = default;

        CreateMaterialClinicalDetailHandler handler = new(loggerMock, validatorMock, mapperMock, repositoryMock);
        CreateMaterialClinicalDetailCommand cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult(new ValidationFailure[1] { new("test", "error message") }));

        // Act
        Either<CreateMaterialClinicalDetailCommandResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

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
            await validatorMock.ValidateAsync(Arg.Any<CreateMaterialClinicalDetailCommand>(), cancellationToken).Received(1);
            mapperMock.Map(Arg.Any<CreateMaterialClinicalDetailCommand>()).Received(0);
            await repositoryMock.Create(Arg.Any<MaterialClinicalDetail>(), cancellationToken).Received(0);
        });
    }

    [Fact]
    public async Task If_errors_are_returned_from_repository_then_they_must_be_forwarded_from_handler()
    {
        // Arrange
        CreateMaterialClinicalDetailCommandValidator validatorMock = Substitute.For<CreateMaterialClinicalDetailCommandValidator>();
        ILogger<CreateMaterialClinicalDetailHandler> loggerMock = Substitute.For<ILogger<CreateMaterialClinicalDetailHandler>>();
        IMaterialClinicalDetailWriteRepository repositoryMock = Substitute.For<IMaterialClinicalDetailWriteRepository>();
        ICreateMaterialClinicalDetailMapper mapperMock = Substitute.For<ICreateMaterialClinicalDetailMapper>();
        CancellationToken cancellationToken = default;

        CreateMaterialClinicalDetailHandler handler = new(loggerMock, validatorMock, mapperMock, repositoryMock);
        CreateMaterialClinicalDetailCommand cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult());

        MaterialClinicalDetail materialclinicaldetail = new();
        mapperMock.Map(cmd).ReturnsForAnyArgs(materialclinicaldetail);

        // TODO: change error type
        repositoryMock
            .Create(materialclinicaldetail, cancellationToken)
            .ReturnsForAnyArgs(Task.FromResult<Either<MaterialClinicalDetail, Errors>>(
                new(new Errors(ErrorType.Validation, "I should be changed to a persistence error type"))));

        // Act
        Either<CreateMaterialClinicalDetailCommandResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

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
            await validatorMock.ValidateAsync(Arg.Any<CreateMaterialClinicalDetailCommand>(), cancellationToken).Received(1);
            mapperMock.Map(Arg.Any<CreateMaterialClinicalDetailCommand>()).Received(1);
            await repositoryMock.Create(Arg.Any<MaterialClinicalDetail>(), cancellationToken).Received(1);
        });
    }
}