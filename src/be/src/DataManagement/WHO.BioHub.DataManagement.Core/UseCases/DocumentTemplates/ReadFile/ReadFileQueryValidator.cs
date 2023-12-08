using FluentValidation;

namespace WHO.BioHub.DataManagement.Core.UseCases.DocumentTemplates.ReadFile;

public class ReadFileQueryValidator : AbstractValidator<ReadFileQuery>
{
    public ReadFileQueryValidator()
    {
        RuleFor(cmd => cmd);
    }
}