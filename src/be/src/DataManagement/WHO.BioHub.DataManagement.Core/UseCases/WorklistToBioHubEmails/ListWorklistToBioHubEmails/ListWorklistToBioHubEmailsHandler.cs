using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.WorklistToBioHubEmails;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UseCases.WorklistToBioHubEmails.ListWorklistToBioHubEmails;

public interface IListWorklistToBioHubEmailsHandler
{
    Task<Either<ListWorklistToBioHubEmailsQueryResponse, Errors>> Handle(ListWorklistToBioHubEmailsQuery query, CancellationToken cancellationToken);
}

public class ListWorklistToBioHubEmailsHandler : IListWorklistToBioHubEmailsHandler
{
    private readonly ILogger<ListWorklistToBioHubEmailsHandler> _logger;
    private readonly ListWorklistToBioHubEmailsQueryValidator _validator;
    private readonly IWorklistToBioHubEmailReadRepository _readRepository;

    public ListWorklistToBioHubEmailsHandler(
        ILogger<ListWorklistToBioHubEmailsHandler> logger,
        ListWorklistToBioHubEmailsQueryValidator validator,
        IWorklistToBioHubEmailReadRepository readRepository)
    {
        _logger = logger;
        _validator = validator;
        _readRepository = readRepository;
    }

    public async Task<Either<ListWorklistToBioHubEmailsQueryResponse, Errors>> Handle(
        ListWorklistToBioHubEmailsQuery query,
        CancellationToken cancellationToken)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(query, cancellationToken);
        if (!validationResult.IsValid)
            return new(new Errors(validationResult));

        try
        {
            IEnumerable<WorklistToBioHubEmail> worklisttobiohubemails = await _readRepository.List(cancellationToken);
            return new(new ListWorklistToBioHubEmailsQueryResponse(worklisttobiohubemails));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error retrieving all WorklistToBioHubEmails");
            throw;
        }
    }
}