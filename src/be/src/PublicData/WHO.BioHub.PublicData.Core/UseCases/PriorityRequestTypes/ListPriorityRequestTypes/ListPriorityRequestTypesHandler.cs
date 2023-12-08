using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.PriorityRequestTypes;
using WHO.BioHub.Shared.Utils;
using WHO.BioHub.Public.SQL.Abstractions;

namespace WHO.BioHub.PublicData.Core.UseCases.PriorityRequestTypes.ListPriorityRequestTypes;

public interface IListPriorityRequestTypesHandler
{
    Task<Either<ListPriorityRequestTypesQueryResponse, Errors>> Handle(ListPriorityRequestTypesQuery query, CancellationToken cancellationToken);
}

public class ListPriorityRequestTypesHandler : IListPriorityRequestTypesHandler
{
    private readonly ILogger<ListPriorityRequestTypesHandler> _logger;
    private readonly ListPriorityRequestTypesQueryValidator _validator;
    private readonly IPriorityRequestTypePublicReadRepository _readRepository;

    public ListPriorityRequestTypesHandler(
        ILogger<ListPriorityRequestTypesHandler> logger,
        ListPriorityRequestTypesQueryValidator validator,
        IPriorityRequestTypePublicReadRepository readRepository)
    {
        _logger = logger;
        _validator = validator;
        _readRepository = readRepository;
    }

    public async Task<Either<ListPriorityRequestTypesQueryResponse, Errors>> Handle(
        ListPriorityRequestTypesQuery query,
        CancellationToken cancellationToken)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(query, cancellationToken);
        if (!validationResult.IsValid)
            return new(new Errors(validationResult));

        try
        {
            IEnumerable<PriorityRequestType> priorityrequesttypes = await _readRepository.List(cancellationToken);
            return new(new ListPriorityRequestTypesQueryResponse(priorityrequesttypes));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error retrieving all PriorityRequestTypes");
            throw;
        }
    }
}