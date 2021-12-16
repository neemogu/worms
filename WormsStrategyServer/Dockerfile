FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["WormsStrategyServer/WormsStrategyServer.csproj", "WormsStrategyServer/"]
RUN dotnet restore "WormsStrategyServer/WormsStrategyServer.csproj"
COPY . .
WORKDIR "/src/WormsStrategyServer"
RUN dotnet build "WormsStrategyServer.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WormsStrategyServer.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WormsStrategyServer.dll"]
