FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["InitialFilling/InitialFilling.csproj", "InitialFilling/"]
COPY ["ChocolateData/ChocolateData.csproj", "ChocolateData/"]
COPY ["ChocolateDomain/ChocolateDomain.csproj", "ChocolateDomain/"]
COPY ["ApiContracts/ApiContracts.csproj", "ApiContracts/"]
RUN dotnet restore "InitialFilling/InitialFilling.csproj"
COPY . .
WORKDIR "/src/InitialFilling"
RUN dotnet build "InitialFilling.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "InitialFilling.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "InitialFilling.dll"]
