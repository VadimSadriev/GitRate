FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build

RUN mkdir -p /build

WORKDIR /build

COPY . /build

WORKDIR /build/GitRate.Auth.Web

RUN dotnet publish GitRate.Auth.Web.csproj -c Release -o /build/out

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim

RUN mkdir -p /app

WORKDIR /app

EXPOSE 5000

COPY --from=build /build/out /app

ENTRYPOINT ["dotnet", "GitRate.Auth.Web.dll"]