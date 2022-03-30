FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["IncomePayments.API/IncomePayments.API.csproj", "IncomePayments.API/"]
RUN dotnet restore "IncomePayments.API/IncomePayments.API.csproj"
COPY . .
WORKDIR "/src/IncomePayments.API"
RUN dotnet build "IncomePayments.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "IncomePayments.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "IncomePayments.API.dll"]
