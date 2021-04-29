#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["SchoolRepairSystem.Api.csproj", ""]
COPY ["../SchoolRepairSystem.Service/SchoolRepairSystem.Service.csproj", "../SchoolRepairSystem.Service/"]
COPY ["../SchoolRepairSystem.Models/SchoolRepairSystem.Models.csproj", "../SchoolRepairSystem.Models/"]
COPY ["../SchoolRepairSystem.IService/SchoolRepairSystem.IService.csproj", "../SchoolRepairSystem.IService/"]
COPY ["../SchoolRepairSystem.IRepository/SchoolRepairSystem.IRepository.csproj", "../SchoolRepairSystem.IRepository/"]
COPY ["../SchoolRepairSystem.Extensions/SchoolRepairSystem.Extensions.csproj", "../SchoolRepairSystem.Extensions/"]
COPY ["../SchoolRepairSystem.Common/SchoolRepairSystem.Common.csproj", "../SchoolRepairSystem.Common/"]
COPY ["../SchoolRepairSystem.Repository/SchoolRepairSystem.Repository.csproj", "../SchoolRepairSystem.Repository/"]
RUN dotnet restore "./SchoolRepairSystem.Api.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "SchoolRepairSystem.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SchoolRepairSystem.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SchoolRepairSystem.Api.dll"]