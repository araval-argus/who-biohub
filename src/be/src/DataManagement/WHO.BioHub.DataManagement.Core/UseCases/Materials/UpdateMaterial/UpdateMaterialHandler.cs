using FluentValidation.Results;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.Materials;
using WHO.BioHub.Shared.Enums;
using WHO.BioHub.Shared.Permissions;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UseCases.Materials.UpdateMaterial;

public interface IUpdateMaterialHandler
{
    Task<Either<UpdateMaterialCommandResponse, Errors>> Handle(UpdateMaterialCommand command, CancellationToken cancellationToken);
}

public class UpdateMaterialHandler : IUpdateMaterialHandler
{
    private readonly ILogger<UpdateMaterialHandler> _logger;
    private readonly UpdateMaterialCommandValidator _validator;
    private readonly IUpdateMaterialMapper _mapper;
    private readonly IMaterialWriteRepository _writeRepository;

    public UpdateMaterialHandler(
        ILogger<UpdateMaterialHandler> logger,
        UpdateMaterialCommandValidator validator,
        IUpdateMaterialMapper mapper,
        IMaterialWriteRepository writeRepository
    )
    {
        _logger = logger;
        _validator = validator;
        _mapper = mapper;
        _writeRepository = writeRepository;
    }

    public async Task<Either<UpdateMaterialCommandResponse, Errors>> Handle(
        UpdateMaterialCommand command,
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

            if (material.Status != MaterialStatus.Completed)
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

            material = _mapper.Map(material, command);

            if (command.UserPermissions.Contains(PermissionNames.CanSetMaterialReadyToShare))
            {                
                material.BHFShareReadiness = command.BHFShareReadiness;
            }

            if (command.UserPermissions.Contains(PermissionNames.CanSetMaterialPublic))
            {               
                material.PublicShare = command.PublicShare;
            }

            if (command.UserPermissions.Contains(PermissionNames.CanAddMaterialNewVials))
            {
                material.CurrentNumberOfVials = command.NumberOfVialsToAdd != null ? material.CurrentNumberOfVials + command.NumberOfVialsToAdd : material.CurrentNumberOfVials;
                material.LastAliquotsAdditionDate = command.LastAliquotsAdditionDate;
                material.AddedAliquots = command.NumberOfVialsToAdd != null ? command.NumberOfVialsToAdd : 0;
            }

            if (command.UserPermissions.Contains(PermissionNames.CanEditMaterialOwnerBioHubFacility))
            {
                material.OwnerBioHubFacilityId = command.OwnerBioHubFacilityId != null ? command.OwnerBioHubFacilityId : material.OwnerBioHubFacilityId;
            }

            if (command.UserPermissions.Contains(PermissionNames.CanEditMaterialWarningEmailCurrentNumberOfVialsThreshold))
            {
                material.WarningEmailCurrentNumberOfVialsThreshold = command.WarningEmailCurrentNumberOfVialsThreshold != null ? command.WarningEmailCurrentNumberOfVialsThreshold : material.WarningEmailCurrentNumberOfVialsThreshold;
            }

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

            return new(new UpdateMaterialCommandResponse(material.Id));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error updating Material");
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