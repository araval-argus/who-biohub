using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.TransportCategories;
using WHO.BioHub.Shared.Utils;
using WHO.BioHub.Public.SQL.Abstractions;

namespace WHO.BioHub.PublicData.Core.UseCases.TransportCategories.ReadTransportCategory;

public interface IReadTransportCategoryHandler
{
    Task<Either<ReadTransportCategoryQueryResponse, Errors>> Handle(ReadTransportCategoryQuery query, CancellationToken cancellationToken);
}

public class ReadTransportCategoryHandler : IReadTransportCategoryHandler
{
    private readonly ILogger<ReadTransportCategoryHandler> _logger;
    private readonly ReadTransportCategoryQueryValidator _validator;
    private readonly ITransportCategoryPublicReadRepository _readRepository;

    public ReadTransportCategoryHandler(
        ILogger<ReadTransportCategoryHandler> logger,
        ReadTransportCategoryQueryValidator validator,
        ITransportCategoryPublicReadRepository readRepository)
    {
        _logger = logger;
        _validator = validator;
        _readRepository = readRepository;
    }

    public async Task<Either<ReadTransportCategoryQueryResponse, Errors>> Handle(
        ReadTransportCategoryQuery query,
        CancellationToken cancellationToken)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(query, cancellationToken);
        if (!validationResult.IsValid)
            return new(new Errors(validationResult));

        try
        {
            TransportCategory transportcategory = await _readRepository.Read(query.Id, cancellationToken);
            if (transportcategory == null)
                return new(new Errors(ErrorType.NotFound, $"TransportCategory with Id {query.Id} not found"));

            return new(new ReadTransportCategoryQueryResponse(transportcategory));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error reading TransportCategory with Id {id}", query.Id);
            throw;
        }
    }
}