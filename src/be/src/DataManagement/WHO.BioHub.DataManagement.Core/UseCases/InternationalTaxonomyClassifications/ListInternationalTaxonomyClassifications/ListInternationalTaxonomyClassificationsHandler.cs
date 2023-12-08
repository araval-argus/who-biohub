using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.InternationalTaxonomyClassifications;
using WHO.BioHub.Shared.Dto;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UseCases.InternationalTaxonomyClassifications.ListInternationalTaxonomyClassifications;

public interface IListInternationalTaxonomyClassificationsHandler
{
    Task<Either<ListInternationalTaxonomyClassificationsQueryResponse, Errors>> Handle(ListInternationalTaxonomyClassificationsQuery query, CancellationToken cancellationToken);
}

public class ListInternationalTaxonomyClassificationsHandler : IListInternationalTaxonomyClassificationsHandler
{
    private readonly ILogger<ListInternationalTaxonomyClassificationsHandler> _logger;
    private readonly ListInternationalTaxonomyClassificationsQueryValidator _validator;
    private readonly IInternationalTaxonomyClassificationReadRepository _readRepository;

    public ListInternationalTaxonomyClassificationsHandler(
        ILogger<ListInternationalTaxonomyClassificationsHandler> logger,
        ListInternationalTaxonomyClassificationsQueryValidator validator,
        IInternationalTaxonomyClassificationReadRepository readRepository)
    {
        _logger = logger;
        _validator = validator;
        _readRepository = readRepository;
    }

    public async Task<Either<ListInternationalTaxonomyClassificationsQueryResponse, Errors>> Handle(
        ListInternationalTaxonomyClassificationsQuery query,
        CancellationToken cancellationToken)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(query, cancellationToken);
        if (!validationResult.IsValid)
            return new(new Errors(validationResult));

        try
        {
            IEnumerable<InternationalTaxonomyClassification> internationaltaxonomyclassifications = await _readRepository.List(cancellationToken);
            var internationalTaxonomyClassificationDtos = new List<InternationalTaxonomyClassificationDto>();
            foreach (var internationalTaxonomyClassification in internationaltaxonomyclassifications)
            {
                InternationalTaxonomyClassificationDto InternationalTaxonomyClassificationDto = new()
                {
                    Id = internationalTaxonomyClassification.Id,                    
                    Name = internationalTaxonomyClassification.Name,
                    Description = internationalTaxonomyClassification.Description,
                    IsActive = internationalTaxonomyClassification.IsActive
                };

                internationalTaxonomyClassificationDtos.Add(InternationalTaxonomyClassificationDto);
            }

            return new(new ListInternationalTaxonomyClassificationsQueryResponse(internationalTaxonomyClassificationDtos));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error retrieving all InternationalTaxonomyClassifications");
            throw;
        }
    }
}