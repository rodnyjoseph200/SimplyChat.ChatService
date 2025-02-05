# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


### This stage is used to build the service project ###
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
# Copy MSBuild property files
COPY ["Directory.Build.props", "src/Apps/ChatService.APIs.REST/"]
COPY ["Directory.Packages.props", "src/Apps/ChatService.APIs.REST/"]
# Copy the csproj file and restore dependencies
COPY ["src/Apps/ChatService.APIs.REST/ChatService.APIs.REST.csproj", "src/Apps/ChatService.APIs.REST/"]
RUN dotnet restore "./src/Apps/ChatService.APIs.REST/ChatService.APIs.REST.csproj"
# Copy the remaining source code and build the project
COPY . .
WORKDIR "/src/src/Apps/ChatService.APIs.REST"
RUN dotnet build "./ChatService.APIs.REST.csproj" -c $BUILD_CONFIGURATION -o /app/build

### This stage is used to publish the service project to be copied to the final stage ###
# Publish the application
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./ChatService.APIs.REST.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

### This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration) ###
# Use ASP.NET runtime image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ChatService.APIs.REST.dll"]