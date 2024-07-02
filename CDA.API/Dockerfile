#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["CDA.API/CDA.API.csproj", "CDA.API/"]
COPY ["CDA.Access/CDA.Access.csproj", "CDA.Access/"]
COPY ["CDA.Core/CDA.Core.csproj", "CDA.Core/"]
COPY ["CDA.Data/CDA.Data.csproj", "CDA.Data/"]
COPY ["CDA.IAccess/CDA.IAccess.csproj", "CDA.IAccess/"]
COPY ["CDA.RedisCache/CDA.RedisCache.csproj", "CDA.RedisCache/"]
COPY ["CDA.Utilities/CDA.Utilities.csproj", "CDA.Utilities/"]
COPY ["CDA.GraphQL/CDA.GraphQL.csproj", "CDA.GraphQL/"]
COPY ["CDA.Managers/CDA.Managers.csproj", "CDA.Managers/"]
RUN dotnet restore "./CDA.API/CDA.API.csproj"
COPY . .
WORKDIR "/src/CDA.API"
RUN dotnet build "./CDA.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./CDA.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CDA.API.dll"]