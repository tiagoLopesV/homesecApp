FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5241

ENV ASPNETCORE_URLS=http://+:5241

# USER app
FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG configuration=Release
WORKDIR /src
COPY ["PrimacyServer/PrimacyServer.csproj", "PrimacyServer/"]
RUN dotnet restore "PrimacyServer/PrimacyServer.csproj"
COPY . .
WORKDIR "/src/PrimacyServer"
RUN dotnet build "PrimacyServer.csproj" -c $configuration -o /app/build

FROM build AS publish
ARG configuration=Release
RUN dotnet publish "PrimacyServer.csproj" -c $configuration -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PrimacyServer.dll"]
