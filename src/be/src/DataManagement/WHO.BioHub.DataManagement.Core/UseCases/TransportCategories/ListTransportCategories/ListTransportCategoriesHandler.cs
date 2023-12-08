using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.TransportCategories;
using WHO.BioHub.Shared.Dto;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UseCases.TransportCategories.ListTransportCategories;

public interface IListTransportCategoriesHandler
{
    Task<Either<ListTransportCategoriesQueryResponse, Errors>> Handle(ListTransportCategoriesQuery query, CancellationToken cancellationToken);
}

public class ListTransportCategoriesHandler : IListTransportCategoriesHandler
{
    private readonly ILogger<ListTransportCategoriesHandler> _logger;
    private readonly ListTransportCategoriesQueryValidator _validator;
    private readonly ITransportCategoryReadRepository _readRepository;

    public ListTransportCategoriesHandler(
        ILogger<ListTransportCategoriesHandler> logger,
        ListTransportCategoriesQueryValidator validator,
        ITransportCategoryReadRepository readRepository)
    {
        _logger = logger;
        _validator = validator;
        _readRepository = readRepository;
    }

    public async Task<Either<ListTransportCategoriesQueryResponse, Errors>> Handle(
        ListTransportCategoriesQuery query,
        CancellationToken cancellationToken)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(query, cancellationToken);
        if (!validationResult.IsValid)
            return new(new Errors(validationResult));

        try
        {
            IEnumerable<TransportCategory> transportcategories = await _readRepository.List(cancellationToken);

            var transportCategoryDtos = new List<TransportCategoryDto>();
            foreach (var transportCategory in transportcategories)
            {
                TransportCategoryDto transportCategoryDto = new()
                {
                    Id = transportCategory.Id,
                    Name = transportCategory.Name,
                    Description = transportCategory.Description,
                    HexColor = transportCategory.HexColor,
                    IsActive = transportCategory.IsActive
                };

                transportCategoryDtos.Add(transportCategoryDto);
            }

            return new(new ListTransportCategoriesQueryResponse(transportCategoryDtos));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error retrieving all TransportCategories");
            throw;
        }
    }
}