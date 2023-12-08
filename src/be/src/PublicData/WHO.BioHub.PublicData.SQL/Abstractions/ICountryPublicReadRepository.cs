using WHO.BioHub.Models.Models;

namespace WHO.BioHub.Public.SQL.Abstractions;

public interface ICountryPublicReadRepository
{
    Task<Country> Read(Guid id, CancellationToken cancellationToken);
    Task<IEnumerable<Country>> List(CancellationToken cancellationToken);
}
