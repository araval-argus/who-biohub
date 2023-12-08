using WHO.BioHub.Shared.Utils;
using WHO.BioHub.Models.Models;
using Microsoft.EntityFrameworkCore.Storage;
using WHO.BioHub.Shared.Dto;

namespace WHO.BioHub.Models.Repositories.Materials;

public interface IMaterialWriteRepository
{
    Task<IDbContextTransaction> BeginTransactionAsync();
    Task<Either<Material, Errors>> Create(Material material, CancellationToken cancellationToken);
    Task<Errors?> CreateMaterialHistoryItem(Material material, CancellationToken cancellationToken, IDbContextTransaction transaction);
    Task<Errors?> Delete(Guid id, CancellationToken cancellationToken);
    Task<Material> ReadForUpdate(Guid id, CancellationToken cancellationToken);
    Task<Material> ReadForUpdateForLaboratoryUser(Guid id, Guid userLaboratoryId, CancellationToken cancellationToken);
    Task<IEnumerable<MaterialCollectedSpecimenType>> ReadMaterialCollectedSpecimenTypes(Guid id, CancellationToken cancellationToken);
    Task<IEnumerable<MaterialGSDInfo>> ReadMaterialGSDInfo(Guid id, CancellationToken cancellationToken);
    Task<Errors?> Update(Material material, List<MaterialGSDInfoDto> materialGSDInfoDto, CancellationToken cancellationToken, IDbContextTransaction? transaction = null);
    Task<Errors?> Update(Material material, List<MaterialGSDInfoDto> materialGSDInfoDto, List<Guid?> specimenTypeIds, CancellationToken cancellationToken, IDbContextTransaction? transaction = null);
    Task<Errors?> Update(Material material, CancellationToken cancellationToken, IDbContextTransaction? transaction = null);
}
