using FluentValidation;
using Microsoft.AspNetCore.Http;
using WHO.BioHub.Models.Repositories.Documents;
using WHO.BioHub.Models.Repositories.WorklistToBioHubItems;
using WHO.BioHub.Shared.Dto;
using WHO.BioHub.Shared.Enums;

namespace WHO.BioHub.DataManagement.Core.UseCases.WorklistToBioHubItems.UpdateWorklistToBioHubItemShipmentDocuments;

public class UpdateWorklistToBioHubItemShipmentDocumentsCommandValidator : AbstractValidator<UpdateWorklistToBioHubItemShipmentDocumentsCommand>
{    
    public UpdateWorklistToBioHubItemShipmentDocumentsCommandValidator()
    {
       
        RuleFor(cmd => cmd.CurrentStatus)
            .NotNull()
            .WithMessage("Current Status is required");

        RuleFor((cmd) => cmd.ShipmentDocumentOperationType)
            .NotNull()
            .WithMessage("Missing ShipmentDocumentOperationType");


        When(cmd => cmd.ShipmentDocumentOperationType == ShipmentDocumentOperationType.Add, () =>
        {
            RuleFor(cmd => cmd.File)
           .NotNull()
           .WithMessage("File is required")
           .Must((f) => IsExtensionValid(f))
           .WithMessage("Invalid File format");
        });
        When(cmd => cmd.ShipmentDocumentOperationType == ShipmentDocumentOperationType.Update, () =>
        {
            RuleFor(cmd => cmd.ShipmentDocumentId)
           .NotNull()
           .WithMessage("Shipment DocumentId is required");

            RuleFor(cmd => cmd.ShipmentDocumentNewName)
           .NotNull()
           .WithMessage("Shipment Document New Name is required");
        });

        When(cmd => cmd.ShipmentDocumentOperationType == ShipmentDocumentOperationType.Delete, () =>
        {
            RuleFor(cmd => cmd.ShipmentDocumentId)
           .NotNull()
           .WithMessage("Shipment DocumentId is required");
        });

    }

    protected bool IsExtensionValid(IFormFile file)
    {
        string extension = Path.GetExtension(file.FileName);

        if (string.IsNullOrEmpty(extension))
        {
            return false;
        }

        extension = extension.Replace(".", "").ToLower();

        switch (extension)
        {
            case "doc":
            case "docx":
            case "xls":
            case "xlsx":
            case "ppt":
            case "pptx":
            case "pdf":
            case "jpg":
            case "jpeg":
            case "mp4":
                return true;
            default:
                return false;

        }
    }
}