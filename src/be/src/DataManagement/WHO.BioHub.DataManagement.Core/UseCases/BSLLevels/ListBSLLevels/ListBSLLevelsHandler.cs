using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.BSLLevels;
using WHO.BioHub.Shared.Dto;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UseCases.BSLLevels.ListBSLLevels;

public interface IListBSLLevelsHandler
{
    Task<Either<ListBSLLevelsQueryResponse, Errors>> Handle(ListBSLLevelsQuery query, CancellationToken cancellationToken);
}

public class ListBSLLevelsHandler : IListBSLLevelsHandler
{
    private readonly ILogger<ListBSLLevelsHandler> _logger;
    private readonly ListBSLLevelsQueryValidator _validator;
    private readonly IBSLLevelReadRepository _readRepository;

    public ListBSLLevelsHandler(
        ILogger<ListBSLLevelsHandler> logger,
        ListBSLLevelsQueryValidator validator,
        IBSLLevelReadRepository readRepository)
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
            var BSLLevelDtos = new List<BSLLevelDto>();
            foreach (var bslLevel in bsllevels)
            {
                BSLLevelDto BSLLevelDto = new()
                {
                    Id = bslLevel.Id,
                    Code = bslLevel.Code,
                    Name = bslLevel.Name,
                    Description = bslLevel.Description
                };

                BSLLevelDtos.Add(BSLLevelDto);
            }
            return new(new ListBSLLevelsQueryResponse(BSLLevelDtos));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error retrieving all BSLLevels");
            throw;
        }
    }
}