using WHO.BioHub.Models.Models;

namespace WHO.BioHub.Models.Repositories.TransportCategories;

public interface ITransportCategoryReadRepository
{
    Task<TransportCategory> Read(Guid id, CancellationToken cancellationToken);
    Task<IEnumerable<TransportCategory>> List(CancellationToken cancellationToken);
}
