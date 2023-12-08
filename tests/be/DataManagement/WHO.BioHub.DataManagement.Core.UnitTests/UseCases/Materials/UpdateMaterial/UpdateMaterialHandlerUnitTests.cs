using FluentValidation.Results;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.Materials;
using WHO.BioHub.DataManagement.Core.UseCases.Materials.UpdateMaterial;
using WHO.BioHub.Shared.Utils;
using WHO.BioHub.Shared.Dto;
using Microsoft.EntityFrameworkCore.Storage;

namespace WHO.BioHub.DataManagement.Core.UnitTests.UseCases.Materials.UpdateMaterial;

public class UpdateMaterialHandlerUnitTests
{
    [Fact]
    public async Task If_resource_is_updated_then_a_valid_response_is_returned()
    {
        // Arrange
        IMaterialReadRepository readRepositoryMock = Substitute.For<IMaterialReadRepository>();
        UpdateMaterialCommandValidator validatorMock = Substitute.For<UpdateMaterialCommandValidator>(readRepositoryMock);
        ILogger<UpdateMaterialHandler> loggerMock = Substitute.For<ILogger<UpdateMaterialHandler>>();
        IMaterialWriteRepository repositoryMock = Substitute.For<IMaterialWriteRepository>();
        IUpdateMaterialMapper mapperMock = Substitute.For<IUpdateMaterialMapper>();
        CancellationToken cancellationToken = default;
        IDbContextTransaction transactionMock = Substitute.For<IDbContextTransaction>();

        UpdateMaterialHandler handler = new(loggerMock, validatorMock, mapperMock, repositoryMock);

        Guid referenceId = Guid.NewGuid();

        UpdateMaterialCommand cmd = new() { ReferenceId = referenceId, UserPermissions = new List<string>() };

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult());

       

        Material material = new() { Id = Guid.NewGuid(), ReferenceId = referenceId };
        Material materialMapped = new() { Id = material.Id };
        List<MaterialGSDInfoDto> materialGSDInfo = new List<MaterialGSDInfoDto>();

        mapperMock.Map(material, cmd).ReturnsForAnyArgs(materialMapped);
        
        readRepositoryMock.ReferenceNumberAlreadyPresentForCreation(material.ReferenceNumber, cancellationToken).ReturnsForAnyArgs(Task.FromResult(false));
        
        repositoryMock.BeginTransactionAsync().ReturnsForAnyArgs(Task.FromResult(transactionMock));
        
        repositoryMock
            .ReadForUpdate(cmd.Id, cancellationToken)
            .ReturnsForAnyArgs(Task.FromResult(material));

        repositoryMock
            .Update(material, materialGSDInfo, cancellationToken, transactionMock)
            .ReturnsForAnyArgs(Task.FromResult<Errors?>(null));

        repositoryMock.CreateMaterialHistoryItem(material, cancellationToken, transactionMock)
            .ReturnsForAnyArgs(Task.FromResult<Errors?>(null));

        // Act
        Either<UpdateMaterialCommandResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

