FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["Searcher/Searcher.csproj", "Searcher/"]
COPY ["Searcher.BLL/Searcher.BLL.csproj", "Searcher.BLL/"]
COPY ["Searcher.DAL/Searcher.DAL.csproj", "Searcher.DAL/"]
RUN dotnet restore "Searcher/Searcher.csproj"
COPY . .
WORKDIR "/src/Searcher"
RUN dotnet build "Searcher.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Searcher.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Searcher.dll"]