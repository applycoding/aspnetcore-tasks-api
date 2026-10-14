# ASP.NET Core Tasks API

A small C# Web API on .NET 8 with controllers, validation, dependency injection and xUnit tests.

## Endpoints
- `GET /api/tasks` lists tasks
- `GET /api/tasks/{id}` fetches one task (404 if missing)
- `POST /api/tasks` creates a task (`{"title": "..."}`, validated)
- `POST /api/tasks/{id}/complete` marks a task done
- `DELETE /api/tasks/{id}` deletes a task
- `GET /health` health check
- Swagger UI at `/swagger` in Development

## Run
```bash
dotnet run --project src/TasksApi
```

## Test
```bash
dotnet test
```
Six xUnit tests: unit tests for the store and integration tests via `WebApplicationFactory`.
