#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443
EXPOSE 2000

# FROM mcr.microsoft.com/mssql/server:2022-latest AS db

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["JWTAuthentication/JWTAuthentication.csproj", "JWTAuthentication/"]
WORKDIR /src
COPY ["./PensionManagementSystem.Data/PensionManagementSystem.Data.csproj", "PensionManagementSystem.Data/"]
WORKDIR /src
COPY ["./PensionManagementSystem.Models/PensionManagementSystem.Models.csproj", "PensionManagementSystem.Models/"]

RUN dotnet restore "JWTAuthentication/JWTAuthentication.csproj"
RUN dotnet restore "PensionManagementSystem.Data/PensionManagementSystem.Data.csproj"
RUN dotnet restore "PensionManagementSystem.Models/PensionManagementSystem.Models.csproj"
COPY . .
WORKDIR "/src/JWTAuthentication"
RUN dotnet build "JWTAuthentication.csproj" -c Release -o /app/build
WORKDIR "/src/PensionManagementSystem.Data"
RUN dotnet build "PensionManagementSystem.Data.csproj" -c Release -o /app/build
WORKDIR "/src/PensionManagementSystem.Models"
RUN dotnet build "PensionManagementSystem.Models.csproj" -c Release -o /app/build

FROM build AS publish
WORKDIR "/src/JWTAuthentication"
RUN dotnet publish "JWTAuthentication.csproj" -c Release -o /app/publish
WORKDIR "/src/PensionManagementSystem.Data"
RUN dotnet publish "PensionManagementSystem.Data.csproj" -c Release -o /app/publish
WORKDIR "/src/PensionManagementSystem.Models"
RUN dotnet publish "PensionManagementSystem.Models.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "JWTAuthentication.dll"]