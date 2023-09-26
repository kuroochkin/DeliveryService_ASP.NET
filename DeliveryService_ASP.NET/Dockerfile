#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["DeliveryService_ASP.NET/DeliveryService.API.csproj", "DeliveryService_ASP.NET/"]
COPY ["DeliveryService.App/DeliveryService.App.csproj", "DeliveryService.App/"]
COPY ["DeliveryService.Contracts/DeliveryService.Contracts.csproj", "DeliveryService.Contracts/"]
COPY ["DeliveryService.Domain/DeliveryService.Domain.csproj", "DeliveryService.Domain/"]
COPY ["DeliveryService.infrastructure/DeliveryService.infrastructure.csproj", "DeliveryService.infrastructure/"]
RUN dotnet restore "DeliveryService_ASP.NET/DeliveryService.API.csproj"
COPY . .
WORKDIR "/src/DeliveryService_ASP.NET"
RUN dotnet build "DeliveryService.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DeliveryService.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DeliveryService.API.dll"]