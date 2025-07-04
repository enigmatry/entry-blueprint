# Copilot Instructions for Enigmatry Entry Blueprint

## Project Overview
This is an enterprise-grade .NET solution template following Clean Architecture principles with a vertical slice architecture approach. The solution uses .NET 9.0, Entity Framework Core, MediatR for CQRS, and Angular for the frontend.

## Architecture & Patterns

### Clean Architecture Structure
- **Domain Layer**: Contains entities, value objects, domain events, and business rules
- **Application Services**: Contains use cases, command/query handlers, and application logic  
- **Infrastructure**: Contains data access, external services, and cross-cutting concerns
- **API Layer**: Contains controllers, endpoints, and presentation logic
- **Core**: Contains shared abstractions and base classes

### Key Patterns Used
- **CQRS with MediatR**: Commands and queries are separated with dedicated handlers
- **Repository Pattern**: Generic repositories with Entity Framework implementation
- **Unit of Work**: Transaction management with `IUnitOfWork`
- **Domain Events**: Domain-driven design with event publishing
- **Vertical Slice Architecture**: Features organized by business capability
- **Specification Pattern**: For complex queries and business rules

## Coding Standards

### C# Conventions
- **Nullable Reference Types**: Enabled project-wide
- **Implicit Usings**: Enabled for cleaner code
- **File-scoped namespaces**: Use file-scoped namespace declarations
- **Expression-bodied members**: Prefer for simple properties and methods
- **var usage**: Use `var` for built-in types and when type is apparent
- **String handling**: Use `String.Empty` instead of `""` for empty strings
- **Max line length**: 140 characters
- **Indentation**: 4 spaces for C#, 2 spaces for XML/JSON

### Entity Framework Conventions
- **DbContext**: `AppDbContext` with configuration assembly pattern
- **Configurations**: Separate configuration classes implementing `IEntityTypeConfiguration<T>`
- **Migrations**: Separate assembly `Enigmatry.Entry.Blueprint.Data.Migrations`
- **Interceptors**: Use for auditing (`PopulateCreatedUpdatedInterceptor`) and domain events (`PublishDomainEventsInterceptor`)
- **Smart Enums**: Use Ardalis SmartEnum for type-safe enumerations with Entity Framework integration

### Domain Layer Patterns
- **Entities**: Inherit from `EntityWithGuidId` or `EntityWithCreatedUpdated`
- **Aggregates**: Use private setters and static factory methods
- **Domain Events**: Inherit from `AuditableDomainEvent` and use `AddDomainEvent()`
- **Value Objects**: Implement proper equality and immutability

### Application Layer Patterns
- **Commands**: Use static classes with nested `Command`, `Result`, `Validator`, and `Handler` classes
- **Queries**: Use static classes with nested `Request`, `Response`, `MappingProfile`, and `RequestHandler` classes
- **Validation**: Use FluentValidation with `AbstractValidator<T>`
- **Authorization**: Use `IQuery<T>` for queries and `IRequest<T>` for commands with permission attributes

## Project Structure Guidelines

### Feature Organization
```
Features/
├── Products/
│   ├── Commands/
│   │   ├── ProductCreateOrUpdate.cs
│   │   └── ProductCreateOrUpdateCommandHandler.cs
│   ├── Queries/
│   │   ├── GetProducts.cs
│   │   └── GetProductDetails.cs
│   └── Product.cs (Entity)
```

### API Controllers
- Use static classes for feature endpoints
- Queries implement `IQuery<T>` interface, commands implement `IRequest<T>` interface
- Include mapping profiles with AutoMapper
- Use `[PublicAPI]` attribute for public DTOs

### Test Organization
- **Unit Tests**: Test domain logic and business rules using NUnit
- **Integration Tests**: Test API endpoints and database interactions using NUnit
- **Test Base Classes**: Use `IntegrationFixtureBase` and `SchedulerFixtureBase`
- **Test Categories**: Use `[Category("unit")]` and `[Category("integration")]`
- **Test Framework**: NUnit for .NET testing, Jest for Angular unit tests

## Dependencies & Libraries

### Core Libraries
- **.NET 9.0**: Target framework
- **Entity Framework Core**: Data access with SQL Server
- **MediatR**: CQRS and mediator pattern
- **AutoMapper**: Object-object mapping
- **FluentValidation**: Input validation
- **Serilog**: Structured logging
- **Autofac**: Dependency injection container
- **NUnit**: Unit and integration testing framework

### Frontend (Angular)
- **Angular 19+**: Frontend framework
- **Angular Material**: UI components
- **TypeScript**: Type-safe JavaScript
- **Jest**: Unit testing
- **Playwright**: End-to-end testing

## Best Practices

### When Creating New Features
1. **Domain First**: Start with domain entities and business rules
2. **Commands/Queries**: Create separate command and query handlers
3. **Validation**: Add FluentValidation rules
4. **Tests**: Write unit tests for domain logic and integration tests for APIs using NUnit
5. **Configuration**: Add Entity Framework configuration if needed
6. **Permissions**: Define appropriate authorization attributes

### Database Migrations
- Use descriptive migration names
- Keep migrations focused on single changes
- Always review generated SQL before applying
- Use seed data for reference data

### Error Handling
- Use domain exceptions for business rule violations
- Implement global exception handling middleware
- Return appropriate HTTP status codes
- Log errors with structured logging

### Security
- Use Azure AD authentication
- Implement permission-based authorization
- Validate all inputs
- Use HTTPS only
- Enable CORS appropriately

## Code Generation
The solution includes code generation tools:
- **entry-codegen**: Generates Angular components and services from .NET commands/queries
- **NSwag**: Generates TypeScript clients from OpenAPI specifications

## DevOps & Deployment
- **Azure Pipelines**: CI/CD configuration in `/Pipelines`
- **Docker**: Containerization ready
- **Health Checks**: Implemented for monitoring
- **Application Insights**: Telemetry and monitoring

## Common Commands
```powershell
# Add migration
scripts/add-migration.ps1 -MigrationName "YourMigrationName"

# Update database
scripts/update-databases.ps1

# Run code generation
npm run codegen:run

# Run tests
dotnet test

# Start Angular dev server
cd enigmatry-entry-blueprint-app && npm start
```

## File Naming Conventions
- **Entities**: PascalCase (e.g., `Product.cs`, `User.cs`)
- **Commands**: Use static classes with descriptive names (e.g., `ProductCreateOrUpdate`)
- **Queries**: Use static classes starting with "Get" (e.g., `GetProductDetails`)
- **Tests**: End with "Fixture" for test classes (e.g., `ProductFixture`)
- **Configurations**: End with "Configuration" (e.g., `ProductConfiguration`)

## When Modifying Existing Code
- Follow existing patterns and conventions
- Update tests accordingly
- Consider backward compatibility
- Update documentation if needed
- Review impact on dependent projects

Remember to always prioritize clean, maintainable code that follows the established patterns in this solution.

### Additional Notes
- When running commands in PowerShell, avoid using `&&` for chaining commands as it can lead to unexpected behavior. Instead, use separate lines for clarity.
- nswag and frontend