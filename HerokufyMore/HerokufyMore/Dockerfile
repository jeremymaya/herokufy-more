#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443
ARG HEROKU_POSTGRESQL_OLIVE_URL
ARG HEROKU_POSTGRESQL_RED_URL
ARG ADMIN_EMAIL
ARG ADMIN_PASSWORD
#ARG is not available after the image build
ENV ADMIN_EMAIL_ENV=$ADMIN_EMAIL
ENV ADMIN_PASSWORD_ENV=$ADMIN_PASSWORD

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["HerokufyMore.csproj", "./"]
RUN dotnet restore "./HerokufyMore.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "HerokufyMore.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "HerokufyMore.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
CMD ASPNETCORE_URLS=http://*:$PORT dotnet HerokufyMore.dll
