using WHO.BioHub.Models.Models;

namespace WHO.BioHub.Models.Repositories.SpecimenTypes;

public interface ISpecimenTypeReadRepository
{
    Task<SpecimenType> Read(Guid id, CancellationToken cancellationToken);
    Task<IEnumerable<SpecimenType>> List(CancellationToken cancellationToken);
}
