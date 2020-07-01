FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-env
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY TicketPusher.Domain/*.csproj ./TicketPusher.Domain/
RUN dotnet restore TicketPusher.Domain

COPY TicketPusher.Domain/ ./TicketPusher.Domain/
RUN dotnet build TicketPusher.Domain

# Copy csproj and restore as distinct layers
COPY TicketPusher.DataTransfer/*.csproj ./TicketPusher.DataTransfer/
RUN dotnet restore TicketPusher.DataTransfer

COPY TicketPusher.DataTransfer/ ./TicketPusher.DataTransfer/
RUN dotnet build TicketPusher.DataTransfer

# Copy csproj and restore as distinct layers
COPY TicketPusher.Server/*.csproj ./TicketPusher.Server/
RUN dotnet restore TicketPusher.Server

# Copy everything else and build
COPY TicketPusher.Server/ ./TicketPusher.Server/
RUN dotnet publish TicketPusher.Server -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "TicketPusher.Server.dll"]