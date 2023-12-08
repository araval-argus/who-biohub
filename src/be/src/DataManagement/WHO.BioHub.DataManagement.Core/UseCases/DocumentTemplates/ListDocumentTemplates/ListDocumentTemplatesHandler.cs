using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Shared.Dto;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.DocumentTemplates;
using WHO.BioHub.Shared.Enums;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UseCases.DocumentTemplates.ListDocumentTemplates;

public interface IListDocumentTemplatesHandler
{
    Task<Either<ListDocumentTemplatesQueryResponse, Errors>> Handle(ListDocumentTemplatesQuery query, CancellationToken cancellationToken);
}

public class ListDocumentTemplatesHandler : IListDocumentTemplatesHandler
{
    private readonly ILogger<ListDocumentTemplatesHandler> _logger;
    private readonly ListDocumentTemplatesQueryValidator _validator;
    private readonly IDocumentTemplateReadRepository _readRepository;

    public ListDocumentTemplatesHandler(
        ILogger<ListDocumentTemplatesHandler> logger,
        ListDocumentTemplatesQueryValidator validator,
        IDocumentTemplateReadRepository readRepository)
    {
        _logger = logger;
        _validator = validator;
        _readRepository = readRepository;
    }

    public async Task<Either<ListDocumentTemplatesQueryResponse, Errors>> Handle(
        ListDocumentTemplatesQuery query,
        CancellationToken cancellationToken)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(query, cancellationToken);
        if (!validationResult.IsValid)
            return new(new Errors(validationResult));

        try
        {
            IEnumerable<DocumentTemplate> documenttemplates = await _readRepository.List(query.Id, cancellationToken);
            List<DocumentTemplateItem> documenttemplatesItem = new List<DocumentTemplateItem>();
            foreach (DocumentTemplate documenttemplate in documenttemplates)
            {
                DocumentTemplateItem documentTemplateItem = new DocumentTemplateItem();
                documentTemplateItem.Id = documenttemplate.Id;
                documentTemplateItem.Name = documenttemplate.Name;
                documentTemplateItem.ParentId = documenttemplate.ParentId;
                documentTemplateItem.UploadTime = documenttemplate.UploadTime;
                documentTemplateItem.UploadedBy = documenttemplate.Type == DocumentTemplateType.File ? documenttemplate.UploadedBy.FirstName + " " + documenttemplate.UploadedBy.LastName : String.Empty;
                documentTemplateItem.UploadedById = documenttemplate.UploadedById;
                documentTemplateItem.Type = documenttemplate.Type;
                documentTemplateItem.FileType = documenttemplate.FileType;
                documentTemplateItem.Current = documenttemplate.Current;
                documentTemplateItem.Extension = documenttemplate.Extension;
                documenttemplatesItem.Add(documentTemplateItem);
            }
            documenttemplatesItem = documenttemplatesItem.OrderByDescending(x => x.UploadTime).ToList();
            return new(new ListDocumentTemplatesQueryResponse(documenttemplatesItem));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error retrieving all DocumentTemplates");
            throw;
        }
    }
}