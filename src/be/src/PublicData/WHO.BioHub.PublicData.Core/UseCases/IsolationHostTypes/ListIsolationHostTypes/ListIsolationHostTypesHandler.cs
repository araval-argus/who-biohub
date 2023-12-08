using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.IsolationHostTypes;
using WHO.BioHub.Shared.Utils;
using WHO.BioHub.Public.SQL.Abstractions;
using WHO.BioHub.PublicData.Core.Dto;

namespace WHO.BioHub.PublicData.Core.UseCases.IsolationHostTypes.ListIsolationHostTypes;

public interface IListIsolationHostTypesHandler
{
    Task<Either<ListIsolationHostTypesQueryResponse, Errors>> Handle(ListIsolationHostTypesQuery query, CancellationToken cancellationToken);
}

public class ListIsolationHostTypesHandler : IListIsolationHostTypesHandler
{
    private readonly ILogger<ListIsolationHostTypesHandler> _logger;
    private readonly ListIsolationHostTypesQueryValidator _validator;
    private readonly IIsolationHostTypePublicReadRepository _readRepository;

    public ListIsolationHostTypesHandler(
        ILogger<ListIsolationHostTypesHandler> logger,
        ListIsolationHostTypesQueryValidator validator,
        IIsolationHostTypePublicReadRepository readRepository)
    {
        _logger = logger;
        _validator = validator;
        _readRepository = readRepository;
    }

    public async Task<Either<ListIsolationHostTypesQueryResponse, Errors>> Handle(
        ListIsolationHostTypesQuery query,
        CancellationToken cancellationToken)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(query, cancellationToken);
        if (!validationResult.IsValid)
            return new(new Errors(validationResult));

        try
        {
            IEnumerable<IsolationHostType> isolationhosttypes = await _readRepository.List(cancellationToken);
            var isolationHostTypeDtos = new List<IsolationHostTypePublicDto>();
            foreach (var isolationHostType in isolationhosttypes)
            {
                IsolationHostTypePublicDto IsolationHostTypeDto = new()
                {
                    Id = isolationHostType.Id,
                    Name = isolationHostType.Name,
                    Description = isolationHostType.Description,
                    IsActive = isolationHostType.IsActive
                };

                isolationHostTypeDtos.Add(IsolationHostTypeDto);
            }
            return new(new ListIsolationHostTypesQueryResponse(isolationHostTypeDtos));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error retrieving all IsolationHostTypes");
            throw;
        }
    }
}