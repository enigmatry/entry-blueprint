# Copilot Instructions

## Project Overview

Enigmatry Entry Blueprint is a **production-ready full-stack starter template** for .NET 9 + Angular 20 applications. It demonstrates enterprise patterns: Vertical Slice Architecture, CQRS via MediatR, DDD-style domain entities, EF Core with SQL Server, Autofac DI, background scheduling, and Angular code generation from C# configuration. The working features (Users, Products) serve as reference implementations to copy when adding new features.

## Backend

```sh
dotnet build
dotnet run --project Enigmatry.Entry.Blueprint.Api
dotnet run --project Enigmatry.Entry.Blueprint.AppHost # runs all services together via .NET Aspire
dotnet test
dotnet test --filter "FullyQualifiedName~UsersControllerFixture.TestGetAll"  # single test
dotnet test --filter "Category=integration"                                   # integration tests only
```

**Database migrations (from `scripts/`):**
```sh
./add-migration.ps1     # prompts for migration name
./update-databases.ps1
# or directly:
dotnet-ef migrations add <Name> --project ../Enigmatry.Entry.Blueprint.Data.Migrations --startup-project ../Enigmatry.Entry.Blueprint.Data.Migrations --context AppDbContext
```

## Frontend

All commands run from `enigmatry-entry-blueprint-app/`.

```sh
npm install
npm start                                              # dev server at http://localhost:4200
npm run build
npm test                                               # Jest, no coverage
npm run test:ci                                        # Jest with coverage (CI)
npm test -- --testNamePattern="my test name"           # single test
npm run lint                                           # ESLint + Stylelint, auto-fixes
npx playwright test                                    # E2E tests (run from enigmatry-entry-blueprint-tests/)
```

**Code generation (build `CodeGeneration.Setup` first):**
```sh
npm run nswag          # regenerate TypeScript API client from Swagger (requires API running)
npm run codegen:run    # regenerate Angular form/list components
```

---

## Architecture

This is a full-stack **blueprint template** using **Vertical Slice Architecture + CQRS** with .NET 9 and Angular 20.

**Backend projects:**
| Project | Role |
|---------|------|
| `Api` | ASP.NET Core – controllers + query handlers, organized by feature |
| `Domain` | Domain entities, commands, FluentValidation validators, domain events |
| `Core` | Base entity types and shared interfaces |
| `Infrastructure` | EF Core `AppDbContext`, Autofac DI modules, EF configurations, identity |
| `ApplicationServices` | Application-level cross-cutting services (e.g. auditing) |
| `Scheduler` | Background jobs via Quartz.NET (`EntryJob<TRequest>` base) |
| `Data.Migrations` | EF Core migrations and `AppDbContext` startup project |
| `CodeGeneration.Setup` | C# configuration that drives Angular component code generation |
| `ServiceDefaults` | Shared .NET Aspire configuration (OpenTelemetry, health checks) |
| `AppHost` | .NET Aspire orchestration host |

**Frontend:** Angular 20 standalone components in `enigmatry-entry-blueprint-app/src/app/`, structured as `core/`, `features/`, `shared/`, `api/`.

**Database:** SQL Server via EF Core 9 (code-first, repository pattern). Tests use Testcontainers for isolated SQL Server containers.

---

## Key Conventions

- **Commands** (writes) live in `Domain/{Feature}/Commands/` as a static class with a nested `Command : IRequest<T>`, a nested `Validator : AbstractValidator<Command>`, and a separate `*CommandHandler.cs` in the same folder.
- **Queries** (reads) live in `Api/Features/{Feature}/Get*.cs` as a static class with a nested `Request`, a nested `Response` (containing the DTO and an inner `MappingProfile : Profile`), and a `RequestHandler`.
- **Controllers** only dispatch via MediatR — no business logic. Use primary-constructor `IMediator` injection, return `response.ToActionResult()`, and decorate every endpoint with `[UserHasPermission(PermissionId.X)]`.
- **Domain entities** inherit from `EntityWithCreatedUpdated`, have `private` setters, expose a static `Create(Command)` factory and an `Update(Command)` method, and raise domain events via `AddDomainEvent()`.
- **Smart Enums** — use `Ardalis.SmartEnum` instead of C# `enum` for all status/type values; ID types live in `Domain/{Feature}/{Name}Id.cs`; EF, JSON, and Swagger converters are wired automatically via `Enigmatry.Entry.SmartEnums.*`.
- **DI** is configured via Autofac modules in `Infrastructure/Autofac/Modules/`; `ServiceModule` auto-registers any class whose name ends with `Service`.
- **EF configurations** live in `Infrastructure/Data/Configurations/` as `IEntityTypeConfiguration<T>` classes; use `EntityWithEnumIdConfiguration<T>` for entities with SmartEnum primary keys.
- **Central Package Management** — all NuGet versions are declared in `Directory.Packages.props` only; never add `Version=` to a `<PackageReference>` in a `.csproj`.
- **Code quality** — `TreatWarningsAsErrors`, `EnforceCodeStyleInBuild`, and `Nullable` are all enabled; use `[PublicAPI]` on DTOs/request/response types and `[UsedImplicitly]` on handlers and validators.
- **Integration tests** inherit from `IntegrationFixtureBase` (WebApplicationFactory + Testcontainers SQL Server), use `Client.GetAsync<T>()` / `Client.PostAsync<>()` helpers, assert with `Verify()` snapshot files (`.verified.txt`), and are marked `[Category("integration")]`.
- **Test builders** in `Domain.Tests/{Feature}/{Entity}Builder.cs` use fluent `With*()` methods and an implicit conversion to the entity type for seeding test data.
- **Angular code generation** — add `IListComponentConfiguration<T>` / `IFormComponentConfiguration<T>` to `CodeGeneration.Setup/Features/{Feature}/`, rebuild that project, then run `npm run codegen:run`; generated files in `src/app/features/` must not be edited manually.

## Git Conventions

- **Branch naming**: `BP-{id}-{short-description}`
- **Commit messages**: `https://enigmatry.atlassian.net/browse/BP-{id} BP-{id} - {Description}`
- **PR titles**: `BP-{id} - {Description}`

## CI/CD

Azure DevOps Pipelines (`Pipelines/azure-pipelines.yml`). Stages: Build → Test → Acceptance → Production. Deployment uses publish profiles in `Deployment/`.
- **Organization**: `enigmatry`
- **Project**: `Enigmatry Blueprint Template` (ID: `b3d80552-b808-495c-9332-82888c12c1bf`)
