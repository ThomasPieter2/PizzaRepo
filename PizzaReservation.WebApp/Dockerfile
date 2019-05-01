FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY PizzaReservation.WebApp/PizzaReservation.WebApp.csproj PizzaReservation.WebApp/
COPY PizzaReservation.Models/PizzaReservation.Models.csproj PizzaReservation.Models/
RUN dotnet restore PizzaReservation.WebApp/PizzaReservation.WebApp.csproj
COPY . .
WORKDIR /src/PizzaReservation.WebApp
RUN dotnet build PizzaReservation.WebApp.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish PizzaReservation.WebApp.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "PizzaReservation.WebApp.dll"]
