using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.Countries;
using WHO.BioHub.Shared.Dto;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UseCases.Countries.ListCountries;

public interface IListCountriesHandler
{
    Task<Either<ListCountriesQueryResponse, Errors>> Handle(ListCountriesQuery query, CancellationToken cancellationToken);
}

public class ListCountriesHandler : IListCountriesHandler
{
    private readonly ILogger<ListCountriesHandler> _logger;
    private readonly ListCountriesQueryValidator _validator;
    private readonly ICountryReadRepository _readRepository;

    public ListCountriesHandler(
        ILogger<ListCountriesHandler> logger,
        ListCountriesQueryValidator validator,
        ICountryReadRepository readRepository)
    {
        _logger = logger;
        _validator = validator;
        _readRepository = readRepository;
    }

    public async Task<Either<ListCountriesQueryResponse, Errors>> Handle(
        ListCountriesQuery query,
        CancellationToken cancellationToken)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(query, cancellationToken);
        if (!validationResult.IsValid)
            return new(new Errors(validationResult));

        try
        {
            IEnumerable<Country> countries = await _readRepository.List(cancellationToken);
            var countryDtos = new List<CountryDto>();
            foreach (var country in countries)
            {
                CountryDto countryDto = new()
                {
                    Id = country.Id,
                    Uncode = country.Uncode,
                    Name = country.Name,
                    FullName = country.FullName,
                    Iso2 = country.Iso2,
                    Iso3 = country.Iso3,
                    Latitude = country.Latitude,
                    Longitude = country.Longitude,
                    GmtHour = country.GmtHour,
                    GmtMin = country.GmtMin,
                    IsActive = country.IsActive
                };

                countryDtos.Add(countryDto);
            }
            return new(new ListCountriesQueryResponse(countryDtos));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error retrieving all Countries");
            throw;
        }
    }
}