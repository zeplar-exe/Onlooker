FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["NoiseGenerationTesting/NoiseGenerationTesting.csproj", "NoiseGenerationTesting/"]
RUN dotnet restore "NoiseGenerationTesting/NoiseGenerationTesting.csproj"
COPY . .
WORKDIR "/src/NoiseGenerationTesting"
RUN dotnet build "NoiseGenerationTesting.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "NoiseGenerationTesting.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NoiseGenerationTesting.dll"]
