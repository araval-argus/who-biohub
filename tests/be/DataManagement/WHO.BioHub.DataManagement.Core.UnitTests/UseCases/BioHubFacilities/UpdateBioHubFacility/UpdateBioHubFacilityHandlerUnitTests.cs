using FluentValidation.Results;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.BioHubFacilities;
using WHO.BioHub.DataManagement.Core.UseCases.BioHubFacilities.UpdateBioHubFacility;
using WHO.BioHub.Shared.Utils;
using Microsoft.EntityFrameworkCore.Storage;

namespace WHO.BioHub.DataManagement.Core.UnitTests.UseCases.BioHubFacilities.UpdateBioHubFacility;

public class UpdateBioHubFacilityHandlerUnitTests
{
    [Fact]
    public async Task If_resource_is_updated_then_a_valid_response_is_returned()
    {
        // Arrange
        UpdateBioHubFacilityCommandValidator validatorMock = Substitute.For<UpdateBioHubFacilityCommandValidator>();
        ILogger<UpdateBioHubFacilityHandler> loggerMock = Substitute.For<ILogger<UpdateBioHubFacilityHandler>>();
        IBioHubFacilityWriteRepository repositoryMock = Substitute.For<IBioHubFacilityWriteRepository>();
        IUpdateBioHubFacilityMapper mapperMock = Substitute.For<IUpdateBioHubFacilityMapper>();
        CancellationToken cancellationToken = default;

        UpdateBioHubFacilityHandler handler = new(loggerMock, validatorMock, mapperMock, repositoryMock);
        UpdateBioHubFacilityCommand cmd = new();
        IDbContextTransaction transactionMock = Substitute.For<IDbContextTransaction>();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult());

        BioHubFacility biohubfacility = new() { Id = Guid.NewGuid() };
        BioHubFacility biohubfacilityMapped = new() { Id = biohubfacility.Id };

        mapperMock.Map(biohubfacility, cmd).ReturnsForAnyArgs(biohubfacilityMapped);

        repositoryMock.BeginTransactionAsync().ReturnsForAnyArgs(Task.FromResult(transactionMock));

        repositoryMock
            .ReadForUpdate(cmd.Id, cancellationToken)
            .ReturnsForAnyArgs(biohubfacility);

        repositoryMock
            .Update(biohubfacility, cancellationToken, transactionMock)
            .ReturnsForAnyArgs(Task.FromResult<Errors?>(null));
        
        repositoryMock.CreateBioHubFacilityHistoryItem(biohubfacility, cancellationToken, transactionMock)
            .ReturnsForAnyArgs(Task.FromResult<Errors?>(null));
        // Act
        Either<UpdateBioHubFacilityCommandResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

        // Assert
        response.Should()
            .NotBeNull(because: "A response is expected in this scenario");
        response.IsLeft.Should()
            .BeTrue(because: "Response is expected in this scenario");
        response.Left.Should()
            .NotBeNull(because: "Response should NOT be null");
        response.Left.Id.Should()
            .NotBeEmpty(because: "BioHubFacility should NOT be null");
        //response.Left.Id.Should()
        //    .BeEquivalentTo(biohubfacilityMapped, because: "Returned biohubfacility must mach the one provided in request");

        Received.InOrder(async () =>
        {
            await validatorMock.ValidateAsync(Arg.Any<UpdateBioHubFacilityCommand>(), cancellationToken).Received(1);
            mapperMock.Map(biohubfacility, Arg.Any<UpdateBioHubFacilityCommand>()).Received(1);
            await repositoryMock.Update(Arg.Any<BioHubFacility>(), cancellationToken, transactionMock).Received(1);
        });
    }

    [Fact]
    public async Task If_validation_fails_then_errors_must_be_returned()
    {
        // Arrange
        UpdateBioHubFacilityCommandValidator validatorMock = Substitute.For<UpdateBioHubFacilityCommandValidator>();
        ILogger<UpdateBioHubFacilityHandler> loggerMock = Substitute.For<ILogger<UpdateBioHubFacilityHandler>>();
        IBioHubFacilityWriteRepository repositoryMock = Substitute.For<IBioHubFacilityWriteRepository>();
        IUpdateBioHubFacilityMapper mapperMock = Substitute.For<IUpdateBioHubFacilityMapper>();
        CancellationToken cancellationToken = default;

        UpdateBioHubFacilityHandler handler = new(loggerMock, validatorMock, mapperMock, repositoryMock);
        UpdateBioHubFacilityCommand cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult(new ValidationFailure[1] { new("test", "error message") }));

        // Act
        Either<UpdateBioHubFacilityCommandResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

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
            await validatorMock.ValidateAsync(Arg.Any<UpdateBioHubFacilityCommand>(), cancellationToken).Received(1);
            mapperMock.Map(Arg.Any<BioHubFacility>(), Arg.Any<UpdateBioHubFacilityCommand>()).Received(0);
            await repositoryMock.Update(Arg.Any<BioHubFacility>(), cancellationToken).Received(0);
        });
    }

    [Fact]
    public async Task If_errors_are_returned_from_repository_then_they_must_be_forwarded_from_handler()
    {
        // Arrange
        UpdateBioHubFacilityCommandValidator validatorMock = Substitute.For<UpdateBioHubFacilityCommandValidator>();
        ILogger<UpdateBioHubFacilityHandler> loggerMock = Substitute.For<ILogger<UpdateBioHubFacilityHandler>>();
        IBioHubFacilityWriteRepository repositoryMock = Substitute.For<IBioHubFacilityWriteRepository>();
        IUpdateBioHubFacilityMapper mapperMock = Substitute.For<IUpdateBioHubFacilityMapper>();
        CancellationToken cancellationToken = default;

        IDbContextTransaction transactionMock = Substitute.For<IDbContextTransaction>();

        UpdateBioHubFacilityHandler handler = new(loggerMock, validatorMock, mapperMock, repositoryMock);
        UpdateBioHubFacilityCommand cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult());

        BioHubFacility biohubfacility = new();
        BioHubFacility biohubfacilityMapped = new();
        mapperMock.Map(biohubfacility, cmd).ReturnsForAnyArgs(biohubfacilityMapped);

        repositoryMock.BeginTransactionAsync().ReturnsForAnyArgs(Task.FromResult(transactionMock));

        repositoryMock
           .ReadForUpdate(cmd.Id, cancellationToken)
           .ReturnsForAnyArgs(biohubfacility);

        repositoryMock
            .Update(biohubfacility, cancellationToken, transactionMock)
            .ReturnsForAnyArgs(Task.FromResult<Errors?>(
                new Errors(ErrorType.Validation, "I should be changed to a persistence error type")));

        // Act
        Either<UpdateBioHubFacilityCommandResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

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
            await validatorMock.ValidateAsync(Arg.Any<UpdateBioHubFacilityCommand>(), cancellationToken).Received(1);
            mapperMock.Map(Arg.Any<BioHubFacility>(), Arg.Any<UpdateBioHubFacilityCommand>()).Received(1);
            await repositoryMock.Update(Arg.Any<BioHubFacility>(), cancellationToken).Received(1);
        });
    }
}