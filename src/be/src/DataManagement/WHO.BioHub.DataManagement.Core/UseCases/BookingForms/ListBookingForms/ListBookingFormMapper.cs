using WHO.BioHub.DataManagement.Core.UseCases.BookingForms.ListBookingForms;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Shared.Dto;

namespace WHO.BioHub.DataManagement.Core.UseCases.BookingForms.ListBookingForm;

public interface IListBookingFormsMapper
{
    IEnumerable<CourierBookingFormDto> Map(IEnumerable<BookingForm> entities);
}

public class ListBookingFormsMapper : IListBookingFormsMapper
{
    public IEnumerable<CourierBookingFormDto> Map(IEnumerable<BookingForm> entities)
    {        

        List<CourierBookingFormDto> courierBookingFormsDto = new List<CourierBookingFormDto>();

        foreach (var entity in entities)
        {
            CourierBookingFormDto courierBookingFormDto = new()
            {
                Id = entity.Id,
                WorklistFromBioHubItemId = entity.WorklistFromBioHubItemId,
                WorklistToBioHubItemId = entity.WorklistToBioHubItemId,
                ShipmentDirection = entity.WorklistToBioHubItemId != null ? "Into BioHub (SMTA 1)" : "Out of BioHub (SMTA 2)",
                ShipmentReferenceNumber = entity.ShipmentReferenceNumber,
                WorklistReferenceNumber = entity.WorklistFromBioHubItem != null ? entity.WorklistFromBioHubItem.ReferenceNumber : entity.WorklistToBioHubItem.ReferenceNumber,
                Date = entity.Date
            };
            courierBookingFormsDto.Add(courierBookingFormDto);
        };

        return courierBookingFormsDto;
    }
}