# Stage 1: Base Image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 80
EXPOSE 443

# Stage 2: Build Image
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src/RentEZ.webapi/

# Copy project files and restore dependencies
COPY ["RentEZ.WebAPI/RentEZ.WebAPI.csproj", "RentEZ.WebAPI/"]
COPY ["Service/Service.csproj", "RentEZ.Service/"]
COPY ["Repository/Repository.csproj", "RentEZ.Repository/"]
COPY ["Utility/Utility.csproj", "RentEZ.Utility/"]
RUN dotnet restore "./RentEZ.WebAPI/RentEZ.WebAPI.csproj"

# Copy the source code and build
COPY . .
WORKDIR "/src/RentEZ.webapi/RentEZ.WebAPI"
RUN dotnet build "./RentEZ.WebAPI.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Stage 3: Publish Image
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./RentEZ.WebAPI.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Stage 4: Final Image
FROM base AS final
WORKDIR /app

# Copy published app
COPY --from=publish /app/publish .

# Attempt to copy secrets.json using Docker Build Secrets
USER root
RUN --mount=type=secret,id=usersecrets,required=false \
    if [ -f /run/secrets/usersecrets ]; then \
        cp /run/secrets/usersecrets /app/secrets.json && \
        echo "Using Docker Build Secret for secrets.json"; \
    else \
        echo "Docker Build Secret not provided, using fallback COPY command"; \
    fi

# Fallback to COPY if the secret was not provided
COPY RentEZ.WebAPI/secrets.json /app/secrets.json

# Set environment variables for secrets (optional)
ENV ASPNETCORE_ENVIRONMENT=Production
ENV SECRETS_FILE_PATH=/app/secrets.json

# Revert back to non-root user
USER app

ENTRYPOINT ["dotnet", "RentEZ.WebAPI.dll"]
