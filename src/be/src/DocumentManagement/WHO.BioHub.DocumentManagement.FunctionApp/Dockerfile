#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/azure-functions/dotnet:4 AS base
WORKDIR /home/site/wwwroot
EXPOSE 80
ENV AzureWebJobsScriptRoot=/home/site/wwwroot \
    AzureFunctionsJobHost__Logging__Console__IsEnabled=true
USER nobody
MAINTAINER proge-software

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS restore
WORKDIR /src
COPY [ \
    "WHO.BioHub.DAL/WHO.BioHub.DAL.csproj", \
    "WHO.BioHub.DAL/WHO.BioHub.DAL.csproj" ]
COPY [ \
    "WHO.BioHub.Models/WHO.BioHub.Models.csproj", \
    "WHO.BioHub.Models/WHO.BioHub.Models.csproj"]
COPY [ \
    "WHO.BioHub.Shared/WHO.BioHub.Shared.csproj", \
    "WHO.BioHub.Shared/WHO.BioHub.Shared.csproj"]
COPY [ \
    "WHO.BioHub.API.Http.Extensions/WHO.BioHub.API.Http.Extensions.csproj", \
    "WHO.BioHub.API.Http.Extensions/WHO.BioHub.API.Http.Extensions.csproj"]
COPY [ \
    "DocumentManagement/WHO.BioHub.DocumentManagement.Core/WHO.BioHub.DocumentManagement.Core.csproj", \
    "DocumentManagement/WHO.BioHub.DocumentManagement.Core/WHO.BioHub.DocumentManagement.Core.csproj" ]
COPY [ \
    "DocumentManagement/WHO.BioHub.DocumentManagement.API.Http/WHO.BioHub.DocumentManagement.API.Http.csproj", \
    "DocumentManagement/WHO.BioHub.DocumentManagement.API.Http/WHO.BioHub.DocumentManagement.API.Http.csproj" ]
COPY [ \
    "DocumentManagement/WHO.BioHub.DocumentManagement.FunctionApp/WHO.BioHub.DocumentManagement.FunctionApp.csproj", \
    "DocumentManagement/WHO.BioHub.DocumentManagement.FunctionApp/WHO.BioHub.DocumentManagement.FunctionApp.csproj" ]

RUN find . -iname '*.csproj' | xargs -I {} dotnet restore {}

FROM restore as build
WORKDIR "/src"

COPY [ "WHO.BioHub.DAL", "WHO.BioHub.DAL" ]
COPY [ "WHO.BioHub.Models", "WHO.BioHub.Models"]
COPY [ "WHO.BioHub.Shared", "WHO.BioHub.Shared"]
COPY [ "WHO.BioHub.API.Http.Extensions", "WHO.BioHub.API.Http.Extensions"]
COPY [ \
    "DocumentManagement/WHO.BioHub.DocumentManagement.Core", \
    "DocumentManagement/WHO.BioHub.DocumentManagement.Core" ]
COPY [ \
    "DocumentManagement/WHO.BioHub.DocumentManagement.API.Http", \
    "DocumentManagement/WHO.BioHub.DocumentManagement.API.Http" ]
COPY [ \
    "DocumentManagement/WHO.BioHub.DocumentManagement.FunctionApp", \
    "DocumentManagement/WHO.BioHub.DocumentManagement.FunctionApp" ]

WORKDIR "/src/DocumentManagement/WHO.BioHub.DocumentManagement.FunctionApp/"
RUN dotnet build "WHO.BioHub.DocumentManagement.FunctionApp.csproj" -c Release --no-restore

FROM build as publish
WORKDIR "/src/DocumentManagement/WHO.BioHub.DocumentManagement.FunctionApp/"
RUN dotnet publish "WHO.BioHub.DocumentManagement.FunctionApp.csproj" -c Release --no-build -o /app/publish

FROM base AS final
# COPY ["WHO.BioHub.DocumentManagement.FunctionApp/appsettings.json", "/home/site/wwwroot/appsettings.json"]
COPY --from=publish /app/publish /home/site/wwwroot
