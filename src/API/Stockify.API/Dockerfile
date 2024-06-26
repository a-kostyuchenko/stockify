﻿FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/API/Stockify.API/Stockify.API.csproj", "src/API/Stockify.API/"]
COPY ["src/Modules/Users/Stockify.Modules.Users.Infrastructure/Stockify.Modules.Users.Infrastructure.csproj", "src/Modules/Users/Stockify.Modules.Users.Infrastructure/"]
COPY ["src/Common/Stockify.Common.Infrastructure/Stockify.Common.Infrastructure.csproj", "src/Common/Stockify.Common.Infrastructure/"]
COPY ["src/Common/Stockify.Common.Application/Stockify.Common.Application.csproj", "src/Common/Stockify.Common.Application/"]
COPY ["src/Common/Stockify.Common.Domain/Stockify.Common.Domain.csproj", "src/Common/Stockify.Common.Domain/"]
COPY ["src/Modules/Users/Stockify.Modules.Users.Application/Stockify.Modules.Users.Application.csproj", "src/Modules/Users/Stockify.Modules.Users.Application/"]
COPY ["src/Modules/Users/Stockify.Modules.Users.Domain/Stockify.Modules.Users.Domain.csproj", "src/Modules/Users/Stockify.Modules.Users.Domain/"]
RUN dotnet restore "src/API/Stockify.API/Stockify.API.csproj"
COPY . .
WORKDIR "/src/src/API/Stockify.API"
RUN dotnet build "Stockify.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Stockify.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Stockify.API.dll"]
