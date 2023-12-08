using WHO.BioHub.Models.Models;

namespace WHO.BioHub.DataManagement.Core.UseCases.Couriers.CreateCourier;

public interface ICreateCourierMapper
{
    Courier Map(CreateCourierCommand command);
}

public class CreateCourierMapper : ICreateCourierMapper
{
    public Courier Map(CreateCourierCommand command)
    {       

        Courier courier = new()
        {
            Id = Guid.NewGuid(),
            CreationDate = DateTime.UtcNow,
            Name = command.Name,
            Email = command.Email.Replace(" ",""),
            Address = command.Address,
            BusinessPhone = command.BusinessPhone,
            WHOAccountNumber = command.WHOAccountNumber,
            Latitude = command.Latitude,
            Longitude = command.Longitude,
            IsActive = command.IsActive,
            CountryId = command.CountryId,
            Description = command.Description,
            LastOperationByUserId = command.OperationById,
            LastOperationDate = DateTime.UtcNow,         

            DeletedOn = null,
        };

        return courier;
    }
}