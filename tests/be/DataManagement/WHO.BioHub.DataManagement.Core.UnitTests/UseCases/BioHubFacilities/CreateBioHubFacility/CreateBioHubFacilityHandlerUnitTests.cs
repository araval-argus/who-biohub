using FluentValidation.Results;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.BioHubFacilities;
using WHO.BioHub.DataManagement.Core.UseCases.BioHubFacilities.CreateBioHubFacility;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UnitTests.UseCases.BioHubFacilities.CreateBioHubFacility;

public class CreateBioHubFacilityHandlerUnitTests
{
    [Fact]
    public async Task If_resource_is_created_then_a_valid_response_is_returned()
    {
        // Arrange
        CreateBioHubFacilityCommandValidator validatorMock = Substitute.For<CreateBioHubFacilityCommandValidator>();
        ILogger<CreateBioHubFacilityHandler> loggerMock = Substitute.For<ILogger<CreateBioHubFacilityHandler>>();
        IBioHubFacilityWriteRepository repositoryMock = Substitute.For<IBioHubFacilityWriteRepository>();
        ICreateBioHubFacilityMapper mapperMock = Substitute.For<ICreateBioHubFacilityMapper>();
        CancellationToken cancellationToken = default;

        CreateBioHubFacilityHandler handler = new(loggerMock, validatorMock, mapperMock, repositoryMock);
        CreateBioHubFacilityCommand cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult());

        BioHubFacility biohubfacility = new();
        mapperMock.Map(cmd).ReturnsForAnyArgs(biohubfacility);

        Guid assignedId = Guid.NewGuid();
        repositoryMock
            .Create(biohubfacility, cancellationToken)
            .ReturnsForAnyArgs(
                Task.Run<Either<BioHubFacility, Errors>>(() =>
                {
                    biohubfacility.Id = assignedId;
                    return new(biohubfacility);
                }));

        // Act
        Either<CreateBioHubFacilityCommandResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

        // Assert
        response.Should()
            .NotBeNull(because: "A response is expected in this scenario");
        response.IsLeft.Should()
            .BeTrue(because: "Response is expected in this scenario");
        response.Left.Should()
            .NotBeNull(because: "Response should NOT be null");        
        response.Left.Id.Should()
            .NotBeEmpty(because: "An Id should be assigned to the BioHubFacility")
            .And.Be(assignedId, because: "Returned biohubfacility Id must mach the one assigned by repository");

        Received.InOrder(async () =>
        {
            await validatorMock.ValidateAsync(Arg.Any<CreateBioHubFacilityCommand>(), cancellationToken).Received(1);
            mapperMock.Map(Arg.Any<CreateBioHubFacilityCommand>()).Received(1);
            await repositoryMock.Create(Arg.Any<BioHubFacility>(), cancellationToken).Received(1);
        });
    }

    [Fact]
    public async Task If_validation_fails_then_errors_must_be_returned()
    {
        // Arrange
        CreateBioHubFacilityCommandValidator validatorMock = Substitute.For<CreateBioHubFacilityCommandValidator>();
        ILogger<CreateBioHubFacilityHandler> loggerMock = Substitute.For<ILogger<CreateBioHubFacilityHandler>>();
        IBioHubFacilityWriteRepository repositoryMock = Substitute.For<IBioHubFacilityWriteRepository>();
        ICreateBioHubFacilityMapper mapperMock = Substitute.For<ICreateBioHubFacilityMapper>();
        CancellationToken cancellationToken = default;

        CreateBioHubFacilityHandler handler = new(loggerMock, validatorMock, mapperMock, repositoryMock);
        CreateBioHubFacilityCommand cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult(new ValidationFailure[1] { new("test", "error message") }));

        // Act
        Either<CreateBioHubFacilityCommandResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

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
            await validatorMock.ValidateAsync(Arg.Any<CreateBioHubFacilityCommand>(), cancellationToken).Received(1);
            mapperMock.Map(Arg.Any<CreateBioHubFacilityCommand>()).Received(0);
            await repositoryMock.Create(Arg.Any<BioHubFacility>(), cancellationToken).Received(0);
        });
    }

    [Fact]
    public async Task If_errors_are_returned_from_repository_then_they_must_be_forwarded_from_handler()
    {
        // Arrange
        CreateBioHubFacilityCommandValidator validatorMock = Substitute.For<CreateBioHubFacilityCommandValidator>();
        ILogger<CreateBioHubFacilityHandler> loggerMock = Substitute.For<ILogger<CreateBioHubFacilityHandler>>();
        IBioHubFacilityWriteRepository repositoryMock = Substitute.For<IBioHubFacilityWriteRepository>();
        ICreateBioHubFacilityMapper mapperMock = Substitute.For<ICreateBioHubFacilityMapper>();
        CancellationToken cancellationToken = default;

        CreateBioHubFacilityHandler handler = new(loggerMock, validatorMock, mapperMock, repositoryMock);
        CreateBioHubFacilityCommand cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult());

        BioHubFacility biohubfacility = new();
        mapperMock.Map(cmd).ReturnsForAnyArgs(biohubfacility);

        // TODO: change error type
        repositoryMock
            .Create(biohubfacility, cancellationToken)
            .ReturnsForAnyArgs(Task.FromResult<Either<BioHubFacility, Errors>>(
                new(new Errors(ErrorType.Validation, "I should be changed to a persistence error type"))));

        // Act
        Either<CreateBioHubFacilityCommandResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

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
            await validatorMock.ValidateAsync(Arg.Any<CreateBioHubFacilityCommand>(), cancellationToken).Received(1);
            mapperMock.Map(Arg.Any<CreateBioHubFacilityCommand>()).Received(1);
            await repositoryMock.Create(Arg.Any<BioHubFacility>(), cancellationToken).Received(1);
        });
    }
}