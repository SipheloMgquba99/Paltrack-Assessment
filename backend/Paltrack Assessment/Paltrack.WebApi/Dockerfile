FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ["Paltrack.WebApi/Paltrack.WebApi.csproj", "Paltrack.WebApi/"]
COPY ["../Paltrack.Application/Paltrack.Application.csproj", "Paltrack.Application/"]
COPY ["../Paltrack.Domain/Paltrack.Domain.csproj", "Paltrack.Domain/"]
COPY ["../Paltrack.Infrastructure/Paltrack.Infrastructure.csproj", "Paltrack.Infrastructure/"]

RUN dotnet restore "./Paltrack.WebApi/Paltrack.WebApi.csproj"
COPY . .
WORKDIR "/src/Paltrack.WebApi"
RUN dotnet build "./Paltrack.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Paltrack.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Paltrack.WebApi.dll"]

