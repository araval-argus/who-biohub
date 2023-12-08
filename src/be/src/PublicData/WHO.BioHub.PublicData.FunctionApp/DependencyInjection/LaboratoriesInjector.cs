﻿using Microsoft.Extensions.DependencyInjection;
using WHO.BioHub.PublicData.API.Http.Extensions;
using WHO.BioHub.PublicData.Core.Extensions;
using WHO.BioHub.DAL.Extensions;

namespace WHO.BioHub.PublicData.DependencyInjection;

public class LaboratoriesInjector : IInjector
{
    public IServiceCollection InjectDependencies(IServiceCollection services)
    {
        return services
                // driving adapters
                .AddAPIHttpLaboratories()
                // application
                .AddCoreLaboratories()
                // driven adapters
                .AddPublicDALLaboratories();
    }
}
