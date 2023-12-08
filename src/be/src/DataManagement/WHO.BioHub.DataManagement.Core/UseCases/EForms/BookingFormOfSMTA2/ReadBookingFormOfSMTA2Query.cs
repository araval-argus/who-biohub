using WHO.BioHub.Shared.Enums;

namespace WHO.BioHub.Data.Core.UseCases.EForms.BookingFormOfSMTA2;

public record struct ReadBookingFormOfSMTA2Query(Guid WorklistId, RoleType? RoleType, Guid? BioHubFacilityId, Guid? LaboratoryId, IEnumerable<string> UserPermissions) { }