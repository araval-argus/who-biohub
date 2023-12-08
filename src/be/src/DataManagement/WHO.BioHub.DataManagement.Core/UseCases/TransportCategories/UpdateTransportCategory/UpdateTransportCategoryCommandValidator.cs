using FluentValidation;

namespace WHO.BioHub.DataManagement.Core.UseCases.TransportCategories.UpdateTransportCategory;

public class UpdateTransportCategoryCommandValidator : AbstractValidator<UpdateTransportCategoryCommand>
{
    public UpdateTransportCategoryCommandValidator()
    {
        RuleFor(cmd => cmd);
    }
}