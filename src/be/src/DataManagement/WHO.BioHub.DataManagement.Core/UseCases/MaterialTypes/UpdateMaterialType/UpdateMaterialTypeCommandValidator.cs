using FluentValidation;

namespace WHO.BioHub.DataManagement.Core.UseCases.MaterialTypes.UpdateMaterialType;

public class UpdateMaterialTypeCommandValidator : AbstractValidator<UpdateMaterialTypeCommand>
{
    public UpdateMaterialTypeCommandValidator()
    {
        RuleFor(cmd => cmd);
    }
}