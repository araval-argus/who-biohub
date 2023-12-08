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
    "Search/WHO.BioHub.Search.SQL/WHO.BioHub.Search.SQL.csproj", \
    "Search/WHO.BioHub.Search.SQL/WHO.BioHub.Search.SQL.csproj" ]
COPY [ \
    "Search/WHO.BioHub.Search.Core/WHO.BioHub.Search.Core.csproj", \
    "Search/WHO.BioHub.Search.Core/WHO.BioHub.Search.Core.csproj" ]
COPY [ \
    "Search/WHO.BioHub.Search.API.Http/WHO.BioHub.Search.API.Http.csproj", \
    "Search/WHO.BioHub.Search.API.Http/WHO.BioHub.Search.API.Http.csproj" ]
COPY [ \
    "Search/WHO.BioHub.Search.FunctionApp/WHO.BioHub.Search.FunctionApp.csproj", \
    "Search/WHO.BioHub.Search.FunctionApp/WHO.BioHub.Search.FunctionApp.csproj" ]

RUN find . -iname '*.csproj' | xargs -I {} dotnet restore {}

FROM restore as build
WORKDIR "/src"

COPY [ "WHO.BioHub.DAL", "WHO.BioHub.DAL" ]
COPY [ "WHO.BioHub.Models", "WHO.BioHub.Models"]
COPY [ "WHO.BioHub.Shared", "WHO.BioHub.Shared"]
COPY [ "WHO.BioHub.API.Http.Extensions", "WHO.BioHub.API.Http.Extensions"]
COPY [ \
    "Search/WHO.BioHub.Search.SQL", \
    "Search/WHO.BioHub.Search.SQL" ]
COPY [ \
    "Search/WHO.BioHub.Search.Core", \
    "Search/WHO.BioHub.Search.Core" ]
COPY [ \
    "Search/WHO.BioHub.Search.API.Http", \
    "Search/WHO.BioHub.Search.API.Http" ]
COPY [ \
    "Search/WHO.BioHub.Search.FunctionApp", \
    "Search/WHO.BioHub.Search.FunctionApp" ]

WORKDIR "/src/Search/WHO.BioHub.Search.FunctionApp/"
RUN dotnet build "WHO.BioHub.Search.FunctionApp.csproj" -c Release --no-restore

FROM build as publish
WORKDIR "/src/Search/WHO.BioHub.Search.FunctionApp/"
RUN dotnet publish "WHO.BioHub.Search.FunctionApp.csproj" -c Release --no-build -o /app/publish

FROM base AS final
# COPY ["WHO.BioHub.Search.FunctionApp/appsettings.json", "/home/site/wwwroot/appsettings.json"]
COPY --chown=nobody:nogroup --from=publish /app/publish /home/site/wwwroot
