using WHO.BioHub.Models.Models;
using WHO.BioHub.Shared.Dto;
namespace WHO.BioHub.Data.Core.UseCases.EForms.BookingFormOfSMTA2;

public record struct ReadBookingFormOfSMTA2QueryResponse(BookingFormOfSMTA2DataViewModel BookingFormOfSMTA2Data) { }