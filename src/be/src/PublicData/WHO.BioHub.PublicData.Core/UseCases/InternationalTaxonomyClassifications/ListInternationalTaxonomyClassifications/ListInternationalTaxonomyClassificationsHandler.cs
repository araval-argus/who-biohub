using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.InternationalTaxonomyClassifications;
using WHO.BioHub.Shared.Utils;
using WHO.BioHub.Public.SQL.Abstractions;
using WHO.BioHub.PublicData.Core.Dto;

namespace WHO.BioHub.PublicData.Core.UseCases.InternationalTaxonomyClassifications.ListInternationalTaxonomyClassifications;

public interface IListInternationalTaxonomyClassificationsHandler
{
    Task<Either<ListInternationalTaxonomyClassificationsQueryResponse, Errors>> Handle(ListInternationalTaxonomyClassificationsQuery query, CancellationToken cancellationToken);
}

public class ListInternationalTaxonomyClassificationsHandler : IListInternationalTaxonomyClassificationsHandler
{
    private readonly ILogger<ListInternationalTaxonomyClassificationsHandler> _logger;
    private readonly ListInternationalTaxonomyClassificationsQueryValidator _validator;
    private readonly IInternationalTaxonomyClassificationPublicReadRepository _readRepository;

    public ListInternationalTaxonomyClassificationsHandler(
        ILogger<ListInternationalTaxonomyClassificationsHandler> logger,
        ListInternationalTaxonomyClassificationsQueryValidator validator,
        IInternationalTaxonomyClassificationPublicReadRepository readRepository)
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
            var internationalTaxonomyClassificationDtos = new List<InternationalTaxonomyClassificationPublicDto>();
            foreach (var internationalTaxonomyClassification in internationaltaxonomyclassifications)
            {
                InternationalTaxonomyClassificationPublicDto InternationalTaxonomyClassificationDto = new()
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