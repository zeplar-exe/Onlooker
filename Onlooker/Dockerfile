FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Onlooker/Onlooker.csproj", "Onlooker/"]
RUN dotnet restore "Onlooker/Onlooker.csproj"
COPY . .
WORKDIR "/src/Onlooker"
RUN dotnet build "Onlooker.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Onlooker.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Onlooker.dll"]
