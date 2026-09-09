# Estágio de Compilação (SDK)
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copia e restaura as dependências primeiro (otimiza o cache do Docker)
COPY *.csproj ./
RUN dotnet restore

# Copia todo o restante dos arquivos e publica a aplicação
COPY . ./
RUN dotnet publish -c Release -o /app/publish /p:UseAppHost=false

# Estágio de Execução (Runtime ASP.NET)
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app

# Copia os arquivos compilados do estágio anterior
COPY --from=build /app/publish .

# Define a porta que o Railway injeta automaticamente na variável PORT
ENV ASPNETCORE_URLS=http://+:${PORT}

# Substitua 'MeuProjeto.dll' pelo nome exato do seu arquivo .dll final
ENTRYPOINT ["dotnet", "ServSegFacilitiesAPI"]