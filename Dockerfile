# syntax=docker/dockerfile:1

# ---- build ----
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY *.sln ./
COPY src/DocLicensing.Core/*.csproj src/DocLicensing.Core/
COPY src/DocLicensing.Infrastructure/*.csproj src/DocLicensing.Infrastructure/
COPY src/DocLicensing.Api/*.csproj src/DocLicensing.Api/
COPY tests/DocLicensing.Tests/*.csproj tests/DocLicensing.Tests/
RUN dotnet restore src/DocLicensing.Api/DocLicensing.Api.csproj

COPY . .
RUN dotnet publish src/DocLicensing.Api/DocLicensing.Api.csproj -c Release -o /app

# ---- runtime ----
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app ./
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080
ENTRYPOINT ["dotnet", "DocLicensing.Api.dll"]
