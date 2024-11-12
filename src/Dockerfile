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

# Set environment variables from build arguments
ENV Authentication__Google__ClientId=$GOOGLE_CLIENT_ID
ENV Authentication__Google__ClientSecret=$GOOGLE_CLIENT_SECRET
ENV AdminUser__Username=$ADMIN_USERNAME
ENV AdminUser__Password=$ADMIN_PASSWORD

ENTRYPOINT ["dotnet", "RentEZ.WebAPI.dll"]
