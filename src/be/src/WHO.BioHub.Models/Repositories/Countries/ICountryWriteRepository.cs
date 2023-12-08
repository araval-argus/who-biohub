using WHO.BioHub.Shared.Utils;
using WHO.BioHub.Models.Models;

namespace WHO.BioHub.Models.Repositories.Countries;

public interface ICountryWriteRepository
{
    Task<Either<Country, Errors>> Create(Country country, CancellationToken cancellationToken);
    Task<Errors?> Delete(Guid id, CancellationToken cancellationToken);
    Task<Country> ReadForUpdate(Guid id, CancellationToken cancellationToken);
    Task<Errors?> Update(Country country, CancellationToken cancellationToken);
}
