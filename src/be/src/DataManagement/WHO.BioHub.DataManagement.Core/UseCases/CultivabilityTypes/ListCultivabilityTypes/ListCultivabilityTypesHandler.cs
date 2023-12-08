using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.CultivabilityTypes;
using WHO.BioHub.Shared.Dto;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UseCases.CultivabilityTypes.ListCultivabilityTypes;

public interface IListCultivabilityTypesHandler
{
    Task<Either<ListCultivabilityTypesQueryResponse, Errors>> Handle(ListCultivabilityTypesQuery query, CancellationToken cancellationToken);
}

public class ListCultivabilityTypesHandler : IListCultivabilityTypesHandler
{
    private readonly ILogger<ListCultivabilityTypesHandler> _logger;
    private readonly ListCultivabilityTypesQueryValidator _validator;
    private readonly ICultivabilityTypeReadRepository _readRepository;

    public ListCultivabilityTypesHandler(
        ILogger<ListCultivabilityTypesHandler> logger,
        ListCultivabilityTypesQueryValidator validator,
        ICultivabilityTypeReadRepository readRepository)
    {
        _logger = logger;
        _validator = validator;
        _readRepository = readRepository;
    }

    public async Task<Either<ListCultivabilityTypesQueryResponse, Errors>> Handle(
        ListCultivabilityTypesQuery query,
        CancellationToken cancellationToken)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(query, cancellationToken);
        if (!validationResult.IsValid)
            return new(new Errors(validationResult));

        try
        {
            IEnumerable<CultivabilityType> cultivabilitytypes = await _readRepository.List(cancellationToken);

            var CultivabilityTypeDtos = new List<CultivabilityTypeDto>();
            foreach (var cultivabilitytype in cultivabilitytypes)
            {
                CultivabilityTypeDto cultivabilityTypeDto = new()
                {
                    Id = cultivabilitytype.Id,
                    IsActive = cultivabilitytype.IsActive,
                    Name = cultivabilitytype.Name,
                    Description = cultivabilitytype.Description
                };

                CultivabilityTypeDtos.Add(cultivabilityTypeDto);
            }

            return new(new ListCultivabilityTypesQueryResponse(CultivabilityTypeDtos));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error retrieving all CultivabilityTypes");
            throw;
        }
    }
}