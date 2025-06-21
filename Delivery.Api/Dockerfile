FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app

COPY . .

WORKDIR /app/Delivery.Api

RUN dotnet restore

RUN dotnet publish -c Release -o ../out