# Use the ASP.NET Core runtime image as the base
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 8081

# Build stage
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copy the solution file and all project files
COPY AccountService.sln .  
# Ensure you have a solution file
COPY AccountService/*.csproj AccountService/
COPY BAL/*.csproj BAL/

# Restore dependencies
RUN dotnet restore

# Copy all the source code
COPY AccountService/. AccountService/
COPY BAL/. BAL/

# Build the application
WORKDIR "/src/AccountService"
RUN dotnet build "AccountService.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Publish stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "AccountService.csproj" -c $BUILD_CONFIGURATION -o /app/publish

# Final stage
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AccountService.dll"]