using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.Laboratories;
using WHO.BioHub.Shared.Dto;
using WHO.BioHub.Shared.Enums;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UseCases.Laboratories.ListMapLaboratories;

public interface IListMapLaboratoriesHandler
{
    Task<Either<ListMapLaboratoriesQueryResponse, Errors>> Handle(ListMapLaboratoriesQuery query, CancellationToken cancellationToken);
}

public class ListMapLaboratoriesHandler : IListMapLaboratoriesHandler
{
    private readonly ILogger<ListMapLaboratoriesHandler> _logger;
    private readonly ListMapLaboratoriesQueryValidator _validator;
    private readonly ILaboratoryReadRepository _readRepository;
    private readonly IListMapLaboratoryMapper _mapper;

    public ListMapLaboratoriesHandler(
        ILogger<ListMapLaboratoriesHandler> logger,
        ListMapLaboratoriesQueryValidator validator,
        ILaboratoryReadRepository readRepository,
        IListMapLaboratoryMapper mapper)
    {
        _logger = logger;
        _validator = validator;
        _readRepository = readRepository;
        _mapper = mapper;
    }

    public async Task<Either<ListMapLaboratoriesQueryResponse, Errors>> Handle(
        ListMapLaboratoriesQuery query,
        CancellationToken cancellationToken)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(query, cancellationToken);
        if (!validationResult.IsValid)
            return new(new Errors(validationResult));

        IEnumerable<LaboratoryMapViewModel> laboratoriesViewModel;

        try
        {
            IEnumerable<Laboratory> laboratories;
            switch (query.RoleType)
            {
                case RoleType.WHO:
                case RoleType.BioHubFacility:
                    laboratories = await _readRepository.ListMap(cancellationToken);
                    laboratoriesViewModel = await _mapper.Map(laboratories, query.RoleType.GetValueOrDefault(), cancellationToken);
                    return new(new ListMapLaboratoriesQueryResponse(laboratoriesViewModel));
                    break;
                case RoleType.Laboratory:
                    laboratories = await _readRepository.ListMapForLaboratoryUser(query.LaboratoryId, cancellationToken);
                    laboratoriesViewModel = await _mapper.Map(laboratories, query.RoleType.GetValueOrDefault(), cancellationToken, query.LaboratoryId);
                    return new(new ListMapLaboratoriesQueryResponse(laboratoriesViewModel));
                    break;
                default:
                    throw new InvalidOperationException();
                    break;
            }

        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error retrieving all Laboratories");
            throw;
        }
    }
}