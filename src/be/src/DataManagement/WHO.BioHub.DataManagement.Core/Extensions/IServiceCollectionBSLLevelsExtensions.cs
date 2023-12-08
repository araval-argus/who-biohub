using Microsoft.Extensions.DependencyInjection;
using WHO.BioHub.DataManagement.Core.UseCases.BSLLevels.CreateBSLLevel;
using WHO.BioHub.DataManagement.Core.UseCases.BSLLevels.DeleteBSLLevel;
using WHO.BioHub.DataManagement.Core.UseCases.BSLLevels.ListBSLLevels;
using WHO.BioHub.DataManagement.Core.UseCases.BSLLevels.ReadBSLLevel;
using WHO.BioHub.DataManagement.Core.UseCases.BSLLevels.UpdateBSLLevel;

namespace WHO.BioHub.DataManagement.Core.Extensions;

public static class IServiceCollectionBSLLevelsExtensions
{
    public static IServiceCollection AddCoreBSLLevels(this IServiceCollection services)
    {
        services
            .AddScoped<ICreateBSLLevelHandler, CreateBSLLevelHandler>()
            .AddScoped<ICreateBSLLevelMapper, CreateBSLLevelMapper>()
            .AddScoped<CreateBSLLevelCommandValidator>()

            .AddScoped<IReadBSLLevelHandler, ReadBSLLevelHandler>()
            .AddScoped<ReadBSLLevelQueryValidator>()

            .AddScoped<IUpdateBSLLevelHandler, UpdateBSLLevelHandler>()
            .AddScoped<IUpdateBSLLevelMapper, UpdateBSLLevelMapper>()
            .AddScoped<UpdateBSLLevelCommandValidator>()

            .AddScoped<IDeleteBSLLevelHandler, DeleteBSLLevelHandler>()
            .AddScoped<DeleteBSLLevelCommandValidator>()

            .AddScoped<IListBSLLevelsHandler, ListBSLLevelsHandler>()
            .AddScoped<ListBSLLevelsQueryValidator>()
            ;

        return services;
    }
}