FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 5000

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build_back
WORKDIR /src
COPY ["ChocolateBackEnd/ChocolateBackEnd.csproj", "ChocolateBackEnd/"]
COPY ["ChocolateData/ChocolateData.csproj", "ChocolateData/"]
COPY ["ChocolateDomain/ChocolateDomain.csproj", "ChocolateDomain/"]
COPY ["ApiContracts/ApiContracts.csproj", "ApiContracts/"]
COPY ["Services/Services.csproj", "Services/"]

RUN dotnet restore "ChocolateBackEnd/ChocolateBackEnd.csproj"
COPY . .
WORKDIR "/src/ChocolateBackEnd"
RUN dotnet build "ChocolateBackEnd.csproj" -c Release -o /app/build

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build_ui
WORKDIR /src
COPY ["ChocolateUI/ChocolateUI.csproj", "ChocolateUI/"]
RUN dotnet restore "ChocolateUI/ChocolateUI.csproj"
COPY . .
WORKDIR "/src/ChocolateUI"
RUN dotnet build "ChocolateUI.csproj" -c Release -o /app/build

FROM build_back AS publishBack
WORKDIR "/src/ChocolateBackEnd"
RUN dotnet publish "ChocolateBackEnd.csproj" -c Release -o /app/publish

FROM build_ui AS publishUI
WORKDIR "/src/ChocolateUI"
RUN dotnet publish "ChocolateUI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publishBack /app/publish .
COPY --from=publishUI /app/publish .
ENTRYPOINT ["dotnet", "ChocolateBackEnd.dll"]
