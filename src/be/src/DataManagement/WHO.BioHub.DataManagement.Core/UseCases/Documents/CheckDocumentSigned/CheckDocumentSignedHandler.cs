using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.Documents;
using WHO.BioHub.Shared.Enums;
using WHO.BioHub.Shared.Utils;
using WHO.BioHub.StorageAccount;

namespace WHO.BioHub.DataManagement.Core.UseCases.Documents.CheckDocumentSigned;

public interface ICheckDocumentSignedHandler
{
    Task<Either<CheckDocumentSignedQueryResponse, Errors>> Handle(CheckDocumentSignedQuery query, CancellationToken cancellationToken);
}

public class CheckDocumentSignedHandler : ICheckDocumentSignedHandler
{
    private readonly ILogger<CheckDocumentSignedHandler> _logger;
    private readonly CheckDocumentSignedQueryValidator _validator;
    private readonly IDocumentReadRepository _readRepository;

    public CheckDocumentSignedHandler(
        ILogger<CheckDocumentSignedHandler> logger,
        CheckDocumentSignedQueryValidator validator,
        IDocumentReadRepository readRepository)
    {
        _logger = logger;
        _validator = validator;
        _readRepository = readRepository;
    }

    public async Task<Either<CheckDocumentSignedQueryResponse, Errors>> Handle(
        CheckDocumentSignedQuery query,
        CancellationToken cancellationToken)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(query, cancellationToken);
        if (!validationResult.IsValid)
            return new(new Errors(validationResult));

        try
        {
            var isDocumentSigned = await _readRepository.IsDocumentSignedByLaboratoryId(query.LaboratoryId.GetValueOrDefault(), query.Type, cancellationToken);

            return new(new CheckDocumentSignedQueryResponse(IsSigned: isDocumentSigned));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error checking Document for LaboratoryId {id}", query.LaboratoryId);
            throw;
        }
    }
}