        // Assert
        response.Should()
            .NotBeNull(because: "A response is expected in this scenario");
        response.IsLeft.Should()
            .BeTrue(because: "Response is expected in this scenario");
        response.Left.Should()
            .NotBeNull(because: "Response should NOT be null");
        response.Left.Id.Should()
            .NotBeEmpty(because: "Material should NOT be null");
        response.Left.Id.Should()
            .Be(material.Id, because: "Returned material must mach the one provided in request");
        Received.InOrder(async () =>
        {           
            mapperMock.Map(material, Arg.Any<UpdateMaterialCommand>()).Received(1);
            await repositoryMock.Update(Arg.Any<Material>(), Arg.Any<List<MaterialGSDInfoDto>>(), cancellationToken, transactionMock).Received(1);
        });
    }

    [Fact]
    public async Task If_validation_fails_then_errors_must_be_returned()
    {
        // Arrange
        IMaterialReadRepository readRepositoryMock = Substitute.For<IMaterialReadRepository>();
        UpdateMaterialCommandValidator validatorMock = Substitute.For<UpdateMaterialCommandValidator>(readRepositoryMock);
        ILogger<UpdateMaterialHandler> loggerMock = Substitute.For<ILogger<UpdateMaterialHandler>>();
        IMaterialWriteRepository repositoryMock = Substitute.For<IMaterialWriteRepository>();
        IUpdateMaterialMapper mapperMock = Substitute.For<IUpdateMaterialMapper>();
        CancellationToken cancellationToken = default;

        UpdateMaterialHandler handler = new(loggerMock, validatorMock, mapperMock, repositoryMock);
        UpdateMaterialCommand cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult(new ValidationFailure[1] { new("test", "error message") }));

        // Act
        Either<UpdateMaterialCommandResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

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
            await validatorMock.ValidateAsync(Arg.Any<UpdateMaterialCommand>(), cancellationToken).Received(1);
            mapperMock.Map(Arg.Any<Material>(), Arg.Any<UpdateMaterialCommand>()).Received(0);
            await repositoryMock.Update(Arg.Any<Material>(), Arg.Any<List<MaterialGSDInfoDto>>(), cancellationToken).Received(0);
        });
    }

    [Fact]
    public async Task If_errors_are_returned_from_repository_then_they_must_be_forwarded_from_handler()
    {
        // Arrange
        IMaterialReadRepository readRepositoryMock = Substitute.For<IMaterialReadRepository>();
        UpdateMaterialCommandValidator validatorMock = Substitute.For<UpdateMaterialCommandValidator>(readRepositoryMock);
        ILogger<UpdateMaterialHandler> loggerMock = Substitute.For<ILogger<UpdateMaterialHandler>>();
        IMaterialWriteRepository repositoryMock = Substitute.For<IMaterialWriteRepository>();
        IUpdateMaterialMapper mapperMock = Substitute.For<IUpdateMaterialMapper>();
        CancellationToken cancellationToken = default;
        IDbContextTransaction transactionMock = Substitute.For<IDbContextTransaction>();
        UpdateMaterialHandler handler = new(loggerMock, validatorMock, mapperMock, repositoryMock);
        
        
        Guid referenceId = Guid.NewGuid();

        UpdateMaterialCommand cmd = new() { ReferenceId = referenceId, UserPermissions = new List<string>() };
        
        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult());

        Material material = new() { ReferenceId = referenceId };
        Material materialMapped = new();
        mapperMock.Map(material, cmd).ReturnsForAnyArgs(materialMapped);
        
        var materialGSDInfoDto = new List<MaterialGSDInfoDto>();

        readRepositoryMock.ReferenceNumberAlreadyPresentForCreation(material.ReferenceNumber, cancellationToken).ReturnsForAnyArgs(Task.FromResult(false));

        repositoryMock.BeginTransactionAsync().ReturnsForAnyArgs(Task.FromResult(transactionMock));

        repositoryMock
            .ReadForUpdate(cmd.Id, cancellationToken)
            .ReturnsForAnyArgs(Task.FromResult(material));
        // TODO: change error type
        repositoryMock
            .Update(material, materialGSDInfoDto, cancellationToken, transactionMock)
            .ReturnsForAnyArgs(Task.FromResult<Errors?>(
                new Errors(ErrorType.Validation, "I should be changed to a persistence error type")));

        // Act
        Either<UpdateMaterialCommandResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

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
            await validatorMock.ValidateAsync(Arg.Any<UpdateMaterialCommand>(), cancellationToken).Received(1);
            mapperMock.Map(Arg.Any<Material>(), Arg.Any<UpdateMaterialCommand>()).Received(1);
            await repositoryMock.Update(Arg.Any<Material>(), Arg.Any<List<MaterialGSDInfoDto>>(), cancellationToken, transactionMock).Received(1);
        });
    }
}