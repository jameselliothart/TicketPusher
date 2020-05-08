# Ticket Pusher

## Development Setup

A local PostgreSQL database instance is required for running and testing the application. The `docker` command below can be used to easily set this up.

```sh
docker run --rm --name tp-postgres -e POSTGRES_PASSWORD=docker -d -p 5432:5432 -v $HOME/dockervolumes/TicketPusher:/var/lib/postgresql/data postgres
```

## Docker

TicketPusher is served via two Docker containers: one for the API, one for the Blazor front end. The images can be built from the solution root with the below commands

- API: `docker build -f api.dockerfile -t api .`
- Web: `docker build -f web.dockerfile -t web .`

Running the containers individually:

- API: `docker run --rm -d -p 8080:80 -e CUSTOMCONNSTR_TicketPusherDb="host=172.17.0.1;database=TicketPusherDb;user id=postgres;password=docker;" --name tp-api api`
- Web: `docker run --rm -d -p 8080:80 --name tp-web web`

Running with Docker Compose:

`docker-compose up --build`

## Azure

Pushing containers to ACR manually

```sh
az login
az acr login --name ticketpusherregistry
docker push ticketpusherregistry.azurecr.io/ticketpusherapi
docker push ticketpusherregistry.azurecr.io/ticketpusherweb
```