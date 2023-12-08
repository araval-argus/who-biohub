﻿using Microsoft.Extensions.DependencyInjection;
using WHO.BioHub.DataManagement.API.Http.Extensions;
using WHO.BioHub.DataManagement.Core.Extensions;
using WHO.BioHub.DAL.Extensions;

namespace WHO.BioHub.DataManagement.DependencyInjection;

public class SMTA1WorkflowHistoryItemsInjector : IInjector
{
    public IServiceCollection InjectDependencies(IServiceCollection services)
    {
        return services
                // driving adapters
                .AddAPIHttpSMTA1WorkflowHistoryItems()
                // application
                .AddCoreSMTA1WorkflowHistoryItems()
                // driven adapters
                .AddDALSMTA1WorkflowHistoryItems();
    }
}
