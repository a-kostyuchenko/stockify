FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
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
COPY ["src/Common/Stockify.Common.Presentation/Stockify.Common.Presentation.csproj", "src/Common/Stockify.Common.Presentation/"]
COPY ["src/Modules/Users/Stockify.Modules.Users.Application/Stockify.Modules.Users.Application.csproj", "src/Modules/Users/Stockify.Modules.Users.Application/"]
COPY ["src/Modules/Users/Stockify.Modules.Users.Domain/Stockify.Modules.Users.Domain.csproj", "src/Modules/Users/Stockify.Modules.Users.Domain/"]
COPY ["src/Modules/Users/Stockify.Modules.Users.IntegrationEvents/Stockify.Modules.Users.IntegrationEvents.csproj", "src/Modules/Users/Stockify.Modules.Users.IntegrationEvents/"]
COPY ["src/Modules/Users/Stockify.Modules.Users.Presentation/Stockify.Modules.Users.Presentation.csproj", "src/Modules/Users/Stockify.Modules.Users.Presentation/"]
COPY ["src/Modules/Risks/Stockify.Modules.Risks.Application/Stockify.Modules.Risks.Application.csproj", "src/Modules/Risks/Stockify.Modules.Risks.Application/"]
COPY ["src/Modules/Risks/Stockify.Modules.Risks.Domain/Stockify.Modules.Risks.Domain.csproj", "src/Modules/Risks/Stockify.Modules.Risks.Domain/"]
COPY ["src/Modules/Risks/Stockify.Modules.Risks.Infrastructure/Stockify.Modules.Risks.Infrastructure.csproj", "src/Modules/Risks/Stockify.Modules.Risks.Infrastructure/"]
COPY ["src/Modules/Risks/Stockify.Modules.Risks.Presentation/Stockify.Modules.Risks.Presentation.csproj", "src/Modules/Risks/Stockify.Modules.Risks.Presentation/"]
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
