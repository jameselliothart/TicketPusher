# Ticket Pusher

## Development Setup

```sh
docker run --rm --name tp-postgres -e POSTGRES_PASSWORD=docker -d -p 5432:5432 -v $HOME/dockervolumes/TicketPusher:/var/lib/postgresql/data postgres
```
