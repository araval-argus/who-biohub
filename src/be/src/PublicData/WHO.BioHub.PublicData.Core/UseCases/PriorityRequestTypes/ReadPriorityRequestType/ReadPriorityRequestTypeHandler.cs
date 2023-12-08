using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.PriorityRequestTypes;
using WHO.BioHub.Shared.Utils;
using WHO.BioHub.Public.SQL.Abstractions;

namespace WHO.BioHub.PublicData.Core.UseCases.PriorityRequestTypes.ReadPriorityRequestType;

public interface IReadPriorityRequestTypeHandler
{
    Task<Either<ReadPriorityRequestTypeQueryResponse, Errors>> Handle(ReadPriorityRequestTypeQuery query, CancellationToken cancellationToken);
}

public class ReadPriorityRequestTypeHandler : IReadPriorityRequestTypeHandler
{
    private readonly ILogger<ReadPriorityRequestTypeHandler> _logger;
    private readonly ReadPriorityRequestTypeQueryValidator _validator;
    private readonly IPriorityRequestTypePublicReadRepository _readRepository;

    public ReadPriorityRequestTypeHandler(
        ILogger<ReadPriorityRequestTypeHandler> logger,
        ReadPriorityRequestTypeQueryValidator validator,
        IPriorityRequestTypePublicReadRepository readRepository)
    {
        _logger = logger;
        _validator = validator;
        _readRepository = readRepository;
    }

    public async Task<Either<ReadPriorityRequestTypeQueryResponse, Errors>> Handle(
        ReadPriorityRequestTypeQuery query,
        CancellationToken cancellationToken)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(query, cancellationToken);
        if (!validationResult.IsValid)
            return new(new Errors(validationResult));

        try
        {
            PriorityRequestType priorityrequesttype = await _readRepository.Read(query.Id, cancellationToken);
            if (priorityrequesttype == null)
                return new(new Errors(ErrorType.NotFound, $"PriorityRequestType with Id {query.Id} not found"));

            return new(new ReadPriorityRequestTypeQueryResponse(priorityrequesttype));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error reading PriorityRequestType with Id {id}", query.Id);
            throw;
        }
    }
}