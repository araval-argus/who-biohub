using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.Laboratories;
using WHO.BioHub.Shared.Utils;
using WHO.BioHub.Public.SQL.Abstractions;
using WHO.BioHub.PublicData.Core.Dto;

namespace WHO.BioHub.PublicData.Core.UseCases.Laboratories.ListMapLaboratories;

public interface IListMapLaboratoriesHandler
{
    Task<Either<ListMapLaboratoriesQueryResponse, Errors>> Handle(ListMapLaboratoriesQuery query, CancellationToken cancellationToken);
}

public class ListMapLaboratoriesHandler : IListMapLaboratoriesHandler
{
    private readonly ILogger<ListMapLaboratoriesHandler> _logger;
    private readonly ListMapLaboratoriesQueryValidator _validator;
    private readonly ILaboratoryPublicReadRepository _readRepository;
    private readonly IListMapLaboratoryMapper _mapper;

    public ListMapLaboratoriesHandler(
        ILogger<ListMapLaboratoriesHandler> logger,
        ListMapLaboratoriesQueryValidator validator,
        ILaboratoryPublicReadRepository readRepository,
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

        try
        {
            IEnumerable<Laboratory> laboratories = await _readRepository.ListMap(cancellationToken);

            IEnumerable<LaboratoryPublicMapViewModel> laboratoryPublicViewModels = await _mapper.Map(laboratories, cancellationToken);
            return new(new ListMapLaboratoriesQueryResponse(laboratoryPublicViewModels));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error retrieving all Laboratories");
            throw;
        }
    }
}