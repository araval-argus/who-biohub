#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app
USER nobody
MAINTAINER proge-software

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS restore
WORKDIR /src/
COPY [ \
    "WHO.BioHub.DAL/WHO.BioHub.DAL.csproj", \
    "WHO.BioHub.DAL/WHO.BioHub.DAL.csproj"]
COPY [ \
    "WHO.BioHub.Models/WHO.BioHub.Models.csproj", \
    "WHO.BioHub.Models/WHO.BioHub.Models.csproj"]
COPY [ \
    "WHO.BioHub.Shared/WHO.BioHub.Shared.csproj", \
    "WHO.BioHub.Shared/WHO.BioHub.Shared.csproj"]
COPY [ \
    "WHO.BioHub.DAL.Migrator/WHO.BioHub.DAL.Migrator.csproj", \
    "WHO.BioHub.DAL.Migrator/WHO.BioHub.DAL.Migrator.csproj"]

RUN dotnet restore "WHO.BioHub.DAL.Migrator/WHO.BioHub.DAL.Migrator.csproj"

FROM restore AS build
WORKDIR /src

COPY [ "WHO.BioHub.DAL", "WHO.BioHub.DAL"]
COPY [ "WHO.BioHub.Models", "WHO.BioHub.Models"]
COPY [ "WHO.BioHub.Shared", "WHO.BioHub.Shared"]
COPY [ "WHO.BioHub.DAL.Migrator", "WHO.BioHub.DAL.Migrator"]

RUN dotnet build "WHO.BioHub.DAL.Migrator/WHO.BioHub.DAL.Migrator.csproj" -c Release --no-restore

FROM build AS publish
WORKDIR /src
RUN dotnet publish "WHO.BioHub.DAL.Migrator/WHO.BioHub.DAL.Migrator.csproj" -c Release --no-build -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT [ "dotnet" ]
CMD ["WHO.BioHub.DAL.Migrator.dll"]
