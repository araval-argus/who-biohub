using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.Laboratories;
using WHO.BioHub.Shared.Dto;
using WHO.BioHub.Shared.Enums;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UseCases.Laboratories.ListLaboratories;

public interface IListLaboratoriesHandler
{
    Task<Either<ListLaboratoriesQueryResponse, Errors>> Handle(ListLaboratoriesQuery query, CancellationToken cancellationToken);
}

public class ListLaboratoriesHandler : IListLaboratoriesHandler
{
    private readonly ILogger<ListLaboratoriesHandler> _logger;
    private readonly ListLaboratoriesQueryValidator _validator;
    private readonly ILaboratoryReadRepository _readRepository;
    private readonly IListLaboratoryMapper _mapper;

    public ListLaboratoriesHandler(
        ILogger<ListLaboratoriesHandler> logger,
        ListLaboratoriesQueryValidator validator,
        ILaboratoryReadRepository readRepository,
        IListLaboratoryMapper mapper)
    {
        _logger = logger;
        _validator = validator;
        _readRepository = readRepository;
        _mapper = mapper;
    }

    public async Task<Either<ListLaboratoriesQueryResponse, Errors>> Handle(
        ListLaboratoriesQuery query,
        CancellationToken cancellationToken)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(query, cancellationToken);
        if (!validationResult.IsValid)
            return new(new Errors(validationResult));

        try
        {
            IEnumerable<Laboratory> laboratories;
            IEnumerable<LaboratoryViewModel> laboratoriesViewModels;

            switch (query.RoleType)
            {
                case RoleType.WHO:
                case RoleType.BioHubFacility:
                    laboratories = await _readRepository.List(cancellationToken);
                    laboratoriesViewModels = _mapper.Map(laboratories);
                    break;
                case RoleType.Laboratory:
                    laboratories = await _readRepository.ListForLaboratoryUser(query.LaboratoryId, cancellationToken);
                    laboratoriesViewModels = _mapper.MapForLaboratoryUser(laboratories);
                    break;
                default:
                    throw new InvalidOperationException();
                    break;

                   
            }
            
            return new(new ListLaboratoriesQueryResponse(laboratoriesViewModels));

        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error retrieving all Laboratories");
            throw;
        }
    }
}