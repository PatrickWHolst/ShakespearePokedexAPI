FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY ["ShakespearePokedexAPI/ShakespearePokedexAPI.csproj", "ShakespearePokedexAPI/"]
RUN dotnet restore "./ShakespearePokedexAPI.csproj"
COPY . .
WORKDIR "/src/ShakespearePokedexAPI"
RUN dotnet build "./ShakespearePokedexAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "./ShakespearePokedexAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ShakespearePokedexAPI.dll"]