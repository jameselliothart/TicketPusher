# Ticket Pusher

## Development Setup

A local PostgreSQL database instance is required for running and testing the application. The `docker` command below can be used to easily set this up.

```sh
docker run --rm --name tp-postgres -e POSTGRES_PASSWORD=docker -d -p 5432:5432 -v $HOME/dockervolumes/TicketPusher:/var/lib/postgresql/data postgres
```
