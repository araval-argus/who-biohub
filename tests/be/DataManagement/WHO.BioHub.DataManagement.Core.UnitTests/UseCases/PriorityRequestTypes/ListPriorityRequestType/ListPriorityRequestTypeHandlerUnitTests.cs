using FluentValidation.Results;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.PriorityRequestTypes;
using WHO.BioHub.DataManagement.Core.UseCases.PriorityRequestTypes.ListPriorityRequestTypes;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UnitTests.UseCases.PriorityRequestTypes.ListPriorityRequestTypes;

public class ListPriorityRequestTypesHandlerUnitTests
{
    [Fact]
    public async Task If_no_priorityrequesttypes_exists_then_empty_array_is_returned()
    {
        // Arrange
        ListPriorityRequestTypesQueryValidator validatorMock = Substitute.For<ListPriorityRequestTypesQueryValidator>();
        ILogger<ListPriorityRequestTypesHandler> loggerMock = Substitute.For<ILogger<ListPriorityRequestTypesHandler>>();
        IPriorityRequestTypeReadRepository repositoryMock = Substitute.For<IPriorityRequestTypeReadRepository>();
        CancellationToken cancellationToken = default;

        ListPriorityRequestTypesHandler handler = new(loggerMock, validatorMock, repositoryMock);
        ListPriorityRequestTypesQuery cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult());

        Guid assignedId = Guid.NewGuid();
        IEnumerable<PriorityRequestType> priorityrequesttypes = Array.Empty<PriorityRequestType>();
        repositoryMock
            .List(cancellationToken)
            .ReturnsForAnyArgs(Task.FromResult(priorityrequesttypes));

        // Act
        Either<ListPriorityRequestTypesQueryResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

        // Assert
        response.Should()
            .NotBeNull(because: "A response is expected in this scenario");
        response.IsLeft.Should()
            .BeTrue(because: "Response is expected in this scenario");
        response.Left.Should()
            .NotBeNull(because: "Response should NOT be null");
        response.Left.PriorityRequestTypes.Should()
            .BeEquivalentTo(priorityrequesttypes, because: "Expected returned priorityrequesttypes to be equivalent to ones returned from repository");

        Received.InOrder(async () =>
        {
            await validatorMock.ValidateAsync(Arg.Any<ListPriorityRequestTypesQuery>(), cancellationToken).Received(1);
            await repositoryMock.List(cancellationToken).Received(1);
        });
    }

    [Fact]
    public async Task If_at_least_on_priorityrequesttype_exists_then_it_is_returned()
    {
        // Arrange
        ListPriorityRequestTypesQueryValidator validatorMock = Substitute.For<ListPriorityRequestTypesQueryValidator>();
        ILogger<ListPriorityRequestTypesHandler> loggerMock = Substitute.For<ILogger<ListPriorityRequestTypesHandler>>();
        IPriorityRequestTypeReadRepository repositoryMock = Substitute.For<IPriorityRequestTypeReadRepository>();
        CancellationToken cancellationToken = default;

        ListPriorityRequestTypesHandler handler = new(loggerMock, validatorMock, repositoryMock);
        ListPriorityRequestTypesQuery cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult());

        Guid assignedId = Guid.NewGuid();
        IEnumerable<PriorityRequestType> priorityrequesttypes = new PriorityRequestType[1] { new() };
        repositoryMock
            .List(cancellationToken)
            .ReturnsForAnyArgs(Task.FromResult(priorityrequesttypes));

        // Act
        Either<ListPriorityRequestTypesQueryResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

        // Assert
        response.Should()
            .NotBeNull(because: "A response is expected in this scenario");
        response.IsLeft.Should()
            .BeTrue(because: "Response is expected in this scenario");
        response.Left.Should()
            .NotBeNull(because: "Response should NOT be null");
        response.Left.PriorityRequestTypes.Should()
            .BeEquivalentTo(priorityrequesttypes, because: "Expected returned priorityrequesttypes to be equivalent to ones returned from repository");

        Received.InOrder(async () =>
        {
            await validatorMock.ValidateAsync(Arg.Any<ListPriorityRequestTypesQuery>(), cancellationToken).Received(1);
            await repositoryMock.List(cancellationToken).Received(1);
        });
    }


    [Fact]
    public async Task If_validation_fails_then_errors_must_be_returned()
    {
        // Arrange
        ListPriorityRequestTypesQueryValidator validatorMock = Substitute.For<ListPriorityRequestTypesQueryValidator>();
        ILogger<ListPriorityRequestTypesHandler> loggerMock = Substitute.For<ILogger<ListPriorityRequestTypesHandler>>();
        IPriorityRequestTypeReadRepository repositoryMock = Substitute.For<IPriorityRequestTypeReadRepository>();
        CancellationToken cancellationToken = default;

        ListPriorityRequestTypesHandler handler = new(loggerMock, validatorMock, repositoryMock);
        ListPriorityRequestTypesQuery cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult(new ValidationFailure[1] { new("test", "error message") }));

        // Act
        Either<ListPriorityRequestTypesQueryResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

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
            await validatorMock.ValidateAsync(Arg.Any<ListPriorityRequestTypesQuery>(), cancellationToken).Received(1);
            await repositoryMock.List(cancellationToken).Received(0);
        });
    }
}