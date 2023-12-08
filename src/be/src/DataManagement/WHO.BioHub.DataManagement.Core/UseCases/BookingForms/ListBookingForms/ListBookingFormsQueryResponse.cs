using WHO.BioHub.Models.Models;
using WHO.BioHub.Shared.Dto;

namespace WHO.BioHub.DataManagement.Core.UseCases.BookingForms.ListBookingForms;

public record struct ListBookingFormsQueryResponse(IEnumerable<CourierBookingFormDto> CourierBookingForms) { }