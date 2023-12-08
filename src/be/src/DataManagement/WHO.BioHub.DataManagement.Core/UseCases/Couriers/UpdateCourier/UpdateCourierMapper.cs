using WHO.BioHub.Models.Models;

namespace WHO.BioHub.DataManagement.Core.UseCases.Couriers.UpdateCourier;

public interface IUpdateCourierMapper
{
    Courier Map(Courier courier, UpdateCourierCommand command);
}

public class UpdateCourierMapper : IUpdateCourierMapper
{
    public Courier Map(Courier courier, UpdateCourierCommand command)
    {        

        courier.Id = command.Id;
        courier.CreationDate = DateTime.UtcNow;
        courier.Name = command.Name;
        courier.Email = command.Email.Replace(" ", "");
        courier.BusinessPhone = command.BusinessPhone;
        courier.Address = command.Address;
        courier.WHOAccountNumber = command.WHOAccountNumber;
        courier.Latitude = command.Latitude;
        courier.Longitude = command.Longitude;
        courier.IsActive = command.IsActive;
        courier.CountryId = command.CountryId;
        courier.Description = command.Description;

        courier.LastOperationByUserId = command.OperationById;
        courier.LastOperationDate = DateTime.UtcNow;

        courier.DeletedOn = null;

        return courier;
    }
}