using Microsoft.Extensions.DependencyInjection;
using WHO.BioHub.DataManagement.Core.UseCases.DocumentTemplates.UploadFile;
using WHO.BioHub.DataManagement.Core.UseCases.DocumentTemplates.DeleteDocumentTemplate;
using WHO.BioHub.DataManagement.Core.UseCases.DocumentTemplates.ListDocumentTemplates;
using WHO.BioHub.DataManagement.Core.UseCases.DocumentTemplates.ReadFile;
using WHO.BioHub.DataManagement.Core.UseCases.DocumentTemplates.UpdateDocumentTemplate;
using WHO.BioHub.DataManagement.Core.UseCases.DocumentTemplates.CreateFolder;
using WHO.BioHub.DataManagement.Core.UseCases.DocumentTemplates.CheckOtherCurrentPresent;
using WHO.BioHub.DataManagement.Core.UseCases.DocumentTemplates.CheckCurrentsForDelete;
using WHO.BioHub.DataManagement.Core.UseCases.DocumentTemplates.ListSMTADocumentTemplates;
using WHO.BioHub.DataManagement.Core.UseCases.DocumentTemplates.ReadEFormFile;

namespace WHO.BioHub.DataManagement.Core.Extensions;

public static class IServiceCollectionDocumentTemplatesExtensions
{
    public static IServiceCollection AddCoreDocumentTemplates(this IServiceCollection services)
    {
        services
            .AddScoped<IUploadFileHandler, UploadFileHandler>()
            .AddScoped<IUploadFileMapper, UploadFileMapper>()
            .AddScoped<UploadFileCommandValidator>()

            .AddScoped<ICreateFolderHandler, CreateFolderHandler>()
            .AddScoped<ICreateFolderMapper, CreateFolderMapper>()
            .AddScoped<CreateFolderCommandValidator>()

            .AddScoped<IReadFileHandler, ReadFileHandler>()
            .AddScoped<ReadFileQueryValidator>()

            .AddScoped<IReadEFormFileHandler, ReadEFormFileHandler>()
            .AddScoped<ReadEFormFileQueryValidator>()

            .AddScoped<ICheckOtherCurrentPresentHandler, CheckOtherCurrentPresentHandler>()
            .AddScoped<CheckOtherCurrentPresentQueryValidator>()

            .AddScoped<ICheckCurrentsForDeleteHandler, CheckCurrentsForDeleteHandler>()
            .AddScoped<CheckCurrentsForDeleteQueryValidator>()


            .AddScoped<IUpdateDocumentTemplateHandler, UpdateDocumentTemplateHandler>()
            .AddScoped<IUpdateDocumentTemplateMapper, UpdateDocumentTemplateMapper>()
            .AddScoped<UpdateDocumentTemplateCommandValidator>()

            .AddScoped<IDeleteDocumentTemplateHandler, DeleteDocumentTemplateHandler>()
            .AddScoped<DeleteDocumentTemplateCommandValidator>()

            .AddScoped<IListDocumentTemplatesHandler, ListDocumentTemplatesHandler>()
            .AddScoped<ListDocumentTemplatesQueryValidator>()

            .AddScoped<IListSMTADocumentTemplatesHandler, ListSMTADocumentTemplatesHandler>()
            .AddScoped<ListSMTADocumentTemplatesQueryValidator>()

            ;

        return services;
    }
}