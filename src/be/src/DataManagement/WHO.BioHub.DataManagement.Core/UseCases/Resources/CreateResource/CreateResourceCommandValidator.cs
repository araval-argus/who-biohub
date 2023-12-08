using FluentValidation;
using WHO.BioHub.Shared.Enums;

namespace WHO.BioHub.DataManagement.Core.UseCases.Resources.CreateResource;

public class CreateResourceCommandValidator : AbstractValidator<CreateResourceCommand>
{
    public CreateResourceCommandValidator()
    {
        RuleFor(cmd => cmd.Id)
            .NotEmpty()
            .WithMessage("Id is mandatory")
            .NotNull()
            .WithMessage("Id is mandatory");
            
        RuleFor(cmd => cmd.FileCompleteName)
            .NotEmpty()
            .WithMessage("File is mandatory")
            .NotNull()
            .WithMessage("File is mandatory")
            .Must((f) => IsFileNameValid(f))
            .WithMessage("Invalid File name")
            .Must((f) => IsExtensionValid(f))
            .WithMessage("Invalid File format");

        RuleFor(cmd => cmd.FileType)
           .NotNull()
           .WithMessage("File Type is mandatory")
           .Must((d) => IsFileTypeValid(d))
           .WithMessage("Invalid File Type");
    }

    protected bool IsFileTypeValid(ResourceFileType? type)
    {

        switch (type)
        {
            case ResourceFileType.SMTA1:
            case ResourceFileType.SMTA2:
            case ResourceFileType.Shipment:
            case ResourceFileType.BiosafetyAndBiosecurity:
                return true;
            default:
                return false;

        }
    }

    protected bool IsExtensionValid(string completeFileName)
    {
        if (!completeFileName.Contains("."))
        {
            return false;
        }

        string extension = GetFileExtension(completeFileName);

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

    protected bool IsFileNameValid(string completeFileName)
    {
        string fileName = GetFileName(completeFileName);

        if (string.IsNullOrEmpty(fileName))
        {
            return false;
        }

        return true;
    }

    private string GetFileExtension(string fileCompleteName)
    {
        var nameParts = fileCompleteName.Split(".");

        return nameParts.LastOrDefault() ?? string.Empty;
    }

    private string GetFileName(string fileCompleteName)
    {
        var nameParts = fileCompleteName.Split(".");

        string fileName = string.Join(".", nameParts.SkipLast(1).ToArray());

        return fileName;
    }
}