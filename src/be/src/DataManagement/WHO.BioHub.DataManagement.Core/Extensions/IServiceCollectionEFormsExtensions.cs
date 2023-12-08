using Microsoft.Extensions.DependencyInjection;
using WHO.BioHub.Data.Core.UseCases.EForms.Annex2OfSMTA1;
using WHO.BioHub.Data.Core.UseCases.EForms.Annex2OfSMTA2;
using WHO.BioHub.Data.Core.UseCases.EForms.BiosafetyChecklistOfSMTA2;
using WHO.BioHub.Data.Core.UseCases.EForms.BookingFormOfSMTA1;
using WHO.BioHub.Data.Core.UseCases.EForms.BookingFormOfSMTA2;
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
using WHO.BioHub.DataManagement.Core.UseCases.EForms.ListEForm;
using WHO.BioHub.DataManagement.Core.UseCases.EForms.ListEForms;

namespace WHO.BioHub.DataManagement.Core.Extensions;

public static class IServiceCollectionEFormsExtensions
{
    public static IServiceCollection AddCoreEForms(this IServiceCollection services)
    {
        services
            .AddScoped<IReadAnnex2OfSMTA1Handler, ReadAnnex2OfSMTA1Handler>()            
            .AddScoped<ReadAnnex2OfSMTA1QueryValidator>()

            .AddScoped<IReadBookingFormOfSMTA1Handler, ReadBookingFormOfSMTA1Handler>()            
            .AddScoped<ReadBookingFormOfSMTA1QueryValidator>()

            .AddScoped<IReadAnnex2OfSMTA2Handler, ReadAnnex2OfSMTA2Handler>()
            .AddScoped<ReadAnnex2OfSMTA2QueryValidator>()

            .AddScoped<IReadBiosafetyChecklistOfSMTA2Handler, ReadBiosafetyChecklistOfSMTA2Handler>()
            .AddScoped<ReadBiosafetyChecklistOfSMTA2QueryValidator>()

            .AddScoped<IReadBookingFormOfSMTA2Handler, ReadBookingFormOfSMTA2Handler>()
            .AddScoped<ReadBookingFormOfSMTA2QueryValidator>()

            .AddScoped<IListEFormsHandler, ListEFormsHandler>()
            .AddScoped<IListEFormsMapper, ListEFormsMapper>()
            .AddScoped<ListEFormsQueryValidator>()
            ;

        return services;
    }
}