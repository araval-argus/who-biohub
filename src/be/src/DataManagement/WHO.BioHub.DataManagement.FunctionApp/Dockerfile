#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/azure-functions/dotnet:4 AS base
WORKDIR /home/site/wwwroot
EXPOSE 80
ENV AzureWebJobsScriptRoot=/home/site/wwwroot \
    AzureFunctionsJobHost__Logging__Console__IsEnabled=true
RUN mkdir -p "/home/LogFiles/Application/Functions/Host" \
    && chown -R nobody:nogroup "/home/LogFiles/Application/Functions/Host"
USER nobody
MAINTAINER proge-software

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS restore
WORKDIR /src
COPY [ \
    "WHO.BioHub.Captcha/WHO.BioHub.Captcha.csproj", \
    "WHO.BioHub.Captcha/WHO.BioHub.Captcha.csproj" ]
COPY [ \
    "WHO.BioHub.DAL/WHO.BioHub.DAL.csproj", \
    "WHO.BioHub.DAL/WHO.BioHub.DAL.csproj" ]
COPY [ \
    "WHO.BioHub.Graph/WHO.BioHub.Graph.csproj", \
    "WHO.BioHub.Graph/WHO.BioHub.Graph.csproj" ]
COPY [ \
    "WHO.BioHub.Identity/WHO.BioHub.Identity.csproj", \
    "WHO.BioHub.Identity/WHO.BioHub.Identity.csproj" ]
COPY [ \
    "WHO.BioHub.Models/WHO.BioHub.Models.csproj", \
    "WHO.BioHub.Models/WHO.BioHub.Models.csproj"]
COPY [ \
    "WHO.BioHub.Notifications/WHO.BioHub.Notifications.csproj", \
    "WHO.BioHub.Notifications/WHO.BioHub.Notifications.csproj" ]
COPY [ \
    "WHO.BioHub.Shared/WHO.BioHub.Shared.csproj", \
    "WHO.BioHub.Shared/WHO.BioHub.Shared.csproj"]
COPY [ \
    "WHO.BioHub.StorageAccount/WHO.BioHub.StorageAccount.csproj", \
    "WHO.BioHub.StorageAccount/WHO.BioHub.StorageAccount.csproj" ]
COPY [ \
    "WHO.BioHub.WorkflowEngine/WHO.BioHub.WorkflowEngine.csproj", \
    "WHO.BioHub.WorkflowEngine/WHO.BioHub.WorkflowEngine.csproj" ]
COPY [ \
    "WHO.BioHub.API.Http.Extensions/WHO.BioHub.API.Http.Extensions.csproj", \
    "WHO.BioHub.API.Http.Extensions/WHO.BioHub.API.Http.Extensions.csproj"]
COPY [ \
    "DataManagement/WHO.BioHub.DataManagement.Core/WHO.BioHub.DataManagement.Core.csproj", \
    "DataManagement/WHO.BioHub.DataManagement.Core/WHO.BioHub.DataManagement.Core.csproj" ]
COPY [ \
    "DataManagement/WHO.BioHub.DataManagement.API.Http/WHO.BioHub.DataManagement.API.Http.csproj", \
    "DataManagement/WHO.BioHub.DataManagement.API.Http/WHO.BioHub.DataManagement.API.Http.csproj" ]
COPY [ \
    "DataManagement/WHO.BioHub.DataManagement.FunctionApp/WHO.BioHub.DataManagement.FunctionApp.csproj", \
    "DataManagement/WHO.BioHub.DataManagement.FunctionApp/WHO.BioHub.DataManagement.FunctionApp.csproj" ]

RUN find . -iname '*.csproj' | xargs -I {} dotnet restore {}

FROM restore as build
WORKDIR "/src"

COPY [ "WHO.BioHub.Captcha", "WHO.BioHub.Captcha" ]
COPY [ "WHO.BioHub.DAL", "WHO.BioHub.DAL" ]
COPY [ "WHO.BioHub.Graph", "WHO.BioHub.Graph" ]
COPY [ "WHO.BioHub.Identity", "WHO.BioHub.Identity" ]
COPY [ "WHO.BioHub.Models", "WHO.BioHub.Models"]
COPY [ "WHO.BioHub.Notifications", "WHO.BioHub.Notifications" ]
COPY [ "WHO.BioHub.Shared", "WHO.BioHub.Shared"]
COPY [ "WHO.BioHub.StorageAccount", "WHO.BioHub.StorageAccount" ]
COPY [ "WHO.BioHub.WorkflowEngine", "WHO.BioHub.WorkflowEngine" ]
COPY [ "WHO.BioHub.API.Http.Extensions", "WHO.BioHub.API.Http.Extensions"]
COPY [ \
    "DataManagement/WHO.BioHub.DataManagement.Core", \
    "DataManagement/WHO.BioHub.DataManagement.Core" ]
COPY [ \
    "DataManagement/WHO.BioHub.DataManagement.API.Http", \
    "DataManagement/WHO.BioHub.DataManagement.API.Http" ]
COPY [ \
    "DataManagement/WHO.BioHub.DataManagement.FunctionApp", \
    "DataManagement/WHO.BioHub.DataManagement.FunctionApp" ]

WORKDIR "/src/DataManagement/WHO.BioHub.DataManagement.FunctionApp/"
RUN dotnet build "WHO.BioHub.DataManagement.FunctionApp.csproj" -c Release --no-restore

FROM build as publish
WORKDIR "/src/DataManagement/WHO.BioHub.DataManagement.FunctionApp/"
RUN dotnet publish "WHO.BioHub.DataManagement.FunctionApp.csproj" -c Release --no-build -o /app/publish

FROM base AS final
# COPY ["WHO.BioHub.DataManagement.FunctionApp/appsettings.json", "/home/site/wwwroot/appsettings.json"]
COPY --chown=nobody:nogroup --from=publish /app/publish /home/site/wwwroot
