#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 5020
EXPOSE 5021

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
ENV ASPNETCORE_ENVIRONMENT=Docker
WORKDIR /src
COPY ["VendasWebApi/VendasWebApi.csproj", "VendasWebApi/"]
COPY ["VendasWebApplication/VendasWebApplication.csproj", "VendasWebApplication/"]
COPY ["VendasWebCore/VendasWebCore.csproj", "VendasWebCore/"]
COPY ["VendasWebInfrastructure/VendasWebInfrastructure.csproj", "VendasWebInfrastructure/"]
RUN dotnet restore "./VendasWebApi/VendasWebApi.csproj"
COPY . .
WORKDIR "/src/VendasWebApi"
RUN dotnet build "./VendasWebApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./VendasWebApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "VendasWebApi.dll"]