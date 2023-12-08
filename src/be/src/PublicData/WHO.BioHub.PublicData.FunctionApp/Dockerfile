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
    "WHO.BioHub.Models/WHO.BioHub.Models.csproj", \
    "WHO.BioHub.Models/WHO.BioHub.Models.csproj"]
COPY [ \
    "WHO.BioHub.Notifications/WHO.BioHub.Notifications.csproj", \
    "WHO.BioHub.Notifications/WHO.BioHub.Notifications.csproj" ]
COPY [ \
    "WHO.BioHub.Shared/WHO.BioHub.Shared.csproj", \
    "WHO.BioHub.Shared/WHO.BioHub.Shared.csproj"]
COPY [ \
    "WHO.BioHub.API.Http.Extensions/WHO.BioHub.API.Http.Extensions.csproj", \
    "WHO.BioHub.API.Http.Extensions/WHO.BioHub.API.Http.Extensions.csproj"]
COPY [ \
    "PublicData/WHO.BioHub.PublicData.Core/WHO.BioHub.PublicData.Core.csproj", \
    "PublicData/WHO.BioHub.PublicData.Core/WHO.BioHub.PublicData.Core.csproj" ]
COPY [ \
    "PublicData/WHO.BioHub.PublicData.SQL/WHO.BioHub.PublicData.SQL.csproj", \
    "PublicData/WHO.BioHub.PublicData.SQL/WHO.BioHub.PublicData.SQL.csproj" ]
COPY [ \
    "PublicData/WHO.BioHub.PublicData.API.Http/WHO.BioHub.PublicData.API.Http.csproj", \
    "PublicData/WHO.BioHub.PublicData.API.Http/WHO.BioHub.PublicData.API.Http.csproj" ]
COPY [ \
    "PublicData/WHO.BioHub.PublicData.FunctionApp/WHO.BioHub.PublicData.FunctionApp.csproj", \
    "PublicData/WHO.BioHub.PublicData.FunctionApp/WHO.BioHub.PublicData.FunctionApp.csproj" ]

RUN find . -iname '*.csproj' | xargs -I {} dotnet restore {}

FROM restore as build
WORKDIR "/src"

COPY [ "WHO.BioHub.Captcha", "WHO.BioHub.Captcha" ]
COPY [ "WHO.BioHub.DAL", "WHO.BioHub.DAL" ]
COPY [ "WHO.BioHub.Models", "WHO.BioHub.Models"]
COPY [ "WHO.BioHub.Notifications", "WHO.BioHub.Notifications" ]
COPY [ "WHO.BioHub.Shared", "WHO.BioHub.Shared"]
COPY [ "WHO.BioHub.API.Http.Extensions", "WHO.BioHub.API.Http.Extensions"]
COPY [ \
    "PublicData/WHO.BioHub.PublicData.Core", \
    "PublicData/WHO.BioHub.PublicData.Core" ]
COPY [ \
    "PublicData/WHO.BioHub.PublicData.SQL", \
    "PublicData/WHO.BioHub.PublicData.SQL" ]
COPY [ \
    "PublicData/WHO.BioHub.PublicData.API.Http", \
    "PublicData/WHO.BioHub.PublicData.API.Http" ]
COPY [ \
    "PublicData/WHO.BioHub.PublicData.FunctionApp", \
    "PublicData/WHO.BioHub.PublicData.FunctionApp" ]

WORKDIR "/src/PublicData/WHO.BioHub.PublicData.FunctionApp/"
RUN dotnet build "WHO.BioHub.PublicData.FunctionApp.csproj" -c Release --no-restore

FROM build as publish
WORKDIR "/src/PublicData/WHO.BioHub.PublicData.FunctionApp/"
RUN dotnet publish "WHO.BioHub.PublicData.FunctionApp.csproj" -c Release --no-build -o /app/publish

FROM base AS final
# COPY ["WHO.BioHub.PublicData.FunctionApp/appsettings.json", "/home/site/wwwroot/appsettings.json"]
COPY --chown=nobody:nogroup --from=publish /app/publish /home/site/wwwroot
