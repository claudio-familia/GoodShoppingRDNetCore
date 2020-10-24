FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS base
WORKDIR /app
FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src
COPY ["GoodShoppingRD/GoodShoppingRD.csproj", "GoodShoppingRD/"]
RUN dotnet restore "GoodShoppingRD/GoodShoppingRD.csproj"
COPY . .
WORKDIR "/src/GoodShoppingRD"
RUN dotnet build "GoodShoppingRD.csproj" -c Release -o /app/build
FROM build AS publish
RUN dotnet publish "GoodShoppingRD.csproj" -c Release -o /app/publish
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
CMD ASPNETCORE_URLS=http://*:$PORT dotnet GoodShoppingRD.dll