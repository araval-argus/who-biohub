using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.BSLLevels;
using WHO.BioHub.Shared.Utils;
using WHO.BioHub.Shared.Dto;

namespace WHO.BioHub.DataManagement.Core.UseCases.BSLLevels.ReadBSLLevel;

public interface IReadBSLLevelHandler
{
    Task<Either<ReadBSLLevelQueryResponse, Errors>> Handle(ReadBSLLevelQuery query, CancellationToken cancellationToken);
}

public class ReadBSLLevelHandler : IReadBSLLevelHandler
{
    private readonly ILogger<ReadBSLLevelHandler> _logger;
    private readonly ReadBSLLevelQueryValidator _validator;
    private readonly IBSLLevelReadRepository _readRepository;

    public ReadBSLLevelHandler(
        ILogger<ReadBSLLevelHandler> logger,
        ReadBSLLevelQueryValidator validator,
        IBSLLevelReadRepository readRepository)
    {
        _logger = logger;
        _validator = validator;
        _readRepository = readRepository;
    }

    public async Task<Either<ReadBSLLevelQueryResponse, Errors>> Handle(
        ReadBSLLevelQuery query,
        CancellationToken cancellationToken)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(query, cancellationToken);
        if (!validationResult.IsValid)
            return new(new Errors(validationResult));

        try
        {
            BSLLevel bsllevel = await _readRepository.Read(query.Id, cancellationToken);
            if (bsllevel == null)
                return new(new Errors(ErrorType.NotFound, $"BSLLevel with Id {query.Id} not found"));

            BSLLevelDto BSLLevelDto = new()
            {
                Id = bsllevel.Id,
                Code = bsllevel.Code,
                Name = bsllevel.Name,
                Description = bsllevel.Description
            };

            return new(new ReadBSLLevelQueryResponse(BSLLevelDto));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error reading BSLLevel with Id {id}", query.Id);
            throw;
        }
    }
}