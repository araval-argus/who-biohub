namespace WHO.BioHub.DataManagement.Core.UseCases.Countries.UpdateCountry;

public record struct UpdateCountryCommand(Guid Id,
    string Name,
    string FullName,
    string Uncode,
    string Iso2,
    string Iso3,
    double Latitude,
    double Longitude,
    int? GmtHour,
    int? GmtMin,
    bool IsActive);