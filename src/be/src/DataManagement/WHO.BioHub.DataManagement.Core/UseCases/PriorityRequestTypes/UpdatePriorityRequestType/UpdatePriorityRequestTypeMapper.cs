using WHO.BioHub.Models.Models;

namespace WHO.BioHub.DataManagement.Core.UseCases.PriorityRequestTypes.UpdatePriorityRequestType;

public interface IUpdatePriorityRequestTypeMapper
{
    PriorityRequestType Map(PriorityRequestType priorityrequesttype, UpdatePriorityRequestTypeCommand command);
}

public class UpdatePriorityRequestTypeMapper : IUpdatePriorityRequestTypeMapper
{
    public PriorityRequestType Map(PriorityRequestType priorityrequesttype, UpdatePriorityRequestTypeCommand command)
    {

        priorityrequesttype.Id = command.Id;
        priorityrequesttype.Name = command.Name;
        priorityrequesttype.Description = command.Description;
        priorityrequesttype.HexColor = command.HexColor;
        priorityrequesttype.IsActive = command.IsActive;
        priorityrequesttype.DeletedOn = null;

        return priorityrequesttype;
    }
}