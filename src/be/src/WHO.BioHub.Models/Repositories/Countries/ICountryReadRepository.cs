using WHO.BioHub.Models.Models;

namespace WHO.BioHub.Models.Repositories.Countries;

public interface ICountryReadRepository
{
    Task<Country> Read(Guid id, CancellationToken cancellationToken);
    Task<IEnumerable<Country>> List(CancellationToken cancellationToken);
}
