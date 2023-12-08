using Microsoft.Extensions.DependencyInjection;
using WHO.BioHub.DataManagement.Core.UseCases.Documents.CanStartSMTA2Request;
using WHO.BioHub.DataManagement.Core.UseCases.Documents.CheckDocumentSigned;
using WHO.BioHub.DataManagement.Core.UseCases.Documents.CreateDocument;
using WHO.BioHub.DataManagement.Core.UseCases.Documents.DeleteDocument;
using WHO.BioHub.DataManagement.Core.UseCases.Documents.ListDocument;
using WHO.BioHub.DataManagement.Core.UseCases.Documents.ListDocuments;
using WHO.BioHub.DataManagement.Core.UseCases.Documents.ListSignedSMTADocument;
using WHO.BioHub.DataManagement.Core.UseCases.Documents.ListSignedSMTADocuments;
using WHO.BioHub.DataManagement.Core.UseCases.Documents.ReadDocument;
using WHO.BioHub.DataManagement.Core.UseCases.Documents.UpdateDocument;

namespace WHO.BioHub.DataManagement.Core.Extensions;

public static class IServiceCollectionDocumentsExtensions
{
    public static IServiceCollection AddCoreDocuments(this IServiceCollection services)
    {
        services
            .AddScoped<ICreateDocumentHandler, CreateDocumentHandler>()
            .AddScoped<ICreateDocumentMapper, CreateDocumentMapper>()
            .AddScoped<CreateDocumentCommandValidator>()

            .AddScoped<IReadDocumentHandler, ReadDocumentHandler>()
            .AddScoped<ReadDocumentQueryValidator>()

            .AddScoped<IUpdateDocumentHandler, UpdateDocumentHandler>()
            .AddScoped<IUpdateDocumentMapper, UpdateDocumentMapper>()
            .AddScoped<UpdateDocumentCommandValidator>()

            .AddScoped<IDeleteDocumentHandler, DeleteDocumentHandler>()
            .AddScoped<DeleteDocumentCommandValidator>()

            .AddScoped<IListDocumentsHandler, ListDocumentsHandler>()
            .AddScoped<IListDocumentsMapper, ListDocumentsMapper>()
            .AddScoped<ListDocumentsQueryValidator>()

            .AddScoped<ICheckDocumentSignedHandler, CheckDocumentSignedHandler>()
            .AddScoped<CheckDocumentSignedQueryValidator>()

            .AddScoped<ICanStartSMTARequestHandler, CanStartSMTARequestHandler>()
            .AddScoped<CanStartSMTARequestQueryValidator>()

            .AddScoped<IListSignedSMTADocumentsHandler, ListSignedSMTADocumentsHandler>()
            .AddScoped<IListSignedSMTADocumentsMapper, ListSignedSMTADocumentsMapper>()
            .AddScoped<ListSignedSMTADocumentsQueryValidator>()

            ;

        return services;
    }
}