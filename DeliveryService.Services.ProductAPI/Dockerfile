#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["DeliveryService.Services.ProductAPI/DeliveryService.Services.ProductAPI.csproj", "DeliveryService.Services.ProductAPI/"]
COPY ["DeliveryService.Services.ProductAPI.App/DeliveryService.Services.ProductAPI.App.csproj", "DeliveryService.Services.ProductAPI.App/"]
COPY ["DeliveryService.App/DeliveryService.App.csproj", "DeliveryService.App/"]
COPY ["DeliveryService.Contracts/DeliveryService.Contracts.csproj", "DeliveryService.Contracts/"]
COPY ["DeliveryService.Domain/DeliveryService.Domain.csproj", "DeliveryService.Domain/"]
COPY ["DeliveryService.Services.ProductAPI.Domain/DeliveryService.Services.ProductAPI.Domain.csproj", "DeliveryService.Services.ProductAPI.Domain/"]
COPY ["DeliveryService.Services.ProductAPI.Contracts/DeliveryService.Services.ProductAPI.Contracts.csproj", "DeliveryService.Services.ProductAPI.Contracts/"]
COPY ["DeliveryService.Services.ProductAPI.Infrastructure/DeliveryService.Services.ProductAPI.Infrastructure.csproj", "DeliveryService.Services.ProductAPI.Infrastructure/"]
COPY ["DeliveryService.infrastructure/DeliveryService.infrastructure.csproj", "DeliveryService.infrastructure/"]
RUN dotnet restore "DeliveryService.Services.ProductAPI/DeliveryService.Services.ProductAPI.csproj"
COPY . .
WORKDIR "/src/DeliveryService.Services.ProductAPI"
RUN dotnet build "DeliveryService.Services.ProductAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DeliveryService.Services.ProductAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DeliveryService.Services.ProductAPI.dll"]