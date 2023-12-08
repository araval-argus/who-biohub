using FluentValidation;
using Microsoft.AspNetCore.Http;
using WHO.BioHub.Models.Repositories.Documents;
using WHO.BioHub.Shared.Dto;
using WHO.BioHub.Shared.Enums;

namespace WHO.BioHub.DataManagement.Core.UseCases.SMTA1WorkflowItems.UpdateSMTA1WorkflowItem;

public class UpdateSMTA1WorkflowItemCommandValidator : AbstractValidator<UpdateSMTA1WorkflowItemCommand>
{
    private readonly IDocumentReadRepository _documentReadRepository;
    public UpdateSMTA1WorkflowItemCommandValidator(IDocumentReadRepository documentReadRepository)
    {
        _documentReadRepository = documentReadRepository;

        RuleFor(cmd => cmd.CurrentStatus)
            .NotNull()
            .WithMessage("Current Status is required");

        RuleFor(cmd => cmd.LastSubmissionApproved)
            .NotNull()
            .WithMessage("Last Submission Approved is required");

        When(cmd => cmd.IsPast == true, () =>
        {
            RuleFor(cmd => cmd.AssignedOperationDate)
                .NotNull()
                .WithMessage("'Operation Date' is required")
                .NotEmpty()
                .WithMessage("'Operation Date' is required");
        });

        When(cmd => cmd.LastSubmissionApproved == true, () =>
        {
            When(cmd => cmd.CurrentStatus == SMTA1WorkflowStatus.SubmitSMTA1, () =>
            {
                WhenAsync(async (cmd, token) => !(await CanSkipSMTA1Phase(cmd, token)), () =>
                {
                    RuleFor(cmd => cmd.File)
                    .NotNull()
                    .WithMessage("File is required")
                    .Must((f) => IsExtensionValid(f))
                    .WithMessage("Invalid File format");
                });

            });


            When(cmd => cmd.CurrentStatus == SMTA1WorkflowStatus.WaitingForSMTA1SECsApproval, () =>
            {

                WhenAsync(async (cmd, token) => !(await CanSkipSMTA1Phase(cmd, token)), () =>
                {
                    RuleFor(cmd => cmd.File)
                    .NotNull()
                    .WithMessage("File is required")
                    .Must((f) => IsExtensionValid(f))
                    .WithMessage("Invalid File format");
                });

            });


        });

        When(cmd => cmd.LastSubmissionApproved == false, () =>
        {
            RuleFor(cmd => cmd.Comment)
            .NotNull()
            .WithMessage("Reject Reason is required");
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

    protected async Task<bool> CanSkipSMTA1Phase(UpdateSMTA1WorkflowItemCommand cmd, CancellationToken cancellationToken)
    {
        var laboratoryId = cmd.UserLaboratoryId.GetValueOrDefault();
        DocumentFileType documentType = DocumentFileType.SMTA1;
        var res = await _documentReadRepository.IsDocumentSignedByLaboratoryId(laboratoryId, documentType, cancellationToken);
        return res;
    }
}