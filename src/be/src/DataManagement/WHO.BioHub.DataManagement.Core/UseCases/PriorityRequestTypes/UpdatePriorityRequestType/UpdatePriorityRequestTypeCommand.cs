namespace WHO.BioHub.DataManagement.Core.UseCases.PriorityRequestTypes.UpdatePriorityRequestType;

public record struct UpdatePriorityRequestTypeCommand(Guid Id,
    string Name,
    string Description,
    string HexColor,
    bool IsActive);