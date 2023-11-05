#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Services/RestaurantAPI/DeliveryService.Services.RestaurantAPI/DeliveryService.Services.RestaurantAPI.csproj", "Services/RestaurantAPI/DeliveryService.Services.RestaurantAPI/"]
COPY ["Services/RestaurantAPI/DeliveryService.Services.RestaurantAPI.App/DeliveryService.Services.RestaurantAPI.App.csproj", "Services/RestaurantAPI/DeliveryService.Services.RestaurantAPI.App/"]
COPY ["Services/RestaurantAPI/DeliveryService.Services.RestaurantAPI.Domain/DeliveryService.Services.RestaurantAPI.Domain.csproj", "Services/RestaurantAPI/DeliveryService.Services.RestaurantAPI.Domain/"]
COPY ["Services/RestaurantAPI/DeliveryService.Services.RestaurantAPI.Contracts/DeliveryService.Services.RestaurantAPI.Contracts.csproj", "Services/RestaurantAPI/DeliveryService.Services.RestaurantAPI.Contracts/"]
RUN dotnet restore "Services/RestaurantAPI/DeliveryService.Services.RestaurantAPI/DeliveryService.Services.RestaurantAPI.csproj"
COPY . .
WORKDIR "/src/Services/RestaurantAPI/DeliveryService.Services.RestaurantAPI"
RUN dotnet build "DeliveryService.Services.RestaurantAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DeliveryService.Services.RestaurantAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DeliveryService.Services.RestaurantAPI.dll"]