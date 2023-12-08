using FluentValidation;

namespace WHO.BioHub.DataManagement.Core.UseCases.TransportCategories.DeleteTransportCategory;

public class DeleteTransportCategoryCommandValidator : AbstractValidator<DeleteTransportCategoryCommand>
{
    public DeleteTransportCategoryCommandValidator()
    {
        RuleFor(cmd => cmd);
    }
}