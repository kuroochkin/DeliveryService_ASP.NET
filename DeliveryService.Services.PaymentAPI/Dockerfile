#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["DeliveryService.Services.PaymentAPI/DeliveryService.Services.PaymentAPI.csproj", "DeliveryService.Services.PaymentAPI/"]
COPY ["DeliveryService.App/DeliveryService.App.csproj", "DeliveryService.App/"]
COPY ["DeliveryService.Contracts/DeliveryService.Contracts.csproj", "DeliveryService.Contracts/"]
COPY ["DeliveryService.Domain/DeliveryService.Domain.csproj", "DeliveryService.Domain/"]
COPY ["DeliveryService.Services.PaymentAPI.App/DeliveryService.Services.PaymentAPI.App.csproj", "DeliveryService.Services.PaymentAPI.App/"]
COPY ["DeliveryService.Services.PaymentAPI.Domain/DeliveryService.Services.PaymentAPI.Domain.csproj", "DeliveryService.Services.PaymentAPI.Domain/"]
COPY ["DeliveryService.Services.PaymentAPI.Contracts/DeliveryService.Services.PaymentAPI.Contracts.csproj", "DeliveryService.Services.PaymentAPI.Contracts/"]
COPY ["DeliveryService.Services.PaymentAPI.Infrastructure/DeliveryService.Services.PaymentAPI.Infrastructure.csproj", "DeliveryService.Services.PaymentAPI.Infrastructure/"]
COPY ["DeliveryService.infrastructure/DeliveryService.infrastructure.csproj", "DeliveryService.infrastructure/"]
COPY ["DeliveryService_ASP.NET/DeliveryService.API.csproj", "DeliveryService_ASP.NET/"]
RUN dotnet restore "DeliveryService.Services.PaymentAPI/DeliveryService.Services.PaymentAPI.csproj"
COPY . .
WORKDIR "/src/DeliveryService.Services.PaymentAPI"
RUN dotnet build "DeliveryService.Services.PaymentAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DeliveryService.Services.PaymentAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DeliveryService.Services.PaymentAPI.dll"]