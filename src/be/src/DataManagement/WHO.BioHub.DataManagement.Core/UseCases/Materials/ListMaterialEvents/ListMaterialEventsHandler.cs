using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.Materials;
using WHO.BioHub.Shared.Dto;
using WHO.BioHub.Shared.Enums;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UseCases.MaterialEvents.ListMaterialEvents;

public interface IListMaterialEventsHandler
{
    Task<Either<ListMaterialEventsQueryResponse, Errors>> Handle(ListMaterialEventsQuery query, CancellationToken cancellationToken);
}

public class ListMaterialEventsHandler : IListMaterialEventsHandler
{
    private readonly ILogger<ListMaterialEventsHandler> _logger;
    private readonly ListMaterialEventsQueryValidator _validator;
    private readonly IMaterialReadRepository _readRepository;
    private readonly IListMaterialEventsMapper _mapper;

    public ListMaterialEventsHandler(
        ILogger<ListMaterialEventsHandler> logger,
        ListMaterialEventsQueryValidator validator,
        IMaterialReadRepository readRepository,
        IListMaterialEventsMapper mapper)
    {
        _logger = logger;
        _validator = validator;
        _readRepository = readRepository;
        _mapper = mapper;
    }

    public async Task<Either<ListMaterialEventsQueryResponse, Errors>> Handle(
        ListMaterialEventsQuery query,
        CancellationToken cancellationToken)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(query, cancellationToken);
        if (!validationResult.IsValid)
            return new(new Errors(validationResult));
        Material material;
        WorklistTimeline materialTimeline;

        try
        {
            Guid? instituteId = null;

            switch (query.RoleType)
            {
                case RoleType.WHO:
                    material = await _readRepository.ReadWithHistory(query.Id, cancellationToken);
                    if (material == null)
                        return new(new Errors(ErrorType.NotFound, $"Material with Id {query.Id} not found"));

                    break;

                case RoleType.BioHubFacility:
                    material = await _readRepository.ReadForBioHubFacilityUserWithHistory(query.Id, query.UserBioHubFacilityId.GetValueOrDefault(), cancellationToken);
                    if (material == null)
                        return new(new Errors(ErrorType.NotFound, $"Material with Id {query.Id} not found"));

                    break;


                case RoleType.Laboratory:
                    material = await _readRepository.ReadForLaboratoryUserWithHistory(query.Id, query.UserLaboratoryId.GetValueOrDefault(), cancellationToken);
                    if (material == null)
                        return new(new Errors(ErrorType.NotFound, $"Material with Id {query.Id} not found"));

                    break;
                default:
                    throw new InvalidOperationException();
                    break;                    
            }
            materialTimeline = _mapper.Map(material);
            return new(new ListMaterialEventsQueryResponse(materialTimeline));

        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error retrieving all MaterialEvents");
            throw;
        }
    }
}