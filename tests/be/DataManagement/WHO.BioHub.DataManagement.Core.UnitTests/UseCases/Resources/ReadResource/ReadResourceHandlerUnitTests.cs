using FluentValidation.Results;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.Resources;
using WHO.BioHub.DataManagement.Core.UseCases.Resources.ReadResourceFileToken;
using WHO.BioHub.Shared.Utils;
using WHO.BioHub.StorageAccount;

namespace WHO.BioHub.DataManagement.Core.UnitTests.UseCases.Resources.ReadResourceFileToken;

public class ReadResourceFileTokenHandlerUnitTests
{
    [Fact]
    public async Task If_resource_is_read_then_a_valid_response_is_returned()
    {
        // Arrange
        ReadResourceFileTokenQueryValidator validatorMock = Substitute.For<ReadResourceFileTokenQueryValidator>();
        ILogger<ReadResourceFileTokenHandler> loggerMock = Substitute.For<ILogger<ReadResourceFileTokenHandler>>();
        IResourceReadRepository repositoryMock = Substitute.For<IResourceReadRepository>();
        IStorageAccountUtility storageAccountUtilityMock = Substitute.For<IStorageAccountUtility>();
        CancellationToken cancellationToken = default;

        ReadResourceFileTokenHandler handler = new(loggerMock, validatorMock, repositoryMock, storageAccountUtilityMock);
        ReadResourceFileTokenQuery cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult());

        Guid id = Guid.NewGuid();
        Resource resource = new() { Id = id };

        Guid assignedId = Guid.NewGuid();
        repositoryMock
            .Read(id, cancellationToken)
            .ReturnsForAnyArgs(Task.FromResult(resource));

        // Act
        Either<ReadResourceFileTokenQueryResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

        // Assert
        response.Should()
            .NotBeNull(because: "A response is expected in this scenario");


        Received.InOrder(async () =>
        {
            await validatorMock.ValidateAsync(Arg.Any<ReadResourceFileTokenQuery>(), cancellationToken).Received(1);
            await repositoryMock.Read(id, cancellationToken).Received(1);
        });
    }

    [Fact]
    public async Task If_validation_fails_then_errors_must_be_returned()
    {
        // Arrange
        ReadResourceFileTokenQueryValidator validatorMock = Substitute.For<ReadResourceFileTokenQueryValidator>();
        ILogger<ReadResourceFileTokenHandler> loggerMock = Substitute.For<ILogger<ReadResourceFileTokenHandler>>();
        IResourceReadRepository repositoryMock = Substitute.For<IResourceReadRepository>();
        CancellationToken cancellationToken = default;
        IStorageAccountUtility storageAccountUtilityMock = Substitute.For<IStorageAccountUtility>();
        ReadResourceFileTokenHandler handler = new(loggerMock, validatorMock, repositoryMock, storageAccountUtilityMock);
        ReadResourceFileTokenQuery cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult(new ValidationFailure[1] { new("test", "error message") }));

        // Act
        Either<ReadResourceFileTokenQueryResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

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
            await validatorMock.ValidateAsync(Arg.Any<ReadResourceFileTokenQuery>(), cancellationToken).Received(1);
            await repositoryMock.Read(Arg.Any<Guid>(), cancellationToken).Received(0);
        });
    }
}