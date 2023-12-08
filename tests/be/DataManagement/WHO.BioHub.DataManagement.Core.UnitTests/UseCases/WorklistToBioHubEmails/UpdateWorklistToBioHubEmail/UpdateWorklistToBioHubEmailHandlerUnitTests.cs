using FluentValidation.Results;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.WorklistToBioHubEmails;
using WHO.BioHub.DataManagement.Core.UseCases.WorklistToBioHubEmails.UpdateWorklistToBioHubEmail;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UnitTests.UseCases.WorklistToBioHubEmails.UpdateWorklistToBioHubEmail;

public class UpdateWorklistToBioHubEmailHandlerUnitTests
{
    [Fact]
    public async Task If_resource_is_updated_then_a_valid_response_is_returned()
    {
        // Arrange
        UpdateWorklistToBioHubEmailCommandValidator validatorMock = Substitute.For<UpdateWorklistToBioHubEmailCommandValidator>();
        ILogger<UpdateWorklistToBioHubEmailHandler> loggerMock = Substitute.For<ILogger<UpdateWorklistToBioHubEmailHandler>>();
        IWorklistToBioHubEmailWriteRepository repositoryMock = Substitute.For<IWorklistToBioHubEmailWriteRepository>();
        IUpdateWorklistToBioHubEmailMapper mapperMock = Substitute.For<IUpdateWorklistToBioHubEmailMapper>();
        CancellationToken cancellationToken = default;

        UpdateWorklistToBioHubEmailHandler handler = new(loggerMock, validatorMock, mapperMock, repositoryMock);
        UpdateWorklistToBioHubEmailCommand cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult());

        WorklistToBioHubEmail worklisttobiohubemail = new() { Id = Guid.NewGuid() };
        WorklistToBioHubEmail worklisttobiohubemailMapped = new() { Id = worklisttobiohubemail.Id };

        mapperMock.Map(worklisttobiohubemail, cmd).ReturnsForAnyArgs(worklisttobiohubemailMapped);

        repositoryMock
            .Update(worklisttobiohubemail, cancellationToken)
            .ReturnsForAnyArgs(Task.FromResult<Errors?>(null));

        // Act
        Either<UpdateWorklistToBioHubEmailCommandResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

        // Assert
        response.Should()
            .NotBeNull(because: "A response is expected in this scenario");
        response.IsLeft.Should()
            .BeTrue(because: "Response is expected in this scenario");
        response.Left.Should()
            .NotBeNull(because: "Response should NOT be null");
        response.Left.WorklistToBioHubEmail.Should()
            .NotBeNull(because: "WorklistToBioHubEmail should NOT be null");
        response.Left.WorklistToBioHubEmail.Should()
            .BeEquivalentTo(worklisttobiohubemailMapped, because: "Returned worklisttobiohubemail must mach the one provided in request");

        Received.InOrder(async () =>
        {
            await validatorMock.ValidateAsync(Arg.Any<UpdateWorklistToBioHubEmailCommand>(), cancellationToken).Received(1);
            mapperMock.Map(worklisttobiohubemail, Arg.Any<UpdateWorklistToBioHubEmailCommand>()).Received(1);
            await repositoryMock.Update(Arg.Any<WorklistToBioHubEmail>(), cancellationToken).Received(1);
        });
    }

    [Fact]
    public async Task If_validation_fails_then_errors_must_be_returned()
    {
        // Arrange
        UpdateWorklistToBioHubEmailCommandValidator validatorMock = Substitute.For<UpdateWorklistToBioHubEmailCommandValidator>();
        ILogger<UpdateWorklistToBioHubEmailHandler> loggerMock = Substitute.For<ILogger<UpdateWorklistToBioHubEmailHandler>>();
        IWorklistToBioHubEmailWriteRepository repositoryMock = Substitute.For<IWorklistToBioHubEmailWriteRepository>();
        IUpdateWorklistToBioHubEmailMapper mapperMock = Substitute.For<IUpdateWorklistToBioHubEmailMapper>();
        CancellationToken cancellationToken = default;

        UpdateWorklistToBioHubEmailHandler handler = new(loggerMock, validatorMock, mapperMock, repositoryMock);
        UpdateWorklistToBioHubEmailCommand cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult(new ValidationFailure[1] { new("test", "error message") }));

        // Act
        Either<UpdateWorklistToBioHubEmailCommandResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

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
            await validatorMock.ValidateAsync(Arg.Any<UpdateWorklistToBioHubEmailCommand>(), cancellationToken).Received(1);
            mapperMock.Map(Arg.Any<WorklistToBioHubEmail>(), Arg.Any<UpdateWorklistToBioHubEmailCommand>()).Received(0);
            await repositoryMock.Update(Arg.Any<WorklistToBioHubEmail>(), cancellationToken).Received(0);
        });
    }

    [Fact]
    public async Task If_errors_are_returned_from_repository_then_they_must_be_forwarded_from_handler()
    {
        // Arrange
        UpdateWorklistToBioHubEmailCommandValidator validatorMock = Substitute.For<UpdateWorklistToBioHubEmailCommandValidator>();
        ILogger<UpdateWorklistToBioHubEmailHandler> loggerMock = Substitute.For<ILogger<UpdateWorklistToBioHubEmailHandler>>();
        IWorklistToBioHubEmailWriteRepository repositoryMock = Substitute.For<IWorklistToBioHubEmailWriteRepository>();
        IUpdateWorklistToBioHubEmailMapper mapperMock = Substitute.For<IUpdateWorklistToBioHubEmailMapper>();
        CancellationToken cancellationToken = default;

        UpdateWorklistToBioHubEmailHandler handler = new(loggerMock, validatorMock, mapperMock, repositoryMock);
        UpdateWorklistToBioHubEmailCommand cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult());

        WorklistToBioHubEmail worklisttobiohubemail = new();
        WorklistToBioHubEmail worklisttobiohubemailMapped = new();
        mapperMock.Map(worklisttobiohubemail, cmd).ReturnsForAnyArgs(worklisttobiohubemailMapped);

        // TODO: change error type
        repositoryMock
            .Update(worklisttobiohubemail, cancellationToken)
            .ReturnsForAnyArgs(Task.FromResult<Errors?>(
                new Errors(ErrorType.Validation, "I should be changed to a persistence error type")));

        // Act
        Either<UpdateWorklistToBioHubEmailCommandResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

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
            await validatorMock.ValidateAsync(Arg.Any<UpdateWorklistToBioHubEmailCommand>(), cancellationToken).Received(1);
            mapperMock.Map(Arg.Any<WorklistToBioHubEmail>(), Arg.Any<UpdateWorklistToBioHubEmailCommand>()).Received(1);
            await repositoryMock.Update(Arg.Any<WorklistToBioHubEmail>(), cancellationToken).Received(1);
        });
    }
}