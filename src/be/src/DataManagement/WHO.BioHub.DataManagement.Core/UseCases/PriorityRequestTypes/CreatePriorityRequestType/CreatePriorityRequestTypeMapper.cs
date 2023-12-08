using WHO.BioHub.Models.Models;

namespace WHO.BioHub.DataManagement.Core.UseCases.PriorityRequestTypes.CreatePriorityRequestType;

public interface ICreatePriorityRequestTypeMapper
{
    PriorityRequestType Map(CreatePriorityRequestTypeCommand command);
}

public class CreatePriorityRequestTypeMapper : ICreatePriorityRequestTypeMapper
{
    public PriorityRequestType Map(CreatePriorityRequestTypeCommand command)
    {        

        PriorityRequestType priorityrequesttype = new()
        {
            Id = Guid.NewGuid(),
            CreationDate = DateTime.UtcNow,
            Name = command.Name,
            Description = command.Description,
            HexColor = command.HexColor,
            IsActive = command.IsActive,
            DeletedOn = null,
        };

        return priorityrequesttype;
    }
}