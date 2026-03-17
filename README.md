# Cart Service

Shopping-cart microservice built with ASP.NET Core (.NET 10), SQL Server, and Redis.

## Project Structure

```
cart-service/
├── Cart.API/
│   ├── Contracts/    # Request/response DTOs
│   ├── Data/         # DbContext and migrations
│   ├── Helpers/      # Utility / extension classes
│   ├── Models/       # Domain entities
│   ├── Services/     # Business logic
│   └── Sql/          # Raw SQL scripts
├── db/               # Database seed / migration scripts
├── docker-compose.yml
├── Dockerfile
└── README.md
```

## Getting Started

### Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- [Docker](https://www.docker.com/get-started)

### Run with Docker

```bash
docker compose up --build -d
```

### Initialize the database

Once SQL Server is healthy, seed the schema and demo data:

```bash
docker cp ./db/init.sql cart-sqlserver:/init.sql
docker exec -it cart-sqlserver /opt/mssql-tools18/bin/sqlcmd \
  -S localhost -U sa -P 'YourStrong!Passw0rd' -C -i /init.sql
```

The API will be available at `http://localhost:5000`.

### Run locally

```bash
cd Cart.API
dotnet run
```

## Configuration

| Variable | Default | Description |
|---|---|---|
| `ConnectionStrings__SqlServer` | *(see docker-compose)* | SQL Server connection string |
| `ConnectionStrings__Redis` | `redis:6379` | Redis connection string |
| `ASPNETCORE_ENVIRONMENT` | `Development` | Runtime environment |
