using FluentValidation;
using Microsoft.AspNetCore.Http;
using WHO.BioHub.Models.Repositories.Documents;
using WHO.BioHub.Shared.Dto;
using WHO.BioHub.Shared.Enums;

namespace WHO.BioHub.DataManagement.Core.UseCases.SMTA2WorkflowItems.UpdateSMTA2WorkflowItem;

public class UpdateSMTA2WorkflowItemCommandValidator : AbstractValidator<UpdateSMTA2WorkflowItemCommand>
{
    private readonly IDocumentReadRepository _documentReadRepository;
    public UpdateSMTA2WorkflowItemCommandValidator(IDocumentReadRepository documentReadRepository)
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
            When(cmd => cmd.CurrentStatus == SMTA2WorkflowStatus.SubmitSMTA2, () =>
            {

                WhenAsync(async (cmd, token) => !(await CanSkipSMTA2Phase(cmd, token)), () =>
                {
                    RuleFor(cmd => cmd.File)
                    .NotNull()
                    .WithMessage("File is required")
                    .Must((f) => IsExtensionValid(f))
                    .WithMessage("Invalid File format");
                });

            });

            When(cmd => cmd.CurrentStatus == SMTA2WorkflowStatus.WaitingForSMTA2SECsApproval, () =>
            {

                WhenAsync(async (cmd, token) => !(await CanSkipSMTA2Phase(cmd, token)), () =>
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

    protected async Task<bool> CanSkipSMTA2Phase(UpdateSMTA2WorkflowItemCommand cmd, CancellationToken cancellationToken)
    {
        var laboratoryId = cmd.UserLaboratoryId.GetValueOrDefault();
        DocumentFileType documentType = DocumentFileType.SMTA2;
        var res = await _documentReadRepository.IsDocumentSignedByLaboratoryId(laboratoryId, documentType, cancellationToken);
        return res;
    }


}