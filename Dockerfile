#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

ARG mongodb_connection_string
ENV MONGODB_CONNECTION_STRING=$mongodb_connection_string

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["tft-module/tft-module.csproj", "tft-module/"]
RUN dotnet restore "tft-module/tft-module.csproj"
COPY . .
WORKDIR "/src/tft-module"
RUN dotnet build "tft-module.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "tft-module.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "tft-module.dll"]