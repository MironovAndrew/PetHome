# См. статью по ссылке https://aka.ms/customizecontainer, чтобы узнать как настроить контейнер отладки и как Visual Studio использует этот Dockerfile для создания образов для ускорения отладки.

# Этот этап используется при запуске из VS в быстром режиме (по умолчанию для конфигурации отладки)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


# Этот этап используется для сборки проекта службы
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/PetHome.API/PetHome.API.csproj", "src/PetHome.API/"]
COPY ["src/PetHome.Domain/PetHome.Domain.csproj", "src/PetHome.Domain/"]
COPY ["src/PetHome.Application/PetHome.Application.csproj", "src/PetHome.Application/"]
COPY ["src/PetHome.Infrastructure/PetHome.Infrastructure.csproj", "src/PetHome.Infrastructure/"]
RUN dotnet restore "./src/PetHome.API/PetHome.API.csproj"
COPY . .
WORKDIR "/src/src/PetHome.API"
RUN dotnet build "./PetHome.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Этот этап используется для публикации проекта службы, который будет скопирован на последний этап
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./PetHome.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Этот этап используется в рабочей среде или при запуске из VS в обычном режиме (по умолчанию, когда конфигурация отладки не используется)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PetHome.API.dll"]