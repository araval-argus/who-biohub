using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.Laboratories;
using WHO.BioHub.Shared.Utils;
using WHO.BioHub.Public.SQL.Abstractions;
using WHO.BioHub.PublicData.Core.Dto;

namespace WHO.BioHub.PublicData.Core.UseCases.Laboratories.ListLaboratories;

public interface IListLaboratoriesHandler
{
    Task<Either<ListLaboratoriesQueryResponse, Errors>> Handle(ListLaboratoriesQuery query, CancellationToken cancellationToken);
}

public class ListLaboratoriesHandler : IListLaboratoriesHandler
{
    private readonly ILogger<ListLaboratoriesHandler> _logger;
    private readonly ListLaboratoriesQueryValidator _validator;
    private readonly ILaboratoryPublicReadRepository _readRepository;
    private readonly IListLaboratoryMapper _mapper;

    public ListLaboratoriesHandler(
        ILogger<ListLaboratoriesHandler> logger,
        ListLaboratoriesQueryValidator validator,
        ILaboratoryPublicReadRepository readRepository,
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
            IEnumerable<Laboratory> laboratories = await _readRepository.List(cancellationToken);

            IEnumerable<LaboratoryPublicViewModel> laboratoryPublicViewModels = _mapper.Map(laboratories);
            return new(new ListLaboratoriesQueryResponse(laboratoryPublicViewModels));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error retrieving all Laboratories");
            throw;
        }
    }
}