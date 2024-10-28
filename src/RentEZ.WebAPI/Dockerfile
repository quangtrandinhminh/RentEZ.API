FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src/RentEZ.webapi/
COPY ["RentEZ.WebAPI/RentEZ.WebAPI.csproj", "RentEZ.WebAPI/"]
COPY ["Service/Service.csproj", "RentEZ.Service/"]
COPY ["Repository/Repository.csproj", "RentEZ.Repository/"]
COPY ["Utility/Utility.csproj", "RentEZ.Utility/"]
RUN dotnet restore "./RentEZ.WebAPI/./RentEZ.WebAPI.csproj"
COPY . .
WORKDIR "/src/RentEZ.webapi/RentEZ.WebAPI"
RUN dotnet build "./RentEZ.WebAPI.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./RentEZ.WebAPI.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RentEZ.WebAPI.dll"]