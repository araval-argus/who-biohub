using WHO.BioHub.Models.Models;
using WHO.BioHub.Shared.Dto;
namespace WHO.BioHub.Data.Core.UseCases.EForms.BookingFormOfSMTA1;

public record struct ReadBookingFormOfSMTA1QueryResponse(BookingFormOfSMTA1DataViewModel BookingFormOfSMTA1Data) { }