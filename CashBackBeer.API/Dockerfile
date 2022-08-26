#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["CashBackBeer.API/CashBackBeer.API.csproj", "CashBackBeer.API/"]
COPY ["CashBackBeer.Application/CashBackBeer.Application.csproj", "CashBackBeer.Application/"]
COPY ["CashBackBeer.Domain/CashBackBeer.Domain.csproj", "CashBackBeer.Domain/"]
COPY ["CashBackBeer.Infra.IoC/CashBackBeer.Infra.IoC.csproj", "CashBackBeer.Infra.IoC/"]
COPY ["CashBackBeer.Infra.Data/CashBackBeer.Infra.Data.csproj", "CashBackBeer.Infra.Data/"]
RUN dotnet restore "CashBackBeer.API/CashBackBeer.API.csproj"
COPY . .
WORKDIR "/src/CashBackBeer.API"
RUN dotnet build "CashBackBeer.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CashBackBeer.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CashBackBeer.API.dll"]