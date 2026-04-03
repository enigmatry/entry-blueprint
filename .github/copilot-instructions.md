# Copilot Instructions

## Granular Stack Instructions

The `.github/instructions/` folder contains per-technology instruction files (`*.instructions.md`).
**Always check and apply the relevant file(s) from that folder** when the user's request touches a specific tech stack:

| Title | Description |
| ----- | ----------- |
| [Accessibility instructions](instructions/a11y.instructions.md) | Guidance for creating more accessible code |
| [ASP.NET REST API Development](instructions/aspnet-rest-apis.instructions.md) | Guidelines for building REST APIs with ASP.NET |
| [Azure DevOps Pipeline YAML Best Practices](instructions/azure-devops-pipelines.instructions.md) | Best practices for Azure DevOps Pipeline YAML files |
| [Generic Code Review Instructions](instructions/code-review-generic.instructions.md) | Generic code review instructions customized for this project |
| [C# Development](instructions/csharp.instructions.md) | Guidelines for building C# applications |
| [DDD Systems & .NET Guidelines](instructions/dotnet-architecture-good-practices.instructions.md) | DDD and .NET architecture guidelines |
| [TypeScript Development](instructions/typescript-5-es2022.instructions.md) | Guidelines for TypeScript Development targeting TypeScript 5.x and ES2022 output |

These files take precedence over the general conventions described below for their respective areas.
New files may be added to `.github/instructions/` at any time — always scan the folder for relevant matches before starting work.

## Project Overview

Enigmatry Entry Blueprint is a **production-ready full-stack starter template** for .NET 9 + Angular 20 applications. It demonstrates enterprise patterns: Vertical Slice Architecture, CQRS via MediatR, DDD-style domain entities, EF Core with SQL Server, Autofac DI, background scheduling, and Angular code generation from C# configuration. The working features (Users, Products) serve as reference implementations to copy when adding new features.

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

## Backend Scripts

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

## Frontend Scripts

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

## CI/CD

Azure DevOps Pipelines (`Pipelines/azure-pipelines.yml`). Stages: Build → Test → Acceptance → Production. Deployment uses publish profiles in `Deployment/`.
- **Organization**: `enigmatry`
- **Project**: `Enigmatry Blueprint Template` (ID: `b3d80552-b808-495c-9332-82888c12c1bf`)

## Git Branch Workflow

When the user asks to create a new branch and provides a Jira ticket ID:

1. **Fetch the Jira ticket** by its ID to retrieve its **summary/title** and current status.
2. **Assign the ticket to yourself**: call `atlassian-atlassianUserInfo` to get your account ID, then `atlassian-editJiraIssue` to set `assignee.accountId`.
3. **Transition to In Progress** (if not already): call `atlassian-getTransitionsForJiraIssue` to find the "In Progress" transition ID, then `atlassian-transitionJiraIssue` to apply it.
4. Convert the title to **kebab-case** (lowercase, spaces → hyphens, strip special characters).
5. Create and switch to the branch: `BP-<TICKET-ID>-<kebab-case-title>`
   - Example: ticket `BP-42` with title "Add product search endpoint" → `BP-42-add-product-search-endpoint`
6. Run `git checkout -b <branch-name>` (or `git switch -c <branch-name>`) to create and immediately switch to it.
7. Confirm the active branch, assignment, and ticket status to the user.

**Rules:**
- Ticket ID is uppercase as-is (e.g. `BP-42`, not `bp-42`).
- Kebab-case segment: lowercase only, hyphens instead of spaces/underscores, remove any characters that are not alphanumeric or hyphens.
- Always assign the ticket and transition it before creating the branch.
- Skip the transition step (but still assign) if the ticket is already "In Progress".

## Pull Request Workflow

When the user asks to create a PR (pull request):

1. **Fetch the Jira ticket** (if not already known) to get the title.
   - Jira cloud ID: `enigmatry.atlassian.net`
2. **Get current branch name**: `git branch --show-current`
3. **Create the PR** via GitHub using:
   - `sourceRefName`: current branch (`refs/heads/<branch-name>`)
   - `targetRefName`: `refs/heads/master`
   - `title`: `BP-<TICKET-ID> - <Jira ticket title>`
   - `description`: `https://enigmatry.atlassian.net/browse/BP-<TICKET-ID>`
   - `isDraft`: false (unless user requests a draft)
4. Confirm the PR URL to the user.

**Known identifiers:**
- GitHub org/repo: `enigmatry/entry-blueprint`
- Default branch: `master`
- Jira project: `BP`

**Rules:**
- PR title format: `BP-<TICKET-ID> - <ticket title>` (uppercase ticket ID, space-dash-space separator).
- PR description must be the direct Jira link.
- Always use Squash merge strategy.
- Always enable Auto-complete — verify `autoCompleteSetBy` is non-null in the response.
- Never create the PR without the Jira ticket title (fetch it if not in context).
