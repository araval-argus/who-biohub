using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.IsolationHostTypes;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UseCases.IsolationHostTypes.ReadIsolationHostType;

public interface IReadIsolationHostTypeHandler
{
    Task<Either<ReadIsolationHostTypeQueryResponse, Errors>> Handle(ReadIsolationHostTypeQuery query, CancellationToken cancellationToken);
}

public class ReadIsolationHostTypeHandler : IReadIsolationHostTypeHandler
{
    private readonly ILogger<ReadIsolationHostTypeHandler> _logger;
    private readonly ReadIsolationHostTypeQueryValidator _validator;
    private readonly IIsolationHostTypeReadRepository _readRepository;

    public ReadIsolationHostTypeHandler(
        ILogger<ReadIsolationHostTypeHandler> logger,
        ReadIsolationHostTypeQueryValidator validator,
        IIsolationHostTypeReadRepository readRepository)
    {
        _logger = logger;
        _validator = validator;
        _readRepository = readRepository;
    }

    public async Task<Either<ReadIsolationHostTypeQueryResponse, Errors>> Handle(
        ReadIsolationHostTypeQuery query,
        CancellationToken cancellationToken)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(query, cancellationToken);
        if (!validationResult.IsValid)
            return new(new Errors(validationResult));

        try
        {
            IsolationHostType isolationhosttype = await _readRepository.Read(query.Id, cancellationToken);
            if (isolationhosttype == null)
                return new(new Errors(ErrorType.NotFound, $"IsolationHostType with Id {query.Id} not found"));

            return new(new ReadIsolationHostTypeQueryResponse(isolationhosttype));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error reading IsolationHostType with Id {id}", query.Id);
            throw;
        }
    }
}