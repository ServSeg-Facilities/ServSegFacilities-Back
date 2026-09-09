# Estágio de Compilação (SDK)
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copia o arquivo .csproj do caminho exato da subpasta
COPY ServSegFacilitiesAPI/ServSegFacilitiesAPI/*.csproj ServSegFacilitiesAPI/ServSegFacilitiesAPI/
RUN dotnet restore ServSegFacilitiesAPI/ServSegFacilitiesAPI/*.csproj

# Copia todo o código-fonte
COPY . .

# Altera a pasta de trabalho para onde está o código antes de publicar
WORKDIR /src/ServSegFacilitiesAPI/ServSegFacilitiesAPI
RUN dotnet publish -c Release -o /app/publish /p:UseAppHost=false

# Estágio de Execução (Runtime ASP.NET)
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app

# Copia os arquivos compilados do estágio de build
COPY --from=build /app/publish .

# Define a porta injetada pelo Railway
ENV ASPNETCORE_URLS=http://+:${PORT}

# Executa a DLL da sua API
ENTRYPOINT ["dotnet", "ServSegFacilitiesAPI.dll"]