# Imagen base de .NET 8 para construir la aplicaci�n
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# Imagen base para compilar la aplicaci�n
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["TestAPI.WebApi.csproj", "./"]
RUN dotnet restore "./TestAPI.WebApi.csproj"

# Copiar todo el c�digo fuente y compilar
COPY . .
RUN dotnet publish "TestAPI.WebApi.csproj" -c Release -o /app/publish

# Imagen final para ejecutar la app
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "TestAPI.WebApi.dll"]
