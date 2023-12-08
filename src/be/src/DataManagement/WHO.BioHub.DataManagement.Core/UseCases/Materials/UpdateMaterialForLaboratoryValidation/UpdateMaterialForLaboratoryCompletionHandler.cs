using FluentValidation.Results;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.Materials;
using WHO.BioHub.Shared.Enums;
using WHO.BioHub.Shared.Permissions;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UseCases.Materials.UpdateMaterialForLaboratoryCompletion;

public interface IUpdateMaterialForLaboratoryCompletionHandler
{
    Task<Either<UpdateMaterialForLaboratoryCompletionCommandResponse, Errors>> Handle(UpdateMaterialForLaboratoryCompletionCommand command, CancellationToken cancellationToken);
}

public class UpdateMaterialForLaboratoryCompletionHandler : IUpdateMaterialForLaboratoryCompletionHandler
{
    private readonly ILogger<UpdateMaterialForLaboratoryCompletionHandler> _logger;
    private readonly UpdateMaterialForLaboratoryCompletionCommandValidator _validator;
    private readonly IUpdateMaterialForLaboratoryCompletionMapper _mapper;
    private readonly IMaterialWriteRepository _writeRepository;

    public UpdateMaterialForLaboratoryCompletionHandler(
        ILogger<UpdateMaterialForLaboratoryCompletionHandler> logger,
        UpdateMaterialForLaboratoryCompletionCommandValidator validator,
        IUpdateMaterialForLaboratoryCompletionMapper mapper,
        IMaterialWriteRepository writeRepository
    )
    {
        _logger = logger;
        _validator = validator;
        _mapper = mapper;
        _writeRepository = writeRepository;
    }

    public async Task<Either<UpdateMaterialForLaboratoryCompletionCommandResponse, Errors>> Handle(
        UpdateMaterialForLaboratoryCompletionCommand command,
        CancellationToken cancellationToken)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (!validationResult.IsValid)
            return new(new Errors(validationResult));

        Errors? errors;               

        IDbContextTransaction transaction = null;

        try
        {
            Material material = await _writeRepository.ReadForUpdate(command.Id, cancellationToken);

            if (material.Status != MaterialStatus.WaitingForLaboratoryCompletion)
            {
                return new(new Errors(ErrorType.NotFound, $"Item Status mismatch with the current page status - Please refresh the page"));
            }

            if (material.ReferenceId != command.ReferenceId)
            {
                return new(new Errors(ErrorType.NotFound, $"The current page is not up to date - Please refresh the page"));
            }

            if (material.BHFShareReadiness == Readiness.NotReady && command.PublicShare == YesNoOption.Yes)
            {
                return new(new Errors(ErrorType.RequestParsing, $"A not Ready to share material cannot be set as public"));
            }

            if (material.PublicShare == YesNoOption.Yes && command.BHFShareReadiness == Readiness.NotReady)
            {
                return new(new Errors(ErrorType.RequestParsing, $"A public material cannot be set as not Ready to share"));
            }

            if (material.Status != MaterialStatus.Completed && command.PublicShare == YesNoOption.Yes)
            {
                return new(new Errors(ErrorType.RequestParsing, $"A material cannot be set as public if it is not yet approved"));
            }

            if (material.ShipmentMaterialCondition == ShipmentMaterialCondition.Damaged && !command.UserPermissions.Contains(PermissionNames.CanEditMaterialShipmentInformation))
            {
                return new(new Errors(ErrorType.Unauthorized, $"Cannot modify a damaged material"));
            }
            
            transaction = await _writeRepository.BeginTransactionAsync();
            
            await _writeRepository.CreateMaterialHistoryItem(material, cancellationToken, transaction);

            if (command.UserPermissions.Contains(PermissionNames.CanEditMaterial))
            {
                material = _mapper.MapFields(material, command);
            }

            if (command.UserPermissions.Contains(PermissionNames.CanVerifyMaterial))
            {
                material = _mapper.MapValidations(material, command);
            }

            if (command.UserPermissions.Contains(PermissionNames.CanSetMaterialReadyToShare))
            {                
                material.BHFShareReadiness = command.BHFShareReadiness; 
            }

            if (command.UserPermissions.Contains(PermissionNames.CanSetMaterialPublic))
            {               
                material.PublicShare = command.PublicShare;
            }

            if (command.UserPermissions.Contains(PermissionNames.CanApproveLaboratoryCompletion))
            {
                material = _mapper.MapApprove(material, command);
            }

            if (command.UserPermissions.Contains(PermissionNames.CanAddMaterialNewVials))
            {
                material.CurrentNumberOfVials = command.NumberOfVialsToAdd != null ? material.CurrentNumberOfVials + command.NumberOfVialsToAdd : material.CurrentNumberOfVials;
                material.LastAliquotsAdditionDate = command.LastAliquotsAdditionDate;
                material.AddedAliquots = command.NumberOfVialsToAdd != null ? command.NumberOfVialsToAdd : 0;
            }

            if (command.UserPermissions.Contains(PermissionNames.CanEditMaterialWarningEmailCurrentNumberOfVialsThreshold))
            {
                material.WarningEmailCurrentNumberOfVialsThreshold = command.WarningEmailCurrentNumberOfVialsThreshold != null ? command.WarningEmailCurrentNumberOfVialsThreshold : material.WarningEmailCurrentNumberOfVialsThreshold;
            }

            material.LastOperationById = command.UserId;
            material.LastOperationDate = DateTime.UtcNow;
            material.ReferenceId = Guid.NewGuid();

            if (command.UserPermissions.Contains(PermissionNames.CanEditMaterialShipmentInformation))
            {
                errors = await _writeRepository.Update(material, command.MaterialGSDInfo, command.MaterialCollectedSpecimenTypes.ToList(), cancellationToken);
            }

            else
            {
                errors = await _writeRepository.Update(material, command.MaterialGSDInfo, cancellationToken);
            }

            if (errors.HasValue)
            {
                await Rollback(transaction);
                return new(errors.Value);
            }

            await transaction.CommitAsync();
            await transaction.DisposeAsync();

            return new(new UpdateMaterialForLaboratoryCompletionCommandResponse(material.Id));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error creating a new Material");
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