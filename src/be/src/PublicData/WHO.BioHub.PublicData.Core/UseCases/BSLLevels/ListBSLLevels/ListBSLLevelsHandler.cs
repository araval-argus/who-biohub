using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.BSLLevels;
using WHO.BioHub.Shared.Utils;
using WHO.BioHub.Public.SQL.Abstractions;

namespace WHO.BioHub.PublicData.Core.UseCases.BSLLevels.ListBSLLevels;

public interface IListBSLLevelsHandler
{
    Task<Either<ListBSLLevelsQueryResponse, Errors>> Handle(ListBSLLevelsQuery query, CancellationToken cancellationToken);
}

public class ListBSLLevelsHandler : IListBSLLevelsHandler
{
    private readonly ILogger<ListBSLLevelsHandler> _logger;
    private readonly ListBSLLevelsQueryValidator _validator;
    private readonly IBSLLevelPublicReadRepository _readRepository;

    public ListBSLLevelsHandler(
        ILogger<ListBSLLevelsHandler> logger,
        ListBSLLevelsQueryValidator validator,
        IBSLLevelPublicReadRepository readRepository)
    {
        _logger = logger;
        _validator = validator;
        _readRepository = readRepository;
    }

    public async Task<Either<ListBSLLevelsQueryResponse, Errors>> Handle(
        ListBSLLevelsQuery query,
        CancellationToken cancellationToken)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(query, cancellationToken);
        if (!validationResult.IsValid)
            return new(new Errors(validationResult));

        try
        {
            IEnumerable<BSLLevel> bsllevels = await _readRepository.List(cancellationToken);
            return new(new ListBSLLevelsQueryResponse(bsllevels));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error retrieving all BSLLevels");
            throw;
        }
    }
}