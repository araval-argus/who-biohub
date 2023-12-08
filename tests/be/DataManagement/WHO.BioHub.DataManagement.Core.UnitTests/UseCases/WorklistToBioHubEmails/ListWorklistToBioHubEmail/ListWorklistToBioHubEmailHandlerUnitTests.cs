using FluentValidation.Results;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.WorklistToBioHubEmails;
using WHO.BioHub.DataManagement.Core.UseCases.WorklistToBioHubEmails.ListWorklistToBioHubEmails;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UnitTests.UseCases.WorklistToBioHubEmails.ListWorklistToBioHubEmails;

public class ListWorklistToBioHubEmailsHandlerUnitTests
{
    [Fact]
    public async Task If_no_worklisttobiohubemails_exists_then_empty_array_is_returned()
    {
        // Arrange
        ListWorklistToBioHubEmailsQueryValidator validatorMock = Substitute.For<ListWorklistToBioHubEmailsQueryValidator>();
        ILogger<ListWorklistToBioHubEmailsHandler> loggerMock = Substitute.For<ILogger<ListWorklistToBioHubEmailsHandler>>();
        IWorklistToBioHubEmailReadRepository repositoryMock = Substitute.For<IWorklistToBioHubEmailReadRepository>();
        CancellationToken cancellationToken = default;

        ListWorklistToBioHubEmailsHandler handler = new(loggerMock, validatorMock, repositoryMock);
        ListWorklistToBioHubEmailsQuery cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult());

        Guid assignedId = Guid.NewGuid();
        IEnumerable<WorklistToBioHubEmail> worklisttobiohubemails = Array.Empty<WorklistToBioHubEmail>();
        repositoryMock
            .List(cancellationToken)
            .ReturnsForAnyArgs(Task.FromResult(worklisttobiohubemails));

        // Act
        Either<ListWorklistToBioHubEmailsQueryResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

        // Assert
        response.Should()
            .NotBeNull(because: "A response is expected in this scenario");
        response.IsLeft.Should()
            .BeTrue(because: "Response is expected in this scenario");
        response.Left.Should()
            .NotBeNull(because: "Response should NOT be null");
        response.Left.WorklistToBioHubEmails.Should()
            .BeEquivalentTo(worklisttobiohubemails, because: "Expected returned worklisttobiohubemails to be equivalent to ones returned from repository");

        Received.InOrder(async () =>
        {
            await validatorMock.ValidateAsync(Arg.Any<ListWorklistToBioHubEmailsQuery>(), cancellationToken).Received(1);
            await repositoryMock.List(cancellationToken).Received(1);
        });
    }

    [Fact]
    public async Task If_at_least_on_worklisttobiohubemail_exists_then_it_is_returned()
    {
        // Arrange
        ListWorklistToBioHubEmailsQueryValidator validatorMock = Substitute.For<ListWorklistToBioHubEmailsQueryValidator>();
        ILogger<ListWorklistToBioHubEmailsHandler> loggerMock = Substitute.For<ILogger<ListWorklistToBioHubEmailsHandler>>();
        IWorklistToBioHubEmailReadRepository repositoryMock = Substitute.For<IWorklistToBioHubEmailReadRepository>();
        CancellationToken cancellationToken = default;

        ListWorklistToBioHubEmailsHandler handler = new(loggerMock, validatorMock, repositoryMock);
        ListWorklistToBioHubEmailsQuery cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult());

        Guid assignedId = Guid.NewGuid();
        IEnumerable<WorklistToBioHubEmail> worklisttobiohubemails = new WorklistToBioHubEmail[1] { new() };
        repositoryMock
            .List(cancellationToken)
            .ReturnsForAnyArgs(Task.FromResult(worklisttobiohubemails));

        // Act
        Either<ListWorklistToBioHubEmailsQueryResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

        // Assert
        response.Should()
            .NotBeNull(because: "A response is expected in this scenario");
        response.IsLeft.Should()
            .BeTrue(because: "Response is expected in this scenario");
        response.Left.Should()
            .NotBeNull(because: "Response should NOT be null");
        response.Left.WorklistToBioHubEmails.Should()
            .BeEquivalentTo(worklisttobiohubemails, because: "Expected returned worklisttobiohubemails to be equivalent to ones returned from repository");

        Received.InOrder(async () =>
        {
            await validatorMock.ValidateAsync(Arg.Any<ListWorklistToBioHubEmailsQuery>(), cancellationToken).Received(1);
            await repositoryMock.List(cancellationToken).Received(1);
        });
    }


    [Fact]
    public async Task If_validation_fails_then_errors_must_be_returned()
    {
        // Arrange
        ListWorklistToBioHubEmailsQueryValidator validatorMock = Substitute.For<ListWorklistToBioHubEmailsQueryValidator>();
        ILogger<ListWorklistToBioHubEmailsHandler> loggerMock = Substitute.For<ILogger<ListWorklistToBioHubEmailsHandler>>();
        IWorklistToBioHubEmailReadRepository repositoryMock = Substitute.For<IWorklistToBioHubEmailReadRepository>();
        CancellationToken cancellationToken = default;

        ListWorklistToBioHubEmailsHandler handler = new(loggerMock, validatorMock, repositoryMock);
        ListWorklistToBioHubEmailsQuery cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult(new ValidationFailure[1] { new("test", "error message") }));

        // Act
        Either<ListWorklistToBioHubEmailsQueryResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

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
            await validatorMock.ValidateAsync(Arg.Any<ListWorklistToBioHubEmailsQuery>(), cancellationToken).Received(1);
            await repositoryMock.List(cancellationToken).Received(0);
        });
    }
}