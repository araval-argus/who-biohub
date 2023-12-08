using FluentValidation;
using WHO.BioHub.Models.Repositories.Shipments;
using WHO.BioHub.Models.Repositories.WorklistItemUsedReferenceNumber;

namespace WHO.BioHub.DataManagement.Core.UseCases.Shipments.UpdateShipment;

public class UpdateShipmentCommandValidator : AbstractValidator<UpdateShipmentCommand>
{
    private readonly IWorklistItemUsedReferenceNumberReadRepository _readUsedReferenceNumberRepository;
    private readonly IShipmentReadRepository _readShipmentRepository;

    public UpdateShipmentCommandValidator(
        IWorklistItemUsedReferenceNumberReadRepository readUsedReferenceNumberRepository,
        IShipmentReadRepository readShipmentRepository)
    {
        _readUsedReferenceNumberRepository = readUsedReferenceNumberRepository;
        _readShipmentRepository = readShipmentRepository;
      

        RuleFor(cmd => cmd.ReferenceNumber)
            .NotEmpty().WithMessage("'Shipment Reference Number' is required")
            .MustAsync(async (command, referenceNumber, token) => await ReferenceNumberNotPresent(command.Id, referenceNumber, token))
            .WithMessage("'Shipment Reference Number' already present"); ;


    }

    protected async Task<bool> ReferenceNumberNotPresent(Guid id, string referenceNumber, CancellationToken token)
    {
        var shipment = await _readShipmentRepository.Read(id, token);

        if (shipment == null || shipment.ReferenceNumber == referenceNumber)
        {
            return true;
        }       
        
        if (shipment.WorklistToBioHubItem != null && shipment.WorklistToBioHubItem.IsPast != true)
        {
            return false;
        }

        if (shipment.WorklistFromBioHubItem != null && shipment.WorklistFromBioHubItem.IsPast != true)
        {
            return false;
        }

        return !(await _readUsedReferenceNumberRepository.ReferenceNumberAlreadyPresent(referenceNumber, token));
    }
}

