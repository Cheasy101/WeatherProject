FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
USER $APP_UID
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["WorkerServiceA/WorkerServiceA.csproj", "WorkerServiceA/"]
RUN dotnet restore "WorkerServiceA/WorkerServiceA.csproj"
COPY . .
WORKDIR "/src/WorkerServiceA"
RUN dotnet build "WorkerServiceA.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "WorkerServiceA.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WorkerServiceA.dll"]
