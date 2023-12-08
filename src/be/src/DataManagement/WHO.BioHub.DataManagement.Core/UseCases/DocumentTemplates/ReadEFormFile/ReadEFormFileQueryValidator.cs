using FluentValidation;

namespace WHO.BioHub.DataManagement.Core.UseCases.DocumentTemplates.ReadEFormFile;

public class ReadEFormFileQueryValidator : AbstractValidator<ReadEFormFileQuery>
{
    public ReadEFormFileQueryValidator()
    {
        RuleFor(cmd => cmd);
    }
}