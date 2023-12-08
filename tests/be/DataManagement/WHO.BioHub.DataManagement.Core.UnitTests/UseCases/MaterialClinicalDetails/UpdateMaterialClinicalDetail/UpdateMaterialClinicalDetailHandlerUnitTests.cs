using FluentValidation.Results;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.MaterialClinicalDetails;
using WHO.BioHub.DataManagement.Core.UseCases.MaterialClinicalDetails.UpdateMaterialClinicalDetail;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UnitTests.UseCases.MaterialClinicalDetails.UpdateMaterialClinicalDetail;

public class UpdateMaterialClinicalDetailHandlerUnitTests
{
    [Fact]
    public async Task If_resource_is_updated_then_a_valid_response_is_returned()
    {
        // Arrange
        UpdateMaterialClinicalDetailCommandValidator validatorMock = Substitute.For<UpdateMaterialClinicalDetailCommandValidator>();
        ILogger<UpdateMaterialClinicalDetailHandler> loggerMock = Substitute.For<ILogger<UpdateMaterialClinicalDetailHandler>>();
        IMaterialClinicalDetailWriteRepository repositoryMock = Substitute.For<IMaterialClinicalDetailWriteRepository>();
        IUpdateMaterialClinicalDetailMapper mapperMock = Substitute.For<IUpdateMaterialClinicalDetailMapper>();
        CancellationToken cancellationToken = default;

        UpdateMaterialClinicalDetailHandler handler = new(loggerMock, validatorMock, mapperMock, repositoryMock);
        UpdateMaterialClinicalDetailCommand cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult());

        MaterialClinicalDetail materialclinicaldetail = new() { Id = Guid.NewGuid() };
        MaterialClinicalDetail materialclinicaldetailMapped = new() { Id = materialclinicaldetail.Id };

        mapperMock.Map(materialclinicaldetail, cmd).ReturnsForAnyArgs(materialclinicaldetailMapped);

        repositoryMock
            .Update(materialclinicaldetail, cancellationToken)
            .ReturnsForAnyArgs(Task.FromResult<Errors?>(null));

        // Act
        Either<UpdateMaterialClinicalDetailCommandResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

        // Assert
        response.Should()
            .NotBeNull(because: "A response is expected in this scenario");
        response.IsLeft.Should()
            .BeTrue(because: "Response is expected in this scenario");
        response.Left.Should()
            .NotBeNull(because: "Response should NOT be null");
        response.Left.MaterialClinicalDetail.Should()
            .NotBeNull(because: "MaterialClinicalDetail should NOT be null");
        response.Left.MaterialClinicalDetail.Should()
            .BeEquivalentTo(materialclinicaldetailMapped, because: "Returned materialclinicaldetail must mach the one provided in request");

        Received.InOrder(async () =>
        {
            await validatorMock.ValidateAsync(Arg.Any<UpdateMaterialClinicalDetailCommand>(), cancellationToken).Received(1);
            mapperMock.Map(materialclinicaldetail, Arg.Any<UpdateMaterialClinicalDetailCommand>()).Received(1);
            await repositoryMock.Update(Arg.Any<MaterialClinicalDetail>(), cancellationToken).Received(1);
        });
    }

    [Fact]
    public async Task If_validation_fails_then_errors_must_be_returned()
    {
        // Arrange
        UpdateMaterialClinicalDetailCommandValidator validatorMock = Substitute.For<UpdateMaterialClinicalDetailCommandValidator>();
        ILogger<UpdateMaterialClinicalDetailHandler> loggerMock = Substitute.For<ILogger<UpdateMaterialClinicalDetailHandler>>();
        IMaterialClinicalDetailWriteRepository repositoryMock = Substitute.For<IMaterialClinicalDetailWriteRepository>();
        IUpdateMaterialClinicalDetailMapper mapperMock = Substitute.For<IUpdateMaterialClinicalDetailMapper>();
        CancellationToken cancellationToken = default;

        UpdateMaterialClinicalDetailHandler handler = new(loggerMock, validatorMock, mapperMock, repositoryMock);
        UpdateMaterialClinicalDetailCommand cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult(new ValidationFailure[1] { new("test", "error message") }));

        // Act
        Either<UpdateMaterialClinicalDetailCommandResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

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
            await validatorMock.ValidateAsync(Arg.Any<UpdateMaterialClinicalDetailCommand>(), cancellationToken).Received(1);
            mapperMock.Map(Arg.Any<MaterialClinicalDetail>(), Arg.Any<UpdateMaterialClinicalDetailCommand>()).Received(0);
            await repositoryMock.Update(Arg.Any<MaterialClinicalDetail>(), cancellationToken).Received(0);
        });
    }

    [Fact]
    public async Task If_errors_are_returned_from_repository_then_they_must_be_forwarded_from_handler()
    {
        // Arrange
        UpdateMaterialClinicalDetailCommandValidator validatorMock = Substitute.For<UpdateMaterialClinicalDetailCommandValidator>();
        ILogger<UpdateMaterialClinicalDetailHandler> loggerMock = Substitute.For<ILogger<UpdateMaterialClinicalDetailHandler>>();
        IMaterialClinicalDetailWriteRepository repositoryMock = Substitute.For<IMaterialClinicalDetailWriteRepository>();
        IUpdateMaterialClinicalDetailMapper mapperMock = Substitute.For<IUpdateMaterialClinicalDetailMapper>();
        CancellationToken cancellationToken = default;

        UpdateMaterialClinicalDetailHandler handler = new(loggerMock, validatorMock, mapperMock, repositoryMock);
        UpdateMaterialClinicalDetailCommand cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult());

        MaterialClinicalDetail materialclinicaldetail = new();
        MaterialClinicalDetail materialclinicaldetailMapped = new();
        mapperMock.Map(materialclinicaldetail, cmd).ReturnsForAnyArgs(materialclinicaldetailMapped);

        // TODO: change error type
        repositoryMock
            .Update(materialclinicaldetail, cancellationToken)
            .ReturnsForAnyArgs(Task.FromResult<Errors?>(
                new Errors(ErrorType.Validation, "I should be changed to a persistence error type")));

        // Act
        Either<UpdateMaterialClinicalDetailCommandResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

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
            await validatorMock.ValidateAsync(Arg.Any<UpdateMaterialClinicalDetailCommand>(), cancellationToken).Received(1);
            mapperMock.Map(Arg.Any<MaterialClinicalDetail>(), Arg.Any<UpdateMaterialClinicalDetailCommand>()).Received(1);
            await repositoryMock.Update(Arg.Any<MaterialClinicalDetail>(), cancellationToken).Received(1);
        });
    }
}