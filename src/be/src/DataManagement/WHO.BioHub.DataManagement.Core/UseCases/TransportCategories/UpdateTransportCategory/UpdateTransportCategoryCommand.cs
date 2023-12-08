namespace WHO.BioHub.DataManagement.Core.UseCases.TransportCategories.UpdateTransportCategory;

public record struct UpdateTransportCategoryCommand(Guid Id,
    string Name,
    string Description,
    string HexColor,
    bool IsActive);