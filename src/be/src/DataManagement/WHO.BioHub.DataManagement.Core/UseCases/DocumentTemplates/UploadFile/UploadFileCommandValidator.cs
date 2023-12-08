using FluentValidation;
using Microsoft.AspNetCore.Http;
using WHO.BioHub.Shared.Enums;

namespace WHO.BioHub.DataManagement.Core.UseCases.DocumentTemplates.UploadFile;

public class UploadFileCommandValidator : AbstractValidator<UploadFileCommand>
{
    public UploadFileCommandValidator()
    {
        RuleFor(cmd => cmd.File)
            .NotNull()
            .WithMessage("File is mandatory")
        .NotEmpty()
            .WithMessage("File is mandatory")
        .Must((f) => IsExtensionValid(f))
            .WithMessage("Invalid File format");

        RuleFor(cmd => cmd.DocumentTemplateFileType)
            .NotNull()
            .WithMessage("File Type is mandatory")
            .Must((d) => IsDocumentTemplateTypeValid(d))
            .WithMessage("Invalid File Type");

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
                return true;
            default:
                return false;

        }
    }

    protected bool IsDocumentTemplateTypeValid(DocumentFileType? type)
    {

        switch (type)
        {
            case DocumentFileType.SMTA1:
            case DocumentFileType.SMTA2:
            case DocumentFileType.Annex2OfSMTA1:
            case DocumentFileType.Annex2OfSMTA2:
            case DocumentFileType.BookingFormOfSMTA1:
            case DocumentFileType.BookingFormOfSMTA2:
            case DocumentFileType.PackagingList:
            case DocumentFileType.NonCommercialInvoiceCatA:
            case DocumentFileType.NonCommercialInvoiceCatB:
            case DocumentFileType.BiosafetyChecklist:
            case DocumentFileType.WHOGuidance:
                return true;
            default:
                return false;

        }
    }
}