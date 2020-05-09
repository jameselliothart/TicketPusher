FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-env
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY TicketPusher.Domain/*.csproj ./TicketPusher.Domain/
RUN dotnet restore TicketPusher.Domain

COPY TicketPusher.Domain/ ./TicketPusher.Domain/
RUN dotnet build TicketPusher.Domain

# Copy csproj and restore as distinct layers
COPY TicketPusher.API/*.csproj ./TicketPusher.API/
RUN dotnet restore TicketPusher.API

# Copy everything else and build
COPY TicketPusher.API/ ./TicketPusher.API/
RUN dotnet publish TicketPusher.API -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "TicketPusher.API.dll"]