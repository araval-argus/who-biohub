using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.Materials;
using WHO.BioHub.Shared.Enums;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UseCases.Materials.ReadMaterial;

public interface IReadMaterialHandler
{
    Task<Either<ReadMaterialQueryResponse, Errors>> Handle(ReadMaterialQuery query, CancellationToken cancellationToken);
}

public class ReadMaterialHandler : IReadMaterialHandler
{
    private readonly ILogger<ReadMaterialHandler> _logger;
    private readonly ReadMaterialQueryValidator _validator;
    private readonly IMaterialReadRepository _readRepository;
    private readonly IReadMaterialMapper _mapper;

    public ReadMaterialHandler(
        ILogger<ReadMaterialHandler> logger,
        ReadMaterialQueryValidator validator,
        IMaterialReadRepository readRepository,
        IReadMaterialMapper mapper)
    {
        _logger = logger;
        _validator = validator;
        _readRepository = readRepository;
        _mapper = mapper;
    }

    public async Task<Either<ReadMaterialQueryResponse, Errors>> Handle(
        ReadMaterialQuery query,
        CancellationToken cancellationToken)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(query, cancellationToken);
        if (!validationResult.IsValid)
            return new(new Errors(validationResult));
        try
        {
            Material material;
            Guid? instituteId = null;

            switch (query.RoleType)
            {
                case RoleType.WHO:                
                    material = await _readRepository.Read(query.Id, cancellationToken);
                    if (material == null)
                        return new(new Errors(ErrorType.NotFound, $"Material with Id {query.Id} not found"));

                    if (material.Status != MaterialStatus.Completed)
                        return new(new Errors(ErrorType.NotFound, $"Material with Id {query.Id} not found in the required state"));
                    break;

                case RoleType.BioHubFacility:
                    material = await _readRepository.ReadForBioHubFacilityUser(query.Id, query.UserBioHubFacilityId.GetValueOrDefault(), cancellationToken);
                    if (material == null)
                        return new(new Errors(ErrorType.NotFound, $"Material with Id {query.Id} not found"));

                    if (material.Status != MaterialStatus.Completed)
                        return new(new Errors(ErrorType.NotFound, $"Material with Id {query.Id} not found in the required state"));
                    instituteId = query.UserBioHubFacilityId;
                    break;
               
                
                case RoleType.Laboratory:
                    material = await _readRepository.ReadForLaboratoryUser(query.Id, query.UserLaboratoryId.GetValueOrDefault(), cancellationToken);
                    if (material == null)
                        return new(new Errors(ErrorType.NotFound, $"Material with Id {query.Id} not found"));
                    if (material.Status != MaterialStatus.Completed)
                        return new(new Errors(ErrorType.NotFound, $"Material with Id {query.Id} not found in the required state"));


                    instituteId = query.UserLaboratoryId;

                    break;
                default:
                    throw new InvalidOperationException();
                    break;
            }

            var materialViewModel = _mapper.Map(material, query.RoleType, instituteId);
            return new(new ReadMaterialQueryResponse(materialViewModel));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error reading Material with Id {id}", query.Id);
            throw;
        }
    }
}