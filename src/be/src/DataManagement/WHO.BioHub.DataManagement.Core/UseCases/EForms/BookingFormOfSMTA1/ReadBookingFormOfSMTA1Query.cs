using WHO.BioHub.Shared.Enums;

namespace WHO.BioHub.Data.Core.UseCases.EForms.BookingFormOfSMTA1;

public record struct ReadBookingFormOfSMTA1Query(Guid WorklistId, RoleType? RoleType, Guid? BioHubFacilityId, Guid? LaboratoryId, IEnumerable<string> UserPermissions) { }