# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0-alpine3.18 AS build-env
WORKDIR /app
EXPOSE 5000
COPY *.csproj ./
RUN dotnet restore
COPY . ./
RUN dotnet publish -c Release -o main

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine3.18 AS build-runtime
WORKDIR /app
COPY --from=build-env /app/main .
ENTRYPOINT ["dotnet", "API.dll"]