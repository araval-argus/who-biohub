namespace WHO.BioHub.DataManagement.Core.UseCases.Countries.CreateCountry;

public record struct CreateCountryCommand(
    string Name,
    string FullName,
    string Uncode,
    string Iso2,
    string Iso3,
    double Latitude,
    double Longitude,
    int? GmtHour,
    int? GmtMin,
    bool IsActive
);