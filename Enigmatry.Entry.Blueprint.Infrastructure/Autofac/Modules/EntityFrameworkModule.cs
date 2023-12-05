using Autofac;
using Enigmatry.Entry.Blueprint.Infrastructure.Api.Init;
using Enigmatry.Entry.Blueprint.Infrastructure.Data;
using Enigmatry.Entry.Core.Data;
using Enigmatry.Entry.Core.Settings;
using Enigmatry.Entry.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Module = Autofac.Module;

namespace Enigmatry.Entry.Blueprint.Infrastructure.Autofac.Modules;

public class EntityFrameworkModule : Module
{
    public bool RegisterMigrationsAssembly { get; set; }

    protected override void Load(ContainerBuilder builder)
    {
        if (builder == null)
        {
            throw new ArgumentNullException(nameof(builder));
        }

        builder.RegisterGeneric(typeof(EntityFrameworkRepository<>))
            .As(typeof(IRepository<>))
            .InstancePerLifetimeScope();

        builder.RegisterGeneric(typeof(EntityFrameworkRepository<,>))
            .As(typeof(IRepository<,>))
            .InstancePerLifetimeScope();

        builder.RegisterAssemblyTypes(AssemblyFinder.InfrastructureAssembly)
            .Where(
                type =>
                    ImplementsInterface(typeof(IEntityRepository<>), type) ||
                    type.Name.EndsWith("Repository", StringComparison.InvariantCulture)
            ).AsImplementedInterfaces().InstancePerLifetimeScope();

        // Registering interceptors as self because we want to resolve them individually to add them to the DbContextOptions in the correct order
        builder.RegisterType<PopulateCreatedUpdatedInterceptor>().AsSelf().InstancePerLifetimeScope();
        builder.RegisterType<PublishDomainEventsInterceptor>().AsSelf().InstancePerLifetimeScope();

        builder.Register(CreateDbContextOptions).As<DbContextOptions>().InstancePerLifetimeScope();

        // Needs to be registered both as self and as DbContext or the tests might not work as expected
        builder.RegisterType<BlueprintContext>().AsSelf().As<DbContext>().InstancePerLifetimeScope();
        builder.RegisterType<EntityFrameworkUnitOfWork>().As<IUnitOfWork>().InstancePerLifetimeScope();
    }

    private static bool ImplementsInterface(Type interfaceType, Type concreteType) =>
        concreteType.GetInterfaces().Any(
            t =>
                (interfaceType.IsGenericTypeDefinition && t.IsGenericType
                    ? t.GetGenericTypeDefinition()
                    : t) == interfaceType);

    private DbContextOptions CreateDbContextOptions(IComponentContext container)
    {
        var loggerFactory = container.Resolve<ILoggerFactory>();
        var configuration = container.Resolve<IConfiguration>();
        var dbContextSettings = container.Resolve<DbContextSettings>();

        var optionsBuilder = new DbContextOptionsBuilder();

        optionsBuilder
            .UseLoggerFactory(loggerFactory)
            .EnableSensitiveDataLogging(dbContextSettings.SensitiveDataLoggingEnabled);

        optionsBuilder.UseSqlServer(configuration.GetConnectionString("BlueprintContext")!,
            sqlOptions => SetupSqlOptions(sqlOptions, dbContextSettings));

        // Interceptors will be executed in the order they are added
        optionsBuilder.AddInterceptors(
            container.Resolve<PopulateCreatedUpdatedInterceptor>());
        optionsBuilder.AddInterceptors(
            container.Resolve<PublishDomainEventsInterceptor>());

        return optionsBuilder.Options;
    }

    private SqlServerDbContextOptionsBuilder SetupSqlOptions(SqlServerDbContextOptionsBuilder sqlOptions,
        DbContextSettings dbContextSettings)
    {
        // Configuring Connection Resiliency: https://docs.microsoft.com/en-us/ef/core/miscellaneous/connection-resiliency 
        sqlOptions = sqlOptions.EnableRetryOnFailure(
            dbContextSettings.ConnectionResiliencyMaxRetryCount,
            dbContextSettings.ConnectionResiliencyMaxRetryDelay,
            null);

        if (RegisterMigrationsAssembly)
        {
            sqlOptions = sqlOptions.MigrationsAssembly("Enigmatry.Entry.Blueprint.Data.Migrations");
        }

        // Using DateOnly and TimeOnly with EF Core 6 or 7
        sqlOptions = sqlOptions.UseDateOnlyTimeOnly();

        return sqlOptions;
    }
}
