#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["backendtest.API/backendtest.API.csproj", "backendtest.API/"]
RUN dotnet restore "backendtest.API/backendtest.API.csproj"
COPY . .
WORKDIR "/src/backendtest.API"
RUN dotnet build "backendtest.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "backendtest.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "backendtest.API.dll"]