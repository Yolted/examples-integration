FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY IntegrationSample.csproj IntegrationSample/
#COPY Directory.Build.props .
WORKDIR /src/IntegrationSample
RUN dotnet restore /p:RestoreUseSkipNonexistentTargets="false"

COPY . .
WORKDIR /src/IntegrationSample
RUN dotnet publish -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["dotnet", "IntegrationSample.dll"]

