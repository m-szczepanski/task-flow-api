# TaskFlow API

Task and project management system — REST API built according to the principles of Clean Architecture, CQRS, and Domain-Driven Design.

## Project Overview

TaskFlow API is a backend system for managing projects and tasks, inspired by simplified project management tools like Jira. This project is primarily educational, aiming to demonstrate the practical application of modern architectural patterns within the .NET ecosystem.

### Key Learning Objectives:
* **Clean Architecture** — strict separation of Domain, Application, Infrastructure, and API layers.
* **CQRS with MediatR** — implementation of Commands, Queries, and Handlers to separate read and write operations.
* **FluentValidation** — validating requests before they reach the logic handlers.
* **Entity Framework Core** — Code First approach, migrations, and complex entity relationships.
* **Repository Pattern and Unit of Work** — providing an abstraction over the data access layer.
* **Global Exception Handling** — centralized middleware for consistent error processing.
* **Problem Details (RFC 7807)** — standardized HTTP error responses.

## Tech Stack

| Layer | Technology |
| :--- | :--- |
| Framework | .NET 10, ASP.NET Core Web API |
| ORM | Entity Framework Core 10 |
| Database | PostgreSQL |
| CQRS | MediatR 12 |
| Validation | FluentValidation |
| Mapping | Mapster or AutoMapper |
| Documentation | Scalar / Swagger (Swashbuckle) |
| Testing | xUnit, Moq, FluentAssertions |

## Project Structure

```text
TaskFlow/
├── src/
│   ├── TaskFlow.Domain/          # Entities, value objects, repository interfaces
│   ├── TaskFlow.Application/     # Use cases, CQRS, DTOs, validation logic
│   ├── TaskFlow.Infrastructure/  # EF Core, repository implementations, migrations
│   └── TaskFlow.API/             # Controllers, middleware, DI configuration
├── tests/
│   ├── TaskFlow.UnitTests/       # Business logic and domain tests
│   └── TaskFlow.IntegrationTests/ # API and database integration tests
├── docs/
│   └── technical-spec.md
├── .gitignore
├── docker-compose.yml
├── TaskFlow.sln
└── README.md
```

## Getting Started

### Prerequisites
* .NET 10 SDK
* Docker & Docker Compose (for the database)

### Setup Instructions

1.  **Clone the repository**
    ```bash
    git clone https://github.com/your-username/taskflow-api.git
    cd taskflow-api
    ```
2.  **Start the database**
    ```bash
    docker-compose up -d
    ```
3.  **Apply database migrations**
    ```bash
    dotnet ef database update --project src/TaskFlow.Infrastructure --startup-project src/TaskFlow.API
    ```
4.  **Run the application**
    ```bash
    dotnet run --project src/TaskFlow.API
    ```

The API will be available at `https://localhost:7001` and the Swagger documentation can be accessed at `https://localhost:7001/swagger`.

## Main Functionalities

### Projects

* `GET    /api/projects`          — List all projects
* `GET    /api/projects/{id}`     — Get project details
* `POST   /api/projects`          — Create a new project
* `PUT    /api/projects/{id}`     — Update a project
* `DELETE /api/projects/{id}`     — Remove a project

### Tasks

* `GET    /api/projects/{id}/tasks` — List tasks within a project
* `GET    /api/tasks/{id}`           — Get task details
* `POST   /api/tasks`                — Create a new task
* `PUT    /api/tasks/{id}`           — Update a task
* `PATCH  /api/tasks/{id}/status`    — Update task status
* `DELETE /api/tasks/{id}`           — Remove a task

### Authentication

* `POST   /api/auth/register` — User registration
* `POST   /api/auth/login`    — User login (JWT)

## Architecture

The project follows the **Dependency Rule**: dependencies point inwards. 
* **API Layer** handles HTTP requests and delegates work to the Application layer.
* **Application Layer** contains orchestration logic, CQRS handlers, and validation.
* **Domain Layer** contains the core business logic, entities, and interfaces.
* **Infrastructure Layer** handles database persistence and external service implementations.

## Testing

Run all tests using the following commands:

```bash
# Run all tests
dotnet test

# Run unit tests only
dotnet test tests/TaskFlow.UnitTests

# Run integration tests only
dotnet test tests/TaskFlow.IntegrationTests
```
