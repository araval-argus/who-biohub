using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.Materials;
using WHO.BioHub.Shared.Enums;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UseCases.Materials.ReadMaterialForBioHubFacilityCompletion;

public interface IReadMaterialForBioHubFacilityCompletionHandler
{
    Task<Either<ReadMaterialForBioHubFacilityCompletionQueryResponse, Errors>> Handle(ReadMaterialForBioHubFacilityCompletionQuery query, CancellationToken cancellationToken);
}

public class ReadMaterialForBioHubFacilityCompletionHandler : IReadMaterialForBioHubFacilityCompletionHandler
{
    private readonly ILogger<ReadMaterialForBioHubFacilityCompletionHandler> _logger;
    private readonly ReadMaterialForBioHubFacilityCompletionQueryValidator _validator;
    private readonly IMaterialReadRepository _readRepository;
    private readonly IReadMaterialForBioHubFacilityCompletionMapper _mapper;

    public ReadMaterialForBioHubFacilityCompletionHandler(
        ILogger<ReadMaterialForBioHubFacilityCompletionHandler> logger,
        ReadMaterialForBioHubFacilityCompletionQueryValidator validator,
        IMaterialReadRepository readRepository,
        IReadMaterialForBioHubFacilityCompletionMapper mapper)
    {
        _logger = logger;
        _validator = validator;
        _readRepository = readRepository;
        _mapper = mapper;
    }

    public async Task<Either<ReadMaterialForBioHubFacilityCompletionQueryResponse, Errors>> Handle(
        ReadMaterialForBioHubFacilityCompletionQuery query,
        CancellationToken cancellationToken)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(query, cancellationToken);
        if (!validationResult.IsValid)
            return new(new Errors(validationResult));
        try
        {
            Material material;
            switch (query.RoleType)
            {
                case RoleType.WHO:                
                    material = await _readRepository.Read(query.Id, cancellationToken);
                    if (material == null)
                        return new(new Errors(ErrorType.NotFound, $"Material with Id {query.Id} not found"));

                    if (material.Status != MaterialStatus.WaitingForBioHubFacilityCompletion)
                        return new(new Errors(ErrorType.NotFound, $"Material with Id {query.Id} not found in the required state"));

                    break;

               
                case RoleType.BioHubFacility:
                    material = await _readRepository.ReadForBioHubFacilityUser(query.Id, query.UserBioHubFacilityId.GetValueOrDefault(), cancellationToken);
                    if (material == null)
                        return new(new Errors(ErrorType.NotFound, $"Material with Id {query.Id} not found"));

                    if (material.Status != MaterialStatus.WaitingForBioHubFacilityCompletion)
                        return new(new Errors(ErrorType.NotFound, $"Material with Id {query.Id} not found in the required state"));

                    break;

                case RoleType.Laboratory:
                    material = await _readRepository.ReadForLaboratoryUser(query.Id, query.UserLaboratoryId.GetValueOrDefault(), cancellationToken);
                    if (material == null)
                        return new(new Errors(ErrorType.NotFound, $"Material with Id {query.Id} not found"));

                    if (material.Status != MaterialStatus.WaitingForBioHubFacilityCompletion)
                        return new(new Errors(ErrorType.NotFound, $"Material with Id {query.Id} not found in the required state"));

                    break;
                default:
                    throw new InvalidOperationException();
                    break;
            }

            var materialViewModel = _mapper.Map(material);
            return new(new ReadMaterialForBioHubFacilityCompletionQueryResponse(materialViewModel));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error reading Material with Id {id}", query.Id);
            throw;
        }
    }
}