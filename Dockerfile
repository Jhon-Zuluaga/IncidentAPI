# Etapa 1: Build
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY ["IncidentAPI.Api/IncidentAPI.Api.csproj", "IncidentAPI.Api/"]
RUN dotnet restore "IncidentAPI.Api/IncidentAPI.Api.csproj"

COPY . .
WORKDIR "/src/IncidentAPI.Api"
RUN dotnet publish -c Release -o /app/publish

# Etapa 2: Runtimecd
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "IncidentAPI.Api.dll"]