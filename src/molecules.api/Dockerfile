#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/molecules.api/molecules.api.csproj", "src/molecules.api/"]
COPY ["src/infrastructure/molecules.infrastructure.data/molecules.infrastructure.data.csproj", "src/infrastructure/molecules.infrastructure.data/"]
COPY ["src/infrastructure/molecules.infrastructure.data.interfaces/molecule.infrastructure.data.interfaces/molecule.infrastructure.data.interfaces.csproj", "src/infrastructure/molecules.infrastructure.data.interfaces/molecule.infrastructure.data.interfaces/"]
COPY ["src/molecules.core/molecules.core.csproj", "src/molecules.core/"]
COPY ["src/shared/molecules.shared/molecules.shared.csproj", "src/shared/molecules.shared/"]
RUN dotnet restore "src/molecules.api/molecules.api.csproj"
COPY . .
WORKDIR "/src/src/molecules.api"
RUN dotnet build "molecules.api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "molecules.api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "molecules.api.dll"]