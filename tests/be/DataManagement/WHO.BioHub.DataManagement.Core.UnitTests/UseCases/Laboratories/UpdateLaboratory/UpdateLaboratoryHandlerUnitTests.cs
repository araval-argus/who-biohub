using FluentValidation.Results;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.Laboratories;
using WHO.BioHub.DataManagement.Core.UseCases.Laboratories.UpdateLaboratory;
using WHO.BioHub.Shared.Utils;
using Microsoft.EntityFrameworkCore.Storage;

namespace WHO.BioHub.DataManagement.Core.UnitTests.UseCases.Laboratories.UpdateLaboratory;

public class UpdateLaboratoryHandlerUnitTests
{
    [Fact]
    public async Task If_resource_is_updated_then_a_valid_response_is_returned()
    {
        // Arrange
        UpdateLaboratoryCommandValidator validatorMock = Substitute.For<UpdateLaboratoryCommandValidator>();
        ILogger<UpdateLaboratoryHandler> loggerMock = Substitute.For<ILogger<UpdateLaboratoryHandler>>();
        ILaboratoryWriteRepository repositoryMock = Substitute.For<ILaboratoryWriteRepository>();
        IUpdateLaboratoryMapper mapperMock = Substitute.For<IUpdateLaboratoryMapper>();
        CancellationToken cancellationToken = default;

        UpdateLaboratoryHandler handler = new(loggerMock, validatorMock, mapperMock, repositoryMock);
        UpdateLaboratoryCommand cmd = new();
        IDbContextTransaction transactionMock = Substitute.For<IDbContextTransaction>();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult());

        Laboratory laboratory = new() { Id = Guid.NewGuid() };
        Laboratory laboratoryMapped = new() { Id = laboratory.Id };

        mapperMock.Map(laboratory, cmd).ReturnsForAnyArgs(laboratoryMapped);

        repositoryMock.BeginTransactionAsync().ReturnsForAnyArgs(Task.FromResult(transactionMock));

        repositoryMock
            .ReadForUpdate(cmd.Id, cancellationToken)
            .ReturnsForAnyArgs(laboratory);

        repositoryMock
            .Update(laboratory, cancellationToken, transactionMock)
            .ReturnsForAnyArgs(Task.FromResult<Errors?>(null));

        repositoryMock.CreateLaboratoryHistoryItem(laboratory, cancellationToken, transactionMock)
            .ReturnsForAnyArgs(Task.FromResult<Errors?>(null));
        // Act
        Either<UpdateLaboratoryCommandResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

        // Assert
        response.Should()
            .NotBeNull(because: "A response is expected in this scenario");
        response.IsLeft.Should()
            .BeTrue(because: "Response is expected in this scenario");
        response.Left.Should()
            .NotBeNull(because: "Response should NOT be null");
        response.Left.Id.Should()
            .NotBeEmpty(because: "Laboratory should NOT be null");
        response.Left.Id.Should()
            .Be(laboratory.Id, because: "Returned laboratory must mach the one provided in request");

        Received.InOrder(async () =>
        {
            await validatorMock.ValidateAsync(Arg.Any<UpdateLaboratoryCommand>(), cancellationToken).Received(1);
            mapperMock.Map(laboratory, Arg.Any<UpdateLaboratoryCommand>()).Received(1);
            await repositoryMock.Update(Arg.Any<Laboratory>(), cancellationToken, transactionMock).Received(1);
        });
    }

    [Fact]
    public async Task If_validation_fails_then_errors_must_be_returned()
    {
        // Arrange
        UpdateLaboratoryCommandValidator validatorMock = Substitute.For<UpdateLaboratoryCommandValidator>();
        ILogger<UpdateLaboratoryHandler> loggerMock = Substitute.For<ILogger<UpdateLaboratoryHandler>>();
        ILaboratoryWriteRepository repositoryMock = Substitute.For<ILaboratoryWriteRepository>();
        IUpdateLaboratoryMapper mapperMock = Substitute.For<IUpdateLaboratoryMapper>();
        CancellationToken cancellationToken = default;

        UpdateLaboratoryHandler handler = new(loggerMock, validatorMock, mapperMock, repositoryMock);
        UpdateLaboratoryCommand cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult(new ValidationFailure[1] { new("test", "error message") }));

        // Act
        Either<UpdateLaboratoryCommandResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

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
            await validatorMock.ValidateAsync(Arg.Any<UpdateLaboratoryCommand>(), cancellationToken).Received(1);
            mapperMock.Map(Arg.Any<Laboratory>(), Arg.Any<UpdateLaboratoryCommand>()).Received(0);
            await repositoryMock.Update(Arg.Any<Laboratory>(), cancellationToken).Received(0);
        });
    }

    [Fact]
    public async Task If_errors_are_returned_from_repository_then_they_must_be_forwarded_from_handler()
    {
        // Arrange
        UpdateLaboratoryCommandValidator validatorMock = Substitute.For<UpdateLaboratoryCommandValidator>();
        ILogger<UpdateLaboratoryHandler> loggerMock = Substitute.For<ILogger<UpdateLaboratoryHandler>>();
        ILaboratoryWriteRepository repositoryMock = Substitute.For<ILaboratoryWriteRepository>();
        IUpdateLaboratoryMapper mapperMock = Substitute.For<IUpdateLaboratoryMapper>();
        CancellationToken cancellationToken = default;

        IDbContextTransaction transactionMock = Substitute.For<IDbContextTransaction>();

        UpdateLaboratoryHandler handler = new(loggerMock, validatorMock, mapperMock, repositoryMock);
        UpdateLaboratoryCommand cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult());

        Laboratory laboratory = new();
        Laboratory laboratoryMapped = new();
        mapperMock.Map(laboratory, cmd).ReturnsForAnyArgs(laboratoryMapped);

        repositoryMock.BeginTransactionAsync().ReturnsForAnyArgs(Task.FromResult(transactionMock));

        repositoryMock
           .ReadForUpdate(cmd.Id, cancellationToken)
           .ReturnsForAnyArgs(laboratory);

        repositoryMock
            .Update(laboratory, cancellationToken, transactionMock)
            .ReturnsForAnyArgs(Task.FromResult<Errors?>(
                new Errors(ErrorType.Validation, "I should be changed to a persistence error type")));

        // Act
        Either<UpdateLaboratoryCommandResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

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
            await validatorMock.ValidateAsync(Arg.Any<UpdateLaboratoryCommand>(), cancellationToken).Received(1);
            mapperMock.Map(Arg.Any<Laboratory>(), Arg.Any<UpdateLaboratoryCommand>()).Received(1);
            await repositoryMock.Update(Arg.Any<Laboratory>(), cancellationToken).Received(1);
        });
    }
}