namespace WHO.BioHub.DataManagement.Core.UseCases.TransportCategories.CreateTransportCategory;

public record struct CreateTransportCategoryCommand(
    string Name,
    string Description,
    string HexColor,
    bool IsActive
);