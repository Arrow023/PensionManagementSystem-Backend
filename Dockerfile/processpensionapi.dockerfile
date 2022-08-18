#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443
EXPOSE 2001

# FROM mcr.microsoft.com/mssql/server:2022-latest AS db

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["../Process Pension Module/Process Pension Module.csproj", "Process Pension Module/"]
WORKDIR /src
COPY ["../PensionManagementSystem.Data/PensionManagementSystem.Data.csproj", "PensionManagementSystem.Data/"]
WORKDIR /src
COPY ["../PensionManagementSystem.Models/PensionManagementSystem.Models.csproj", "PensionManagementSystem.Models/"]

RUN dotnet restore "../Process Pension Module/Process Pension Module.csproj"
RUN dotnet restore "../PensionManagementSystem.Data/PensionManagementSystem.Data.csproj"
RUN dotnet restore "../PensionManagementSystem.Models/PensionManagementSystem.Models.csproj"

COPY . .

WORKDIR "/src/Process Pension Module"
RUN dotnet build "Process Pension Module.csproj" -c Release -o /app/build
WORKDIR "/src/PensionManagementSystem.Data"
RUN dotnet build "PensionManagementSystem.Data.csproj" -c Release -o /app/build
WORKDIR "/src/PensionManagementSystem.Models"
RUN dotnet build "PensionManagementSystem.Models.csproj" -c Release -o /app/build

FROM build AS publish
WORKDIR "/src/Process Pension Module"
RUN dotnet publish "Process Pension Module.csproj" -c Release -o /app/publish
WORKDIR "/src/PensionManagementSystem.Data"
RUN dotnet publish "PensionManagementSystem.Data.csproj" -c Release -o /app/publish
WORKDIR "/src/PensionManagementSystem.Models"
RUN dotnet publish "PensionManagementSystem.Models.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Process Pension Module.dll"]