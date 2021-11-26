#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 5000

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["GameHavenMain/GameHavenMain.csproj", "GameHavenMain/"]
COPY ["GameHavenMain.Data/GameHavenMain.Data.csproj", "GameHavenMain.Data/"]
RUN dotnet restore "GameHavenMain/GameHavenMain.csproj"
COPY . .
RUN dotnet publish "GameHavenMain.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=build  /app/publish .
ENTRYPOINT ["dotnet", "GameHavenMain.dll"]
