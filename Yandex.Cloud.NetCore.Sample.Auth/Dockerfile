#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Yandex.Cloud.NetCore.Sample.Auth/Yandex.Cloud.NetCore.Sample.Auth.csproj", "Yandex.Cloud.NetCore.Sample.Auth/"]
RUN dotnet restore "Yandex.Cloud.NetCore.Sample.Auth/Yandex.Cloud.NetCore.Sample.Auth.csproj"
COPY . .
WORKDIR "/src/Yandex.Cloud.NetCore.Sample.Auth"
RUN dotnet build "Yandex.Cloud.NetCore.Sample.Auth.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Yandex.Cloud.NetCore.Sample.Auth.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Yandex.Cloud.NetCore.Sample.Auth.dll"]