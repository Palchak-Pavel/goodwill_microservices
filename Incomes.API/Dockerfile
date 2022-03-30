FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /
COPY ["/Incomes.API/Incomes.API.csproj", "Incomes.API/"]
RUN dotnet restore "Incomes.API/Incomes.API.csproj"
COPY . .
WORKDIR "/Incomes.API"
RUN dotnet build "Incomes.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Incomes.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Incomes.API.dll"]
