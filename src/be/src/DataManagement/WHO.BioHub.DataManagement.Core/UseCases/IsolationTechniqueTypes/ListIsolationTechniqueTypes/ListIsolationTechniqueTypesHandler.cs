using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.IsolationTechniqueTypes;
using WHO.BioHub.Shared.Dto;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UseCases.IsolationTechniqueTypes.ListIsolationTechniqueTypes;

public interface IListIsolationTechniqueTypesHandler
{
    Task<Either<ListIsolationTechniqueTypesQueryResponse, Errors>> Handle(ListIsolationTechniqueTypesQuery query, CancellationToken cancellationToken);
}

public class ListIsolationTechniqueTypesHandler : IListIsolationTechniqueTypesHandler
{
    private readonly ILogger<ListIsolationTechniqueTypesHandler> _logger;
    private readonly ListIsolationTechniqueTypesQueryValidator _validator;
    private readonly IIsolationTechniqueTypeReadRepository _readRepository;

    public ListIsolationTechniqueTypesHandler(
        ILogger<ListIsolationTechniqueTypesHandler> logger,
        ListIsolationTechniqueTypesQueryValidator validator,
        IIsolationTechniqueTypeReadRepository readRepository)
    {
        _logger = logger;
        _validator = validator;
        _readRepository = readRepository;
    }

    public async Task<Either<ListIsolationTechniqueTypesQueryResponse, Errors>> Handle(
        ListIsolationTechniqueTypesQuery query,
        CancellationToken cancellationToken)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(query, cancellationToken);
        if (!validationResult.IsValid)
            return new(new Errors(validationResult));

        try
        {
            IEnumerable<IsolationTechniqueType> isolationtechniquetypes = await _readRepository.List(cancellationToken);
            var isolationTechniqueTypeDtos = new List<IsolationTechniqueTypeDto>();
            foreach (var isolationTechniqueType in isolationtechniquetypes)
            {
                IsolationTechniqueTypeDto IsolationTechniqueTypeDto = new()
                {
                    Id = isolationTechniqueType.Id,
                    Name = isolationTechniqueType.Name,
                    Description = isolationTechniqueType.Description,
                    IsActive = isolationTechniqueType.IsActive
                };

                isolationTechniqueTypeDtos.Add(IsolationTechniqueTypeDto);
            }
            return new(new ListIsolationTechniqueTypesQueryResponse(isolationTechniqueTypeDtos));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error retrieving all IsolationTechniqueTypes");
            throw;
        }
    }
}