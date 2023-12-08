using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.SpecimenTypes;
using WHO.BioHub.Shared.Dto;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UseCases.SpecimenTypes.ListSpecimenTypes;

public interface IListSpecimenTypesHandler
{
    Task<Either<ListSpecimenTypesQueryResponse, Errors>> Handle(ListSpecimenTypesQuery query, CancellationToken cancellationToken);
}

public class ListSpecimenTypesHandler : IListSpecimenTypesHandler
{
    private readonly ILogger<ListSpecimenTypesHandler> _logger;
    private readonly ListSpecimenTypesQueryValidator _validator;
    private readonly ISpecimenTypeReadRepository _readRepository;

    public ListSpecimenTypesHandler(
        ILogger<ListSpecimenTypesHandler> logger,
        ListSpecimenTypesQueryValidator validator,
        ISpecimenTypeReadRepository readRepository)
    {
        _logger = logger;
        _validator = validator;
        _readRepository = readRepository;
    }

    public async Task<Either<ListSpecimenTypesQueryResponse, Errors>> Handle(
        ListSpecimenTypesQuery query,
        CancellationToken cancellationToken)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(query, cancellationToken);
        if (!validationResult.IsValid)
            return new(new Errors(validationResult));

        try
        {
            IEnumerable<SpecimenType> specimentypes = await _readRepository.List(cancellationToken);
            var specimenTypeDtos = new List<SpecimenTypeDto>();
            foreach (var specimenType in specimentypes)
            {
                SpecimenTypeDto specimenTypeDto = new()
                {
                    Id = specimenType.Id,
                    Name = specimenType.Name,
                    Description = specimenType.Description,
                    IsActive = specimenType.IsActive
                };

                specimenTypeDtos.Add(specimenTypeDto);
            }
            return new(new ListSpecimenTypesQueryResponse(specimenTypeDtos));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error retrieving all SpecimenTypes");
            throw;
        }
    }
}