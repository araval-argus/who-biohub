﻿using Microsoft.Extensions.DependencyInjection;
using WHO.BioHub.DataManagement.API.Http.Extensions;
using WHO.BioHub.DataManagement.Core.Extensions;
using WHO.BioHub.DAL.Extensions;

namespace WHO.BioHub.DataManagement.DependencyInjection;

public class DocumentTemplatesInjector : IInjector
{
    public IServiceCollection InjectDependencies(IServiceCollection services)
    {
        return services
                // driving adapters
                .AddAPIHttpDocumentTemplates()
                // application
                .AddCoreDocumentTemplates()
                // driven adapters
                .AddDALDocumentTemplates();
    }
}
