using FluentValidation.Results;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.MaterialShippingInformations;
using WHO.BioHub.DataManagement.Core.UseCases.MaterialShippingInformations.UpdateMaterialShippingInformation;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UnitTests.UseCases.MaterialShippingInformations.UpdateMaterialShippingInformation;

public class UpdateMaterialShippingInformationHandlerUnitTests
{
    [Fact]
    public async Task If_resource_is_updated_then_a_valid_response_is_returned()
    {
        // Arrange
        UpdateMaterialShippingInformationCommandValidator validatorMock = Substitute.For<UpdateMaterialShippingInformationCommandValidator>();
        ILogger<UpdateMaterialShippingInformationHandler> loggerMock = Substitute.For<ILogger<UpdateMaterialShippingInformationHandler>>();
        IMaterialShippingInformationWriteRepository repositoryMock = Substitute.For<IMaterialShippingInformationWriteRepository>();
        IUpdateMaterialShippingInformationMapper mapperMock = Substitute.For<IUpdateMaterialShippingInformationMapper>();
        CancellationToken cancellationToken = default;

        UpdateMaterialShippingInformationHandler handler = new(loggerMock, validatorMock, mapperMock, repositoryMock);
        UpdateMaterialShippingInformationCommand cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult());

        MaterialShippingInformation materialshippinginformation = new() { Id = Guid.NewGuid() };
        MaterialShippingInformation materialshippinginformationMapped = new() { Id = materialshippinginformation.Id };

        mapperMock.Map(materialshippinginformation, cmd).ReturnsForAnyArgs(materialshippinginformationMapped);

        repositoryMock
            .Update(materialshippinginformation, cancellationToken)
            .ReturnsForAnyArgs(Task.FromResult<Errors?>(null));

        // Act
        Either<UpdateMaterialShippingInformationCommandResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

        // Assert
        response.Should()
            .NotBeNull(because: "A response is expected in this scenario");
        response.IsLeft.Should()
            .BeTrue(because: "Response is expected in this scenario");
        response.Left.Should()
            .NotBeNull(because: "Response should NOT be null");
        response.Left.MaterialShippingInformation.Should()
            .NotBeNull(because: "MaterialShippingInformation should NOT be null");
        response.Left.MaterialShippingInformation.Should()
            .BeEquivalentTo(materialshippinginformationMapped, because: "Returned materialshippinginformation must mach the one provided in request");

        Received.InOrder(async () =>
        {
            await validatorMock.ValidateAsync(Arg.Any<UpdateMaterialShippingInformationCommand>(), cancellationToken).Received(1);
            mapperMock.Map(materialshippinginformation, Arg.Any<UpdateMaterialShippingInformationCommand>()).Received(1);
            await repositoryMock.Update(Arg.Any<MaterialShippingInformation>(), cancellationToken).Received(1);
        });
    }

    [Fact]
    public async Task If_validation_fails_then_errors_must_be_returned()
    {
        // Arrange
        UpdateMaterialShippingInformationCommandValidator validatorMock = Substitute.For<UpdateMaterialShippingInformationCommandValidator>();
        ILogger<UpdateMaterialShippingInformationHandler> loggerMock = Substitute.For<ILogger<UpdateMaterialShippingInformationHandler>>();
        IMaterialShippingInformationWriteRepository repositoryMock = Substitute.For<IMaterialShippingInformationWriteRepository>();
        IUpdateMaterialShippingInformationMapper mapperMock = Substitute.For<IUpdateMaterialShippingInformationMapper>();
        CancellationToken cancellationToken = default;

        UpdateMaterialShippingInformationHandler handler = new(loggerMock, validatorMock, mapperMock, repositoryMock);
        UpdateMaterialShippingInformationCommand cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult(new ValidationFailure[1] { new("test", "error message") }));

        // Act
        Either<UpdateMaterialShippingInformationCommandResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

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
            await validatorMock.ValidateAsync(Arg.Any<UpdateMaterialShippingInformationCommand>(), cancellationToken).Received(1);
            mapperMock.Map(Arg.Any<MaterialShippingInformation>(), Arg.Any<UpdateMaterialShippingInformationCommand>()).Received(0);
            await repositoryMock.Update(Arg.Any<MaterialShippingInformation>(), cancellationToken).Received(0);
        });
    }

    [Fact]
    public async Task If_errors_are_returned_from_repository_then_they_must_be_forwarded_from_handler()
    {
        // Arrange
        UpdateMaterialShippingInformationCommandValidator validatorMock = Substitute.For<UpdateMaterialShippingInformationCommandValidator>();
        ILogger<UpdateMaterialShippingInformationHandler> loggerMock = Substitute.For<ILogger<UpdateMaterialShippingInformationHandler>>();
        IMaterialShippingInformationWriteRepository repositoryMock = Substitute.For<IMaterialShippingInformationWriteRepository>();
        IUpdateMaterialShippingInformationMapper mapperMock = Substitute.For<IUpdateMaterialShippingInformationMapper>();
        CancellationToken cancellationToken = default;

        UpdateMaterialShippingInformationHandler handler = new(loggerMock, validatorMock, mapperMock, repositoryMock);
        UpdateMaterialShippingInformationCommand cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult());

        MaterialShippingInformation materialshippinginformation = new();
        MaterialShippingInformation materialshippinginformationMapped = new();
        mapperMock.Map(materialshippinginformation, cmd).ReturnsForAnyArgs(materialshippinginformationMapped);

        // TODO: change error type
        repositoryMock
            .Update(materialshippinginformation, cancellationToken)
            .ReturnsForAnyArgs(Task.FromResult<Errors?>(
                new Errors(ErrorType.Validation, "I should be changed to a persistence error type")));

        // Act
        Either<UpdateMaterialShippingInformationCommandResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

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
            await validatorMock.ValidateAsync(Arg.Any<UpdateMaterialShippingInformationCommand>(), cancellationToken).Received(1);
            mapperMock.Map(Arg.Any<MaterialShippingInformation>(), Arg.Any<UpdateMaterialShippingInformationCommand>()).Received(1);
            await repositoryMock.Update(Arg.Any<MaterialShippingInformation>(), cancellationToken).Received(1);
        });
    }
}