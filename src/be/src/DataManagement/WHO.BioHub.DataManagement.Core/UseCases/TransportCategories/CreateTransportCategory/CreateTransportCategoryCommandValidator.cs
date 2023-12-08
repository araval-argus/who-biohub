using FluentValidation;

namespace WHO.BioHub.DataManagement.Core.UseCases.TransportCategories.CreateTransportCategory;

public class CreateTransportCategoryCommandValidator : AbstractValidator<CreateTransportCategoryCommand>
{
    public CreateTransportCategoryCommandValidator()
    {
        RuleFor(cmd => cmd);
    }
}