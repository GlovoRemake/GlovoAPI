FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /source

COPY ["GlovoAPI/GlovoAPI.csproj", "GlovoAPI/"]
RUN dotnet restore "GlovoAPI/GlovoAPI.csproj"

COPY . .
WORKDIR /source/GlovoAPI
RUN dotnet publish -c Release -o /app

FROM mcr.microsoft.com/dotnet/aspnet:10.0
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["dotnet", "GlovoAPI.dll"]
