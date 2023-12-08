using WHO.BioHub.Models.Models;

namespace WHO.BioHub.Models.Repositories.Couriers;

public interface ICourierReadRepository
{
    Task<Courier> Read(Guid id, CancellationToken cancellationToken);
    Task<IEnumerable<Courier>> List(CancellationToken cancellationToken);
    Task<CourierHistory> ReadPastInformation(Guid id, DateTime date, CancellationToken cancellationToken);
}
