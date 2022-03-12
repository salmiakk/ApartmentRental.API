FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["ApartmentRentalAPI/ApartmentRentalAPI.csproj", "ApartmentRentalAPI/"]
RUN dotnet restore "ApartmentRentalAPI/ApartmentRentalAPI.csproj"
COPY . .
WORKDIR "/src/ApartmentRentalAPI"
RUN dotnet build "ApartmentRentalAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ApartmentRentalAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ApartmentRentalAPI.dll"]
