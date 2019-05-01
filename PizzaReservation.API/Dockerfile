FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["PizzaReservation.API/PizzaReservation.API.csproj", "PizzaReservation.API/"]
COPY ["PizzaReservation.Models/PizzaReservation.Models.csproj", "PizzaReservation.Models/"]
RUN dotnet restore "PizzaReservation.API/PizzaReservation.API.csproj"
COPY . .
WORKDIR "/src/PizzaReservation.API"
RUN dotnet build "PizzaReservation.API.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "PizzaReservation.API.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "PizzaReservation.API.dll"]