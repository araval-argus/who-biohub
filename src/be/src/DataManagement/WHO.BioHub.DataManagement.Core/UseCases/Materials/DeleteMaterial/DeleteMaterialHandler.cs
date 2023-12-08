using FluentValidation.Results;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.Materials;
using WHO.BioHub.Shared.Enums;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UseCases.Materials.DeleteMaterial;

public interface IDeleteMaterialHandler
{
    Task<Either<DeleteMaterialCommandResponse, Errors>> Handle(DeleteMaterialCommand command, CancellationToken cancellationToken);
}

public class DeleteMaterialHandler : IDeleteMaterialHandler
{
    private readonly ILogger<DeleteMaterialHandler> _logger;
    private readonly DeleteMaterialCommandValidator _validator;
    private readonly IMaterialWriteRepository _writeRepository;

    public DeleteMaterialHandler(
        ILogger<DeleteMaterialHandler> logger,
        DeleteMaterialCommandValidator validator,
        IMaterialWriteRepository writeRepository
    )
    {
        _logger = logger;
        _validator = validator;
        _writeRepository = writeRepository;
    }

    public async Task<Either<DeleteMaterialCommandResponse, Errors>> Handle(
        DeleteMaterialCommand command,
        CancellationToken cancellationToken)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (!validationResult.IsValid)
            return new(new Errors(validationResult));
        IDbContextTransaction transaction = null;
        try
        {
            Material material;

            switch (command.RoleType)
            {
                case RoleType.WHO:
                case RoleType.BioHubFacility:
                    material = await _writeRepository.ReadForUpdate(command.Id, cancellationToken);
                    if (material == null)
                        return new(new Errors(ErrorType.NotFound, $"Material with Id {command.Id} not found"));
                    break;
                case RoleType.Laboratory:
                    material = await _writeRepository.ReadForUpdateForLaboratoryUser(command.Id, command.LaboratoryId.GetValueOrDefault(), cancellationToken);
                    if (material == null)
                        return new(new Errors(ErrorType.NotFound, $"Material with Id {command.Id} not found"));
                    break;
                default:
                    throw new InvalidOperationException();
                    break;
            }

            transaction = await _writeRepository.BeginTransactionAsync();

            await _writeRepository.CreateMaterialHistoryItem(material, cancellationToken, transaction);

            material.DeletedOn = DateTime.UtcNow;
            material.LastOperationDate = material.DeletedOn;
            material.LastOperationById = command.OperationById;

            Errors? errors = await _writeRepository.Update(material, cancellationToken, transaction);


            if (errors.HasValue)
            {
                await Rollback(transaction);
                return new(errors.Value);
            }

            await transaction.CommitAsync();
            await transaction.DisposeAsync();

            return new(new DeleteMaterialCommandResponse());
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error deleting the Material with {id}", command.Id);
            await Rollback(transaction);
            throw;
        }
    }

    private async Task Rollback(IDbContextTransaction transaction)
    {
        if (transaction != null)
        {
            await transaction.RollbackAsync();
            await transaction.DisposeAsync();
        }
    }
}