FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /app

COPY . ./
RUN dotnet publish RabbitSample.Banking.API -c Release -o out

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /app
COPY --from=build /app/out .
ENV ASPNETCORE_URLS http://*:5003
ENV ASPNETCORE_ENVIRONMENT docker
ENTRYPOINT ["dotnet", "RabbitSample.Banking.API.dll"]