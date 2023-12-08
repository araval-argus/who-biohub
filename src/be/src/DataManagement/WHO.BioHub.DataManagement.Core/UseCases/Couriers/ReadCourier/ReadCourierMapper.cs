using WHO.BioHub.DataManagement.Core.UseCases.Couriers.ReadCourier;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Shared.Dto;
using WHO.BioHub.Shared.Enums;

namespace WHO.BioHub.DataManagement.Core.UseCases.Couriers.ReadCourier;

public interface IReadCourierMapper
{
    CourierViewModel Map(Courier entity);
}

public class ReadCourierMapper : IReadCourierMapper
{
    public CourierViewModel Map(Courier courier)
    {

        CourierViewModel courierViewModel = new()
        {
            Id = courier.Id,
            Address = courier.Address,
            BusinessPhone = courier.BusinessPhone,
            CountryId = courier.CountryId,
            Description = courier.Description,
            Email = courier.Email,
            IsActive = courier.IsActive,
            Latitude = courier.Latitude,
            Longitude = courier.Longitude,
            Name = courier.Name,
            WHOAccountNumber = courier.WHOAccountNumber,
        };



        return courierViewModel;
    }

}
