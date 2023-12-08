using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Models.Repositories.InternationalTaxonomyClassifications;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UseCases.InternationalTaxonomyClassifications.DeleteInternationalTaxonomyClassification;

public interface IDeleteInternationalTaxonomyClassificationHandler
{
    Task<Either<DeleteInternationalTaxonomyClassificationCommandResponse, Errors>> Handle(DeleteInternationalTaxonomyClassificationCommand command, CancellationToken cancellationToken);
}

public class DeleteInternationalTaxonomyClassificationHandler : IDeleteInternationalTaxonomyClassificationHandler
{
    private readonly ILogger<DeleteInternationalTaxonomyClassificationHandler> _logger;
    private readonly DeleteInternationalTaxonomyClassificationCommandValidator _validator;
    private readonly IInternationalTaxonomyClassificationWriteRepository _writeRepository;

    public DeleteInternationalTaxonomyClassificationHandler(
        ILogger<DeleteInternationalTaxonomyClassificationHandler> logger,
        DeleteInternationalTaxonomyClassificationCommandValidator validator,
        IInternationalTaxonomyClassificationWriteRepository writeRepository
    )
    {
        _logger = logger;
        _validator = validator;
        _writeRepository = writeRepository;
    }

    public async Task<Either<DeleteInternationalTaxonomyClassificationCommandResponse, Errors>> Handle(
        DeleteInternationalTaxonomyClassificationCommand command,
        CancellationToken cancellationToken)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (!validationResult.IsValid)
            return new(new Errors(validationResult));

        try
        {
            Errors? errors = await _writeRepository.Delete(command.Id, cancellationToken);
            if (errors.HasValue)
                return new(errors.Value);

            return new(new DeleteInternationalTaxonomyClassificationCommandResponse());
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error deleting the InternationalTaxonomyClassification with {id}", command.Id);
            throw;
        }
    }
}