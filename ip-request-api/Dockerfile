#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["ip-request-api/ip-request-api.csproj", "ip-request-api/"]
RUN dotnet restore "ip-request-api/ip-request-api.csproj"
COPY . .
WORKDIR "/src/ip-request-api"
RUN dotnet build "ip-request-api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ip-request-api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ip-request-api.dll"]