FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["MicroServicerContainerBindMountConfigFiles.csproj", "./"]
RUN dotnet restore "MicroServicerContainerBindMountConfigFiles.csproj"
COPY . .
RUN dotnet build "MicroServicerContainerBindMountConfigFiles.csproj" -c Release -o /app/build

# Note: Why /p:UseAppHost=false is used in the below command?
# Without the property UseAppHost=false (/p denotes property in Msbuild), 
# this will generate an executable host (called the app host) for the project
# This will generate an .exe file <ProjectName>.exe
# Since Docker containers always run under a known runtime (dotnet command inside the base image),
# the .exe isn’t necessary and is skipped to save space and avoid cross-platform issues.

FROM build AS publish
RUN dotnet publish "MicroServicerContainerBindMountConfigFiles.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MicroServicerContainerBindMountConfigFiles.dll"]

