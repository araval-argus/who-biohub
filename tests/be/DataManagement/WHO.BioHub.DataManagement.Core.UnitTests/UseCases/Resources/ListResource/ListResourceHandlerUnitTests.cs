using FluentValidation.Results;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.Resources;
using WHO.BioHub.DataManagement.Core.UseCases.Resources.ListResources;
using WHO.BioHub.Shared.Utils;
using WHO.BioHub.Shared.Dto;
using WHO.BioHub.Shared.Enums;

namespace WHO.BioHub.DataManagement.Core.UnitTests.UseCases.Resources.ListResources;

public class ListResourcesHandlerUnitTests
{
    [Fact]
    public async Task If_no_resources_exists_then_empty_array_is_returned()
    {
        // Arrange
        ListResourcesQueryValidator validatorMock = Substitute.For<ListResourcesQueryValidator>();
        ILogger<ListResourcesHandler> loggerMock = Substitute.For<ILogger<ListResourcesHandler>>();
        IResourceReadRepository repositoryMock = Substitute.For<IResourceReadRepository>();
        CancellationToken cancellationToken = default;

        ListResourcesHandler handler = new(loggerMock, validatorMock, repositoryMock);
        ListResourcesQuery cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult());

        Guid assignedId = Guid.NewGuid();
        IEnumerable<Resource> resources = Array.Empty<Resource>();
        repositoryMock
            .List(cancellationToken)
            .ReturnsForAnyArgs(Task.FromResult(resources));

        // Act
        Either<ListResourcesQueryResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

        // Assert
        response.Should()
            .NotBeNull(because: "A response is expected in this scenario");
        response.IsLeft.Should()
            .BeTrue(because: "Response is expected in this scenario");
        response.Left.Should()
            .NotBeNull(because: "Response should NOT be null");
        response.Left.Resources.Should()
            .BeEquivalentTo(resources, because: "Expected returned resources to be equivalent to ones returned from repository");

        Received.InOrder(async () =>
        {
            await validatorMock.ValidateAsync(Arg.Any<ListResourcesQuery>(), cancellationToken).Received(1);
            await repositoryMock.List(cancellationToken).Received(1);
        });
    }

    [Fact]
    public async Task If_at_least_on_resource_exists_then_it_is_returned()
    {
        // Arrange
        ListResourcesQueryValidator validatorMock = Substitute.For<ListResourcesQueryValidator>();
        ILogger<ListResourcesHandler> loggerMock = Substitute.For<ILogger<ListResourcesHandler>>();
        IResourceReadRepository repositoryMock = Substitute.For<IResourceReadRepository>();
        CancellationToken cancellationToken = default;

        ListResourcesHandler handler = new(loggerMock, validatorMock, repositoryMock);
        ListResourcesQuery cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult());

        Guid assignedId = Guid.NewGuid();
        IEnumerable<Resource> resources = new Resource[1] { new() { Id = assignedId, Type = ResourceType.Folder } };

        IEnumerable<ResourceItem> resourceItems = new ResourceItem[1] { new() { Id = assignedId, UploadedBy = String.Empty } };

        repositoryMock
            .List(cmd.Id, cancellationToken)
            .ReturnsForAnyArgs(Task.FromResult(resources));

        // Act
        Either<ListResourcesQueryResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

        // Assert
        response.Should()
            .NotBeNull(because: "A response is expected in this scenario");
        response.IsLeft.Should()
            .BeTrue(because: "Response is expected in this scenario");
        response.Left.Should()
            .NotBeNull(because: "Response should NOT be null");
        response.Left.Resources.Should()
            .BeEquivalentTo(resourceItems, because: "Expected returned resources to be equivalent to ones returned from repository");

        Received.InOrder(async () =>
        {
            await validatorMock.ValidateAsync(Arg.Any<ListResourcesQuery>(), cancellationToken).Received(1);
            await repositoryMock.List(cancellationToken).Received(1);
        });
    }


    [Fact]
    public async Task If_validation_fails_then_errors_must_be_returned()
    {
        // Arrange
        ListResourcesQueryValidator validatorMock = Substitute.For<ListResourcesQueryValidator>();
        ILogger<ListResourcesHandler> loggerMock = Substitute.For<ILogger<ListResourcesHandler>>();
        IResourceReadRepository repositoryMock = Substitute.For<IResourceReadRepository>();
        CancellationToken cancellationToken = default;

        ListResourcesHandler handler = new(loggerMock, validatorMock, repositoryMock);
        ListResourcesQuery cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult(new ValidationFailure[1] { new("test", "error message") }));

        // Act
        Either<ListResourcesQueryResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

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
            await validatorMock.ValidateAsync(Arg.Any<ListResourcesQuery>(), cancellationToken).Received(1);
            await repositoryMock.List(cancellationToken).Received(0);
        });
    }
}