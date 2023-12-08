using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.WorklistToBioHubEmails;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UseCases.WorklistToBioHubEmails.ReadWorklistToBioHubEmail;

public interface IReadWorklistToBioHubEmailHandler
{
    Task<Either<ReadWorklistToBioHubEmailQueryResponse, Errors>> Handle(ReadWorklistToBioHubEmailQuery query, CancellationToken cancellationToken);
}

public class ReadWorklistToBioHubEmailHandler : IReadWorklistToBioHubEmailHandler
{
    private readonly ILogger<ReadWorklistToBioHubEmailHandler> _logger;
    private readonly ReadWorklistToBioHubEmailQueryValidator _validator;
    private readonly IWorklistToBioHubEmailReadRepository _readRepository;

    public ReadWorklistToBioHubEmailHandler(
        ILogger<ReadWorklistToBioHubEmailHandler> logger,
        ReadWorklistToBioHubEmailQueryValidator validator,
        IWorklistToBioHubEmailReadRepository readRepository)
    {
        _logger = logger;
        _validator = validator;
        _readRepository = readRepository;
    }

    public async Task<Either<ReadWorklistToBioHubEmailQueryResponse, Errors>> Handle(
        ReadWorklistToBioHubEmailQuery query,
        CancellationToken cancellationToken)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(query, cancellationToken);
        if (!validationResult.IsValid)
            return new(new Errors(validationResult));

        try
        {
            WorklistToBioHubEmail worklisttobiohubemail = await _readRepository.Read(query.Id, cancellationToken);
            if (worklisttobiohubemail == null)
                return new(new Errors(ErrorType.NotFound, $"WorklistToBioHubEmail with Id {query.Id} not found"));

            return new(new ReadWorklistToBioHubEmailQueryResponse(worklisttobiohubemail));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error reading WorklistToBioHubEmail with Id {id}", query.Id);
            throw;
        }
    }
}