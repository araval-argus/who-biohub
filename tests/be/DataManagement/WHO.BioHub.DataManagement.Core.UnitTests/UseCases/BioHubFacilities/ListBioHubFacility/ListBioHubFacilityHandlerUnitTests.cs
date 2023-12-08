using FluentValidation.Results;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.BioHubFacilities;
using WHO.BioHub.DataManagement.Core.UseCases.BioHubFacilities.ListBioHubFacilities;
using WHO.BioHub.Shared.Utils;
using WHO.BioHub.DataManagement.Core.UseCases.Laboratories.ReadBioHubFacility;
using WHO.BioHub.Shared.Dto;
using WHO.BioHub.Shared.Enums;

namespace WHO.BioHub.DataManagement.Core.UnitTests.UseCases.BioHubFacilities.ListBioHubFacilities;

public class ListBioHubFacilitiesHandlerUnitTests
{
    [Fact]
    public async Task If_no_biohubfacilities_exists_then_empty_array_is_returned()
    {
        // Arrange
        ListBioHubFacilitiesQueryValidator validatorMock = Substitute.For<ListBioHubFacilitiesQueryValidator>();
        ILogger<ListBioHubFacilitiesHandler> loggerMock = Substitute.For<ILogger<ListBioHubFacilitiesHandler>>();
        IBioHubFacilityReadRepository repositoryMock = Substitute.For<IBioHubFacilityReadRepository>();
        CancellationToken cancellationToken = default;

        IListBioHubFacilityMapper mapperMock = Substitute.For<IListBioHubFacilityMapper>();
        ListBioHubFacilitiesHandler handler = new(loggerMock, validatorMock, repositoryMock, mapperMock);
        ListBioHubFacilitiesQuery cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult());

        Guid assignedId = Guid.NewGuid();
        IEnumerable<BioHubFacility> biohubfacilities = Array.Empty<BioHubFacility>();
        IEnumerable<BioHubFacilityViewModel> biohubfacilitiesViewModels = Array.Empty<BioHubFacilityViewModel>();
        repositoryMock
            .List(cancellationToken)
            .ReturnsForAnyArgs(Task.FromResult(biohubfacilities));

        // Act
        Either<ListBioHubFacilitiesQueryResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

        // Assert
        response.Should()
            .NotBeNull(because: "A response is expected in this scenario");
        response.IsLeft.Should()
            .BeTrue(because: "Response is expected in this scenario");
        response.Left.Should()
            .NotBeNull(because: "Response should NOT be null");
        response.Left.BioHubFacilities.Should()
            .BeEquivalentTo(biohubfacilitiesViewModels, because: "Expected returned biohubfacilities to be equivalent to ones returned from repository");

        Received.InOrder(async () =>
        {
            await validatorMock.ValidateAsync(Arg.Any<ListBioHubFacilitiesQuery>(), cancellationToken).Received(1);
            await repositoryMock.List(cancellationToken).Received(1);
        });
    }

    [Fact]
    public async Task If_at_least_on_biohubfacility_exists_then_it_is_returned()
    {
        // Arrange
        ListBioHubFacilitiesQueryValidator validatorMock = Substitute.For<ListBioHubFacilitiesQueryValidator>();
        ILogger<ListBioHubFacilitiesHandler> loggerMock = Substitute.For<ILogger<ListBioHubFacilitiesHandler>>();
        IBioHubFacilityReadRepository repositoryMock = Substitute.For<IBioHubFacilityReadRepository>();
        CancellationToken cancellationToken = default;

        IListBioHubFacilityMapper mapperMock = Substitute.For<IListBioHubFacilityMapper>();
        ListBioHubFacilitiesHandler handler = new(loggerMock, validatorMock, repositoryMock, mapperMock);
        ListBioHubFacilitiesQuery cmd = new() { RoleType = RoleType.WHO };

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult());

        Guid assignedId = Guid.NewGuid();
        IEnumerable<BioHubFacility> biohubfacilities = new BioHubFacility[1] { new() { Id = assignedId } };
        IEnumerable<BioHubFacilityViewModel> biohubfacilitiesViewModels = new BioHubFacilityViewModel[1] { new() { Id = assignedId } };

        repositoryMock
            .List(cancellationToken)
            .ReturnsForAnyArgs(Task.FromResult(biohubfacilities));

        mapperMock.Map(biohubfacilities).ReturnsForAnyArgs(biohubfacilitiesViewModels.ToList());

        // Act
        Either<ListBioHubFacilitiesQueryResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

        // Assert
        response.Should()
            .NotBeNull(because: "A response is expected in this scenario");
        response.IsLeft.Should()
            .BeTrue(because: "Response is expected in this scenario");
        response.Left.Should()
            .NotBeNull(because: "Response should NOT be null");
        response.Left.BioHubFacilities.Should()
            .BeEquivalentTo(biohubfacilitiesViewModels, because: "Expected returned biohubfacilities to be equivalent to ones returned from repository");

        Received.InOrder(async () =>
        {
            await validatorMock.ValidateAsync(Arg.Any<ListBioHubFacilitiesQuery>(), cancellationToken).Received(1);
            await repositoryMock.List(cancellationToken).Received(1);
        });
    }


    [Fact]
    public async Task If_validation_fails_then_errors_must_be_returned()
    {
        // Arrange
        ListBioHubFacilitiesQueryValidator validatorMock = Substitute.For<ListBioHubFacilitiesQueryValidator>();
        ILogger<ListBioHubFacilitiesHandler> loggerMock = Substitute.For<ILogger<ListBioHubFacilitiesHandler>>();
        IBioHubFacilityReadRepository repositoryMock = Substitute.For<IBioHubFacilityReadRepository>();
        CancellationToken cancellationToken = default;

        IListBioHubFacilityMapper mapperMock = Substitute.For<IListBioHubFacilityMapper>();
        ListBioHubFacilitiesHandler handler = new(loggerMock, validatorMock, repositoryMock, mapperMock);
        ListBioHubFacilitiesQuery cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult(new ValidationFailure[1] { new("test", "error message") }));

        // Act
        Either<ListBioHubFacilitiesQueryResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

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
            await validatorMock.ValidateAsync(Arg.Any<ListBioHubFacilitiesQuery>(), cancellationToken).Received(1);
            await repositoryMock.List(cancellationToken).Received(0);
        });
    }
}