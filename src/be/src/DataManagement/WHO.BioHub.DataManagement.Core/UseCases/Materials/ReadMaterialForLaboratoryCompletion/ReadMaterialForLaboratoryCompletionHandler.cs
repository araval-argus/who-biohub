using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.Materials;
using WHO.BioHub.Shared.Enums;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UseCases.Materials.ReadMaterialForLaboratoryCompletion;

public interface IReadMaterialForLaboratoryCompletionHandler
{
    Task<Either<ReadMaterialForLaboratoryCompletionQueryResponse, Errors>> Handle(ReadMaterialForLaboratoryCompletionQuery query, CancellationToken cancellationToken);
}

public class ReadMaterialForLaboratoryCompletionHandler : IReadMaterialForLaboratoryCompletionHandler
{
    private readonly ILogger<ReadMaterialForLaboratoryCompletionHandler> _logger;
    private readonly ReadMaterialForLaboratoryCompletionQueryValidator _validator;
    private readonly IMaterialReadRepository _readRepository;
    private readonly IReadMaterialForLaboratoryCompletionMapper _mapper;

    public ReadMaterialForLaboratoryCompletionHandler(
        ILogger<ReadMaterialForLaboratoryCompletionHandler> logger,
        ReadMaterialForLaboratoryCompletionQueryValidator validator,
        IMaterialReadRepository readRepository,
        IReadMaterialForLaboratoryCompletionMapper mapper)
    {
        _logger = logger;
        _validator = validator;
        _readRepository = readRepository;
        _mapper = mapper;
    }

    public async Task<Either<ReadMaterialForLaboratoryCompletionQueryResponse, Errors>> Handle(
        ReadMaterialForLaboratoryCompletionQuery query,
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

                    if (material.Status != MaterialStatus.WaitingForLaboratoryCompletion)
                        return new(new Errors(ErrorType.NotFound, $"Material with Id {query.Id} not found in the required state"));
                    break;
             
                case RoleType.BioHubFacility:
                    material = await _readRepository.ReadForBioHubFacilityUser(query.Id, query.UserBioHubFacilityId.GetValueOrDefault(), cancellationToken);
                    if (material == null)
                        return new(new Errors(ErrorType.NotFound, $"Material with Id {query.Id} not found"));

                    if (material.Status != MaterialStatus.WaitingForLaboratoryCompletion)
                        return new(new Errors(ErrorType.NotFound, $"Material with Id {query.Id} not found in the required state"));
                    break;
      
                case RoleType.Laboratory:
                    material = await _readRepository.ReadForLaboratoryUser(query.Id, query.UserLaboratoryId.GetValueOrDefault(), cancellationToken);
                    if (material == null)
                        return new(new Errors(ErrorType.NotFound, $"Material with Id {query.Id} not found"));

                    if (material.Status != MaterialStatus.WaitingForLaboratoryCompletion)
                        return new(new Errors(ErrorType.NotFound, $"Material with Id {query.Id} not found in the required state"));
                    break;

                default:
                    throw new InvalidOperationException();
                    break;
            }

            var materialViewModel = _mapper.Map(material);
            return new(new ReadMaterialForLaboratoryCompletionQueryResponse(materialViewModel));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error reading MaterialForLaboratoryCompletion with Id {id}", query.Id);
            throw;
        }
    }
}