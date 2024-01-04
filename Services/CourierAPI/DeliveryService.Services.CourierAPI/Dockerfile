#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Services/CourierAPI/DeliveryService.Services.CourierAPI/DeliveryService.Services.CourierAPI.csproj", "Services/CourierAPI/DeliveryService.Services.CourierAPI/"]
COPY ["Services/CourierAPI/DeliveryService.Services.CourierAPI.App/DeliveryService.Services.CourierAPI.App.csproj", "Services/CourierAPI/DeliveryService.Services.CourierAPI.App/"]
COPY ["DeliveryService.App/DeliveryService.App.csproj", "DeliveryService.App/"]
COPY ["DeliveryService.Contracts/DeliveryService.Contracts.csproj", "DeliveryService.Contracts/"]
COPY ["DeliveryService.Domain/DeliveryService.Domain.csproj", "DeliveryService.Domain/"]
COPY ["Services/CourierAPI/DeliveryService.Services.CourierAPI.Domain/DeliveryService.Services.CourierAPI.Domain.csproj", "Services/CourierAPI/DeliveryService.Services.CourierAPI.Domain/"]
COPY ["Services/CourierAPI/DeliveryService.Services.CourierAPI.Contracts/DeliveryService.Services.CourierAPI.Contracts.csproj", "Services/CourierAPI/DeliveryService.Services.CourierAPI.Contracts/"]
COPY ["Services/CourierAPI/DeliveryService.Services.CourierAPI.Infrastructure/DeliveryService.Services.CourierAPI.Infrastructure.csproj", "Services/CourierAPI/DeliveryService.Services.CourierAPI.Infrastructure/"]
COPY ["DeliveryService.infrastructure/DeliveryService.infrastructure.csproj", "DeliveryService.infrastructure/"]
RUN dotnet restore "Services/CourierAPI/DeliveryService.Services.CourierAPI/DeliveryService.Services.CourierAPI.csproj"
COPY . .
WORKDIR "/src/Services/CourierAPI/DeliveryService.Services.CourierAPI"
RUN dotnet build "DeliveryService.Services.CourierAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DeliveryService.Services.CourierAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DeliveryService.Services.CourierAPI.dll"]