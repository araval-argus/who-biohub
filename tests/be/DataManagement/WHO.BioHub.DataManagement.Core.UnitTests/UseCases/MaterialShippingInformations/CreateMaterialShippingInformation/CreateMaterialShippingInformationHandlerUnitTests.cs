using FluentValidation.Results;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.MaterialShippingInformations;
using WHO.BioHub.DataManagement.Core.UseCases.MaterialShippingInformations.CreateMaterialShippingInformation;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UnitTests.UseCases.MaterialShippingInformations.CreateMaterialShippingInformation;

public class CreateMaterialShippingInformationHandlerUnitTests
{
    [Fact]
    public async Task If_resource_is_created_then_a_valid_response_is_returned()
    {
        // Arrange
        CreateMaterialShippingInformationCommandValidator validatorMock = Substitute.For<CreateMaterialShippingInformationCommandValidator>();
        ILogger<CreateMaterialShippingInformationHandler> loggerMock = Substitute.For<ILogger<CreateMaterialShippingInformationHandler>>();
        IMaterialShippingInformationWriteRepository repositoryMock = Substitute.For<IMaterialShippingInformationWriteRepository>();
        ICreateMaterialShippingInformationMapper mapperMock = Substitute.For<ICreateMaterialShippingInformationMapper>();
        CancellationToken cancellationToken = default;

        CreateMaterialShippingInformationHandler handler = new(loggerMock, validatorMock, mapperMock, repositoryMock);
        CreateMaterialShippingInformationCommand cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult());

        MaterialShippingInformation materialshippinginformation = new();
        mapperMock.Map(cmd).ReturnsForAnyArgs(materialshippinginformation);

        Guid assignedId = Guid.NewGuid();
        repositoryMock
            .Create(materialshippinginformation, cancellationToken)
            .ReturnsForAnyArgs(
                Task.Run<Either<MaterialShippingInformation, Errors>>(() =>
                {
                    materialshippinginformation.Id = assignedId;
                    return new(materialshippinginformation);
                }));

        // Act
        Either<CreateMaterialShippingInformationCommandResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

        // Assert
        response.Should()
            .NotBeNull(because: "A response is expected in this scenario");
        response.IsLeft.Should()
            .BeTrue(because: "Response is expected in this scenario");
        response.Left.Should()
            .NotBeNull(because: "Response should NOT be null");
        response.Left.MaterialShippingInformation.Should()
            .NotBeNull(because: "MaterialShippingInformation should NOT be null");
        response.Left.MaterialShippingInformation.Id.Should()
            .NotBeEmpty(because: "An Id should be assigned to the MaterialShippingInformation")
            .And.Be(assignedId, because: "Returned materialshippinginformation Id must mach the one assigned by repository");

        Received.InOrder(async () =>
        {
            await validatorMock.ValidateAsync(Arg.Any<CreateMaterialShippingInformationCommand>(), cancellationToken).Received(1);
            mapperMock.Map(Arg.Any<CreateMaterialShippingInformationCommand>()).Received(1);
            await repositoryMock.Create(Arg.Any<MaterialShippingInformation>(), cancellationToken).Received(1);
        });
    }

    [Fact]
    public async Task If_validation_fails_then_errors_must_be_returned()
    {
        // Arrange
        CreateMaterialShippingInformationCommandValidator validatorMock = Substitute.For<CreateMaterialShippingInformationCommandValidator>();
        ILogger<CreateMaterialShippingInformationHandler> loggerMock = Substitute.For<ILogger<CreateMaterialShippingInformationHandler>>();
        IMaterialShippingInformationWriteRepository repositoryMock = Substitute.For<IMaterialShippingInformationWriteRepository>();
        ICreateMaterialShippingInformationMapper mapperMock = Substitute.For<ICreateMaterialShippingInformationMapper>();
        CancellationToken cancellationToken = default;

        CreateMaterialShippingInformationHandler handler = new(loggerMock, validatorMock, mapperMock, repositoryMock);
        CreateMaterialShippingInformationCommand cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult(new ValidationFailure[1] { new("test", "error message") }));

        // Act
        Either<CreateMaterialShippingInformationCommandResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

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
            await validatorMock.ValidateAsync(Arg.Any<CreateMaterialShippingInformationCommand>(), cancellationToken).Received(1);
            mapperMock.Map(Arg.Any<CreateMaterialShippingInformationCommand>()).Received(0);
            await repositoryMock.Create(Arg.Any<MaterialShippingInformation>(), cancellationToken).Received(0);
        });
    }

    [Fact]
    public async Task If_errors_are_returned_from_repository_then_they_must_be_forwarded_from_handler()
    {
        // Arrange
        CreateMaterialShippingInformationCommandValidator validatorMock = Substitute.For<CreateMaterialShippingInformationCommandValidator>();
        ILogger<CreateMaterialShippingInformationHandler> loggerMock = Substitute.For<ILogger<CreateMaterialShippingInformationHandler>>();
        IMaterialShippingInformationWriteRepository repositoryMock = Substitute.For<IMaterialShippingInformationWriteRepository>();
        ICreateMaterialShippingInformationMapper mapperMock = Substitute.For<ICreateMaterialShippingInformationMapper>();
        CancellationToken cancellationToken = default;

        CreateMaterialShippingInformationHandler handler = new(loggerMock, validatorMock, mapperMock, repositoryMock);
        CreateMaterialShippingInformationCommand cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult());

        MaterialShippingInformation materialshippinginformation = new();
        mapperMock.Map(cmd).ReturnsForAnyArgs(materialshippinginformation);

        // TODO: change error type
        repositoryMock
            .Create(materialshippinginformation, cancellationToken)
            .ReturnsForAnyArgs(Task.FromResult<Either<MaterialShippingInformation, Errors>>(
                new(new Errors(ErrorType.Validation, "I should be changed to a persistence error type"))));

        // Act
        Either<CreateMaterialShippingInformationCommandResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

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
            await validatorMock.ValidateAsync(Arg.Any<CreateMaterialShippingInformationCommand>(), cancellationToken).Received(1);
            mapperMock.Map(Arg.Any<CreateMaterialShippingInformationCommand>()).Received(1);
            await repositoryMock.Create(Arg.Any<MaterialShippingInformation>(), cancellationToken).Received(1);
        });
    }
}