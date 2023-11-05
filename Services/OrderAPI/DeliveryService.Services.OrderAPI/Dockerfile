#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Services/OrderAPI/DeliveryService.Services.OrderAPI/DeliveryService.Services.OrderAPI.csproj", "Services/OrderAPI/DeliveryService.Services.OrderAPI/"]
COPY ["Services/OrderAPI/DeliveryService.Services.OrderAPI.App/DeliveryService.Services.OrderAPI.App.csproj", "Services/OrderAPI/DeliveryService.Services.OrderAPI.App/"]
COPY ["DeliveryService.Services.ProductAPI.App/DeliveryService.Services.ProductAPI.App.csproj", "DeliveryService.Services.ProductAPI.App/"]
COPY ["DeliveryService.App/DeliveryService.App.csproj", "DeliveryService.App/"]
COPY ["DeliveryService.Contracts/DeliveryService.Contracts.csproj", "DeliveryService.Contracts/"]
COPY ["DeliveryService.Domain/DeliveryService.Domain.csproj", "DeliveryService.Domain/"]
COPY ["DeliveryService.Services.ProductAPI.Domain/DeliveryService.Services.ProductAPI.Domain.csproj", "DeliveryService.Services.ProductAPI.Domain/"]
COPY ["Services/OrderAPI/DeliveryService.Services.OrderAPI.Domain/DeliveryService.Services.OrderAPI.Domain.csproj", "Services/OrderAPI/DeliveryService.Services.OrderAPI.Domain/"]
COPY ["Services/OrderAPI/DeliveryService.Services.OrderAPI.Infrastructure/DeliveryService.Services.OrderAPI.Infrastructure.csproj", "Services/OrderAPI/DeliveryService.Services.OrderAPI.Infrastructure/"]
COPY ["DeliveryService.infrastructure/DeliveryService.infrastructure.csproj", "DeliveryService.infrastructure/"]
RUN dotnet restore "Services/OrderAPI/DeliveryService.Services.OrderAPI/DeliveryService.Services.OrderAPI.csproj"
COPY . .
WORKDIR "/src/Services/OrderAPI/DeliveryService.Services.OrderAPI"
RUN dotnet build "DeliveryService.Services.OrderAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DeliveryService.Services.OrderAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DeliveryService.Services.OrderAPI.dll"]