using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.Documents;
using WHO.BioHub.Shared.Enums;
using WHO.BioHub.Shared.Utils;
using WHO.BioHub.StorageAccount;

namespace WHO.BioHub.DataManagement.Core.UseCases.Documents.CanStartSMTA2Request;

public interface ICanStartSMTARequestHandler
{
    Task<Either<CanStartSMTARequestQueryResponse, Errors>> Handle(CanStartSMTARequestQuery query, CancellationToken cancellationToken);
}

public class CanStartSMTARequestHandler : ICanStartSMTARequestHandler
{
    private readonly ILogger<CanStartSMTARequestHandler> _logger;
    private readonly CanStartSMTARequestQueryValidator _validator;
    private readonly IDocumentReadRepository _readRepository;

    public CanStartSMTARequestHandler(
        ILogger<CanStartSMTARequestHandler> logger,
        CanStartSMTARequestQueryValidator validator,
        IDocumentReadRepository readRepository)
    {
        _logger = logger;
        _validator = validator;
        _readRepository = readRepository;
    }

    public async Task<Either<CanStartSMTARequestQueryResponse, Errors>> Handle(
        CanStartSMTARequestQuery query,
        CancellationToken cancellationToken)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(query, cancellationToken);
        if (!validationResult.IsValid)
            return new(new Errors(validationResult));

        try
        {
            var canStartSMTARequest = await _readRepository.CanNewSMTARequestBeStarted(query.LaboratoryId.GetValueOrDefault(), query.Type, cancellationToken);

            return new(new CanStartSMTARequestQueryResponse(CanStartSMTARequest: canStartSMTARequest));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error checking Document for LaboratoryId {id}", query.LaboratoryId);
            throw;
        }
    }
}