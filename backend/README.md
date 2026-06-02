# RoadmApp Backend

Backend em C# com ASP.NET Core, EF Core e uma organizacao em camadas inspirada em Clean Architecture.

## Estrutura

- `RoadmApp.Domain`: entidades e regras de dominio sem dependencia de frameworks.
- `RoadmApp.Application`: DTOs, contratos, servicos de caso de uso e abstracoes.
- `RoadmApp.Infrastructure`: EF Core, SQLite, repositorios e hash de senha.
- `RoadmApp.Api`: endpoints HTTP, Swagger, CORS e composicao de dependencias.

## Rodar localmente

```bash
cd backend
dotnet tool restore
dotnet tool run dotnet-ef database update --project src/RoadmApp.Infrastructure/RoadmApp.Infrastructure.csproj --startup-project src/RoadmApp.Api/RoadmApp.Api.csproj
dotnet run --project src/RoadmApp.Api/RoadmApp.Api.csproj
```

Swagger fica disponivel em `/swagger` no ambiente Development.

## Rotas principais

- `POST /api/auth/register`
- `POST /api/auth/login`
- `GET /api/users/{userId}/dashboard`
- `GET|POST /api/users/{userId}/tasks`
- `PUT|DELETE /api/users/{userId}/tasks/{id}`
- `GET|POST /api/users/{userId}/habits`
- `PUT|DELETE /api/users/{userId}/habits/{id}`
- `GET|POST /api/users/{userId}/goals`
- `PUT|DELETE /api/users/{userId}/goals/{id}`
- `GET|POST /api/users/{userId}/notes`
- `PUT|DELETE /api/users/{userId}/notes/{id}`

## Migrations

```bash
dotnet tool run dotnet-ef migrations add NomeDaMigration --project src/RoadmApp.Infrastructure/RoadmApp.Infrastructure.csproj --startup-project src/RoadmApp.Api/RoadmApp.Api.csproj --context RoadmAppDbContext --output-dir Persistence/Migrations
```
