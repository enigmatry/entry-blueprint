# Entry Blueprint

## Project Overview

Entry Blueprint is a .NET 10 + Angular 21 application built on Enigmatry Entry's `blueprint` template. It uses Vertical Slice Architecture, CQRS via MediatR, DDD-style domain entities, EF Core with SQL Server, Autofac DI, background scheduling, and Angular code generation from C# configuration. The Users and Products features are reference implementations to copy when adding new features.

Coding standards, testing, code review and the Jira/Azure DevOps workflows come from the `entry-blueprint` Claude Code plugin (enabled in `.claude/settings.json`). This file holds what is specific to this repository. The plugin's branch and pull-request skills read the project's Jira and Azure DevOps identifiers from a `## Known identifiers` section (Jira project key, Atlassian cloud ID, Azure DevOps org/repo/project, default branch).

## Architecture

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

**Frontend:** Angular 21 standalone components in `enigmatry-entry-blueprint-app/src/app/`, structured as `core/`, `features/`, `shared/`, `api/`.

**Database:** SQL Server via EF Core 10 (code-first, repository pattern). Tests use Testcontainers for isolated SQL Server containers.

## Key Conventions

- **Feature patterns** (queries, commands, controllers, domain entities, EF configurations, Autofac modules) follow the `entry-blueprint:aspnet-rest-apis` skill; the Products and Users features are the reference implementations.
- **Enums** — `ProductType`, `ProductStatus` and `PermissionId` are plain C# enums. `UserStatus` is the SmartEnum example: an `EntityWithEnumId` entity with the `Ardalis.SmartEnum` id `UserStatusId`, configured with `EntityWithEnumIdConfiguration<TEntity, TId>` (see `UserStatusConfiguration`); EF, JSON and Swagger converters come from `Enigmatry.Entry.SmartEnums.*`. Existing entities keep their identifier types.
- **Central Package Management** — all NuGet versions are declared in `Directory.Packages.props` only; never add `Version=` to a `<PackageReference>` in a `.csproj`.
- **Code quality** — `TreatWarningsAsErrors`, `EnforceCodeStyleInBuild`, and `Nullable` are all enabled.
- **Backend tests** use NUnit 5 (the version in `Directory.Packages.props`), Shouldly, FakeItEasy and Verify. Integration tests inherit from `IntegrationFixtureBase` (WebApplicationFactory + Testcontainers SQL Server), use the `Client.GetAsync<T>()` / `Client.PostAsync<>()` helpers, assert with `Verify()` snapshot files (`.verified.txt`), and are marked `[Category("integration")]`. Test builders in `Domain.Tests/{Feature}/{Entity}Builder.cs` use fluent `With*()` methods and an implicit conversion to the entity.
- **Frontend unit tests** run on Jest (`jest-preset-angular`, config in `jest.config.json`; shared setup and mocks in `src/testing/`). New specs extend this Jest suite; moving the suite to Vitest is a separate, dedicated change.
- **Angular code generation** — add `IListComponentConfiguration<T>` / `IFormComponentConfiguration<T>` to `CodeGeneration.Setup/Features/{Feature}/`, rebuild that project, then run `npm run codegen:run`; generated files in `src/app/features/` must not be edited manually.
- **Content Security Policy** — the CSP header is set by `CspMiddleware` (Infrastructure/Api/Csp), not by web.config. The SPA policy lives in `SpaContentSecurityPolicyValue` (appsettings, Web Deploy token `__spaContentSecurityPolicyValue__`) and must contain `**PLACEHOLDER_NONCE_SERVER**`; the placeholder is replaced per request in the header and in `wwwroot/index.html` (`ngCspNonce` on `<app-root>`), which is served only by the nonce-injecting SPA fallback. Never add `'unsafe-inline'` back.

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

Azure DevOps Pipelines (`Pipelines/azure-pipelines.yml`): pushes to `master`, `main` and `release/*` run the pipeline, and pull requests are validated by a PR build. Deploy branches (`master`, `main`, `release/*`) are packaged and deployed to the test environment; other builds only build and test. To deploy a feature branch to test, set `manualPublishOverride: true` on that branch (never on a deploy branch): its PR build, or a manual pipeline run for the branch, then packages and deploys it. Deployment uses publish profiles in `Deployment/`.
