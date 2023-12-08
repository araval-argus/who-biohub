using FluentValidation.Results;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.SpecimenTypes;
using WHO.BioHub.DataManagement.Core.UseCases.SpecimenTypes.UpdateSpecimenType;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UnitTests.UseCases.SpecimenTypes.UpdateSpecimenType;

public class UpdateSpecimenTypeHandlerUnitTests
{
    [Fact]
    public async Task If_resource_is_updated_then_a_valid_response_is_returned()
    {
        // Arrange
        UpdateSpecimenTypeCommandValidator validatorMock = Substitute.For<UpdateSpecimenTypeCommandValidator>();
        ILogger<UpdateSpecimenTypeHandler> loggerMock = Substitute.For<ILogger<UpdateSpecimenTypeHandler>>();
        ISpecimenTypeWriteRepository repositoryMock = Substitute.For<ISpecimenTypeWriteRepository>();
        IUpdateSpecimenTypeMapper mapperMock = Substitute.For<IUpdateSpecimenTypeMapper>();
        CancellationToken cancellationToken = default;

        UpdateSpecimenTypeHandler handler = new(loggerMock, validatorMock, mapperMock, repositoryMock);
        UpdateSpecimenTypeCommand cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult());

        SpecimenType specimenttype = new() { Id = Guid.NewGuid() };
        SpecimenType specimenttypeMapped = new() { Id = specimenttype.Id };

        mapperMock.Map(specimenttype, cmd).ReturnsForAnyArgs(specimenttypeMapped);

        repositoryMock
            .Update(specimenttype, cancellationToken)
            .ReturnsForAnyArgs(Task.FromResult<Errors?>(null));

        // Act
        Either<UpdateSpecimenTypeCommandResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

        // Assert
        response.Should()
            .NotBeNull(because: "A response is expected in this scenario");
        response.IsLeft.Should()
            .BeTrue(because: "Response is expected in this scenario");
        response.Left.Should()
            .NotBeNull(because: "Response should NOT be null");
        response.Left.SpecimenType.Should()
            .NotBeNull(because: "SpecimenType should NOT be null");
        response.Left.SpecimenType.Should()
            .BeEquivalentTo(specimenttypeMapped, because: "Returned specimenttype must mach the one provided in request");

        Received.InOrder(async () =>
        {
            await validatorMock.ValidateAsync(Arg.Any<UpdateSpecimenTypeCommand>(), cancellationToken).Received(1);
            mapperMock.Map(specimenttype, Arg.Any<UpdateSpecimenTypeCommand>()).Received(1);
            await repositoryMock.Update(Arg.Any<SpecimenType>(), cancellationToken).Received(1);
        });
    }

    [Fact]
    public async Task If_validation_fails_then_errors_must_be_returned()
    {
        // Arrange
        UpdateSpecimenTypeCommandValidator validatorMock = Substitute.For<UpdateSpecimenTypeCommandValidator>();
        ILogger<UpdateSpecimenTypeHandler> loggerMock = Substitute.For<ILogger<UpdateSpecimenTypeHandler>>();
        ISpecimenTypeWriteRepository repositoryMock = Substitute.For<ISpecimenTypeWriteRepository>();
        IUpdateSpecimenTypeMapper mapperMock = Substitute.For<IUpdateSpecimenTypeMapper>();
        CancellationToken cancellationToken = default;

        UpdateSpecimenTypeHandler handler = new(loggerMock, validatorMock, mapperMock, repositoryMock);
        UpdateSpecimenTypeCommand cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult(new ValidationFailure[1] { new("test", "error message") }));

        // Act
        Either<UpdateSpecimenTypeCommandResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

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
            await validatorMock.ValidateAsync(Arg.Any<UpdateSpecimenTypeCommand>(), cancellationToken).Received(1);
            mapperMock.Map(Arg.Any<SpecimenType>(), Arg.Any<UpdateSpecimenTypeCommand>()).Received(0);
            await repositoryMock.Update(Arg.Any<SpecimenType>(), cancellationToken).Received(0);
        });
    }

    [Fact]
    public async Task If_errors_are_returned_from_repository_then_they_must_be_forwarded_from_handler()
    {
        // Arrange
        UpdateSpecimenTypeCommandValidator validatorMock = Substitute.For<UpdateSpecimenTypeCommandValidator>();
        ILogger<UpdateSpecimenTypeHandler> loggerMock = Substitute.For<ILogger<UpdateSpecimenTypeHandler>>();
        ISpecimenTypeWriteRepository repositoryMock = Substitute.For<ISpecimenTypeWriteRepository>();
        IUpdateSpecimenTypeMapper mapperMock = Substitute.For<IUpdateSpecimenTypeMapper>();
        CancellationToken cancellationToken = default;

        UpdateSpecimenTypeHandler handler = new(loggerMock, validatorMock, mapperMock, repositoryMock);
        UpdateSpecimenTypeCommand cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult());

        SpecimenType specimenttype = new();
        SpecimenType specimenttypeMapped = new();
        mapperMock.Map(specimenttype, cmd).ReturnsForAnyArgs(specimenttypeMapped);

        // TODO: change error type
        repositoryMock
            .Update(specimenttype, cancellationToken)
            .ReturnsForAnyArgs(Task.FromResult<Errors?>(
                new Errors(ErrorType.Validation, "I should be changed to a persistence error type")));

        // Act
        Either<UpdateSpecimenTypeCommandResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

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
            await validatorMock.ValidateAsync(Arg.Any<UpdateSpecimenTypeCommand>(), cancellationToken).Received(1);
            mapperMock.Map(Arg.Any<SpecimenType>(), Arg.Any<UpdateSpecimenTypeCommand>()).Received(1);
            await repositoryMock.Update(Arg.Any<SpecimenType>(), cancellationToken).Received(1);
        });
    }
}