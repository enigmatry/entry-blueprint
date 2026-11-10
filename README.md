# Entry blueprint

The Entry blueprint is a complete, ready-to-run .NET 10 and Angular 21 application built on the [Entry building blocks](https://github.com/enigmatry/entry-building-blocks), together with the `dotnet new` template that creates a new solution from it. It is the architecture Enigmatry's tech leads kept rebuilding on every project, built once so that a new system starts from a working whole rather than from an empty solution.

What a solution created from the blueprint contains:

* an API with vertical-slice features (queries, commands, controllers per feature) and CQRS via MediatR;
* domain entities in the DDD style, FluentValidation validators and domain events;
* EF Core with SQL Server, code first, with migrations and a repository pattern;
* Autofac dependency injection, a Quartz.NET scheduler for background jobs, and shared .NET Aspire service defaults with an AppHost that runs everything together;
* an Angular application with components generated from C# configuration by [Entry code generation](https://github.com/enigmatry/entry-code-generation);
* integration tests on Testcontainers SQL Server, and Playwright end-to-end tests;
* the Users and Products features as reference implementations to copy when adding your own.

The blueprint is one of the three parts of Entry, the open-source foundation described at https://www.enigmatry.com/en/entry/.

## Create a solution from the template

```powershell
dotnet new install Enigmatry.Entry.Blueprint.Template
dotnet new blueprint --name Customer.Project --projectName Customer.Project --appProjectName customer-project --friendlyName "Customer Project" --allow-scripts yes
```

`dotnet new blueprint -h` lists every option. [templating.md](templating.md) describes the options, installing a specific version, installing the template from this source tree, and building the template package yourself.

## Run it

From the solution root of a created solution, or of this repository:

```powershell
dotnet build
dotnet run --project Enigmatry.Entry.Blueprint.AppHost   # everything together, via .NET Aspire
dotnet test                                               # backend tests
dotnet test --filter "Category=integration"               # integration tests only (Docker required)
```

The Angular application lives in `enigmatry-entry-blueprint-app/`, the Playwright tests in `enigmatry-entry-blueprint-tests/`; each has its own `package.json` with the scripts to run it. The `scripts/` folder holds helpers for adding EF Core migrations and updating the databases.

## Repository layout

| Project | Role |
|---|---|
| `Enigmatry.Entry.Blueprint.Api` | ASP.NET Core API: controllers and query handlers, organised by feature |
| `Enigmatry.Entry.Blueprint.Domain` | Domain entities, commands, validators, domain events |
| `Enigmatry.Entry.Blueprint.Core` | Base entity types and shared interfaces |
| `Enigmatry.Entry.Blueprint.Infrastructure` | EF Core `AppDbContext`, Autofac modules, EF configurations, identity |
| `Enigmatry.Entry.Blueprint.ApplicationServices` | Cross-cutting application services such as auditing |
| `Enigmatry.Entry.Blueprint.Scheduler` | Background jobs on Quartz.NET |
| `Enigmatry.Entry.Blueprint.Data.Migrations` | EF Core migrations; start-up project for the `AppDbContext` |
| `Enigmatry.Entry.Blueprint.CodeGeneration.Setup` | C# configuration that drives the Angular code generation |
| `Enigmatry.Entry.Blueprint.ServiceDefaults` | Shared .NET Aspire configuration: OpenTelemetry, health checks |
| `Enigmatry.Entry.Blueprint.AppHost` | .NET Aspire orchestration host |
| `enigmatry-entry-blueprint-app/` | The Angular application |
| `enigmatry-entry-blueprint-tests/` | Playwright end-to-end tests |
| `Enigmatry.Entry.Blueprint.Template.csproj` | Packs this repository as the `dotnet new` template |

Further notes live in [Documentation/](Documentation/). The `CLAUDE.md` file describes the conventions in more depth for engineers and coding agents alike.

## Licence

Apache-2.0. See [LICENSE.md](LICENSE.md).

## About Enigmatry

Entry is the open-source foundation Enigmatry builds client systems on. Enigmatry is a software company from Rotterdam, founded in 2006, with teams in Belgrade and Novi Sad.

* How we use Entry for clients: https://www.enigmatry.com/en/entry/
* How we build, with agents under checks: https://www.enigmatry.com/en/how-we-build/

Apache-2.0 © Enigmatry
