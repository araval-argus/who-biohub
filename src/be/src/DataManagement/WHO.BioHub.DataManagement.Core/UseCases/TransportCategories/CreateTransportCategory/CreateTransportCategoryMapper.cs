using WHO.BioHub.Models.Models;

namespace WHO.BioHub.DataManagement.Core.UseCases.TransportCategories.CreateTransportCategory;

public interface ICreateTransportCategoryMapper
{
    TransportCategory Map(CreateTransportCategoryCommand command);
}

public class CreateTransportCategoryMapper : ICreateTransportCategoryMapper
{
    public TransportCategory Map(CreateTransportCategoryCommand command)
    {

        TransportCategory transportcategory = new()
        {
            Id = Guid.NewGuid(),
            CreationDate = DateTime.UtcNow,
            Name = command.Name,
            Description = command.Description,
            HexColor = command.HexColor,
            IsActive = command.IsActive,
            DeletedOn = null,
        };

        return transportcategory;
    }
}