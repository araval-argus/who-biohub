using FluentValidation.Results;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.WorklistToBioHubEmails;
using WHO.BioHub.DataManagement.Core.UseCases.WorklistToBioHubEmails.CreateWorklistToBioHubEmail;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UnitTests.UseCases.WorklistToBioHubEmails.CreateWorklistToBioHubEmail;

public class CreateWorklistToBioHubEmailHandlerUnitTests
{
    [Fact]
    public async Task If_resource_is_created_then_a_valid_response_is_returned()
    {
        // Arrange
        CreateWorklistToBioHubEmailCommandValidator validatorMock = Substitute.For<CreateWorklistToBioHubEmailCommandValidator>();
        ILogger<CreateWorklistToBioHubEmailHandler> loggerMock = Substitute.For<ILogger<CreateWorklistToBioHubEmailHandler>>();
        IWorklistToBioHubEmailWriteRepository repositoryMock = Substitute.For<IWorklistToBioHubEmailWriteRepository>();
        ICreateWorklistToBioHubEmailMapper mapperMock = Substitute.For<ICreateWorklistToBioHubEmailMapper>();
        CancellationToken cancellationToken = default;

        CreateWorklistToBioHubEmailHandler handler = new(loggerMock, validatorMock, mapperMock, repositoryMock);
        CreateWorklistToBioHubEmailCommand cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult());

        WorklistToBioHubEmail worklisttobiohubemail = new();
        mapperMock.Map(cmd).ReturnsForAnyArgs(worklisttobiohubemail);

        Guid assignedId = Guid.NewGuid();
        repositoryMock
            .Create(worklisttobiohubemail, cancellationToken)
            .ReturnsForAnyArgs(
                Task.Run<Either<WorklistToBioHubEmail, Errors>>(() =>
                {
                    worklisttobiohubemail.Id = assignedId;
                    return new(worklisttobiohubemail);
                }));

        // Act
        Either<CreateWorklistToBioHubEmailCommandResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

        // Assert
        response.Should()
            .NotBeNull(because: "A response is expected in this scenario");
        response.IsLeft.Should()
            .BeTrue(because: "Response is expected in this scenario");
        response.Left.Should()
            .NotBeNull(because: "Response should NOT be null");
        response.Left.WorklistToBioHubEmail.Should()
            .NotBeNull(because: "WorklistToBioHubEmail should NOT be null");
        response.Left.WorklistToBioHubEmail.Id.Should()
            .NotBeEmpty(because: "An Id should be assigned to the WorklistToBioHubEmail")
            .And.Be(assignedId, because: "Returned worklisttobiohubemail Id must mach the one assigned by repository");

        Received.InOrder(async () =>
        {
            await validatorMock.ValidateAsync(Arg.Any<CreateWorklistToBioHubEmailCommand>(), cancellationToken).Received(1);
            mapperMock.Map(Arg.Any<CreateWorklistToBioHubEmailCommand>()).Received(1);
            await repositoryMock.Create(Arg.Any<WorklistToBioHubEmail>(), cancellationToken).Received(1);
        });
    }

    [Fact]
    public async Task If_validation_fails_then_errors_must_be_returned()
    {
        // Arrange
        CreateWorklistToBioHubEmailCommandValidator validatorMock = Substitute.For<CreateWorklistToBioHubEmailCommandValidator>();
        ILogger<CreateWorklistToBioHubEmailHandler> loggerMock = Substitute.For<ILogger<CreateWorklistToBioHubEmailHandler>>();
        IWorklistToBioHubEmailWriteRepository repositoryMock = Substitute.For<IWorklistToBioHubEmailWriteRepository>();
        ICreateWorklistToBioHubEmailMapper mapperMock = Substitute.For<ICreateWorklistToBioHubEmailMapper>();
        CancellationToken cancellationToken = default;

        CreateWorklistToBioHubEmailHandler handler = new(loggerMock, validatorMock, mapperMock, repositoryMock);
        CreateWorklistToBioHubEmailCommand cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult(new ValidationFailure[1] { new("test", "error message") }));

        // Act
        Either<CreateWorklistToBioHubEmailCommandResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

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
            await validatorMock.ValidateAsync(Arg.Any<CreateWorklistToBioHubEmailCommand>(), cancellationToken).Received(1);
            mapperMock.Map(Arg.Any<CreateWorklistToBioHubEmailCommand>()).Received(0);
            await repositoryMock.Create(Arg.Any<WorklistToBioHubEmail>(), cancellationToken).Received(0);
        });
    }

    [Fact]
    public async Task If_errors_are_returned_from_repository_then_they_must_be_forwarded_from_handler()
    {
        // Arrange
        CreateWorklistToBioHubEmailCommandValidator validatorMock = Substitute.For<CreateWorklistToBioHubEmailCommandValidator>();
        ILogger<CreateWorklistToBioHubEmailHandler> loggerMock = Substitute.For<ILogger<CreateWorklistToBioHubEmailHandler>>();
        IWorklistToBioHubEmailWriteRepository repositoryMock = Substitute.For<IWorklistToBioHubEmailWriteRepository>();
        ICreateWorklistToBioHubEmailMapper mapperMock = Substitute.For<ICreateWorklistToBioHubEmailMapper>();
        CancellationToken cancellationToken = default;

        CreateWorklistToBioHubEmailHandler handler = new(loggerMock, validatorMock, mapperMock, repositoryMock);
        CreateWorklistToBioHubEmailCommand cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult());

        WorklistToBioHubEmail worklisttobiohubemail = new();
        mapperMock.Map(cmd).ReturnsForAnyArgs(worklisttobiohubemail);

        // TODO: change error type
        repositoryMock
            .Create(worklisttobiohubemail, cancellationToken)
            .ReturnsForAnyArgs(Task.FromResult<Either<WorklistToBioHubEmail, Errors>>(
                new(new Errors(ErrorType.Validation, "I should be changed to a persistence error type"))));

        // Act
        Either<CreateWorklistToBioHubEmailCommandResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

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
            await validatorMock.ValidateAsync(Arg.Any<CreateWorklistToBioHubEmailCommand>(), cancellationToken).Received(1);
            mapperMock.Map(Arg.Any<CreateWorklistToBioHubEmailCommand>()).Received(1);
            await repositoryMock.Create(Arg.Any<WorklistToBioHubEmail>(), cancellationToken).Received(1);
        });
    }
}