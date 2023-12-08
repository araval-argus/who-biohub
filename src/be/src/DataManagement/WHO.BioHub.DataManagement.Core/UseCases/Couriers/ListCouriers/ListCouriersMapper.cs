using WHO.BioHub.Models.Models;
using WHO.BioHub.Shared.Dto;
using WHO.BioHub.Shared.Enums;

namespace WHO.BioHub.DataManagement.Core.UseCases.Couriers.ListCouriers;

public interface IListCouriersMapper
{
    IEnumerable<CourierViewModel> Map(IEnumerable<Courier> couriers);
}

public class ListCouriersMapper : IListCouriersMapper
{
    public IEnumerable<CourierViewModel> Map(IEnumerable<Courier> couriers)
    {      
        List<CourierViewModel> list = new List<CourierViewModel>();

        foreach (var courier in couriers)
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

            list.Add(courierViewModel);
        }

        return list;
    }
}