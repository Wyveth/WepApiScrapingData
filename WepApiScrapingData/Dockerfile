#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

# Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copier les .csproj pour le restore
COPY ["WepApiScrapingData/WepApiScrapingData.csproj", "WepApiScrapingData/"]
COPY ["WebApiScrapingData.Framework/WebApiScrapingData.Core.csproj", "WebApiScrapingData.Framework/"]
COPY ["WebApiScrapingData.Domain/WebApiScrapingData.Domain.csproj", "WebApiScrapingData.Domain/"]
COPY ["WebApiScrapingData.Infrastructure/WebApiScrapingData.Infrastructure.csproj", "WebApiScrapingData.Infrastructure/"]

# Restore tous les projets
RUN dotnet restore "WepApiScrapingData/WepApiScrapingData.csproj"

# Copier tout le code
COPY . .

# Build
WORKDIR "/src/WepApiScrapingData"
RUN dotnet build "WepApiScrapingData.csproj" -c Release -o /app/build

# Publish
FROM build AS publish
RUN dotnet publish "WepApiScrapingData.csproj" -c Release -o /app/publish

# Image finale
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV ASPNETCORE_URLS=http://+:80
ENV ASPNETCORE_ENVIRONMENT=Development
ENTRYPOINT ["dotnet", "WepApiScrapingData.dll"]




