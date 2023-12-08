using Microsoft.Extensions.DependencyInjection;
using WHO.BioHub.DataManagement.Core.UseCases.InternationalTaxonomyClassifications.CreateInternationalTaxonomyClassification;
using WHO.BioHub.DataManagement.Core.UseCases.InternationalTaxonomyClassifications.DeleteInternationalTaxonomyClassification;
using WHO.BioHub.DataManagement.Core.UseCases.InternationalTaxonomyClassifications.ListInternationalTaxonomyClassifications;
using WHO.BioHub.DataManagement.Core.UseCases.InternationalTaxonomyClassifications.ReadInternationalTaxonomyClassification;
using WHO.BioHub.DataManagement.Core.UseCases.InternationalTaxonomyClassifications.UpdateInternationalTaxonomyClassification;

namespace WHO.BioHub.DataManagement.Core.Extensions;

public static class IServiceCollectionInternationalTaxonomyClassificationsExtensions
{
    public static IServiceCollection AddCoreInternationalTaxonomyClassifications(this IServiceCollection services)
    {
        services
            .AddScoped<ICreateInternationalTaxonomyClassificationHandler, CreateInternationalTaxonomyClassificationHandler>()
            .AddScoped<ICreateInternationalTaxonomyClassificationMapper, CreateInternationalTaxonomyClassificationMapper>()
            .AddScoped<CreateInternationalTaxonomyClassificationCommandValidator>()

            .AddScoped<IReadInternationalTaxonomyClassificationHandler, ReadInternationalTaxonomyClassificationHandler>()
            .AddScoped<ReadInternationalTaxonomyClassificationQueryValidator>()

            .AddScoped<IUpdateInternationalTaxonomyClassificationHandler, UpdateInternationalTaxonomyClassificationHandler>()
            .AddScoped<IUpdateInternationalTaxonomyClassificationMapper, UpdateInternationalTaxonomyClassificationMapper>()
            .AddScoped<UpdateInternationalTaxonomyClassificationCommandValidator>()

            .AddScoped<IDeleteInternationalTaxonomyClassificationHandler, DeleteInternationalTaxonomyClassificationHandler>()
            .AddScoped<DeleteInternationalTaxonomyClassificationCommandValidator>()

            .AddScoped<IListInternationalTaxonomyClassificationsHandler, ListInternationalTaxonomyClassificationsHandler>()
            .AddScoped<ListInternationalTaxonomyClassificationsQueryValidator>()
            ;

        return services;
    }
}