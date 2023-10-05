#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["DeliveryService.Services.PaymentAPI/DeliveryService.Services.PaymentAPI.csproj", "DeliveryService.Services.PaymentAPI/"]
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