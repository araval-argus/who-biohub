using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Models.Repositories.GeneticSequenceDatas;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UseCases.GeneticSequenceDatas.DeleteGeneticSequenceData;

public interface IDeleteGeneticSequenceDataHandler
{
    Task<Either<DeleteGeneticSequenceDataCommandResponse, Errors>> Handle(DeleteGeneticSequenceDataCommand command, CancellationToken cancellationToken);
}

public class DeleteGeneticSequenceDataHandler : IDeleteGeneticSequenceDataHandler
{
    private readonly ILogger<DeleteGeneticSequenceDataHandler> _logger;
    private readonly DeleteGeneticSequenceDataCommandValidator _validator;
    private readonly IGeneticSequenceDataWriteRepository _writeRepository;

    public DeleteGeneticSequenceDataHandler(
        ILogger<DeleteGeneticSequenceDataHandler> logger,
        DeleteGeneticSequenceDataCommandValidator validator,
        IGeneticSequenceDataWriteRepository writeRepository
    )
    {
        _logger = logger;
        _validator = validator;
        _writeRepository = writeRepository;
    }

    public async Task<Either<DeleteGeneticSequenceDataCommandResponse, Errors>> Handle(
        DeleteGeneticSequenceDataCommand command,
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

            return new(new DeleteGeneticSequenceDataCommandResponse());
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error deleting the GeneticSequenceData with {id}", command.Id);
            throw;
        }
    }
}