using FluentValidation.Results;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.Laboratories;
using WHO.BioHub.DataManagement.Core.UseCases.Laboratories.ListLaboratories;
using WHO.BioHub.Shared.Utils;
using WHO.BioHub.Shared.Enums;
using WHO.BioHub.Shared.Dto;

namespace WHO.BioHub.DataManagement.Core.UnitTests.UseCases.Laboratories.ListLaboratories;

public class ListLaboratoriesHandlerUnitTests
{
    [Fact]
    public async Task If_no_laboratories_exists_then_empty_array_is_returned()
    {
        // Arrange
        ListLaboratoriesQueryValidator validatorMock = Substitute.For<ListLaboratoriesQueryValidator>();
        ILogger<ListLaboratoriesHandler> loggerMock = Substitute.For<ILogger<ListLaboratoriesHandler>>();
        ILaboratoryReadRepository repositoryMock = Substitute.For<ILaboratoryReadRepository>();
        CancellationToken cancellationToken = default;

        IListLaboratoryMapper mapperMock = Substitute.For<IListLaboratoryMapper>();
        ListLaboratoriesHandler handler = new(loggerMock, validatorMock, repositoryMock, mapperMock);
        ListLaboratoriesQuery cmd = new(RoleType: RoleType.WHO, LaboratoryId: null, BioHubFacilityId: null);

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult());

        Guid assignedId = Guid.NewGuid();
        IEnumerable<Laboratory> laboratories = Array.Empty<Laboratory>();
        repositoryMock
            .List(cancellationToken)
            .ReturnsForAnyArgs(Task.FromResult(laboratories));

        // Act
        Either<ListLaboratoriesQueryResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

        // Assert
        response.Should()
            .NotBeNull(because: "A response is expected in this scenario");
        response.IsLeft.Should()
            .BeTrue(because: "Response is expected in this scenario");
        response.Left.Should()
            .NotBeNull(because: "Response should NOT be null");
        response.Left.Laboratories.Should()
            .BeEquivalentTo(laboratories, because: "Expected returned laboratories to be equivalent to ones returned from repository");

        Received.InOrder(async () =>
        {
            await validatorMock.ValidateAsync(Arg.Any<ListLaboratoriesQuery>(), cancellationToken).Received(1);
            await repositoryMock.List(cancellationToken).Received(1);
        });
    }

    [Fact]
    public async Task If_at_least_on_laboratory_exists_then_it_is_returned()
    {
        // Arrange
        ListLaboratoriesQueryValidator validatorMock = Substitute.For<ListLaboratoriesQueryValidator>();
        ILogger<ListLaboratoriesHandler> loggerMock = Substitute.For<ILogger<ListLaboratoriesHandler>>();
        ILaboratoryReadRepository repositoryMock = Substitute.For<ILaboratoryReadRepository>();
        CancellationToken cancellationToken = default;

        IListLaboratoryMapper mapperMock = Substitute.For<IListLaboratoryMapper>();
        ListLaboratoriesHandler handler = new(loggerMock, validatorMock, repositoryMock, mapperMock);
         ListLaboratoriesQuery cmd = new(RoleType: RoleType.WHO, LaboratoryId: null, BioHubFacilityId: null);

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult());

        Guid assignedId = Guid.NewGuid();
        IEnumerable<Laboratory> laboratories = new Laboratory[1] { new() { Id = assignedId } };
        repositoryMock
            .List(cancellationToken)
            .ReturnsForAnyArgs(Task.FromResult(laboratories));

        IEnumerable<LaboratoryViewModel> laboratoriesViewModel = new LaboratoryViewModel[1] { new() { Id = assignedId } };
        mapperMock
            .Map(laboratories)
            .ReturnsForAnyArgs(laboratoriesViewModel);
        // Act
        Either<ListLaboratoriesQueryResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

        // Assert
        response.Should()
            .NotBeNull(because: "A response is expected in this scenario");
        response.IsLeft.Should()
            .BeTrue(because: "Response is expected in this scenario");
        response.Left.Should()
            .NotBeNull(because: "Response should NOT be null");
        response.Left.Laboratories.Should()
            .BeEquivalentTo(laboratoriesViewModel, because: "Expected returned laboratories to be equivalent to ones returned from repository");

        Received.InOrder(async () =>
        {
            await validatorMock.ValidateAsync(Arg.Any<ListLaboratoriesQuery>(), cancellationToken).Received(1);
            await repositoryMock.List(cancellationToken).Received(1);
        });
    }


    [Fact]
    public async Task If_validation_fails_then_errors_must_be_returned()
    {
        // Arrange
        ListLaboratoriesQueryValidator validatorMock = Substitute.For<ListLaboratoriesQueryValidator>();
        ILogger<ListLaboratoriesHandler> loggerMock = Substitute.For<ILogger<ListLaboratoriesHandler>>();
        ILaboratoryReadRepository repositoryMock = Substitute.For<ILaboratoryReadRepository>();
        CancellationToken cancellationToken = default;

        IListLaboratoryMapper mapperMock = Substitute.For<IListLaboratoryMapper>();
        ListLaboratoriesHandler handler = new(loggerMock, validatorMock, repositoryMock, mapperMock);
         ListLaboratoriesQuery cmd = new(RoleType: RoleType.WHO, LaboratoryId: null, BioHubFacilityId: null);

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult(new ValidationFailure[1] { new("test", "error message") }));

        // Act
        Either<ListLaboratoriesQueryResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

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
            await validatorMock.ValidateAsync(Arg.Any<ListLaboratoriesQuery>(), cancellationToken).Received(1);
            await repositoryMock.List(cancellationToken).Received(0);
        });
    }
}