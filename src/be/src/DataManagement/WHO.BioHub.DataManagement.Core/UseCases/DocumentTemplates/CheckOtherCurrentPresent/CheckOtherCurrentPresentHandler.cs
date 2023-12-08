using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.DocumentTemplates;
using WHO.BioHub.Shared.Enums;
using WHO.BioHub.Shared.Utils;



namespace WHO.BioHub.DataManagement.Core.UseCases.DocumentTemplates.CheckOtherCurrentPresent;

public interface ICheckOtherCurrentPresentHandler
{
    Task<Either<CheckOtherCurrentPresentQueryResponse, Errors>> Handle(CheckOtherCurrentPresentQuery query, CancellationToken cancellationToken);
}

public class CheckOtherCurrentPresentHandler : ICheckOtherCurrentPresentHandler
{
    private readonly ILogger<CheckOtherCurrentPresentHandler> _logger;
    private readonly CheckOtherCurrentPresentQueryValidator _validator;
    private readonly IDocumentTemplateReadRepository _readRepository;


    public CheckOtherCurrentPresentHandler(
        ILogger<CheckOtherCurrentPresentHandler> logger,
        CheckOtherCurrentPresentQueryValidator validator,
        IDocumentTemplateReadRepository readRepository
        )
    {
        _logger = logger;
        _validator = validator;
        _readRepository = readRepository;
    }

    public async Task<Either<CheckOtherCurrentPresentQueryResponse, Errors>> Handle(
        CheckOtherCurrentPresentQuery query,
        CancellationToken cancellationToken)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(query, cancellationToken);
        if (!validationResult.IsValid)
            return new(new Errors(validationResult));

        try
        {
            DocumentTemplate documenttemplate = await _readRepository.Read(query.Id, cancellationToken);
            if (documenttemplate == null || documenttemplate.Type == DocumentTemplateType.Folder)
                return new(new Errors(ErrorType.NotFound, $"DocumentTemplate file with Id {query.Id} not found"));


            bool isOtherCurrentPresent = await _readRepository.IsOtherCurrentPresent(documenttemplate.Id, documenttemplate.FileType.GetValueOrDefault(), cancellationToken);


            return new(new CheckOtherCurrentPresentQueryResponse(IsOtherCurrentPresent: isOtherCurrentPresent));

        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error reading DocumentTemplate with Id {id}", query.Id);
            throw;
        }
    }
}