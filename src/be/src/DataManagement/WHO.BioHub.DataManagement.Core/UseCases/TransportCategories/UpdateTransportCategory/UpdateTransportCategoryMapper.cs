using WHO.BioHub.Models.Models;

namespace WHO.BioHub.DataManagement.Core.UseCases.TransportCategories.UpdateTransportCategory;

public interface IUpdateTransportCategoryMapper
{
    TransportCategory Map(TransportCategory transportcategory, UpdateTransportCategoryCommand command);
}

public class UpdateTransportCategoryMapper : IUpdateTransportCategoryMapper
{
    public TransportCategory Map(TransportCategory transportcategory, UpdateTransportCategoryCommand command)
    {

        transportcategory.Id = command.Id;
        transportcategory.Name = command.Name;
        transportcategory.Description = command.Description;
        transportcategory.HexColor = command.HexColor;
        transportcategory.IsActive = command.IsActive;
        transportcategory.DeletedOn = null;

        return transportcategory;
    }
}