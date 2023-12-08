using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.DocumentTemplates;
using WHO.BioHub.Shared.Enums;
using WHO.BioHub.Shared.Utils;



namespace WHO.BioHub.DataManagement.Core.UseCases.DocumentTemplates.CheckCurrentsForDelete;

public interface ICheckCurrentsForDeleteHandler
{
    Task<Either<CheckCurrentsForDeleteQueryResponse, Errors>> Handle(CheckCurrentsForDeleteQuery query, CancellationToken cancellationToken);
}

public class CheckCurrentsForDeleteHandler : ICheckCurrentsForDeleteHandler
{
    private readonly ILogger<CheckCurrentsForDeleteHandler> _logger;
    private readonly CheckCurrentsForDeleteQueryValidator _validator;
    private readonly IDocumentTemplateReadRepository _readRepository;


    public CheckCurrentsForDeleteHandler(
        ILogger<CheckCurrentsForDeleteHandler> logger,
        CheckCurrentsForDeleteQueryValidator validator,
        IDocumentTemplateReadRepository readRepository
        )
    {
        _logger = logger;
        _validator = validator;
        _readRepository = readRepository;
    }

    public async Task<Either<CheckCurrentsForDeleteQueryResponse, Errors>> Handle(
        CheckCurrentsForDeleteQuery query,
        CancellationToken cancellationToken)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(query, cancellationToken);
        if (!validationResult.IsValid)
            return new(new Errors(validationResult));

        try
        {
            List<Guid> totalIdsToDelete = new List<Guid>() { query.Id };

            List<Guid> currentLevelIdsToDelete = await _readRepository.GetIdsForDeleteCheck(totalIdsToDelete, cancellationToken);

            while (currentLevelIdsToDelete.Any())
            {
                totalIdsToDelete.AddRange(currentLevelIdsToDelete);
                currentLevelIdsToDelete = await _readRepository.GetIdsForDeleteCheck(currentLevelIdsToDelete, cancellationToken);
            }

            var folderContainsCurrent = await _readRepository.ContainsCurrent(totalIdsToDelete, cancellationToken);

            return new(new CheckCurrentsForDeleteQueryResponse(FolderContainsCurrent: folderContainsCurrent));

        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error reading DocumentTemplate with Id {id}", query.Id);
            throw;
        }
    }
}