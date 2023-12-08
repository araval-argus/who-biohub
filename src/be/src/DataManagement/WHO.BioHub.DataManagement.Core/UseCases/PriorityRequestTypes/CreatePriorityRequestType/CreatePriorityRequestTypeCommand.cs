namespace WHO.BioHub.DataManagement.Core.UseCases.PriorityRequestTypes.CreatePriorityRequestType;

public record struct CreatePriorityRequestTypeCommand(
    string Name,
    string Description,
    string HexColor,
    bool IsActive
);