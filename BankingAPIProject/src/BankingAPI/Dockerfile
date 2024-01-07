# Base Image oluşturuluyor
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 8080

# Uygulama Derleme (Build) Aşaması
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["BankingAPI.csproj", "./"]
RUN dotnet restore "BankingAPI.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "BankingAPI.csproj" -c Release -o /app/build

# Uygulama Yayınlama (Publish) Aşaması
FROM build AS publish
RUN dotnet publish "BankingAPI.csproj" -c Release -o /app/publish

# Son (Final) Aşama
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BankingAPI.dll"]
