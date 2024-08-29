# Imagem base com dependências de tempo de execução
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Imagem para construção
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copiar apenas os arquivos de projeto para maximizar o cache do Docker
COPY ["API/API.csproj", "API/"]
RUN dotnet restore "./API/API.csproj"

# Copiar o restante dos arquivos e construir a aplicação
COPY . .
WORKDIR "/src/API"
RUN dotnet build "./API.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Publicação da aplicação
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Imagem final
FROM base AS final

# Definir um usuário não root para execução da aplicação
RUN adduser --disabled-password --gecos "" appuser && chown -R appuser /app
USER appuser

WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "API.dll"]
