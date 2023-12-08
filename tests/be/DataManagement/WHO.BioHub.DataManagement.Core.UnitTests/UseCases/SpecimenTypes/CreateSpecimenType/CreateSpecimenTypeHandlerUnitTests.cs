using FluentValidation.Results;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.SpecimenTypes;
using WHO.BioHub.DataManagement.Core.UseCases.SpecimenTypes.CreateSpecimenType;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UnitTests.UseCases.SpecimenTypes.CreateSpecimenType;

public class CreateSpecimenTypeHandlerUnitTests
{
    [Fact]
    public async Task If_resource_is_created_then_a_valid_response_is_returned()
    {
        // Arrange
        CreateSpecimenTypeCommandValidator validatorMock = Substitute.For<CreateSpecimenTypeCommandValidator>();
        ILogger<CreateSpecimenTypeHandler> loggerMock = Substitute.For<ILogger<CreateSpecimenTypeHandler>>();
        ISpecimenTypeWriteRepository repositoryMock = Substitute.For<ISpecimenTypeWriteRepository>();
        ICreateSpecimenTypeMapper mapperMock = Substitute.For<ICreateSpecimenTypeMapper>();
        CancellationToken cancellationToken = default;

        CreateSpecimenTypeHandler handler = new(loggerMock, validatorMock, mapperMock, repositoryMock);
        CreateSpecimenTypeCommand cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult());

        SpecimenType specimenttype = new();
        mapperMock.Map(cmd).ReturnsForAnyArgs(specimenttype);

        Guid assignedId = Guid.NewGuid();
        repositoryMock
            .Create(specimenttype, cancellationToken)
            .ReturnsForAnyArgs(
                Task.Run<Either<SpecimenType, Errors>>(() =>
                {
                    specimenttype.Id = assignedId;
                    return new(specimenttype);
                }));

        // Act
        Either<CreateSpecimenTypeCommandResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

        // Assert
        response.Should()
            .NotBeNull(because: "A response is expected in this scenario");
        response.IsLeft.Should()
            .BeTrue(because: "Response is expected in this scenario");
        response.Left.Should()
            .NotBeNull(because: "Response should NOT be null");
        response.Left.SpecimenType.Should()
            .NotBeNull(because: "SpecimenType should NOT be null");
        response.Left.SpecimenType.Id.Should()
            .NotBeEmpty(because: "An Id should be assigned to the SpecimenType")
            .And.Be(assignedId, because: "Returned specimenttype Id must mach the one assigned by repository");

        Received.InOrder(async () =>
        {
            await validatorMock.ValidateAsync(Arg.Any<CreateSpecimenTypeCommand>(), cancellationToken).Received(1);
            mapperMock.Map(Arg.Any<CreateSpecimenTypeCommand>()).Received(1);
            await repositoryMock.Create(Arg.Any<SpecimenType>(), cancellationToken).Received(1);
        });
    }

    [Fact]
    public async Task If_validation_fails_then_errors_must_be_returned()
    {
        // Arrange
        CreateSpecimenTypeCommandValidator validatorMock = Substitute.For<CreateSpecimenTypeCommandValidator>();
        ILogger<CreateSpecimenTypeHandler> loggerMock = Substitute.For<ILogger<CreateSpecimenTypeHandler>>();
        ISpecimenTypeWriteRepository repositoryMock = Substitute.For<ISpecimenTypeWriteRepository>();
        ICreateSpecimenTypeMapper mapperMock = Substitute.For<ICreateSpecimenTypeMapper>();
        CancellationToken cancellationToken = default;

        CreateSpecimenTypeHandler handler = new(loggerMock, validatorMock, mapperMock, repositoryMock);
        CreateSpecimenTypeCommand cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult(new ValidationFailure[1] { new("test", "error message") }));

        // Act
        Either<CreateSpecimenTypeCommandResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

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
            await validatorMock.ValidateAsync(Arg.Any<CreateSpecimenTypeCommand>(), cancellationToken).Received(1);
            mapperMock.Map(Arg.Any<CreateSpecimenTypeCommand>()).Received(0);
            await repositoryMock.Create(Arg.Any<SpecimenType>(), cancellationToken).Received(0);
        });
    }

    [Fact]
    public async Task If_errors_are_returned_from_repository_then_they_must_be_forwarded_from_handler()
    {
        // Arrange
        CreateSpecimenTypeCommandValidator validatorMock = Substitute.For<CreateSpecimenTypeCommandValidator>();
        ILogger<CreateSpecimenTypeHandler> loggerMock = Substitute.For<ILogger<CreateSpecimenTypeHandler>>();
        ISpecimenTypeWriteRepository repositoryMock = Substitute.For<ISpecimenTypeWriteRepository>();
        ICreateSpecimenTypeMapper mapperMock = Substitute.For<ICreateSpecimenTypeMapper>();
        CancellationToken cancellationToken = default;

        CreateSpecimenTypeHandler handler = new(loggerMock, validatorMock, mapperMock, repositoryMock);
        CreateSpecimenTypeCommand cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult());

        SpecimenType specimenttype = new();
        mapperMock.Map(cmd).ReturnsForAnyArgs(specimenttype);

        // TODO: change error type
        repositoryMock
            .Create(specimenttype, cancellationToken)
            .ReturnsForAnyArgs(Task.FromResult<Either<SpecimenType, Errors>>(
                new(new Errors(ErrorType.Validation, "I should be changed to a persistence error type"))));

        // Act
        Either<CreateSpecimenTypeCommandResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

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
            await validatorMock.ValidateAsync(Arg.Any<CreateSpecimenTypeCommand>(), cancellationToken).Received(1);
            mapperMock.Map(Arg.Any<CreateSpecimenTypeCommand>()).Received(1);
            await repositoryMock.Create(Arg.Any<SpecimenType>(), cancellationToken).Received(1);
        });
    }
}