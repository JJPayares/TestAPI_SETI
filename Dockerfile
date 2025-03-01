# Imagen base de .NET 8 para construir la aplicación
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5000
EXPOSE 5001


# Imagen base para compilar la aplicación
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["TestAPI.WebApi.csproj", "./"]
RUN dotnet restore "./TestAPI.WebApi.csproj"

# Copiar todo el código fuente y compilar
COPY . .
RUN dotnet publish "TestAPI.WebApi.csproj" -c Release -o /app/publish

# Imagen final para ejecutar la app
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "TestAPI.WebApi.dll"]
